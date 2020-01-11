using System;
using System.IO;
using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using osu.Framework.Lists;
using osu.Framework.Logging;
using osu.Framework.Platform;
using osu.Framework.Screens;
using osuTK;
using osuTK.Input;
using SeeSharp.Models;
using SeeSharp.Screens.Select;
using SeeSharp.Sync;

namespace SeeSharp
{
    public class SeeSharp : Game
    {
        private readonly Bindable<State> _state = new Bindable<State>(new State());
        
        private string _pagesPath;

        [BackgroundDependencyLoader]
        private void load(SeeSharpStorage storage)
        {
            var basePath = storage.GetFullPath(string.Empty);
            var pageStorage = storage.GetStorageForDirectory("pages");
            _pagesPath = pageStorage.GetFullPath(string.Empty);

            var syncManager = new SyncManager(basePath, _pagesPath, _state);

            var selectScreen = new SelectScreen(_state)
            {
                Save = () => { syncManager.Save(); }
            };
            
            Add(new ScreenStack(selectScreen)
            {
                RelativeSizeAxes = Axes.Both,
            });
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);
            host.Window.Title = "SeeSharp";
            host.Window.WindowState = WindowState.Maximized;
            host.Window.FileDrop += fileDrop;
        }

        private void fileDrop(object _, FileDropEventArgs args)
        {
            foreach (var path in args.FileNames)
            {
                try
                {
                    File.Copy(path, Path.Combine(_pagesPath, Path.GetFileName(path)));
                }
                catch (Exception e)
                {
                    Logger.Error(e, "fileDrop failed");
                }
            }  
        }
    }
}