/* Aiding Elements User Interface
 *      CoreCanvasSwitch 
 *  
 * manages the corecanvas screens
 * 
 * init:        2024|01|28
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.AEUI_HelperClasses;
using AidingElementsUserInterface.Core.AEUI_Logic;
using AidingElementsUserInterface.Core.AEUI_UserControls;
using AidingElementsUserInterface.Core.Auxiliaries;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AidingElementsUserInterface.Core.AEUI_SystemControls
{
    /// <summary>
    /// Interaktionslogik für CoreCanvasSwitch.xaml
    /// </summary>
    public partial class CoreCanvasSwitch : UserControl
    {
        private int ACTIVE_CANVAS_ID;

        private ObservableCollection<CoreContainer> _copySelection;
        private ObservableCollection<CoreCanvas> coreCanvasScreens = new ObservableCollection<CoreCanvas>();

        private ObservableCollection<SystemPulseTimer> systemPulseTimers = new ObservableCollection<SystemPulseTimer>();


        internal ObservableCollection<CoreCanvas> Get_coreCanvasScreens => coreCanvasScreens;
        internal ObservableCollection<SystemPulseTimer> Get_systemPulseTimers => systemPulseTimers;

        public CoreCanvasSwitch()
        {
            InitializeComponent();

            initiate();

            registerEvents();
        }

        private void CTB_canvasNameChange_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                canvasNamePanelSwitch();
            }
        }

        private void CL_canvasNameDisplay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                canvasNamePanelSwitch();
            }
        }

        private void CL_canvasNameDisplay_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void canvasNamePanelSwitch()
        {
            if (CL_SWITCH.Visibility == Visibility.Visible)
            {

                CL_SWITCH.Visibility = Visibility.Collapsed;
                CTB_SWITCH.Visibility = Visibility.Visible;
            }
            else
            {
                CL_SWITCH.Visibility = Visibility.Visible;
                CTB_SWITCH.Visibility = Visibility.Collapsed;
            }


            string newName = CTB_canvasNameChange.getText();

            coreCanvasScreens[ACTIVE_CANVAS_ID].getCanvasData().canvasName = newName;
            CL_canvasNameDisplay.setText(newName);
        }

        internal void copy()
        {
            _copySelection = new ObservableCollection<CoreContainer>();

            _copySelection = new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS.get_selected_items();
        }
        internal void move()
        {
            // not allowed to move CoreContainers from System Canvas to CoreCanvasSwitch
            // exception: template containers and elements, copied element types must be
            // instantiated on the fly or within target canvas to avoid that system files
            // and crutial data can be destroyed that way

            // it is allowed to move CoreContainers between CoreCanvasSwitch screens
            // unless a Level restriction or a Screen restriction is in effect
            // and the user does not have sufficient rights or ranks.

            if (!new SharedLogic().GetMainWindow().Get_SYSTEM_ACTIVE_FLAG)
            {
                _copySelection[0].GetCanvas().delete_selected_items();


                foreach (CoreContainer item in _copySelection)
                {
                    coreCanvasScreens[ACTIVE_CANVAS_ID].canvas.Children.Add(item);
                }

                _copySelection.Clear();
            }
        }
        internal void paste()
        {
            if (_copySelection != null)
            {
                foreach (CoreContainer item in _copySelection)
                {
                    Type type = item.GetContainerData().element.GetType();

                    coreCanvasScreens[ACTIVE_CANVAS_ID].GetCentral().ExecuteCommandRequest($">{type.Name}");

                    // add code to position the element via copying the origin container values                    
                }
            }
        }

        private async void initiate()
        {
            UserSpace_xml xml = new UserSpace_xml();

            for (int i = 1; i < CoreCanvasSwitchData.Get_CORECANVAS_CAP; i++)
            {
                CanvasData canvasData = xml.CanvasData_load($"{xml.UserSpace_folder}screen_{i}\\{xml.CanvasData_file}");
                
                if (canvasData == null)
                {
                    canvasData = new CanvasData($"UserSpace_{i}", i);
                }

                canvasData.canvasID = i;

                CoreCanvas screen = new CoreCanvas(canvasData);
                SystemPulseTimer systemPulseTimer = new SystemPulseTimer(i);
                
                coreCanvasScreens.Add(screen);
                systemPulseTimers.Add(systemPulseTimer);
                }

            ACTIVE_CANVAS_ID = 0;
            Set_ACTIVE_CANVAS();
        }        

        private void Left()
        {
            ACTIVE_CANVAS_ID--;

            if (ACTIVE_CANVAS_ID < 0)
            {
                ACTIVE_CANVAS_ID = CoreCanvasSwitchData.Get_CORECANVAS_CAP - 2;
            }

            Set_ACTIVE_CANVAS();
        }

         private void registerEvents()
        {
            CB_left.button.Click += CB_left_Click;
            CB_right.button.Click += CB_right_Click;

            CL_canvasNameDisplay.textblock.MouseLeftButtonDown += CL_canvasNameDisplay_MouseLeftButtonDown;
            CL_canvasNameDisplay.textblock.MouseLeftButtonUp += CL_canvasNameDisplay_MouseLeftButtonUp;
            CTB_canvasNameChange.textbox.KeyUp += CTB_canvasNameChange_KeyUp;
        }

        private void Right()
        {
            ACTIVE_CANVAS_ID++;

            if (ACTIVE_CANVAS_ID > CoreCanvasSwitchData.Get_CORECANVAS_CAP - 2)
            {
                ACTIVE_CANVAS_ID = 0;
            }

            Set_ACTIVE_CANVAS();
        }

        internal void save_Screens()
        {
            //XML_Handler handler = new XML_Handler();

      
            int counter = 0;

            for (int i = 0; i < CoreCanvasSwitchData.Get_CORECANVAS_CAP - 1; i++)
            {
                UserSpace_xml handler = new UserSpace_xml();
                string path = $"{handler.UserSpace_folder}screen_{i + 1}\\Container";

                handler.delete_files(path);

                string path_ = $"{handler.UserSpace_folder}screen_{i + 1}";

                handler.delete_files(path_);
            }


            for (int i = 0; i < CoreCanvasSwitchData.Get_CORECANVAS_CAP - 1; i++)
            {
                UserSpace_xml handler = new UserSpace_xml();
                handler.CanvasData_save(coreCanvasScreens[i],i+1);            

                int id = coreCanvasScreens[i].getCanvasData().canvasID;

                handler.saveLevels(coreCanvasScreens[i].GetLevelSystem());

                foreach (object item in coreCanvasScreens[i].canvas.Children)
                {

                    if (item.GetType() == typeof(CoreContainer))
                    {
                        CoreContainer container = (CoreContainer)item;

                        handler.Container_save(container, counter, id);

                        counter++;
                    }
                }

                counter = 0;
            }
        }


        private void Set_ACTIVE_CANVAS()
        {
            border.Child = null;

            border.Child = coreCanvasScreens[ACTIVE_CANVAS_ID];

            new SharedLogic().GetMainWindow().set_ACTIVE_CANVAS(coreCanvasScreens[ACTIVE_CANVAS_ID]);

            CTB_canvasNameChange.setText(coreCanvasScreens[ACTIVE_CANVAS_ID].getCanvasData().canvasName);

            CL_canvasNameDisplay.setText(CTB_canvasNameChange.getText());
        }

        private void CB_left_Click(object sender, RoutedEventArgs e)
        {
            Left();

            e.Handled = true;
        }

        private void CB_right_Click(object sender, RoutedEventArgs e)
        {
            Right();

            e.Handled = true;
        }

        internal CoreCanvas Get_ACTIVE_CANVAS()
        {
            try
            {
                if (coreCanvasScreens[ACTIVE_CANVAS_ID] != null)
                {
                }

                return coreCanvasScreens[ACTIVE_CANVAS_ID];
            }
            catch (Exception)
            {
                return new SharedLogic().GetMainWindow().Get_SYTEM_CANVAS.Get_SYSTEM_CANVAS;
            }
        }

    }
}
/*  END OF FILE
 * 
 * 
 */