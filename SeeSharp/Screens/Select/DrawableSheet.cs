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
using SeeSharp.Screens.Play;

namespace SeeSharp.Screens.Select
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
            BorderThickness = 0f;
            BorderColour = Color4.Blue;
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
                new PageSprite(_page)
                {
                    RelativeSizeAxes = Axes.None,
                    FillMode = FillMode.Fit,
                    Height = 141f,
                    Width = 100f,
                }, 
                new SpriteText
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Margin = new MarginPadding{Left = 120},
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

        protected override bool OnHover(HoverEvent e)
        {
            BorderThickness = 10f;
            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            BorderThickness = 0f;
        }
    }
}