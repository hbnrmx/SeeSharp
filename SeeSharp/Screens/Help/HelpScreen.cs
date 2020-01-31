using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK;
using osuTK.Input;
using SeeSharp.Text;

namespace SeeSharp.Screens.Help
{
    public class HelpScreen : Screen
    {
        protected readonly TextFlowContainer textFlow;

        protected HelpScreen()
        {
            RelativeSizeAxes = Axes.Both;

            AddInternal(new InfoText
            {
                Text = "Help"
            });

            AddInternal(textFlow = new TextFlowContainer
            {
                RelativeSizeAxes = Axes.X,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                TextAnchor = Anchor.Centre
            });
        }

        protected static void format(SpriteText t)
        {
            t.Font = t.Font.With(size: 50);
        }

        protected static void formatHighlight(SpriteText t)
        {
            format(t);
            t.Colour = Config.Colors["Foreground"];
        }
        
        protected static void formatItalic(SpriteText t)
        {
            format(t);
            t.Shear = new Vector2(0.3f,0);
        }

        protected override void OnKeyUp(KeyUpEvent e)
        {
            switch (e.Key)
            {
                case Key.F1:
                    this.Exit();
                    break;
            }
        }
    }
}