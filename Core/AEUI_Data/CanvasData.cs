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
        // name of canvas
        internal string canvasName { get; set; }

        // element grid width and height
        internal GridLength element_spacing;

        // selection group positioning
        internal int grouping_displacement { get; set; }


        public CanvasData(bool load)
        {

            height = CoreCanvasSwitchData.Get_CORECANVAS_DIMENSION_CAP;
            width = CoreCanvasSwitchData.Get_CORECANVAS_DIMENSION_CAP;

        }


        public CanvasData(string canvas_name = "canvas")
        {
            canvasName = canvas_name;

            grouping_displacement = 25;

            element_spacing = new GridLength(8);

            height = CoreCanvasSwitchData.Get_CORECANVAS_DIMENSION_CAP;
            width = CoreCanvasSwitchData.Get_CORECANVAS_DIMENSION_CAP;

        }

        public CanvasData(CoreData coreData)
        {
            background = coreData.background;
            borderbrush = coreData.borderbrush;
            foreground = coreData.foreground;
            highlight = coreData.highlight;

            cornerRadius = coreData.cornerRadius;
            thickness = coreData.thickness;

            fontSize = coreData.fontSize;
            fontFamily = coreData.fontFamily;

            height = CoreCanvasSwitchData.Get_CORECANVAS_DIMENSION_CAP;
            width = CoreCanvasSwitchData.Get_CORECANVAS_DIMENSION_CAP;

            imageFilePath = coreData.imageFilePath;

            canvasName = "canvas";
            grouping_displacement = 25;

            element_spacing = new GridLength(8);
        }
    }
}
/*  END OF FILE
 * 
 * 
 */