using System.IO;
using SeeSharp.Models;

namespace SeeSharp.Text
{
    public class ModeInfoText : InfoText
    {
        public ModeInfoText(Page page, Mode mode)
        {
            Text = $"{(mode == Mode.Playing ? "Playing" :"Editing")} '{Path.GetFileNameWithoutExtension(page.FileInfo.Name)}'";
        }
    }

    public enum Mode
    {
        Editing,
        Playing
    }
}