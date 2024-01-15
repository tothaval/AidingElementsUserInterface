/* Aiding Elements User Interface
 *      ContainerData class
 *      
 * inherits from: CoreData class
 * 
 * container properties
 * 
 * init:        2023|11|29
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.Auxiliaries;
using AidingElementsUserInterface.Core.MyNote_Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

using UserControl = System.Windows.Controls.UserControl;

namespace AidingElementsUserInterface.Core.AEUI_Data
{

    internal class ContainerData
    {
        internal CoreData settings { get; set; }
        internal UserControl element { get; set; }

        internal string? CanvasName { get; set; }
        internal string? ContainerDataFilename { get; set; }

        internal int z_position;

        public ContainerData()
        {
            settings = new SharedLogic().GetDataHandler().GetCoreData();

            if (settings == null)
            {
                settings = new CoreData();
            }
        }

        public ContainerData(CoreData coreData)
        {
            settings = coreData;
        }

        public ContainerData(UserControl _content)
        {
            element = _content;

            z_position = 0;
        }

        public ContainerData(CoreData coreData, UserControl _content)
        {
            UpdateCoreData(coreData);

            element = _content;

            z_position = 0;
        }

        internal void UpdateCoreData(CoreData coreData)
        {
            settings = coreData;
        }

        internal UserControl GetElement()
        {
            return element;
        }

        internal void SetCanvasName(string canvasName)
        {
            this.CanvasName = canvasName;
            
        }

        internal void SetContainerDataFilename(string containerDataFilename)
        {
            this.ContainerDataFilename = containerDataFilename;
        }

        internal void SetElement(UserControl userControl)
        {
            this.element = userControl;
        }
    }
}
/*  END OF FILE
 * 
 * 
 */