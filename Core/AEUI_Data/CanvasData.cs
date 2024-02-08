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
using AidingElementsUserInterface.Core.AEUI_Logic;
using System.Windows;

namespace AidingElementsUserInterface.Core.AEUI_Data
{
    internal class CanvasData : CoreData
    {
        // name of canvas
        internal string canvasName { get; set; }
        internal int canvasID { get; set; }

        // element grid width and height
        internal GridLength element_spacing;

        // selection group positioning
        internal int grouping_displacement { get; set; }

        private LevelSystem levelSystem { get; set; }


        public CanvasData(bool load)
        {
            height = CoreCanvasSwitchData.Get_CORECANVAS_DIMENSION_CAP;
            width = CoreCanvasSwitchData.Get_CORECANVAS_DIMENSION_CAP;
        }


        public CanvasData(string canvas_name, int canvasID)
        {
            canvasName = canvas_name;

            grouping_displacement = 25;

            element_spacing = new GridLength(8);

            height = CoreCanvasSwitchData.Get_CORECANVAS_DIMENSION_CAP;
            width = CoreCanvasSwitchData.Get_CORECANVAS_DIMENSION_CAP;
            this.canvasID = canvasID;
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

            canvasName = "canvas";
            grouping_displacement = 25;

            element_spacing = new GridLength(8);
        }

        internal LevelSystem GetLevelSystem()
        {
            return levelSystem;
        }


        internal void setCanvasID(int identifierIndexDigit)
        {
            canvasID = identifierIndexDigit;
        }

        internal void SetLevelSystem(LevelSystem levelSystem)
        {
            this.levelSystem = levelSystem;
        }
    }
}
/*  END OF FILE
 * 
 * 
 */