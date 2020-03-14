using System.Collections.Generic;
using osuTK.Graphics;
using osu.Framework.Extensions.Color4Extensions;

namespace SeeSharp
{
    public static class Config
    {
        public static readonly Dictionary<string, Color4> Colors = new Dictionary<string, Color4>
        {
            {"White", Color4Extensions.FromHex("FFFFFF")},
            {"Primary", Color4Extensions.FromHex("064BDF")},
            {"Foreground", Color4Extensions.FromHex("6C91E1")},
            {"ForegroundAlt", Color4Extensions.FromHex("6589D7")},
            {"Background", Color4Extensions.FromHex("011642")},
            {"BackgroundAlt", Color4Extensions.FromHex("011D5A")},
        };
    }
}