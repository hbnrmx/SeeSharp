using System;
using System.Linq;
using osu.Framework.Allocation;
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
        public Action<float> zoomChanged;
        public Action<float> speedChanged;
        public Action<float> currentBarChanged;
        
        private readonly BindablePage _page;
        private readonly Container zoomContainer;
        private readonly BindableFloat currentBar = new BindableFloat();
        private readonly PageSprite spriteA, spriteB;
        private PageSprite spriteFront, spriteBack;
        private bool running;
        private const float PAGE_SEPARATOR_WIDTH = 3f;
        
        public PlayZone(BindablePage page)
        {
            _page = page;
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
                    spriteA = spriteFront = new PageSprite(page)
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        FillMode = FillMode.Fit
                    },
                    spriteB = spriteBack = new PageSprite(page)
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                        FillMode = FillMode.Fit
                    }
                }
            };

            currentBar.ValueChanged += _ => resetBar();
        }
        
        [BackgroundDependencyLoader]
        private void Load() => Scheduler.AddDelayed(jumpToFirstBar, 100);

        protected override void Update()
        {
            base.Update();
            
            zoomContainer.ScaleTo(_page.Value.Zoom);
            
            if (running)
            {
                //horizontal Panning
                float xOffset = (float) Time.Elapsed * _page.Value.Speed / 10f;
                
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
            _page.Value.Zoom = Math.Clamp(_page.Value.Zoom + amount, 0.6f, 8f);
            zoomChanged.Invoke(_page.Value.Zoom);
        }

        private void adjustSpeed(float amount)
        {
            _page.Value.Speed = Math.Clamp(_page.Value.Speed + amount, 0.1f, 4f);
            speedChanged.Invoke(_page.Value.Speed);
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
        
        public void jumpToFirstBar() => currentBar.Value = firstBar();

        private void jumpToPreviousBar(bool loop = false) => currentBar.Value = previousBar(loop);

        private void jumpToNextBar(bool loop = false) => currentBar.Value = nextBar(loop);

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
            currentBarChanged.Invoke(currentBar.Value);
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
                    running = !running;
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
                    if (running)
                    {
                        adjustSpeed(-0.1f);
                        return true;
                    }

                    resetOrPreviousBar();
                    return true;

                case Key.Right:
                case Key.D:
                    if (running)
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
