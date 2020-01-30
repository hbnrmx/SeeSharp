using System.IO;
using System.Linq;
using Newtonsoft.Json;
using osu.Framework.Bindables;
using osu.Framework.Configuration;
using osu.Framework.Lists;
using osu.Framework.Logging;
using SeeSharp.Models;

namespace SeeSharp.Sync
{
    public class SyncManager : IConfigManager
    {
        private readonly string _basePath;
        private readonly string _pagesPath;
        private string configPath() => Path.Combine(_basePath, "pages.json");
        private readonly Bindable<State> _state;
        private readonly string[] allowedFileExtensions = {".jpg", ".jpeg", ".png",".bmp",".gif"};
        
        public SyncManager(string basePath, string pagesPath, Bindable<State> state)
        {
            _basePath = basePath;
            _pagesPath = pagesPath;
            _state = state;

            Load();
            Save();

            var configWatcher = new FileWatcher(basePath, "pages.json")
            {
                OnChange = Load
            };

            new FileWatcher(_pagesPath, "*.*")
            {
                OnChange = () => { 
                    Load();
                    
                    configWatcher.Disable();
                    Save();
                    configWatcher.Enable();
                }
            };
        }

        public void Load()
        {
            State state;
            
            try
            {
                state = LoadFromConfig<State>(configPath());
            }
            catch (IOException e)
            {
                Logger.Error(e, "failed to retrieve config. Supplying standard config instead.");
                state = new State()
                {
                    DefaultSpeed = 0.3f,
                    DefaultZoom = 3.0f
                };
            }
            
            var registeredFileNames = state.Pages.Value.Select(p => p.Value.Name);
            
            //add pages which have not been registered yet.
            var newItems = new DirectoryInfo(_pagesPath)
                .GetFiles("*.*")
                .Where(file => allowedFileExtensions.Contains(file.Extension.ToLower()))
                .Where(file => !registeredFileNames.Contains(file.Name))
                .Select(file => new Page
                {
                    Name = file.Name,
                    Speed = state.DefaultSpeed,
                    Zoom = state.DefaultZoom,
                    Bars = new SortedList<float>()
                })
                .Select(page => new BindablePage(page));
                
                state.Pages.Value.AddRange(newItems);
            
            //remove pages which cannot be found in the folder anymore.
            var oldItems = state.Pages.Value
                .Where(page => !File.Exists(Path.Combine(_pagesPath, page.Value.Name)));

            state.Pages.Value.RemoveAll(item => oldItems.Contains(item));
            
            //set new Value
            _state.Value = state;
        }

        public bool Save()
        {
            var state = _state;
            return SaveToConfig(configPath(), state);
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