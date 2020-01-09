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
                NotifyFilter = NotifyFilters.LastWrite
                             | NotifyFilters.Size
                             | NotifyFilters.Security
                             | NotifyFilters.Attributes
                             | NotifyFilters.FileName
                             | NotifyFilters.LastAccess
            };

            watcher.Created += Update;
            watcher.Deleted += Update;
            watcher.Changed += Update;
            watcher.Renamed += Update;          
        }

        private void Update(object _, FileSystemEventArgs __) => parsePages();
        
        private void parsePages()
        {
            _pages.Value = new DirectoryInfo(_path)
                .GetFiles("*.*")
                .Where(file => file.Extension.ToLower() == ".jpg"
                          || file.Extension.ToLower() == ".jpeg"
                          || file.Extension.ToLower() == ".png"
                          || file.Extension.ToLower() == ".bmp"
                          || file.Extension.ToLower() == ".gif")
                .Select(file => new Page
                {
                    Name = file.Name,
                    Zoom = 1.0f,
                    Speed = 1.0f,
                    Bars = new SortedList<float>()
                })
                .Select(p => new Bindable<Page>(p));
        }
    }
}