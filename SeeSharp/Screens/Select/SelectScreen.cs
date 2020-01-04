using System.Collections.Generic;
using System.Linq;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK;
using osuTK.Input;
using SeeSharp.Models;
using SeeSharp.Screens.Help;
using SeeSharp.Screens.Play;

namespace SeeSharp.Screens.Select
{
    public class SelectScreen : Screen
    {
        private readonly List<MenuItem> sheets = new List<MenuItem>();

        public SelectScreen(Bindable<IEnumerable<Bindable<Page>>> pages)
        {
            foreach (var page in pages.Value)
            {
                sheets.Add(new MenuItem(page) {PageSelected = pageSelected});
            }

            RelativeSizeAxes = Axes.Both;

            if (pages.Value.Any())
            {
                AddInternal(new BasicScrollContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    ScrollbarVisible = false,
                    Padding = new MarginPadding{Top = 50f, Left = 50f, Right = 350f, Bottom = 50f},
                    Child = new FillFlowContainer<MenuItem>
                    {
                        Direction = FillDirection.Vertical,
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Children = sheets
                    }
                });
                AddInternal(new AddPagesContainer()
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    Margin = new MarginPadding(90f),
                    Scale = new Vector2(0.8f,0.8f)
                });
            }
            else
            {
                AddInternal(new AddPagesContainer());
            }
        }

        private void pageSelected(Bindable<Page> page)
        {
            this.Push(new PlayScreen(page));
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            switch (e.Key)
            {
                case Key.F1:
                    this.Push(new SelectHelpScreen());
                    return true;

                default:
                    return false;
            }
        }
    }
}