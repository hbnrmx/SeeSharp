namespace SeeSharp.Screens.Help
{
    public class PlayHelpScreen : HelpScreen
    {
        public PlayHelpScreen()
        {
            textFlow.AddText("Press ", format);
            textFlow.AddText("E ", formatHighlight);
            textFlow.AddText("to edit the document.", format);
            
            textFlow.NewLine();
            
            textFlow.AddText("Press the ", format);
            textFlow.AddText("spacebar ", formatHighlight);
            textFlow.AddText("to start playing.", format);
            
            textFlow.NewLine();
            
            textFlow.AddText("Press the ", format);
            textFlow.AddText("arrow keys ", formatHighlight);
            textFlow.AddText("to switch from bar to bar.", format);   
            
            textFlow.NewLine();
            
            textFlow.AddText("Scroll up/down ", formatHighlight);
            textFlow.AddText("to change zoom.", format);
            
            textFlow.NewLine();
            
            textFlow.AddText("Scroll up/down with shift ", formatHighlight);
            textFlow.AddText("to change speed.", format);
        }
    }
}