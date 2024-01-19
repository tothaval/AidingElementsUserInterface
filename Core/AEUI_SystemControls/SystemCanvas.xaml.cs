using AidingElementsUserInterface.Core.AEUI_UserControls;
using AidingElementsUserInterface.Core.Auxiliaries;
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
    /// Interaktionslogik für SystemCanvas.xaml
    /// </summary>
    public partial class SystemCanvas : UserControl
    {
        internal CoreCanvas Get_SYSTEM_CANVAS => SYSTEM_CANVAS;


        public SystemCanvas()
        {
            InitializeComponent();

            load();
        }

        private void load()
        {
            XML_Handler xml_Handler = new XML_Handler();

            foreach (CoreContainer item in xml_Handler.Container_load())
            {
                item.setCanvas(ref SYSTEM_CANVAS);

                SYSTEM_CANVAS.add_element_to_canvas(item, item.get_dragPoint());

                item.load_Container();

                SYSTEM_CANVAS.getCanvasData().canvasName = "SYSTEM_CANVAS";
            }
        }


    }
}
