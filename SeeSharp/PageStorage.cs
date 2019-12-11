using System;
using System.IO;
using osu.Framework.Platform;
using osu.Framework.Platform.Windows;

namespace SeeSharp
{
    public class PageStorage : WindowsStorage
    {
        public PageStorage(string baseName, DesktopGameHost host) : base(baseName, host)
        {
        }
        
        protected override string LocateBasePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"pages/");
        }
    }
}