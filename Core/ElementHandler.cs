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

using AidingElementsUserInterface.Elements;
using AidingElementsUserInterface.Elements.FlatShareCC;
using AidingElementsUserInterface.Elements.MyNote;

namespace AidingElementsUserInterface.Core
{
    internal class ElementHandler : ElementServer
    {
        private Dictionary<CoreContainer, CoreCanvas> containerLocations = new Dictionary<CoreContainer, CoreCanvas>();

        private Dictionary<CoreContainer, UserControl> containerContents = new Dictionary<CoreContainer, UserControl>();
                
        internal ElementHandler()
        {

        }

        internal void addElement(CoreContainer container, CoreCanvas canvas)
        {
            update_instance(container.GetContainerData().getContent());

            containerLocations.Add(container, canvas);

            containerContents.Add(container, container.GetContainerData().getContent());

        }

        internal bool checkElement<T>(T element_content)
        {
            if (element_content is MyNote mynote)
            {
                foreach (CoreContainer content_dict in containerContents.Keys)
                {
                    if (containerContents[content_dict].GetType() == typeof(MyNote))
                    {
                        foreach (CoreContainer location_dict in containerLocations.Keys)
                        {
                            if (content_dict == location_dict)
                            {
                                return false;
                            }

                        }
                    }
                }
            }

            if (element_content is FlatShareCC flatShareCC)
            {
                foreach (CoreContainer content_dict in containerContents.Keys)
                {
                    if (containerContents[content_dict].GetType() == typeof(FlatShareCC))
                    {
                        foreach (CoreContainer location_dict in containerLocations.Keys)
                        {
                            if (content_dict == location_dict)
                            {
                                return false;
                            }

                        }
                    }
                }
            }

            return true;

        }

        internal bool removeElement(CoreContainer container)
        {
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

    }
}
