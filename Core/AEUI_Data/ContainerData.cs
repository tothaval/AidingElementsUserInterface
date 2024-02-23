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
        internal CoreData settings = new CoreData();

        internal object element { get; set; }
        
        internal int CanvasID { get; set; }

        internal string? ContainerDataFilename { get; set; }

        // low prio:
        // integrate level and rotation save and load
        internal int level;
        internal double rotation;

        // low prio:
        // replace current dragPoint return with a ContainerData value
        // use dragPoint solely for dragging
        internal Point position;

        public ContainerData(bool load)
        {
                
        }

        public ContainerData()
        {
            CanvasID = -5;
        }

        public ContainerData(int canvasID)
        {
            CanvasID = canvasID;
        }

        public ContainerData(CoreData coreData, int canvasID, int level = 0, double rotation = 0.0)
        {
            settings = coreData;

            CanvasID = canvasID;

            this.level = level;

            this.rotation = rotation;
        }

        public ContainerData(UserControl _content, int canvasID, int level = 0, double rotation = 0.0)
        {
            element = _content;

            this.level = level;

            this.rotation = rotation;

            CanvasID = canvasID;
        }

        public ContainerData(CoreData coreData, object _content, int canvasID, int level = 0, double rotation = 0.0)
        {
            settings = coreData;            

            element = _content;

            this.level = level;

            this.rotation = rotation;

            CanvasID = canvasID;
        }

        internal void UpdateCoreData(CoreData coreData)
        {
            this.settings = coreData;
        }

        internal object GetElement()
        {
            return element;
        }

        internal void SetCanvasID(int canvasID)
        {
            this.CanvasID = canvasID;
        }

        internal void SetContainerDataFilename(string containerDataFilename)
        {
            this.ContainerDataFilename = containerDataFilename;
        }

        internal void SetElement(object userControl)
        {
            this.element = userControl;
        }
    }
}
/*  END OF FILE
 * 
 * 
 */