using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Platform;
using osu.Framework.Testing;

namespace SeeSharp
{
    public class SeeSharpTestRunner : SeeSharp
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Child = new DrawSizePreservingFillContainer
            {
                Children = new Drawable[]
                {
                    new TestBrowser("SeeSharp"),
                    new CursorContainer()
                }
            };
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);
            host.Window.CursorState |= CursorState.Hidden;
        }
    }
}