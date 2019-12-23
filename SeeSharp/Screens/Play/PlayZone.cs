using System;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Input;
using SeeSharp.Models;

namespace SeeSharp.Screens.Play
{
    public class PlayZone : Container
    {
        private readonly Page _page;
        private readonly Container container;
        private float currentBar;
        private bool running;
        public Action<float> zoomChanged;
        public Action<float> speedChanged;
        public Action<float> currentBarChanged;

        public PlayZone(Page page)
        {
            _page = page;

            //sane default, halfway down the page
            currentBar = page.Bars.Any() ? page.Bars.First() : 0.5f;

            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;
            Origin = Anchor.Centre;
            Anchor = Anchor.Centre;

            Child = container = new Container
            {
                RelativeSizeAxes = Axes.Both,
                Child = new PageSprite(page)
                {
                    FillMode = FillMode.Fit
                },
                Origin = Anchor.CentreLeft,
                Anchor = Anchor.Centre
            };
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
                    this.running = !this.running;
                    return true;

                case Key.Up:
                case Key.W:
                    previousBar();
                    return true;

                case Key.Down:
                case Key.S:
                    nextBar();
                    return true;

                case Key.Left:
                case Key.A:
                case Key.BackSpace:
                    if (this.running)
                    {
                        adjustSpeed(-0.1f);
                        return true;
                    }

                    resetBar();
                    return true;

                case Key.Right:
                case Key.D:
                    if (this.running)
                    {
                        adjustSpeed(0.1f);
                        return true;
                    }

                    previousBar();
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

        protected override void Update()
        {
            base.Update();
            {
                Child.ScaleTo(_page.Zoom);
                foreach (var child in container.Children)
                {
                    float yOffset = -(currentBar - 0.5f) * child.DrawHeight;
                    child.Y = yOffset;
                    if (this.running)
                    {
                        float xOffset = (float) Time.Elapsed * _page.Speed / 10f;
                        child.X -= xOffset;

                        if (child.X < -child.DrawWidth)
                        {
                            nextBar(true);
                        }
                    }
                }
            }
        }

        private void adjustZoom(float amount)
        {
            _page.Zoom = MathHelper.Clamp(_page.Zoom + amount, 0.6f, 8f);
            zoomChanged.Invoke(_page.Zoom);
        }

        private void adjustSpeed(float amount)
        {
            _page.Speed = MathHelper.Clamp(_page.Speed + amount, 0.1f, 4f);
            speedChanged.Invoke(_page.Speed);
        }

        private void previousBar()
        {
            var previous = _page.Bars.LastOrDefault(b => b < currentBar);
            currentBar = previous != 0f ? previous : currentBar;
            currentBarChanged.Invoke(currentBar);
            resetBar();
        }

        private void nextBar(bool loop = false)
        {
            var next = _page.Bars.FirstOrDefault(b => b > currentBar);
            if (next != 0f)
            {
                currentBar = next;
                currentBarChanged.Invoke(currentBar);
                resetBar();
                return;
            }

            var first = _page.Bars.FirstOrDefault();
            if (first != 0f)
            {
                currentBar = first;
                currentBarChanged.Invoke(currentBar);
                resetBar();
                return;
            }

            resetBar();
        }

        private void resetBar()
        {
            foreach (var child in container.Children)
            {
                child.X = 0;
            }
        }
    }
}