using osu.Framework.Graphics;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK.Input;
using SeeSharp.Text;

namespace SeeSharp.Screens
{
    public class HelpScreen : Screen
    {
        public HelpScreen()
        {
            RelativeSizeAxes = Axes.Both;

            AddInternal(new InfoText
            {
                Text = "Help (F1)"
            });

            AddRangeInternal(new Drawable[]
            {
                new InfoText
                {
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    Text = "TODO"
                }
            });
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            switch (e.Key)
            {
                case Key.F1:
                case Key.Escape:
                    this.Exit();
                    return true;

                default:
                    return false;
            }
        }
    }
}