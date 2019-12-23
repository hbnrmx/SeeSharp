using osu.Framework.Graphics;
using SeeSharp.Models;

namespace SeeSharp.Text
{
    public class ZoomInfoText : InfoText
    {
        public ZoomInfoText(Page page)
        {
            Anchor = Anchor.BottomRight;
            Origin = Anchor.BottomRight;
            Text = $"Zoom: {page.Zoom:P}";
            Margin = new MarginPadding {Right = 30};
        }

        public void UpdateInfo(float zoom)
        {
            Text = $"Zoom: {zoom:P}";
        }
    }
}