/* Aiding Elements User Interface
 *      Data_Handler class
 * 
 * basic data classes management class
 * 
 * init:        2023|12|12
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.AEUI_Data;

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace AidingElementsUserInterface.Core.Auxiliaries
{
    internal class Data_Handler
    {
        private CoreData buttonData;
        private CanvasData canvasData;
        private CoreData coreData;
        private ContainerData containerData;
        private CoreData labelData;
        private LinkData linkData;
        private MainWindowData mainWindowData;
        private CoreData textBoxData;


        private ObservableCollection<object> data_objects;

        public Data_Handler()
        {
            data_objects = new ObservableCollection<object>();                        
        }

        // linkData management
        #region data management

        // AddData 
        #region AddData method group
        public void AddButtonData(CoreData data)
        {
            data_objects.Add(data);

            buttonData = data;
        }

        public void AddCanvasData(CanvasData data)
        {
            data_objects.Add(data);

            canvasData = data;
        }
        public void AddContainerData(ContainerData data)
        {
            data_objects.Add(data);

            containerData = data;
        }
        public void AddCoreData(CoreData data)
        {
            data_objects.Add(data);

            coreData = data;
        }

        public void AddLabelData(CoreData data)
        {
            data_objects.Add(data);

            labelData = data;
        }

        public void AddMainWindowData(MainWindowData data)
        {
            data_objects.Add(data);

            mainWindowData = data;
        }

        public void AddTextBoxData(CoreData data)
        {
            data_objects.Add(data);

            textBoxData = data;
        }
        #endregion AddData method group

        // Get...Data()
        #region get data methods
        public CoreData? GetButtonData()
        {
            return buttonData;
        }
        public CanvasData? GetCanvasData()
        {
            return canvasData;
        }
        public ContainerData? GetContainerData()
        {
            return containerData;
        }
        public CoreData? GetCoreData()
        {
            return coreData;
        }
        public CoreData? GetLabelData()
        {
            return labelData;
        }
        public MainWindowData? GetMainWindowData()
        {
            return mainWindowData;
        }

        public CoreData? GetTextBoxData()
        {
            return textBoxData;
        }
        #endregion get data methods

        // Load...Data()
        #region load data methods
        public CoreData? LoadButtonData()
        {
            CoreData buttonData = new XML_Handler().ButtonData_load();

            if (buttonData != null)
            {
                AddButtonData(buttonData);
            }
            else
            {
                AddButtonData(new CoreData());
            }

            return GetButtonData();
        }

        //public CanvasData? LoadCanvasData()
        //{
        //    if (canvasData != null)
        //    {
        //        AddCanvasData(canvasData);
        //    }
        //    else
        //    {
        //        AddCanvasData(new CanvasData());
        //    }

        //    return GetCanvasData();
        //}

        public CoreData? LoadCoreData()
        {
            CoreData coreData = new XML_Handler().CoreData_load();

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

        public ContainerData? LoadContainerData()
        {
            //ContainerData coreData = new XML_Handler().co();

            //if (coreData != null)
            //{
            //    AddCoreData(coreData);
            //}
            //else
            //{
            //    AddCoreData(new CoreData());
            //}

            //return GetCoreData();

            return new ContainerData(coreData, -5);
        }

        public CoreData? LoadLabelData()
        {
            CoreData labelData = new XML_Handler().LabelData_load();

            if (labelData != null)
            {
                AddLabelData(labelData);
            }
            else
            {
                AddLabelData(new CoreData());
            }

            return GetLabelData();
        }
        public MainWindowData? LoadMainWindowData()
        {
            MainWindowData mainWindowData = new XML_Handler().MainWindowData_load();

            if (mainWindowData != null)
            {
                AddMainWindowData(mainWindowData);
            }
            else
            {
                AddMainWindowData(new MainWindowData());
            }

            return GetMainWindowData();
        }

        public CoreData? LoadTextBoxData()
        {
            CoreData textboxData = new XML_Handler().TextBoxData_load();

            if (textboxData != null)
            {
                AddTextBoxData(textboxData);
            }
            else
            {
                AddTextBoxData(new CoreData());
            }

            return GetTextBoxData();
        }
        #endregion load data methods
        #endregion CoreData management

        // SetData
        #region SetData method group
        public void SetButtonData(CoreData data)
        {
            AddButtonData(data);            

            new XML_Handler().save_ButtonData(data);
        }

        public void SetCanvasData(CanvasData data)
        {
            AddCanvasData(data);

            new XML_Handler().CanvasData_save(data);

            canvasData = data;
        }

        public void SetContainerData(ContainerData data)
        {
            AddContainerData(data);

            new XML_Handler().ContainerDataTemplate_save(data);

            containerData = data;
        }


        public void SetCoreData(CoreData data)
        {
            AddCoreData(data);

            new XML_Handler().CoreData_save();

            coreData = data;
        }
        public void SetLabelData(CoreData data)
        {
            AddLabelData(data);

            new XML_Handler().save_LabelData(data);
        }
        public void SetMainWindowData(MainWindowData data)
        {
            AddMainWindowData(data);

            new XML_Handler().MainWindowData_save();

            mainWindowData = data;
        }

        public void SetTextBoxData(CoreData data)
        {
            AddTextBoxData(data);

            new XML_Handler().TextBoxData_save();
        }
        #endregion RemoveData method group
    }
}
/*  END OF FILE
 * 
 * 
 */