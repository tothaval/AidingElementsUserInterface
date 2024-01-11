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
using System.Windows.Controls;

namespace AidingElementsUserInterface.Core.AEUI_Data
{

    internal class ContainerData : CoreData
    {
        private UserControl content;
        private ColorData colorData;

        internal string containerLocation { get; set; }

        internal bool imageIsBackground = false;
        internal string imageFilePath { get; set; }

        internal int z_position;

        public ContainerData()
        {
        }

        public ContainerData(UserControl _content)
        {
            content = _content;
            colorData = new ColorData();

            z_position = 0;
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