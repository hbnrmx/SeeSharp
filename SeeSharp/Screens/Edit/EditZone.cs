using System.IO;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using SeeSharp.Models;
using SixLabors.ImageSharp;

namespace SeeSharp.Screens.Edit
{
    public class EditZone : Container
    {
        private readonly BindablePage _page = new BindablePage();

        public EditZone(BindablePage page)
        {
            _page.BindTo(page);

            FillMode = FillMode.Fit;
            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;
            Origin = Anchor.Centre;
            Anchor = Anchor.Centre;
            Masking = true;

            Child = new PageSprite(_page);
        }

        [BackgroundDependencyLoader]
        private void load(SeeSharpStorage storage)
        {
            var image = Image.Load(Path.Combine(storage.GetStorageForDirectory("pages").GetFullPath(string.Empty), _page.Value.Name));
            FillAspectRatio = (float) image.Width / (float) image.Height;
            
            foreach (var bar in _page.Value.Bars)
                AddInternal(new ScanLine((float) bar)
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
            var newLine = new ScanLine(y)
            {
                OnRemove = removeLine,
                OnPositionChange = updateLine
            };

            _page.Value.Bars.Add(newLine.Y);
            AddInternal(newLine);
        }

        private void removeLine(ScanLine scanLine)
        {
            _page.Value.Bars.Remove(scanLine.Y);
            RemoveInternal(scanLine);
        }

        private void updateLine(float oldY, float newY)
        {
            _page.Value.Bars.Remove(oldY);
            _page.Value.Bars.Add(newY);
        }
    }
}
