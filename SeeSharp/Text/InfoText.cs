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
            Font = new FontUsage().With(size: 50);
            Shadow = true;
            ShadowColour = Color4.Black;
            ShadowOffset = new Vector2(0.05f, 0.05f);
            Depth = -1;
        }
    }
}