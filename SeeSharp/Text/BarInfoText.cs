using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using SeeSharp.Models;

namespace SeeSharp.Text
{
    public class BarInfoText : InfoText
    {
        private readonly BindablePage _page = new BindablePage();

        public BarInfoText(BindablePage page)
        {
            _page.BindTo(page);
            Margin = new MarginPadding{Top = 40};
            Font = new FontUsage().With(size: 150);
        }

        public void UpdateInfo(float currentBar)
        {
            var index = _page.Value.Bars.IndexOf(currentBar);

            if (index == -1) return;

            Text = (++index).ToString();
        }
    }
}