/* Aiding Elements User Interface
 *      ContainerData class
 *      
 * inherits from: CoreData class
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;

namespace AidingElementsUserInterface.Core.AEUI_Data
{

    internal class ContainerData : CoreData
    {
        private const int SPACING = 8;

        private UserControl content;
        private ColorData colorData;


        internal string containerLocation { get; set; }

        internal bool imageIsBackground = false;
        internal string imageFilePath { get; set; }

        internal int z_position;
        internal int dragLevel;
        internal GridLength element_spacing;


        public ContainerData()
        {
        }

        public ContainerData(UserControl _content)
        {
            content = _content;
            colorData = new ColorData();

            z_position = 0;
            dragLevel = 30000;
            element_spacing = new GridLength(SPACING);
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

            element_spacing = new GridLength(SPACING);
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