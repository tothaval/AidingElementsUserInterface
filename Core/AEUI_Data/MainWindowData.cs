/* Aiding Elements User Interface
 *      MainWindowData class
 * 
 * inherits from: CoreData class
 * 
 * mainwindow properties
 * 
 * init:        2023|12|14
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace AidingElementsUserInterface.Core.AEUI_Data
{
    internal class MainWindowData : CoreData
    {
        // main window size values
        internal Point initialPosition { get; set; }

        internal int mainWindowHeight { get; set; }
        internal int mainWindowWidth { get; set; }
        internal string? language { get; set; }

        internal MainWindowData(bool load)
        {
        }

        internal MainWindowData()
        {
            initialPosition = new Point(25, 25);

            mainWindowHeight = 480;
            mainWindowWidth = 640;

            language = "english";
        }

        internal MainWindowData(CoreData coreData)
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

            initialPosition = new Point(25, 25);

            mainWindowHeight = 480;
            mainWindowWidth = 640;

            language = "english";
        }
    }
}
/*  END OF FILE
 * 
 * 
 */