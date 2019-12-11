using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using SeeSharp.Text;

namespace SeeSharp.Screens.Select
{
    public class AddPagesContainer : FillFlowContainer
    {
        private SeeSharpStorage _storage;
        private SpriteIcon _icon;
        private InfoText _text;

        public AddPagesContainer()
        {
            AutoSizeAxes = Axes.Both;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Child = new FillFlowContainer
            {
                AutoSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Direction = FillDirection.Vertical,
                Children = new Drawable[]
                {
                    _icon = new SpriteIcon()
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Height = 75f,
                        Width = 75f,
                        Icon = FontAwesome.Solid.FolderPlus
                    },
                    _text = new InfoText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Text = "Add pages"
                    }
                }
            };
        }
        
        [BackgroundDependencyLoader]
        private void load(SeeSharpStorage storage)
        {
            _storage = storage;
        }

        protected override bool OnClick(ClickEvent e)
        {
            _storage.OpenInNativeExplorer();
            return true;
        }

        protected override bool OnHover(HoverEvent e)
        {
            _icon.Colour = Config.Colors["Foreground"];
            _text.Colour = Config.Colors["Foreground"];
            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            _icon.Colour = Config.Colors["White"];
            _text.Colour = Config.Colors["White"];
        }
    }
}

