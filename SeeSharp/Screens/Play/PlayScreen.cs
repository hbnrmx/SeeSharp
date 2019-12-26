using osu.Framework.Graphics;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK.Input;
using SeeSharp.Models;
using SeeSharp.Screens.Edit;
using SeeSharp.Screens.Help;
using SeeSharp.Text;

namespace SeeSharp.Screens.Play
{
    public class PlayScreen : Screen
    {
        private readonly Page _page;

        public PlayScreen(Page page)
        {
            _page = page;
            
            ZoomInfoText zoom;
            SpeedInfoText speed;
            BarInfoText currentBar;

            RelativeSizeAxes = Axes.Both;

            AddInternal(new ModeInfoText(page, Mode.Playing));
            AddInternal(currentBar = new BarInfoText(page));
            AddInternal(speed = new SpeedInfoText(page));
            AddInternal(zoom = new ZoomInfoText(page));
            AddInternal(new PlayZone(page)
            {
                speedChanged = speed.UpdateInfo,
                zoomChanged = zoom.UpdateInfo,
                currentBarChanged = currentBar.UpdateInfo
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