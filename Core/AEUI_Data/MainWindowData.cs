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
using System.Windows;

namespace AidingElementsUserInterface.Core.AEUI_Data
{
    internal class MainWindowData : CoreData
    {
        // main window size values
        internal Point initialPosition { get; set; }                
        internal string? language { get; set; }

        internal MainWindowData(bool load)
        {
        }

        internal MainWindowData()
        {
            //CoreData properties
            height = 480;
            width = 640;

            //MainWindowData properties
            initialPosition = new Point(25, 25);
            language = "english";
        }

        internal MainWindowData(CoreData coreData)
        {
            //CoreData properties
            background = coreData.background;
            borderbrush = coreData.borderbrush;
            foreground = coreData.foreground;
            highlight = coreData.highlight;

            cornerRadius = coreData.cornerRadius;
            thickness = coreData.thickness;

            fontSize = coreData.fontSize;
            fontFamily = coreData.fontFamily;

            height = 480;
            width = 640;

            imageFilePath = coreData.imageFilePath;

            //MainWindowData properties
            initialPosition = new Point(25, 25);
            language = "english";
        }
    }
}
/*  END OF FILE
 * 
 * 
 */