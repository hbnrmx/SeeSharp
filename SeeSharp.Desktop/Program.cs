using System;
using osu.Framework;
using osu.Framework.Platform;

namespace SeeSharp.Desktop
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableHost(@"SeeSharp", false, false))
            using (Game game = new SeeSharp())
            //using (Game game = new SeeSharpTestRunner())
            {
                host.Dependencies.CacheAs(new PageStorage(string.Empty, (DesktopGameHost) host));
                host.Run(game);
            }
        }
    }
}