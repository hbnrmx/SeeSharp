using System;
using System.Linq;
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
        public Action Save;
        public Action<Page, bool> NextPage;
        
        private readonly BindablePage _page = new BindablePage();
        private readonly PlayZone _playZone;

        public PlayScreen(BindablePage page, bool runningStart = false)
        {
            _page.BindTo(page);

            ZoomInfoText zoom;
            SpeedInfoText speed;
            BarInfoText currentBar;

            RelativeSizeAxes = Axes.Both;

            AddInternal(new ModeInfoText(_page, Mode.Playing));
            AddInternal(currentBar = new BarInfoText(_page));
            AddInternal(speed = new SpeedInfoText(_page));
            AddInternal(zoom = new ZoomInfoText(_page));
            AddInternal(_playZone = new PlayZone(_page, runningStart)
            {
                speedChanged = speed.UpdateInfo,
                zoomChanged = zoom.UpdateInfo,
                currentBarChanged = currentBar.UpdateInfo,
                PageEnd = onPageEnd
            });

            //skip right to EditScreen when empty
            OnLoadComplete += _ =>
            {
                if (!_page.Value.Bars.Any())
                {
                    this.Push(new EditScreen(_page)
                    {
                        Save = Save,
                        FinishedEditing = _playZone.jumpToFirstBar
                    });
                }
            };
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Save.Invoke();
                    this.Exit();
                    return true;

                case Key.E:
                    this.Push(new EditScreen(_page)
                    {
                        Save = Save,
                        FinishedEditing = _playZone.jumpToFirstBar
                    });
                    return true;

                case Key.F1:
                    this.Push(new PlayHelpScreen());
                    return true;

                default:
                    return false;
            }
        }

        //skip right to SelectScreen when empty
        public override void OnResuming(IScreen _)
        {
            if (!_page.Value.Bars.Any())
            {
                this.Exit();
            }
        }

        private void onPageEnd(bool runningStart)
        {
            this.Exit();
            NextPage?.Invoke(_page.Value, runningStart);
        }
    }
}