/* Aiding Elements User Interface
 *      ContainerData class
 * 
 * container element properties
 * 
 * init:        2023|11|29
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

    internal class ContainerData : CoreData
    {
        private UserControl content;

        public int brushOrientation = 0;
        public int brushType = 0;
        public string color1_string = "#DDDDDDDD";
        public string color2_string = "#DDDD0000";
        public string color3_string = "#DD00DD00";
        public string color4_string = "#DD0000DD";
        public bool imageIsBackground = false;
        public string imageFilepath = "-";

        public int z_position { get; set; }
        public int dragLevel { get; set; }

        public ContainerData(CoreData coreData, UserControl _content)
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

            content = _content;

            z_position = 0;
            dragLevel = 30000;
        }


        public ContainerData(UserControl _content)
        {
            content = _content;

            z_position = 0;
            dragLevel = 30000;
        }

        internal UserControl getContent()
        {
            return content;
        }

        internal void setContent(UserControl userControl)
        {
            content = userControl;
        }
    }
}
/*  END OF FILE
 * 
 * 
 */