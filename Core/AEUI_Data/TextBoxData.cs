/* Aiding Elements User Interface
 *      TextBoxData class
 * 
 * inherits from: CoreData class
 * 
 * CoreTextBox properties
 * 
 * init:        2023|12|02
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 *  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace AidingElementsUserInterface.Core.AEUI_Data
{
    internal class TextBoxData : CoreData
    {
        public TextBoxData(bool load)
        {
            if (load)
            {

            }
        }

        public TextBoxData()
        {

        }

        public TextBoxData(CoreData coreData)
        {
            brushtype = coreData.brushtype;
            background = coreData.background;
            borderbrush = coreData.borderbrush;
            foreground = coreData.foreground;
            highlight = coreData.highlight;

            cornerRadius = coreData.cornerRadius;
            thickness = coreData.thickness;

            fontSize = coreData.fontSize;
            fontFamily = coreData.fontFamily;

        }
    }
}
/*  END OF FILE
 * 
 * 
 */