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
        private void load(PageStorage storage)
        {            
            Textures.AddStore(new TextureLoaderStore(new StorageBackedResourceStore(storage)));

            _pagesPath = storage.GetFullPath(string.Empty);
            _pages = parsePages(_pagesPath);
            
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

        private IEnumerable<Page> parsePages(string path)
        {
            var fileInfos = new DirectoryInfo(path).GetFiles("*.*");
            
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