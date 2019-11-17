using osu.Framework.Graphics;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK.Input;
using SeeSharp.Models;
using SeeSharp.Screens.Play;
using SeeSharp.Text;

namespace SeeSharp.Screens.Edit
{
    public class EditScreen : Screen
    {
        public EditScreen(Page page)
        {
            RelativeSizeAxes = Axes.Both;

            AddInternal(new ModeInfoText(page, Mode.Editing));

            AddInternal(new EditZone(page));
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                case Key.Enter:
                    this.Exit();
                    return true;

                case Key.F1:
                    this.Push(new HelpScreen());
                    return true;

                default:
                    return false;
            }
        }
    }
}