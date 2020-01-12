using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK.Input;
using SeeSharp.Models;
using SeeSharp.Screens.Help;
using SeeSharp.Text;

namespace SeeSharp.Screens.Edit
{
    public class EditScreen : Screen
    {
        public Action SetBarToFirstOrDefault;
        public Action Save;
        
        public EditScreen(BindablePage page)
        {
            RelativeSizeAxes = Axes.Both;
            AddInternal(new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Config.Colors["Background"]
            });

            AddInternal(new ModeInfoText(page, Mode.Editing));

            AddInternal(new EditZone(page));
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                case Key.BackSpace:
                case Key.Enter:
                case Key.KeypadEnter:
                case Key.Space:
                    SetBarToFirstOrDefault.Invoke();
                    Save.Invoke();
                    this.Exit();
                    return true;

                case Key.F1:
                    this.Push(new EditHelpScreen());
                    return true;

                default:
                    return false;
            }
        }
    }
}