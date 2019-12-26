using osu.Framework.Graphics.Sprites;

namespace SeeSharp.Screens.Help
{
    public class EditHelpScreen : HelpScreen
    {
        public EditHelpScreen()
        {            
            textFlow.AddText("Click ", formatHighlight);
            textFlow.AddText("on the page to add a ", format);
            textFlow.AddText("scan-line.", formatItalic);

            textFlow.NewLine();
            
            textFlow.AddText("Scan-lines", formatItalic);
            textFlow.AddText("tell the camera where to look when panning over a page.", format);
            
            textFlow.NewLine();
            
            textFlow.AddText("When you're done, press ", format);
            textFlow.AddText("Enter ", formatHighlight);
            textFlow.AddText("or ", format);
            textFlow.AddText("Escape", formatHighlight);
            textFlow.AddText(".", format);
        }
    }
}