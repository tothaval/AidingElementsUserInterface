using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
