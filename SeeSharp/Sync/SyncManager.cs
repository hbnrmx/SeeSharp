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
        private readonly Bindable<State> _state = new Bindable<State>();
        private readonly string[] allowedFileExtensions = {".jpg", ".jpeg", ".png",".bmp",".gif"};
        
        public SyncManager(string basePath, string pagesPath, Bindable<State> state)
        {
            _basePath = basePath;
            _pagesPath = pagesPath;
            _state.BindTo(state);

            Load();
            Save();

            var configWatcher = new FileWatcher(basePath, "pages.json")
            {
                //OnChange = Load
                //stop loading for now
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
            
            var registeredFileNames = state.Pages.Select(p => p.Value.Name);
            
            //add pages which have not been registered yet.
            var newItems = new DirectoryInfo(_pagesPath)
                .GetFiles("*.*")
                .Where(file => allowedFileExtensions.Contains(file.Extension.ToLower()))
                .Where(file => !registeredFileNames.Contains(file.Name))
                .Select(file => new Page
                {
                    Name = file.Name,
                    Speed = new BindableFloat(state.DefaultSpeed),
                    Zoom = new BindableFloat(state.DefaultZoom),
                    Bars = new SortedList<float>()
                })
                .Select(page => new BindablePage(page));
                
                state.Pages.AddRange(newItems);
            
            //remove pages which cannot be found in the folder anymore.
            var oldItems = state.Pages
                .Where(page => !File.Exists(Path.Combine(_pagesPath, page.Value.Name)));

            state.Pages.RemoveAll(item => oldItems.Contains(item));
            
            //set new Value
            _state.Value = state;
        }

        private readonly object saveLock = new object();
        public bool Save()
        {
            lock (saveLock)
            {
                return SaveToConfig(configPath(), _state);
            }
            
        }

        private T LoadFromConfig<T>(string path)
        {
            try
            {
                using (StreamReader file = File.OpenText(path))
                {
                    if (new FileInfo(path).Length == 0)
                    {
                        throw new FileLoadException();
                    }

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
            
                using (var sw = new StreamWriter(path))
                using (var jw = new JsonTextWriter(sw))
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