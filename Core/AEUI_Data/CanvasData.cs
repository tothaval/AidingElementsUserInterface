/* Aiding Elements User Interface
 *      CanvasData class
 * 
 * canvas element properties
 * 
 * init:        2023|12|14
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace AidingElementsUserInterface.Core.AEUI_Data
{
    internal class CanvasData : CoreData
    {
        public int z_level_MIN { get; set; }

        public int z_level_MAX { get; set; }

        public string backgroundImageFilePath { get; set; }

        public CanvasData()
        {
            z_level_MIN = -100;
            z_level_MAX = 100;

            backgroundImageFilePath = "";
        }

        public CanvasData(CoreData coreData)
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

            z_level_MIN = -100;
            z_level_MAX = 100;

            backgroundImageFilePath = "";
        }
    }
}
/*  END OF FILE
 * 
 * 
 */