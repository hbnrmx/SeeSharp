using System;
using System.IO;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;
using SeeSharp.Models;

namespace SeeSharp.Screens.Selection
{
    public class DrawableSheet : Container
    {
        private readonly Page _page;
        public Action<Page> PageSelected;

        public DrawableSheet(Page page)
        {
            this._page = page;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;
            Masking = true;
            MaskingSmoothness = 1f;
            Margin = new MarginPadding {Bottom = 10f};

            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.DarkSlateGray,
                    EdgeSmoothness = new Vector2(1.5f, 0)
                },
                new SpriteText
                {
                    Padding = new MarginPadding(20),
                    Text = Path.GetFileNameWithoutExtension(_page.FileInfo.Name),
                    Font = new FontUsage().With(size: 60)
                }
            };
        }

        protected override bool OnClick(ClickEvent e)
        {
            PageSelected.Invoke(_page);
            return true;
        }
    }
}