using System.IO;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using SeeSharp.Models;
using SixLabors.ImageSharp;

namespace SeeSharp.Screens.Edit
{
    public class PageSprite : Sprite
    {
        private readonly Page _page;

        public PageSprite(Page page)
        {
            _page = page;
            Anchor = Anchor.TopLeft;
            Origin = Anchor.TopLeft;
            RelativeSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures, SeeSharpStorage storage)
        {
            Texture = textures.Get(_page.FileInfo.Name);

            if (FillMode == FillMode.Fit)
            {
                //read aspect ratio from file
                var image = Image.Load(Path.Combine(storage.GetStorageForDirectory("pages").GetFullPath(string.Empty), _page.FileInfo.Name));
                FillAspectRatio = (float) image.Width / (float) image.Height;
            }
        }
    }
}