/* Aiding Elements User Interface
 *      CoreData class
 * 
 * basic application properties
 * 
 * init:        2023|11|27
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace AidingElementsUserInterface.Core
{
    internal class CoreData
    {
        public string brushtype { get; set; }

        public Color background { get; set; }
        public Color borderbrush { get; set; }
        public Color foreground { get; set; }
        public Color highlight { get; set; }

        public CornerRadius cornerRadius { get; set; }

        public Thickness thickness { get; set; }


        public int fontSize { get; set; }
        public FontFamily fontFamily { get; set; }

        // path values
        //public string imageFilePath { get; set; }
        public string buttonImageFilePath { get; set; }
        public string containerImageFilePath { get; set; }

        // main window size values
        public int mainWindowHeight { get; set; }
        public int mainWindowWidth { get; set; }


        public CoreData()
        {
            brushtype = "SolidColorBrush";

            background = Colors.BlanchedAlmond;
            borderbrush = Colors.Black;
            foreground = Colors.DarkSlateGray;
            highlight = Colors.Azure;

            cornerRadius = new CornerRadius(14);

            thickness = new Thickness(2);

            fontSize = 12;

            fontFamily = new FontFamily("Verdana");
        }

        public CoreData(bool load)
        {
            if (load)
            {

            }
        }
    }
}
/*  END OF FILE
 * 
 * 
 */