/* Aiding Elements User Interface
 *      MainWindowData class
 * 
 * mainwindow properties
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
    internal class MainWindowData : CoreData
    {        
        // main window size values
        public int mainWindowHeight { get; set; }
        public int mainWindowWidth { get; set; }

        public MainWindowData(bool load)
        {
        }

        public MainWindowData()
        {
            mainWindowHeight = 480;
            mainWindowWidth = 640;
        }

        public MainWindowData(CoreData coreData)
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

            mainWindowHeight = 480;
            mainWindowWidth = 640;

        }
    }
}
/*  END OF FILE
 * 
 * 
 */