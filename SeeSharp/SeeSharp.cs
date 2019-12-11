using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using osu.Framework.Lists;
using osu.Framework.Platform;
using osu.Framework.Screens;
using osuTK;
using osuTK.Input;
using SeeSharp.Models;
using SeeSharp.Screens;

namespace SeeSharp
{
    public class SeeSharp : Game
    {
        private IEnumerable<Page> _pages;
        private string _pagesPath;

        [BackgroundDependencyLoader]
        private void load(Storage storage)
        {
            FileInfo[] _pageInfos = new DirectoryInfo(Config.FileLocation).GetFiles("*.*");

            _pages = parsePages(_pageInfos);
            
            var pageStorage = storage.GetStorageForDirectory(Config.FileLocation);
            _pagesPath = pageStorage.GetFullPath(string.Empty);
            
            Textures.AddStore(new TextureLoaderStore(new StorageBackedResourceStore(pageStorage)));

            //Sync sync = new Sync();

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
                catch (Exception){}
            }
           
        }

        private IEnumerable<Page> parsePages(FileInfo[] fileInfos)
        {
            return fileInfos.Select(p => new Page
            {
                FileInfo = p,
                Magnification = 1.0f,
                Speed = 1.0f,
                Bars = new SortedList<float>()
            });
        }
    }
}