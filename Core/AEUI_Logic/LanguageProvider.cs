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
    //  

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