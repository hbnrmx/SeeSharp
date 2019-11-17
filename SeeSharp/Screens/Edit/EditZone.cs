using SixLabors.ImageSharp;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using SeeSharp.Models;

namespace SeeSharp.Screens.Play
{
    public class EditZone : Container
    {
        private readonly Page _page;

        public EditZone(Page page)
        {
            _page = page;

            var image = Image.Load(Config.FileLocation + @"\\" + page.FileInfo.Name);
            FillAspectRatio = (float) image.Width / (float) image.Height;
            FillMode = FillMode.Fit;
            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;
            Origin = Anchor.Centre;
            Anchor = Anchor.Centre;

            Child = new PageSprite(page);
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            foreach (var bar in _page.Bars)
                AddInternal(new FollowLine((float) bar)
                {
                    OnRemove = removeLine
                });
        }

        protected override bool OnClick(ClickEvent e)
        {
            addLine(ToLocalSpace(e.MousePosition).Y / this.DrawHeight);
            return true;
        }

        private void addLine(float y)
        {
            var newLine = new FollowLine(y)
            {
                OnRemove = removeLine,
                OnPositionChange = updateLine
            };

            _page.Bars.Add(newLine.Y);
            AddInternal(newLine);
        }

        private void removeLine(FollowLine followLine)
        {
            _page.Bars.Remove(followLine.Y);
            RemoveInternal(followLine);
        }

        private void updateLine(float oldY, float newY)
        {
            _page.Bars.Remove(oldY);
            _page.Bars.Add(newY);
        }
    }
}
//TODO use BindableList
//TODO decimal?
//TODO duplicates in list