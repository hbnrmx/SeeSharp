using System;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.Lists;
using osu.Framework.Logging;
using osu.Framework.Threading;
using SeeSharp.Models;

namespace SeeSharp.Sync
{
    public class SyncManager : IConfigManager
    {
        private readonly string _basePath;
        private readonly string _pagesPath;
        private string configPath() => Path.Combine(_basePath, "pages.json");
        private readonly Bindable<SortedList<BindablePage>> _pages;

        public SyncManager(string basePath, string pagesPath, Bindable<SortedList<BindablePage>> pages)
        {
            _basePath = basePath;
            _pagesPath = pagesPath;
            _pages = pages;

            Load();
            Save();

            new FileWatcher(basePath, "pages.json")
            {
                OnChange = () => { 
                    Load();
                    Save();
                }
                
            };

            new FileWatcher(_pagesPath, "*.*")
            {
                OnChange = () =>
                {
                    Load();
                    Save();
                }
            };
        }

        public void Load()
        {
            Data data;
            
            try
            {
                data = LoadFromConfig<Data>(configPath());
            }
            catch (IOException e)
            {
                Logger.Error(e, "failed to retrieve config. Supplying standard config instead.");
                data = new Data();
            }

            var registeredFileNames = data.Pages.Value.Select(p => p.Value.Name);
            
            //add pages which have not been registered yet.
            var newItems = new DirectoryInfo(_pagesPath)
                .GetFiles("*.*")
                .Where(file => file.Extension.ToLower() == ".jpg"
                            || file.Extension.ToLower() == ".jpeg"
                            || file.Extension.ToLower() == ".png"
                            || file.Extension.ToLower() == ".bmp"
                            || file.Extension.ToLower() == ".gif")
                .Where(file => !registeredFileNames.Contains(file.Name))
                .Select(file => new Page
                {
                    Name = file.Name,
                    Speed = data.DefaultSpeed,
                    Zoom = data.DefaultZoom,
                    Bars = new SortedList<float>()
                })
                .Select(page => new BindablePage(page));
                
                data.Pages.Value.AddRange(newItems);
            
            //remove pages which cannot be found in the folder anymore.
            var oldItems = data.Pages.Value
                .Where(page => !File.Exists(Path.Combine(_pagesPath, page.Value.Name)));

            data.Pages.Value.RemoveAll(item => oldItems.Contains(item));
            
            //set new Value
            _pages.Value = data.Pages.Value;
        }

        public bool Save()
        {
            var data = new Data {Pages = _pages};
            return SaveToConfig(configPath(), data);
        }

        private T LoadFromConfig<T>(string path)
        {
            try
            {
                using (StreamReader file = File.OpenText(path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    return (T) serializer.Deserialize(file, typeof(T));
                }
            }
            catch (IOException e)
            {
                Logger.Error(e, "failed to load from Config.");
                throw;
            }
        }

        private bool SaveToConfig<T>(string path, T data)
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer {Formatting = Formatting.Indented};
            
                using (StreamWriter sw = new StreamWriter(path))
                using (JsonWriter jw = new JsonTextWriter(sw))
                {
                    serializer.Serialize(jw, data);
                }

                return true;
            }
            catch (IOException e)
            {
                Logger.Error(e, "failed to write to Config.");

                return false;
            }
        }
    }
}