using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using SeeSharp.Models;

namespace SeeSharp.Screens.Edit
{
    public class PageSprite : Sprite
    {
        private readonly BindablePage _page = new BindablePage();

        public PageSprite(BindablePage page)
        {
            _page.BindTo(page);
            Anchor = Anchor.TopLeft;
            Origin = Anchor.TopLeft;
            RelativeSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load(LargeTextureStore textures, SeeSharpStorage storage)
        {
            Texture = textures.Get(_page.Value.Name);
            FillAspectRatio = (float) Texture.Width / (float) Texture.Height;
        }
    }
}