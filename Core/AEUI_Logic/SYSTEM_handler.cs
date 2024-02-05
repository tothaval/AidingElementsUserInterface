using AidingElementsUserInterface.Core.AEUI_UserControls;
using AidingElementsUserInterface.Core.Auxiliaries;
using AidingElementsUserInterface.Elements.FlatShareCC;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AidingElementsUserInterface.Core.AEUI_Logic
{
    class SYSTEM_handler : ElementServer
    {
        private ObservableCollection<CoreContainer> SYSTEM_containers = new ObservableCollection<CoreContainer>();

        internal ObservableCollection<CoreContainer> GET_SYSTEM_containers => SYSTEM_containers;

        public SYSTEM_handler()
        {
                
        }

        internal void addSYSTEMelement(CoreContainer container)
        {
            MessageBox.Show(container.GetContainerData().element.GetType().Name);

            if (container != null) 
            {
                SYSTEM_containers.Add(container);
            }            
        }

        internal bool removeSYSTEMelement(CoreContainer container)
        {
            try
            {
                if (container != null)
                {
                    SYSTEM_containers.Remove(container);
                }

            }
            catch (Exception)
            {
                return false;

            }

            return true;
        }

    }
}
