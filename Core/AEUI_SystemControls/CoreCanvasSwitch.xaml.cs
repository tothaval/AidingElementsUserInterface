using AidingElementsUserInterface.Core.AEUI_Data;
using System;
using System.Collections.Generic;
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

        private CoreCanvas[] coreCanvasScreens = new CoreCanvas[10];

        internal CoreCanvas[] Get_coreCanvasScreens => coreCanvasScreens;

        public CoreCanvasSwitch()
        {
            InitializeComponent();

            registerEvents();

            load_last_ACTIVE_CANVAS();
        }

        private void initiate()
        {
            ACTIVE_CANVAS_ID = 0;

            // 1 corecanvas is reserved for system, that is why i = 1 instead of i = 0;
            for (int i = 1; i < CoreCanvasSwitchData.Get_CORECANVAS_CAP; i++)
            {
                coreCanvasScreens[i - 1] = new CoreCanvas($"coreCanvas_{i}");
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
            border.Child = coreCanvasScreens[ACTIVE_CANVAS_ID];
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
