using osu.Framework.Graphics;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK.Input;
using SeeSharp.Models;
using SeeSharp.Screens.Edit;
using SeeSharp.Screens.Play;
using SeeSharp.Text;

namespace SeeSharp.Screens
{
    public class PlayScreen : Screen
    {
        private readonly Page _page;
        private readonly MagnificationInfoText _magnification;
        private readonly SpeedInfoText _speed;
        private readonly BarInfoText _currentBar;

        public PlayScreen(Page page)
        {
            _page = page;

            RelativeSizeAxes = Axes.Both;

            AddInternal(new ModeInfoText(page, Mode.Playing));
            AddInternal(_currentBar = new BarInfoText(page));
            AddInternal(_speed = new SpeedInfoText(page));
            AddInternal(_magnification = new MagnificationInfoText(page));
            AddInternal(new PlayZone(page)
            {
                speedChanged = _speed.UpdateInfo,
                magnificationChanged = _magnification.UpdateInfo,
                currentBarChanged = _currentBar.UpdateInfo,
            });
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    this.Exit();
                    return true;

                case Key.E:
                    this.Push(new EditScreen(_page));
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