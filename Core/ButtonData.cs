/* Aiding Elements User Interface
 *      ButtonData class
 * 
 * button element properties
 * 
 * init:        2023|11|28
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AidingElementsUserInterface.Core
{
    internal class ButtonData : CoreData
    {
        public double height { get; set; }
        public double width { get; set; }
        public int expanderSize { get; set; }

        public ButtonData()
        {
            height = 40;
            width = 80;
        }
    }
}
