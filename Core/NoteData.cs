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
 * origin:      MyNote_2023_11_01
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AidingElementsUserInterface.Core
{
    internal class NoteData : CoreData
    {
        internal int id { get; set; }
        internal string title { get; set; }
        internal DateTime dateTime { get; set; }
        internal StringBuilder content { get; set; }

        internal NoteData()
        {
            this.id = 0;
            this.dateTime = DateTime.Now;

            this.title = $"title";
            content = new StringBuilder();
        }

    }
}
