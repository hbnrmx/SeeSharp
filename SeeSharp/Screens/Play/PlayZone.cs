using System;
using System.Linq;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Input;
using SeeSharp.Models;
using SeeSharp.Screens.Edit;

namespace SeeSharp.Screens.Play
{
    public class PlayZone : Container
    {
        private readonly Bindable<Page> _page;
        private readonly Container container;
        private float currentBar;
        private bool running;
        public Action<float> zoomChanged;
        public Action<float> speedChanged;
        public Action<float> currentBarChanged;

        public PlayZone(Bindable<Page> page)
        {
            _page = page;

            //sane default, halfway down the page
            currentBar = page.Value.Bars.Any() ? page.Value.Bars.First() : 0.5f;

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
                Child.ScaleTo(_page.Value.Zoom);
                foreach (var child in container.Children)
                {
                    var yOffset = -(currentBar - 0.5f) * child.DrawHeight;
                    child.Y = yOffset;
                    if (this.running)
                    {
                        float xOffset = (float) Time.Elapsed * _page.Value.Speed / 10f;
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
            _page.Value.Zoom = MathHelper.Clamp(_page.Value.Zoom + amount, 0.6f, 8f);
            zoomChanged.Invoke(_page.Value.Zoom);
        }

        private void adjustSpeed(float amount)
        {
            _page.Value.Speed = MathHelper.Clamp(_page.Value.Speed + amount, 0.1f, 4f);
            speedChanged.Invoke(_page.Value.Speed);
        }

        private void previousBar()
        {
            var previous = _page.Value.Bars.LastOrDefault(b => b < currentBar);
            currentBar = previous != 0f ? previous : currentBar;
            resetBar();
        }

        private void nextBar(bool loop = false)
        {
            var next = _page.Value.Bars.FirstOrDefault(b => b > currentBar);
            if (next != 0f)
            {
                currentBar = next;
                resetBar();
                return;
            }

            var first = _page.Value.Bars.FirstOrDefault();
            if (first != 0f)
            {
                currentBar = first;
                resetBar();
                return;
            }

            resetBar();
        }

        private void resetBar()
        {
            currentBarChanged.Invoke(currentBar);
            foreach (var child in container.Children)
            {
                child.X = 0;
            }
        }
    }
}