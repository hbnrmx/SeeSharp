namespace SeeSharp.Screens.Help
{
    public class SelectHelpScreen : HelpScreen
    {   
        public SelectHelpScreen()
        {
            textFlow.AddText("Add sheet music images in ", format);
            textFlow.AddText(".jpg ", formatHighlight);
            textFlow.AddText(", ", format);
            textFlow.AddText(".png", formatHighlight);
            textFlow.AddText(" or ", format);
            textFlow.AddText(".gif", formatHighlight);
            textFlow.AddText(" format to the pages folder.", format);
            
            textFlow.NewLine();
            
            textFlow.AddText("You can also ",format);
            textFlow.AddText("right click the file > Send To > SeeSharp ",formatHighlight);
            textFlow.AddText("or ",format);
            textFlow.AddText("drag and drop ",formatHighlight);
            textFlow.AddText("it into the window.",format);
        }
    }
}