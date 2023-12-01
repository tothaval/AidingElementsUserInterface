using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using AidingElementsUserInterface.Elements;
using AidingElementsUserInterface.Elements.MyNote;

namespace AidingElementsUserInterface.Core
{
    internal class ElementHandler
    {
        Dictionary<CoreContainer, CoreCanvas> containerLocations = new Dictionary<CoreContainer, CoreCanvas>();

        Dictionary<CoreContainer, UserControl> containerContents = new Dictionary<CoreContainer, UserControl>();


        internal ElementHandler()
        {

        }

        internal void addElement(CoreContainer container, CoreCanvas canvas)
        {
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
