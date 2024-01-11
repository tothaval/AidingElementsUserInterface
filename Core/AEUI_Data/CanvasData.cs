/* Aiding Elements User Interface
 *      CanvasData class
 *      
 * inherits from: CoreData class
 * 
 * canvas element properties
 * 
 * init:        2023|12|14
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using System.Windows;

namespace AidingElementsUserInterface.Core.AEUI_Data
{
    internal class CanvasData : CoreData
    {
        internal string canvasName { get; set; }

        internal int grouping_displacement { get; set; }

        internal string imageFilePath { get; set; }

        internal int z_level_MAX { get; set; }

        internal int z_level_MIN { get; set; }

        internal int dragLevel;
        internal int hoverLevel;
        internal GridLength element_spacing;


        public CanvasData(bool load)
        {
        }

        public CanvasData()
        {
            canvasName = "canvas";
            imageFilePath = "";

            grouping_displacement = 25;
            z_level_MIN = -100;
            z_level_MAX = 100;

            dragLevel = 30000;
            hoverLevel = 200;
            element_spacing = new GridLength(8);
        }

        public CanvasData(CoreData coreData)
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

            canvasName = "canvas";
            imageFilePath = "";

            grouping_displacement = 25;
            z_level_MIN = -100;
            z_level_MAX = 100;

            dragLevel = 30000;
            hoverLevel = 200;
            element_spacing = new GridLength(8);
        }
    }
}
/*  END OF FILE
 * 
 * 
 */