using System;
using System.IO;
using System.Threading;

namespace SeeSharp.Sync
{
    public class FileWatcher
    {
        public Action OnChange;
        private readonly FileSystemWatcher _watcher;

        public FileWatcher(string path, string filter)
        {
            _watcher = new FileSystemWatcher
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

            _watcher.Deleted += Update;
            _watcher.Changed += Update;
            _watcher.Renamed += Update;
        }

        private void Update(object _, FileSystemEventArgs __)
        {
            //give some time for the external writing process to unlock the file
            //TODO: find more robust solution later.
            Thread.Sleep(2000);
            OnChange?.Invoke();
        }

        public void Disable() => _watcher.EnableRaisingEvents = false;

        public void Enable() => _watcher.EnableRaisingEvents = true;
    }
}