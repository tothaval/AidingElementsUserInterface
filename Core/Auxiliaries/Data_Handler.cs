using AidingElementsUserInterface.Core.MyNote_Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;

namespace AidingElementsUserInterface.Core.Auxiliaries
{
    internal class Data_Handler
    {
        private ObservableCollection<object> data_objects;

        public Data_Handler()
        {
            data_objects = new ObservableCollection<object>();
        }


        public void AddCoreData(CoreData data)
        {
            int index = 0;

            foreach (object item in data_objects)
            {
                if (item.GetType() == typeof(CoreData))
                {
                    data_objects[data_objects.IndexOf(item)] = data;

                    index++;

                    break;                    
                }
            }

            if (index == 0)
            {
                data_objects.Add(data);
            }
        }


        public CoreData? GetCoreData()
        {
            foreach (object item in data_objects)
            {
                if (item is CoreData coreData)
                {
                    return coreData;
                }
            }

            return null;
        }
        public CoreData? LoadCoreData()
        {
            CoreData coreData = new XML_Handler(new CoreData()).CoreData_load();

            if (coreData != null)
            {
                AddCoreData(coreData); 
            }
            else
            {
                AddCoreData(new CoreData());
            }
            
            return GetCoreData();
        }


        public void RemoveData(CoreData data)
        {
            data_objects.Remove(data);
        }

    }
}
