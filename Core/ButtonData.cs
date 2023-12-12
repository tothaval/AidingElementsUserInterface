/* Aiding Elements User Interface
 *      ButtonData class
 * 
 * button element properties
 * 
 * init:        2023|11|28
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AidingElementsUserInterface.Core
{
    internal class ButtonData : CoreData
    {
        public double height { get; set; }
        public double width { get; set; }
        public int expanderSize { get; set; }

        public ButtonData()
        {
            height = 40;
            width = 80;
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

            buttonImageFilePath = coreData.buttonImageFilePath;
            containerImageFilePath = coreData.containerImageFilePath;

            mainWindowHeight = coreData.mainWindowHeight;
            mainWindowWidth = coreData.mainWindowWidth;

            height = 40;
            width = 80;
        }
    }
}
/*  END OF FILE
 * 
 * 
 */