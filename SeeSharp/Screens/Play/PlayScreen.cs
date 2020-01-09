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
        private readonly BindablePage _page;
        private readonly PlayZone _playZone;

        public PlayScreen(BindablePage page)
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
            AddInternal(_playZone = new PlayZone(page)
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
                    this.Push(new EditScreen(_page)
                    {
                        setBarToFirstOrDefault = _playZone.setBarToFirstOrDefault
                    });
                    return true;

                case Key.F1:
                    this.Push(new PlayHelpScreen());
                    return true;

                default:
                    return false;
            }
        }
    }
}