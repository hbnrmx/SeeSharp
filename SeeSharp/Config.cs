using System;
using System.Collections.Generic;
using osuTK.Graphics;

namespace SeeSharp
{
    public static class Config
    {
        public static readonly Dictionary<string, Color4> Colors = new Dictionary<string, Color4>
        {
            {"White", FromHex("FFFFFF")},
            {"Primary", FromHex("064BDF")},
            {"Foreground", FromHex("6C91E1")},
            {"ForegroundAlt", FromHex("6589D7")},
            {"Background", FromHex("011642")},
            {"BackgroundAlt", FromHex("011D5A")},
        };

        private static Color4 FromHex(string hex)
        {
            if (hex[0] == '#')
                hex = hex.Substring(1);
            
            switch (hex.Length)
            {
                default:
                    throw new ArgumentException(@"Invalid hex string length!");
                
                case 3:
                    return new Color4(
                    (byte)(Convert.ToByte(hex.Substring(0, 1), 16) * 17),
                    (byte)(Convert.ToByte(hex.Substring(1, 1), 16) * 17),
                    (byte)(Convert.ToByte(hex.Substring(2, 1), 16) * 17),
                    255);
                
                case 6:
                    return new Color4(
                    Convert.ToByte(hex.Substring(0, 2), 16),
                    Convert.ToByte(hex.Substring(2, 2), 16),
                    Convert.ToByte(hex.Substring(4, 2), 16),
                    255);
            }
        }
    }
}