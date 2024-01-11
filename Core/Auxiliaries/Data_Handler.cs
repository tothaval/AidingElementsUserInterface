/* Aiding Elements User Interface
 *      Data_Handler class
 * 
 * basic data classes management class
 * 
 * init:        2023|12|12
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 * 
 * improvement questions: 
 * should the code be rewritten using generic types to reduce overloaded methods count or 
 * should it be redesigned using f.e. abstract methods and override?
 * 
 * intention is to ensure only one instance of a core data type exists at any time while running,
 * 
 * because more are not needed, since it is not yet planned to implement fully independent
 * customizable elements within elements.
 * a change in options to alter a color should always effect the related core data type and
 * every element depending on it
 */
using AidingElementsUserInterface.Core.AEUI_Data;

using System.Collections.ObjectModel;

namespace AidingElementsUserInterface.Core.Auxiliaries
{
    internal class Data_Handler
    {
        private ButtonData buttonData;
        private CanvasData canvasData;
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
            buttonData = null;

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

        public void AddData(CanvasData data)
        {
            int index = 0;

            foreach (object item in data_objects)
            {
                if (item.GetType() == typeof(CanvasData))
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

            canvasData = data;
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
        public CanvasData? GetCanvasData()
        {
            return canvasData;
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
        public CanvasData? LoadCanvasData()
        {
            CanvasData canvasData = new XML_Handler(new CanvasData()).CanvasData_load();

            if (canvasData != null)
            {
                AddData(canvasData);
            }
            else
            {
                AddData(new ButtonData());
            }

            return GetCanvasData();
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
        public void RemoveData(CanvasData data)
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

        // SetData
        #region SetData method group
        public void SetData(ButtonData data)
        {
            RemoveData(buttonData);

            AddData(data);

            new XML_Handler(data).save_ButtonData(data);
        }
        public void SetData(CanvasData data)
        {
            canvasData = data;
        }

        public void SetData(CoreData data)
        {
            coreData = data;
        }

        public void SetData(MainWindowData data)
        {
            mainWindowData = data;
        }

        public void SetData(TextBoxData data)
        {
            textBoxData = data;
        }
        #endregion RemoveData method group
    }
}
/*  END OF FILE
 * 
 * 
 */