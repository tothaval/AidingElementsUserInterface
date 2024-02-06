/* Aiding Elements User Interface
 *      IO_Handler class
 * 
 * basic input output properties and logic
 * 
 * init:        2023|11|30
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 * 
 * origin:      MyNote_2023_11_01
 */
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.FlatShareCC_Data;
using AidingElementsUserInterface.Elements.MyNote;

using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace AidingElementsUserInterface.Core.Auxiliaries
{
    internal class IO_Handler
    {
        internal string[] CoreCanvasScreens = new string[10];

        // Core paths
        #region Core paths
        // files
        internal string ButtonData_file = @".\data\Core\buttondata.xml";
        internal string CanvasData_file = @"canvasdata.xml";
        internal string ContainerData_file = @".\data\Core\containerdata.xml";
        internal string CoreData_file = @".\data\Core\coredata.xml";
        internal string LabelData_file = @".\data\Core\labeldata.xml";
        internal string MainWindowData_file = @".\data\Core\mainwindowdata.xml";
        internal string TextBoxData_file = @".\data\Core\textboxdata.xml";
        
        // folders
        internal string Core_xml_folder = @"data\Core\";
        #endregion Core paths

        #region screens
        internal string SYSTEM_folder = @"data\System\";
        internal string SYSTEM_Container_folder = @"data\System\Container";
        internal string UserSpace_folder = @"data\UserSpace\";
        #endregion screens

        //FlatShareCostCalculator paths
        internal string FlatShareCC_data_file = @".\data\FlatShareCC\data.xml";
        internal string FlatShareCC_xml_folder = @"data\FlatShareCC\";

        //MyNote paths
        internal string MyNote_history_file = @".\data\MyNote\history.xml";
        internal string MyNote_log_file = @".\data\MyNote\log_file.xml";
        internal string MyNote_notes_folder = @"data\MyNote\notes\";
        internal string MyNote_notes_matrix_folder = @"data\MyNote\notes_matrix\";
        internal string MyNote_xml_folder = @"\data\MyNote\";

        internal IO_Handler()
        {
            build_path_structure();
        }

        private void build_path_structure()
        {
            check_path(Core_xml_folder);
            check_path(SYSTEM_folder);
            check_path(SYSTEM_Container_folder);

            check_path(UserSpace_folder);
            
            for (int i = 1; i < CoreCanvasSwitchData.Get_CORECANVAS_CAP; i++)
            {
                string screen_folder = $"{UserSpace_folder}\\Screen_{i}\\";
                check_path(screen_folder);

                string container_folder = $"{screen_folder}\\Container\\";
                check_path(container_folder);
            }

            check_path(FlatShareCC_xml_folder);

            check_path(MyNote_notes_folder);
            check_path(MyNote_notes_matrix_folder);
            check_path(MyNote_xml_folder);
        }

        protected bool check_path(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

                return false;
            }

            return true;
        }

        internal void delete_files(string folderpath)
        {
            string[] paths;

            paths = Directory.GetFiles(folderpath);

            foreach (string path in paths)
            {
                File.Delete(path);
            }
        }

        internal void delete_files(string folderpath, string exception)
        {
            string[] paths;

            paths = Directory.GetFiles(folderpath);

            foreach (string path in paths)
            {
                if (path != exception)
                {
                    File.Delete(path);
                }
            }
        }

        internal List<string> scan_directory(string path)
        {
            List<string> scan_list = new List<string>();

            scan_list = Directory.GetFiles(path).ToList<string>();

            StringBuilder stringBuilder = new StringBuilder();

            foreach (string item in scan_list)
            {
                stringBuilder.AppendLine(item);
            }


            //MessageBox.Show(stringBuilder.ToString());

            return scan_list;
        }
    }
}
/*  END OF FILE
 * 
 * 
 */