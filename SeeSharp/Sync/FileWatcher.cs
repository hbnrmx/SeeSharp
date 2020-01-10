using System;
using System.IO;
using System.Threading;

namespace SeeSharp
{
    public class FileWatcher
    {
        public Action OnChange;

        public FileWatcher(string path, string filter)
        {
            var watcher = new FileSystemWatcher
            {
                Path = path,
                Filter = filter,
                EnableRaisingEvents = true,
                NotifyFilter = NotifyFilters.LastWrite
                             | NotifyFilters.Size
                             | NotifyFilters.Security
                             | NotifyFilters.Attributes
                             | NotifyFilters.FileName
            };

            watcher.Created += Update;
            watcher.Deleted += Update;
            watcher.Changed += Update;
            watcher.Renamed += Update;
        }
        
        private void Update(object _, FileSystemEventArgs __)
        {
            //give some time for the external writing process to unlock the file
            //TODO: find more robust solution later.
            Thread.Sleep(2000);
            OnChange?.Invoke();
        }
    }
}