using System.IO;
using SeeSharp.Models;

namespace SeeSharp.Text
{
    public class ModeInfoText : InfoText
    {
        public ModeInfoText(BindablePage page, Mode mode)
        {
            Text = $"{(mode == Mode.Playing ? "Playing" :"Editing")} '{Path.GetFileNameWithoutExtension(page.Value.Name)}'";
        }
    }

    public enum Mode
    {
        Editing,
        Playing
    }
}