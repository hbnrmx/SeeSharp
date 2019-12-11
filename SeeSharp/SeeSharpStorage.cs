using System;
using System.Diagnostics;
using System.IO;
using osu.Framework.Platform;
using osu.Framework.Platform.Windows;

namespace SeeSharp
{
    public class SeeSharpStorage : WindowsStorage
    {
        public SeeSharpStorage(string baseName, DesktopGameHost host) : base(baseName, host)
        {
            BaseName = @"SeeSharp";
        }
        
        protected override string LocateBasePath() => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        
    }
}