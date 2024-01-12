/* Aiding Elements User Interface
 *      TextBoxData class
 * 
 * inherits from: CoreData class
 * 
 * CoreTextBox properties
 * 
 * init:        2023|12|02
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 *  
 */

namespace AidingElementsUserInterface.Core.AEUI_Data
{
    internal class TextBoxData : CoreData
    {
        public double height { get; set; }
        public double width { get; set; }
        public string imageFilePath { get; set; }


        public TextBoxData(bool load)
        {
        }

        public TextBoxData()
        {
            height = 25;
            width = 50;

            imageFilePath = "";
        }

        public TextBoxData(CoreData coreData)
        {
            brushtype = coreData.brushtype;
            background = coreData.background;
            borderbrush = coreData.borderbrush;
            foreground = coreData.foreground;
            highlight = coreData.highlight;

            cornerRadius = coreData.cornerRadius;
            thickness = coreData.thickness;

            fontSize = coreData.fontSize;
            fontFamily = coreData.fontFamily;

            height = 25;
            width = 50;

            imageFilePath = "";
        }
    }
}
/*  END OF FILE
 * 
 * 
 */