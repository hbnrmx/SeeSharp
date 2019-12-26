using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;

namespace SeeSharp.Text
{
    public class InfoText : SpriteText
    {
        public InfoText()
        {
            Padding = new MarginPadding(10);
            Font = new FontUsage().With(size: 55);
            Colour = Config.Colors["Foreground"];
            Shadow = true;
            ShadowColour = Color4.Black;
            ShadowOffset = new Vector2(0.04f, 0.04f);
            Depth = -1;
        }
    }
}