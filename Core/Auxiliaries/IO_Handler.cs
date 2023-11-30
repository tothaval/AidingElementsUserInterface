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
        internal string history_file_path { get; set; }
        internal string log_file_path { get; set; }
        internal string notes_folder_path { get; set; }
        internal string notes_matrix_folder_path { get; set; }
        internal string xml_folder_path { get; set; }

        internal IO_Handler()
        {
            history_file_path = @".\data\history.xml";
            log_file_path = @".\data\log_file.xml";
            notes_folder_path = @"data\notes";
            notes_matrix_folder_path = @"data\notes_matrix";

            xml_folder_path = @"\data\";

            check_paths();
        }

        private void check_paths()
        {

            if (!Directory.Exists(xml_folder_path))
            {
                Directory.CreateDirectory(xml_folder_path);
            }

            if (!Directory.Exists(notes_folder_path))
            {
                Directory.CreateDirectory(notes_folder_path);
            }

            if (!Directory.Exists(notes_matrix_folder_path))
            {
                Directory.CreateDirectory(notes_matrix_folder_path);
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
    }
}
