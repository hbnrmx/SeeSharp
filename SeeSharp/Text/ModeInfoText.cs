using System.IO;
using osu.Framework.Bindables;
using SeeSharp.Models;

namespace SeeSharp.Text
{
    public class ModeInfoText : InfoText
    {
        public ModeInfoText(Bindable<Page> page, Mode mode)
        {
            Text = $"{(mode == Mode.Playing ? "Playing" :"Editing")} '{Path.GetFileNameWithoutExtension(page.Value.FileInfo.Name)}'";
        }
    }

    public enum Mode
    {
        Editing,
        Playing
    }
}