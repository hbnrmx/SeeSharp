using osu.Framework.Bindables;
using osu.Framework.Graphics;
using SeeSharp.Models;

namespace SeeSharp.Text
{
    public class SpeedInfoText : InfoText
    {
        public SpeedInfoText(Bindable<Page> page)
        {
            Anchor = Anchor.BottomRight;
            Origin = Anchor.BottomRight;
            Text = $"Speed: {page.Value.Speed:P}";
            Margin = new MarginPadding {Right = 30, Bottom = 40};
        }

        public void UpdateInfo(float speed)
        {
            Text = $"Speed: {speed:P}";
        }
    }
}