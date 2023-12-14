/* Aiding Elements User Interface
 *      NoteData class
 * 
 * inherits from: CoreData class
 * 
 * MyNote properties
 * 
 * init:        2023|11|30
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 * 
 * origin:      MyNote_2023_11_01(MIT-license) https://github.com/tothaval/MyNote
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AidingElementsUserInterface.Core.AEUI_Data;

namespace AidingElementsUserInterface.Core.MyNote_Data
{
    internal class NoteData : CoreData
    {
        internal int id { get; set; }
        internal string title { get; set; }
        internal DateTime dateTime { get; set; }
        internal StringBuilder content { get; set; }

        internal NoteData()
        {
            id = 0;
            dateTime = DateTime.Now;

            title = $"title";
            content = new StringBuilder();
        }

    }
}
/*  END OF FILE
 * 
 * 
 */