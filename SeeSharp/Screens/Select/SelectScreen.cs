using System.Collections.Generic;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK.Input;
using SeeSharp.Models;
using SeeSharp.Screens.Select;

namespace SeeSharp.Screens
{
    public class SelectScreen : Screen
    {
        private readonly List<DrawableSheet> sheets = new List<DrawableSheet>();

        public SelectScreen(IEnumerable<Page> pages)
        {
            foreach (var page in pages)
            {
                sheets.Add(new DrawableSheet(page) {PageSelected = pageSelected});
            }

            RelativeSizeAxes = Axes.Both;

            if (pages.Any())
            {
                AddInternal(new BasicScrollContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Child = new FillFlowContainer<DrawableSheet>
                    {
                        Direction = FillDirection.Vertical,
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Padding = new MarginPadding(50),
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Children = sheets
                    }
                });
            }
            else
            {
                AddInternal(new AddPagesContainer());
            }
        }

        private void pageSelected(Page page)
        {
            this.Push(new PlayScreen(page));
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            switch (e.Key)
            {
                case Key.F1:
                    this.Push(new HelpScreen());
                    return true;

                default:
                    return false;
            }
        }
    }
}