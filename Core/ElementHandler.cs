/* Aiding Elements User Interface
 *      ElementHandler class
 * 
 * this class manages element storage
 * 
 * init:        2023|11|30
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.AEUI_UserControls;
using AidingElementsUserInterface.Core.Auxiliaries;
using AidingElementsUserInterface.Elements;
using AidingElementsUserInterface.Elements.FlatShareCC;
using AidingElementsUserInterface.Elements.MyNote;

namespace AidingElementsUserInterface.Core
{
    internal class ElementHandler : ElementServer
    {
        private Dictionary<AEUI_Container, CoreCanvas> containerLocations = new Dictionary<AEUI_Container, CoreCanvas>();

        private Dictionary<AEUI_Container, UserControl> containerContents = new Dictionary<AEUI_Container, UserControl>();

        internal ElementHandler()
        {

        }


        internal async void addElement(AEUI_Container container, CoreCanvas canvas)
        {
            container.GetContainerData().containerLocation = canvas.Name;

            containerLocations.Add(container, canvas);

            containerContents.Add(container, container.GetContainerData().getContent());


        }

        internal Dictionary<AEUI_Container, UserControl> GetContainerContentsDict()
        {
            return containerContents;
        }

        internal bool removeElement(AEUI_Container container)
        {
            if (container.GetContainerData().getContent() is FlatShareCC flatShareCC)
            {
                flatShareCC.end_this();
            }

            try
            {
                containerLocations.Remove(container);
                containerContents.Remove(container);
            }
            catch (Exception)
            {
                return false;

            }

            return true;
        }

        internal void save_state(XML_Handler handler)
        {
            int counter = 0;

            handler.delete_files(handler.ContainerData_xml_folder);

            foreach (AEUI_Container container in containerLocations.Keys)
            {
                handler.Container_save(container, counter);

                counter++;
            }
        }

        // public void save_state() ... use for loops, the index is the filename or foldername,
        // save on a per canvas basis, for each canvas save all container including contents
        // each container needs one file, so per canvas there are n files, depending on canvas
        // container element children count.
        // somehow filter, if a canvas is the same in container_locations collection, 
        // start with foreach value canvas in container_locations... count per int within the loop
        
        // think really hard about loading order of canvas files
        // (not yet necessary, because canvas instantiation not yet implemented.)

    }
}
/*  END OF FILE
 * 
 * 
 */