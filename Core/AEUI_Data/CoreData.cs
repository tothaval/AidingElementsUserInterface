/* Aiding Elements User Interface
 *      CoreData class
 * 
 * basic application properties
 * 
 * init:        2023|11|27
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace AidingElementsUserInterface.Core.AEUI_Data
{
    internal class CoreData
    {
        internal bool imageIsBackground { get; set; }

        internal ColorData background { get; set; }
        internal ColorData borderbrush { get; set; }
        internal ColorData foreground { get; set; }
        internal ColorData highlight { get; set; }

        internal CornerRadius cornerRadius { get; set; }
        internal Thickness thickness { get; set; }

        internal int fontSize { get; set; }
        internal FontFamily fontFamily { get; set; }

        internal double height { get; set; }
        internal double width { get; set; }
        internal string imageFilePath { get; set; }


        // replace this colors with ColorData class objects, manipulate via xml or dependencyproperty or the like, as soon as i get it.
        // implement parsing and color building functions within colorData or a new ColorLogic class, dunno yet.

        public CoreData()
        {
            imageIsBackground = false;

            background = new ColorData(1);
            borderbrush = new ColorData(2);
            foreground = new ColorData(3);
            highlight = new ColorData(4);

            cornerRadius = new CornerRadius(14);

            thickness = new Thickness(2);

            fontSize = 12;

            fontFamily = new FontFamily("Verdana");

            height = 25;
            width = 50;

            imageFilePath = "";
        }

        public CoreData(bool load)
        {

        }

        internal void apply_CoreData(CoreData coreData)
        {
            if (coreData != null)
            {
                imageIsBackground = coreData.imageIsBackground;

                background = coreData.background;
                borderbrush = coreData.borderbrush;
                foreground = coreData.foreground;
                highlight = coreData.highlight;

                cornerRadius = coreData.cornerRadius;
                thickness = coreData.thickness;

                fontSize = coreData.fontSize;
                fontFamily = coreData.fontFamily;

                height = coreData.height;
                width = coreData.width;

                imageFilePath = coreData.imageFilePath;
            }
        }

    }
}
/*  END OF FILE
 * 
 * 
 */