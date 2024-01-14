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
using System.Windows.Controls;

using AidingElementsUserInterface.Core.AEUI_UserControls;
using AidingElementsUserInterface.Core.Auxiliaries;
using AidingElementsUserInterface.Elements.FlatShareCC;

namespace AidingElementsUserInterface.Core
{
    internal class ElementHandler : ElementServer
    {
        private Dictionary<CoreContainer, CoreCanvas> containerLocations = new Dictionary<CoreContainer, CoreCanvas>();

        private Dictionary<CoreContainer, UserControl> containerContents = new Dictionary<CoreContainer, UserControl>();

        internal ElementHandler()
        {

        }


        internal async void addElement(CoreContainer container, CoreCanvas canvas)
        {
            container.GetContainerData().CanvasName = canvas.Name;

            containerLocations.Add(container, canvas);

            containerContents.Add(container, container.GetContainerData().GetElement());


        }

        internal Dictionary<CoreContainer, UserControl> GetContainerContentsDict()
        {
            return containerContents;
        }

        internal bool removeElement(CoreContainer container)
        {
            if (container.GetContainerData().GetElement() is FlatShareCC flatShareCC)
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

            foreach (CoreContainer container in containerLocations.Keys)
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