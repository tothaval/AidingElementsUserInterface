using AidingElementsUserInterface.Core.AEUI_Data;
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
        private ButtonData buttonData;
        private CoreData coreData;
        private MainWindowData mainWindowData;
        private TextBoxData textBoxData;


        private ObservableCollection<object> data_objects;

        public Data_Handler()
        {
            data_objects = new ObservableCollection<object>();
        }

        // data management
        #region data management

        // AddData 
        #region AddData method group
        public void AddData(ButtonData data)
        {
            int index = 0;

            foreach (object item in data_objects)
            {
                if (item.GetType() == typeof(ButtonData))
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

            buttonData = data;
        }

        public void AddData(CoreData data)
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

            coreData = data;
        }
        public void AddData(MainWindowData data)
        {
            int index = 0;

            foreach (object item in data_objects)
            {
                if (item.GetType() == typeof(MainWindowData))
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

            mainWindowData = data;
        }

        public void AddData(TextBoxData data)
        {
            int index = 0;

            foreach (object item in data_objects)
            {
                if (item.GetType() == typeof(TextBoxData))
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

            textBoxData = data;
        }
        #endregion AddData method group

        // Get...Data()
        #region get data methods
        public ButtonData? GetButtonData()
        {
            return buttonData;
        }
        public CoreData? GetCoreData()
        {
            return coreData;
        }
        public MainWindowData? GetMainWindowData()
        {
            return mainWindowData;
        }

        public TextBoxData? GetTextBoxData()
        {
            return textBoxData;
        }
        #endregion get data methods

        // Load...Data()
        #region load data methods
        public ButtonData? LoadButtonData()
        {
            ButtonData buttonData = new XML_Handler(new ButtonData()).ButtonData_load();

            if (buttonData != null)
            {
                AddData(buttonData);
            }
            else
            {
                AddData(new ButtonData());
            }

            return GetButtonData();
        }

        public CoreData? LoadCoreData()
        {
            CoreData coreData = new XML_Handler(new CoreData()).CoreData_load();

            if (coreData != null)
            {
                AddData(coreData);
            }
            else
            {
                AddData(new CoreData());
            }

            return GetCoreData();
        }

        public MainWindowData? LoadMainWindowData()
        {
            MainWindowData mainWindowData = new XML_Handler(new MainWindowData()).MainWindowData_load();

            if (mainWindowData != null)
            {
                AddData(mainWindowData);
            }
            else
            {
                AddData(new MainWindowData());
            }

            return GetMainWindowData();
        }

        public TextBoxData? LoadTextBoxData()
        {
            TextBoxData textboxData = new XML_Handler(new TextBoxData()).TextBoxData_load();

            if (textboxData != null)
            {
                AddData(textboxData);
            }
            else
            {
                AddData(new TextBoxData());
            }

            return GetTextBoxData();
        }
        #endregion load data methods
        #endregion CoreData management

        // RemoveData
        #region RemoveData method group
        public void RemoveData(ButtonData data)
        {
            data_objects.Remove(data);
        }

        public void RemoveData(CoreData data)
        {
            data_objects.Remove(data);
        }

        public void RemoveData(MainWindowData data)
        {
            data_objects.Remove(data);
        }

        public void RemoveData(TextBoxData data)
        {
            data_objects.Remove(data);
        }
        #endregion RemoveData method group
    }
}
