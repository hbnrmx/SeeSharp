using System;
using System.IO;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;
using SeeSharp.Models;
using SeeSharp.Screens.Edit;

namespace SeeSharp.Screens.Select
{
    public class MenuItem : Container
    {
        private readonly BindablePage _page;
        public Action<BindablePage> PageSelected;
        private SpriteText _text;

        public MenuItem(BindablePage page)
        {
            _page = page;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            BorderThickness = 0f;
            BorderColour = Config.Colors["Foreground"];
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
                    Colour = Config.Colors["Background"],
                    EdgeSmoothness = new Vector2(1.5f, 0)
                },
                new PageSprite(_page)
                {
                    RelativeSizeAxes = Axes.None,
                    FillMode = FillMode.Fit,
                    Height = 141f,
                    Width = 100f,
                }, 
                _text = new SpriteText
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Margin = new MarginPadding{Left = 120},
                    Text = Path.GetFileNameWithoutExtension(_page.Value.Name),
                    Font = new FontUsage().With(size: 55)
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
            _text.Colour = Config.Colors["Foreground"];
            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            BorderThickness = 0f;
            _text.Colour = Config.Colors["White"];
        }
    }
}