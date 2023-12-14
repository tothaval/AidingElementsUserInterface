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
using AidingElementsUserInterface.Core.MyNote_Data;
using AidingElementsUserInterface.Elements.FlatShareCC;
using AidingElementsUserInterface.Elements.MyNote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AidingElementsUserInterface.Core.Auxiliaries
{
    internal class IO_Handler
    {
        // Core paths
        #region Core paths
        // files
        internal string ButtonData_file = @".\data\Core\buttondata.xml";        
        internal string CoreData_file = @".\data\Core\coredata.xml";
        internal string MainWindowData_file = @".\data\Core\mainwindowdata.xml";
        internal string TextBoxData_file = @".\data\Core\textboxdata.xml";
        // folders
        internal string Core_xml_folder = @"data\Core\";        
        internal string ContainerData_xml_folder = @"data\Core\ContainerData\";
        #endregion Core paths

        //FlatShareCostCalculator paths
        internal string FlatShareCC_data_file = @".\data\FlatShareCC\data.xml";
        internal string FlatShareCC_xml_folder = @"data\FlatShareCC\";

        //MyNote paths
        internal string MyNote_history_file = @".\data\MyNote\history.xml";
        internal string MyNote_log_file = @".\data\MyNote\log_file.xml";
        internal string MyNote_notes_folder = @"data\MyNote\notes\";
        internal string MyNote_notes_matrix_folder = @"data\MyNote\notes_matrix\";
        internal string MyNote_xml_folder = @"\data\MyNote\";

        internal IO_Handler(CoreData coreData)
        {
            check_path(Core_xml_folder);
            check_path(ContainerData_xml_folder);
        }

        internal IO_Handler(FlatData flatShareCC)
        {
            check_path(FlatShareCC_xml_folder);
        }

        internal IO_Handler(MyNote myNote)
        {
            check_path(MyNote_notes_folder);
            check_path(MyNote_notes_matrix_folder);
            check_path(MyNote_xml_folder);
        }

        private void check_path(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
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

        public void delete_files(string folderpath, string exception)
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
    }
}
/*  END OF FILE
 * 
 * 
 */