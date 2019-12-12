using System;
using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;

namespace SeeSharp.Screens.Play
{
    public class ScanLine : Container, IComparable<ScanLine>
    {
        public Action<float, float> OnPositionChange;
        public Action<ScanLine> OnRemove;

        public ScanLine(float y)
        {
            Position = ToLocalSpace(new Vector2(0, y));
            AutoSizeAxes = Axes.Y;
            RelativePositionAxes = Axes.Y;
            RelativeSizeAxes = Axes.X;

            var arrows = new List<SpriteIcon>();
            for (int i = 0; i < 30; i++)
            {
                arrows.Add(new SpriteIcon
                {
                    Margin = new MarginPadding{Right = 15f},
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Icon = FontAwesome.Solid.LongArrowAltRight,
                    Height = 40f,
                    Width = 40f,
                    Colour = Config.Colors["Primary"]
                });
            }

            AddInternal(new FillFlowContainer
            {
                RelativeSizeAxes = Axes.X,
                Height = 25f,
                Direction = FillDirection.Horizontal,
                Children = arrows,
            });
        }

        public int CompareTo(ScanLine other) => Y.CompareTo(other.Y);

        protected override bool OnDragStart(DragStartEvent e) => true;

        protected override bool OnDrag(DragEvent e)
        {
            var oldY = Y;
            Y = MathHelper.Clamp(e.MousePosition.Y / Parent.DrawHeight, 0, 1);
            OnPositionChange.Invoke(oldY, Y);

            return true;
        }

        protected override bool OnClick(ClickEvent e)
        {
            OnRemove.Invoke(this);

            return true;
        }
    }
}