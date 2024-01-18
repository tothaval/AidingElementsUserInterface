/* Aiding Elements User Interface
 *      LanguageProvider 
 *  
 *  this class selects the appropriate language folder and loads the file contents as xaml resources 
 * 
 * init:        2024|01|17
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.Auxiliaries;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AidingElementsUserInterface.Core.AEUI_Logic
{
    //  save language xml files in folder languages\LANGUAGE\FILENAME.xml
    //  major content like manual, license terms or other large aeui system texts
    //  get a file each, if not too much, every other control text will be saved in one file.

    //  with a content save and load system for xml files, a LanguageData class shall be filled
    //  or pointed towards the appropriate folder, if every file is named the same, the folder
    //  is the only relevant criteria for selection, the rest will be done via the xml markup fields

    //  one method in this class must translate all menu item headers before a call to CallCentral
    //  otherwise the elements will not be found

    //  alternatively replace $">{((MenuItem)sender).Header} with ">ELEMENTCLASSNAME"

    // in consequence, TXT_NAME.cs files will be deleted and replaced with this class and a LanguageData class


    internal class LanguageProvider
    {
        public LanguageProvider()
        {
            load();
        }

        private void load()
        {
            XML_Handler handler= new XML_Handler();

            MainWindowData mainWindowData = handler.MainWindowData_load();

            string language = mainWindowData.language;

            if (language != null)
            {
                // process language
                
            }
            else
            {
                language = "default";

                // process language

                // log error|reason for reverting to default 
                //      "language not found, language = "default"
            }
            
            ProcessLanguage(language);
        }

        private void ProcessLanguage(string language)
        {
            string implementTestTextAsResource = $"{language}\fTXT_CLASS.xml";
        }
    }
}
/*  END OF FILE
 * 
 * 
 */