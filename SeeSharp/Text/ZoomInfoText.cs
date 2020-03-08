using osu.Framework.Graphics;
using SeeSharp.Models;

namespace SeeSharp.Text
{
    public class ZoomInfoText : InfoText
    {
        private readonly BindablePage _page = new BindablePage();
        
        public ZoomInfoText(BindablePage page)
        {
            _page.BindTo(page);
            Anchor = Anchor.BottomRight;
            Origin = Anchor.BottomRight;
            Text = $"Zoom: {_page.Value.Zoom.Value:P}";
            Margin = new MarginPadding {Right = 30};
        }

        public void UpdateInfo(float zoom)
        {
            Text = $"Zoom: {zoom:P}";
        }
    }
}