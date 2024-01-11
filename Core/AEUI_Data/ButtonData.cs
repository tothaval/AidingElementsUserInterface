/* Aiding Elements User Interface
 *      ButtonData class
 *      
 * inherits from: CoreData class
 * 
 * button element properties
 * 
 * init:        2023|11|28
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */

namespace AidingElementsUserInterface.Core.AEUI_Data
{
    internal class ButtonData : CoreData
    {
        public double height { get; set; }
        public double width { get; set; }
        public string imageFilePath { get; set; }


        public ButtonData(bool load)
        {
        }

        public ButtonData()
        {
            height = 40;
            width = 80;

            imageFilePath = "";
        }

        public ButtonData(CoreData coreData)
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

            height = 40;
            width = 80;

            imageFilePath = "";
        }
    }
}
/*  END OF FILE
 * 
 * 
 */