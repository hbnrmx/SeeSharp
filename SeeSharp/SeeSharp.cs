using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using osu.Framework.Lists;
using osu.Framework.Platform;
using osu.Framework.Screens;
using osuTK;
using osuTK.Input;
using SeeSharp.Models;
using SeeSharp.Screens.Select;

namespace SeeSharp
{
    public class SeeSharp : Game
    {
        private readonly Bindable<IEnumerable<Bindable<Page>>> _pages = new Bindable<IEnumerable<Bindable<Page>>>();

        private string _pagesPath;

        [BackgroundDependencyLoader]
        private void load(SeeSharpStorage storage)
        {
            var pageStorage = storage.GetStorageForDirectory("pages");
            _pagesPath = pageStorage.GetFullPath(string.Empty);
            Textures.AddStore(new TextureLoaderStore(new StorageBackedResourceStore(pageStorage)));

            new PageSync(_pagesPath, _pages);
            
            Add(new ScreenStack(new SelectScreen(_pages))
            {
                RelativeSizeAxes = Axes.Both
            });
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);
            host.Window.Title = "SeeSharp";
            host.Window.WindowState = WindowState.Maximized;
            host.Window.FileDrop += fileDrop;
        }

        private void fileDrop(object _, FileDropEventArgs e)
        {
            foreach (var path in e.FileNames)
            {
                try
                {
                    File.Copy(path,Path.Combine(_pagesPath, Path.GetFileName(path)));
                }
                catch{}
            }  
        }
    }
}