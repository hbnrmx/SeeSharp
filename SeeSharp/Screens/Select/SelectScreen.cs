﻿using System;
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
        public Action Save;
        private readonly Bindable<State> _state = new Bindable<State>();
        private readonly List<MenuItem> _menuItems = new List<MenuItem>();
        private readonly BasicScrollContainer scroll;
        private readonly AddPagesContainer right;
        private readonly AddPagesContainer center;
        private readonly FillFlowContainer<MenuItem> fillFlow;

        public SelectScreen(Bindable<State> state)
        {
            _state.BindTo(state);
            RelativeSizeAxes = Axes.Both;
            InternalChildren = new Drawable[]
            {
                scroll = new BasicScrollContainer 
                {
                    RelativeSizeAxes = Axes.Both,
                    ScrollbarVisible = false,
                    Padding = new MarginPadding{Top = 50f, Left = 50f, Right = 350f, Bottom = 50f},
                    Child = fillFlow = new FillFlowContainer<MenuItem>
                    {
                        Direction = FillDirection.Vertical,
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Children = _menuItems
                    }
                },
                right = new AddPagesContainer
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    Margin = new MarginPadding(90f),
                    Scale = new Vector2(0.8f,0.8f)
                },
                center = new AddPagesContainer()       
            };

            _state.BindValueChanged(_ => Scheduler.AddOnce(load), true);
        }

        private void load()
        {
            _menuItems.Clear();
            
            foreach (var page in _state.Value.Pages)
            {
                _menuItems.Add(new MenuItem(page) {PageSelected = pageSelected});
            }

            if (_menuItems.Any())
            {
                _menuItems.Sort();
                fillFlow.Children = _menuItems;
                
                scroll.Show();
                right.Show();
                center.Hide();
            }
            else
            {
                scroll.Hide();
                right.Hide();
                center.Show();
            }
        }

        private void pageSelected(BindablePage page, bool runningStart = false)
        {
            this.Push(new PlayScreen(page, runningStart)
            {
                Save = Save,
                NextPage = selectNextPage
            });
        }

        private void selectNextPage(Page currentPage, bool runningStart)
        {
            var pages = _state.Value.Pages;
            
            var currentBindable = pages.First(bind => bind.Value == currentPage);
    
            var nextBindableIndex = (pages.IndexOf(currentBindable) + 1) % pages.Count;
            var nextBindable = pages[nextBindableIndex];
            
            pageSelected(nextBindable, runningStart);
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