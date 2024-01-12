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
using AidingElementsUserInterface.Core.MyNote_Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

using UserControl = System.Windows.Controls.UserControl;

namespace AidingElementsUserInterface.Core.AEUI_Data
{

    internal class ContainerData : CanvasData
    {
        private UserControl content = new UserControl();

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

            z_position = 0;
        }

        public ContainerData(CoreData coreData, UserControl _content)
        {
            ApplyInheritanceDataOverwrite<CoreData>(coreData);

            content = _content;

            z_position = 0;
        }

        //public ContainerData(CanvasData canvasData, UserControl _content)
        //{
        //    //ApplyInheritanceDataOverwrite<CanvasData>(canvasData);

        //    containerLocation = canvasData.canvasName;

        //    content = _content;

        //    z_position = 0;
        //}
        internal void ApplyInheritanceDataOverwrite<T>(T dataclass)
        {
            if (dataclass is CoreData coreData)
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

            }
            //else if (dataclass is CanvasData canvasData)
            //{
            //    brushtype = canvasData.brushtype;
            //    background = canvasData.background;
            //    borderbrush = canvasData.borderbrush;
            //    foreground = canvasData.foreground;
            //    highlight = canvasData.highlight;

            //    cornerRadius = canvasData.cornerRadius;
            //    thickness = canvasData.thickness;

            //    fontSize = canvasData.fontSize;
            //    fontFamily = canvasData.fontFamily;

            //    canvasName = canvasData.canvasName;
            //    imageFilePath = canvasData.imageFilePath;

            //    grouping_displacement = canvasData.grouping_displacement;
            //    z_level_MIN = canvasData.z_level_MIN;
            //    z_level_MAX = canvasData.z_level_MAX;

            //    dragLevel = canvasData.dragLevel;
            //    hoverLevel = canvasData.hoverLevel;
            //    element_spacing = canvasData.element_spacing;

            //    containerLocation = canvasData.canvasName;
            //}
        }

        internal UserControl getContent()
        {
            return content;
        }

        internal void setContent(UserControl userControl)
        {
            this.content = userControl;
        }
    }
}
/*  END OF FILE
 * 
 * 
 */