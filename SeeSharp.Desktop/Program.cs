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
            using (GameHost host = Host.GetSuitableHost(@"SeeSharp", false, true))
            using (Game game = new SeeSharp())
            //using (Game game = new SeeSharpTestRunner())
            {
                host.Run(game);
            }
        }
    }
}