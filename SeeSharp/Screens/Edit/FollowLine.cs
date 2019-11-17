using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;

namespace SeeSharp.Screens.Play
{
    public class FollowLine : Container, IComparable<FollowLine>
    {
        public Action<float, float> OnPositionChange;
        public Action<FollowLine> OnRemove;

        public FollowLine(float y)
        {
            Position = ToLocalSpace(new Vector2(0, y));
            AutoSizeAxes = Axes.Y;
            RelativePositionAxes = Axes.Both;
            RelativeSizeAxes = Axes.X;
            AddInternal(new Box
            {
                RelativeSizeAxes = Axes.X,
                Height = 7f,
                Colour = Color4.Blue
            });
        }

        public int CompareTo(FollowLine other) => Y.CompareTo(other.Y);

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