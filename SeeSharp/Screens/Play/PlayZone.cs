using System;
using System.Linq;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using osuTK.Input;
using SeeSharp.Models;
using SeeSharp.Screens.Edit;

namespace SeeSharp.Screens.Play
{
    public class PlayZone : Container
    {
        public Action<bool> PageEnd;
        public Action<float> zoomChanged;
        public Action<float> speedChanged;
        public Action<float> currentBarChanged;
        
        private readonly BindablePage _page = new BindablePage();
        private readonly Container zoomContainer;
        private readonly BindableFloat currentBar = new BindableFloat();
        private readonly PageSprite spriteA, spriteB;
        private PageSprite spriteFront, spriteBack;
        private bool _running;
        private const float PAGE_SEPARATOR_WIDTH = 3f;
        
        public PlayZone(BindablePage page, bool runningStart = false)
        {
            _page.BindTo(page);

            _running = runningStart;
            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;
            Origin = Anchor.Centre;
            Anchor = Anchor.Centre;

            Child = zoomContainer = new Container
            {
                RelativeSizeAxes = Axes.Both,
                Origin = Anchor.CentreLeft,
                Anchor = Anchor.CentreLeft,
                Children = new Drawable[]
                {
                    spriteA = spriteFront = new PageSprite(_page)
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        FillMode = FillMode.Fit
                    },
                    spriteB = spriteBack = new PageSprite(_page)
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        FillMode = FillMode.Fit
                    }
                }
            };

            currentBar.ValueChanged += e =>
            {
                resetBar();
                currentBarChanged?.Invoke(e.NewValue);
            };

            zoomContainer.ScaleTo(_page.Value.Zoom.Value);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            jumpToFirstBar();
        }

        protected override void Update()
        {
            base.Update();

            if (_running)
            {
                //horizontal Panning
                float xOffset = (float) Time.Elapsed * _page.Value.Speed.Value / 10f;
                
                spriteA.X -= xOffset;
                spriteB.X -= xOffset;
                
                if (spriteBack.X < 0)
                {
                    wrapAround();
                    jumpToNextBar(true);
                }
            }
        }

        private void adjustZoom(float amount)
        {
            _page.Value.Zoom.Value += amount;
            zoomContainer.ScaleTo(_page.Value.Zoom.Value, 100, Easing.InOutQuad);
            zoomChanged.Invoke(_page.Value.Zoom.Value);
        }

        private void adjustSpeed(float amount)
        {
            _page.Value.Speed.Value += amount;
            speedChanged.Invoke(_page.Value.Speed.Value);
        }

        private void resetOrPreviousBar()
        {
            //reset if past a quarter of the current Bar
            if (spriteFront.X < -spriteA.DrawWidth / 4 || currentBar.Value == firstBar())
            {
                resetBar();
                return;
            }

            jumpToPreviousBar();
        }
        
        public void jumpToFirstBar()
        {
            if (currentBar.Value == firstBar())
            {
                currentBar.TriggerChange();
            }
            else
            {
                currentBar.Value = firstBar();
            }
        }

        private void jumpToPreviousBar(bool loop = false) => currentBar.Value = previousBar(loop);

        private void jumpToNextBar(bool loop = false)
        {
            if (currentBar.Value == lastBar())
            {
                PageEnd?.Invoke(_running);
                return;
            }

            currentBar.Value = nextBar(loop);
        }

        private float firstBar() => _page.Value.Bars.DefaultIfEmpty(0.5f).First();

        private float lastBar() => _page.Value.Bars.DefaultIfEmpty(0.5f).Last();

        private float previousBar(bool loop = false)
        {
            var previous = _page.Value.Bars.LastOrDefault(b => b < currentBar.Value);
            
            if (previous == 0)
            {
                if (loop) return lastBar();
                return currentBar.Value;
            }

            return previous;
        }

        private float nextBar(bool loop = false)
        {
            var next = _page.Value.Bars.FirstOrDefault(b => b > currentBar.Value);
            
            if (next == 0)
            {
                if (loop) return firstBar();
                return currentBar.Value;
            }

            return next;
        }

        private void resetBar()
        {
            spriteFront.X = 0;
            spriteFront.Y = -(currentBar.Value - 0.5f) * spriteA.DrawHeight;
            spriteBack.X = spriteA.DrawWidth + PAGE_SEPARATOR_WIDTH;
            spriteBack.Y = -(nextBar(true) - 0.5f) * spriteA.DrawHeight;
            
            if (currentBar.Value == lastBar())
            {
                spriteBack.Hide();
            }
            else
            {
                spriteBack.Show();
            }
        }

        private void wrapAround()
        {
            spriteFront.X = spriteA.DrawWidth + PAGE_SEPARATOR_WIDTH;
            spriteFront.Y = -(nextBar(true) - 0.5f) * spriteA.DrawHeight;

            (spriteFront, spriteBack) = (spriteBack, spriteFront);
        }
        
        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (e.Key == Key.Plus || e.Key == Key.KeypadPlus)
            {
                if (e.ShiftPressed || e.AltPressed || e.ControlPressed)
                {
                    adjustSpeed(0.1f);
                    return true;
                }

                adjustZoom(0.2f);
                return true;
            }

            if (e.Key == Key.Minus || e.Key == Key.KeypadMinus)
            {
                if (e.ShiftPressed || e.AltPressed || e.ControlPressed)
                {
                    adjustSpeed(-0.1f);
                    return true;
                }

                adjustZoom(-0.2f);
                return true;
            }

            switch (e.Key)
            {
                case Key.Space:
                    _running = !_running;
                    return true;

                case Key.Up:
                case Key.W:
                    jumpToPreviousBar();
                    return true;

                case Key.Down:
                case Key.S:
                    jumpToNextBar();
                    return true;

                case Key.Left:
                case Key.A:
                case Key.BackSpace:
                    if (_running)
                    {
                        adjustSpeed(-0.1f);
                        return true;
                    }

                    resetOrPreviousBar();
                    return true;

                case Key.Right:
                case Key.D:
                    if (_running)
                    {
                        adjustSpeed(0.1f);
                        return true;
                    }

                    jumpToNextBar();
                    return true;

                default:
                    return false;
            }
        }

        protected override bool OnScroll(ScrollEvent e)
        {
            if (e.ShiftPressed || e.ControlPressed || e.AltPressed)
            {
                adjustSpeed(e.ScrollDelta.Y / 10);
                return true;
            }

            adjustZoom(e.ScrollDelta.Y / 5);
            return true;
        }
    }
}
