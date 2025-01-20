/* Aiding Elements User Interface
 *      SystemCanvas element
 * 
 * frame element for a corecanvas that serves as system canvas  
 * 
 * init:        2024|01|28
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.AEUI_Logic;
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
        private CoreCanvas SYSTEM_CANVAS;

        internal CoreCanvas Get_SYSTEM_CANVAS => SYSTEM_CANVAS;


        public SystemCanvas()
        {
            InitializeComponent();

            load();
        }

        private void load()
        {
            SYSTEM_xml xml = new SYSTEM_xml();
            CanvasData canvasData = xml.SYSTEM_CanvasData_load($"{xml.SYSTEM_folder}{xml.CanvasData_file}");


            if (canvasData == null)
            {
                canvasData = new CanvasData($"SYSTEM", 0);
            }
            canvasData.canvasID = 0;

            SYSTEM_CANVAS = new CoreCanvas(canvasData);

            SYSTEM_CANVAS.set_SYSTEM_CANVAS_FLAG(true);

            border.Child = SYSTEM_CANVAS;
        }

        internal void save()
        {
            SYSTEM_xml xml = new SYSTEM_xml();
            LevelSystem levelSystem = SYSTEM_CANVAS.GetLevelSystem();

            string path = $"{xml.SYSTEM_Container_folder}";
            xml.delete_files(path);

            string path_ = $"{xml.SYSTEM_folder}";
            xml.delete_files(path_);

            xml.SYSTEM_CanvasData_save(SYSTEM_CANVAS);
            xml.saveLevels(levelSystem);

            foreach (object item in SYSTEM_CANVAS.canvas.Children)
            {
                if (item.GetType() == typeof(CoreContainer))
                {
                    CoreContainer container = item as CoreContainer;

                    xml.SYSTEM_Container_save(container, SYSTEM_CANVAS.canvas.Children.IndexOf(container));
                }
            }
        }
    }
}
/*  END OF FILE
 * 
 * 
 */