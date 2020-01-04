using System.Collections.Generic;
using System.IO;
using System.Linq;
using osu.Framework.Bindables;
using osu.Framework.Lists;
using SeeSharp.Models;

namespace SeeSharp
{
    public class PageSync
    {
        private readonly Bindable<IEnumerable<Bindable<Page>>> _pages;
        private readonly string _path;
        
        public PageSync(string path, Bindable<IEnumerable<Bindable<Page>>> pages)
        {
            _path = path;
            _pages = pages;
            
            parsePages();
            
            var watcher = new FileSystemWatcher
            {
                Path = path,
                Filter = "*.*",
                EnableRaisingEvents = true,
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size,
            };

            watcher.Created += Update;
            watcher.Deleted += Update;
            watcher.Changed += Update;
            watcher.Renamed += Update;          
        }

        private void Update(object _, FileSystemEventArgs __) => parsePages();
        
        private void parsePages()
        {
            var fileInfos = new DirectoryInfo(_path).GetFiles("*.*");
            
            //set Bindable
            _pages.Value = fileInfos
                .Where(fi => fi.Extension == ".png" 
                          || fi.Extension == ".jpg"
                          || fi.Extension == ".gif")
                .Select(fi => new Page
                {
                    FileInfo = fi,
                    Zoom = 1.0f,
                    Speed = 1.0f,
                    Bars = new SortedList<float>()
                })
                .Select(p => new Bindable<Page>(p));
        }
    }
}