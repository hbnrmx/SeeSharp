using System;
using osu.Framework;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;

namespace SeeSharp.Desktop
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableHost(@"SeeSharp"))
            using (Game game = new SeeSharp())
            //using (Game game = new SeeSharpTestRunner())
            {
                var storage = new SeeSharpStorage(string.Empty, (DesktopGameHost) host);
                var pageStorage = storage.GetStorageForDirectory("pages");
                var textureStore = new LargeTextureStore(new TextureLoaderStore(new StorageBackedResourceStore(pageStorage)));
                
                host.Dependencies.Cache(storage);
                host.Dependencies.Cache(textureStore);
                
                host.Run(game);
            }
        }
    }
}