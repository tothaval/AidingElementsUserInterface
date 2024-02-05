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
        
        internal int CanvasID { get; set; }

        internal string? ContainerDataFilename { get; set; }

        internal int level;

        public ContainerData()
        {
            settings = new SharedLogic().GetDataHandler().GetCoreData();
            
            if (settings == null)
            {
                settings = new CoreData();
            }

            CanvasID = -5;
        }

        public ContainerData(int canvasID)
        {
            settings = new SharedLogic().GetDataHandler().GetCoreData();

            if (settings == null)
            {
                settings = new CoreData();
            }

            CanvasID = canvasID;
        }

        public ContainerData(CoreData coreData, int canvasID)
        {
            settings = coreData;

            CanvasID = canvasID;
        }

        public ContainerData(UserControl _content, int canvasID)
        {
            element = _content;

            level = 0;

            CanvasID = canvasID;
        }

        public ContainerData(CoreData coreData, UserControl _content, int canvasID)
        {
            UpdateCoreData(coreData);

            element = _content;

            level = 0;

            CanvasID = canvasID;
        }

        internal void UpdateCoreData(CoreData coreData)
        {
            settings = coreData;
        }

        internal UserControl GetElement()
        {
            return element;
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