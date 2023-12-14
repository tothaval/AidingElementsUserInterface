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

namespace AidingElementsUserInterface.Core.AEUI_Data
{

    internal class ContainerData : CoreData
    {
        private UserControl content;
        private ColorData colorData;

        internal bool imageIsBackground = false;
        internal string imageFilePath { get; set; }

        internal int z_position;
        internal int dragLevel;


        public ContainerData(bool load)
        {
            if (load)
            {

            }
        }

        public ContainerData(UserControl _content)
        {
            content = _content;
            colorData = new ColorData();

            z_position = 0;
            dragLevel = 30000;
        }

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

            content = _content;
            colorData = new ColorData();

            z_position = 0;
            dragLevel = 30000;
        }

        internal ColorData GetColorData()
        {
            return colorData;
        }


        internal UserControl getContent()
        {
            return content;
        }

        internal void SetColorData(ColorData colorData)
        {
            this.colorData = colorData;
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