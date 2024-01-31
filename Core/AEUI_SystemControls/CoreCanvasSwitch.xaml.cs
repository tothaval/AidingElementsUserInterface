using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.AEUI_UserControls;
using AidingElementsUserInterface.Core.Auxiliaries;
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
        internal ObservableCollection<CoreCanvas> Get_coreCanvasScreens => coreCanvasScreens;

        public CoreCanvasSwitch()
        {
            InitializeComponent();

            registerEvents();

            load_last_ACTIVE_CANVAS();

            canvasNamePanel();
        }

        private void canvasNameUpdate()
        {
            CL_canvasNameDisplay.setText(coreCanvasScreens[ACTIVE_CANVAS_ID].get_CANVAS_NAME());
        }


        private void canvasNamePanel()
        {
            CL_canvasNameDisplay.setText(coreCanvasScreens[ACTIVE_CANVAS_ID].get_CANVAS_NAME());

            CL_canvasNameDisplay.textblock.MouseLeftButtonDown += CL_canvasNameDisplay_MouseLeftButtonDown;
            CL_canvasNameDisplay.textblock.MouseLeftButtonUp += CL_canvasNameDisplay_MouseLeftButtonUp;
            CTB_canvasNameChange.textbox.KeyUp += CTB_canvasNameChange_KeyUp;
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

            coreCanvasScreens[ACTIVE_CANVAS_ID].set_CANVAS_NAME(CTB_canvasNameChange.getText());

            canvasNameUpdate();
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


        private void initiate()
        {
            ACTIVE_CANVAS_ID = 0;

            // 1 corecanvas is reserved for system, that is why i = 1 instead of i = 0;
            for (int i = 1; i < CoreCanvasSwitchData.Get_CORECANVAS_CAP; i++)
            {
                coreCanvasScreens.Add(new CoreCanvas($"coreCanvas_{i}"));
            }

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


        private void load_last_ACTIVE_CANVAS()
        {
            bool loadsuccess = false;

            if (loadsuccess)
            {

            }
            else
            {
                initiate();
            }

        }

        private void registerEvents()
        {
            CB_left.button.Click += CB_left_Click;
            CB_right.button.Click += CB_right_Click;
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

        private void Set_ACTIVE_CANVAS()
        {
            border.Child = null;

            border.Child = coreCanvasScreens[ACTIVE_CANVAS_ID];

            canvasNameUpdate();

            new SharedLogic().GetMainWindow().set_ACTIVE_CANVAS(coreCanvasScreens[ACTIVE_CANVAS_ID]);
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
            return coreCanvasScreens[ACTIVE_CANVAS_ID];
        }

    }
}
