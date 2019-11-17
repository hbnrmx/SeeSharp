using osu.Framework.Graphics;
using SeeSharp.Models;

namespace SeeSharp.Text
{
    public class MagnificationInfoText : InfoText
    {
        public MagnificationInfoText(Page page)
        {
            Anchor = Anchor.BottomRight;
            Origin = Anchor.BottomRight;
            Text = $"Magnification: {page.Magnification:P}";
            Margin = new MarginPadding {Right = 30};
        }

        public void UpdateInfo(float magnification)
        {
            Text = $"Magnification: {magnification:P}";
        }
    }
}