using osu.Framework.Graphics;
using SeeSharp.Models;

namespace SeeSharp.Text
{
    public class SpeedInfoText : InfoText
    {
        private readonly BindablePage _page = new BindablePage();
        
        public SpeedInfoText(BindablePage page)
        {
            _page.BindTo(page);
            Anchor = Anchor.BottomRight;
            Origin = Anchor.BottomRight;
            Text = $"Speed: {_page.Value.Speed.Value:P}";
            Margin = new MarginPadding {Right = 30, Bottom = 40};
        }

        public void UpdateInfo(float speed)
        {
            Text = $"Speed: {speed:P}";
        }
    }
}