/* Aiding Elements User Interface
 *      ColorData class
 * 
 * color properties class
 * 
 * init:        2023|12|14
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */

namespace AidingElementsUserInterface.Core.AEUI_Data
{
    internal class ColorData
    {
        public int brushOrientation { get; set; }

        public string brushtype { get; set; }


        // color data
        public string color1_string { get; set; }
        public string color2_string { get; set; }
        public string color3_string { get; set; }
        public string color4_string { get; set; }


        public ColorData(bool load)
        {
            if (load)
            {

            }
        }

        public ColorData()
        {
            brushOrientation = 0;
            brushtype = "SolidColorBrush";
            color1_string = "#DDDDDDDD";
            color2_string = "#DDDD0000";
            color3_string = "#DD00DD00";
            color4_string = "#DD0000DD";
        }
    }
}
/*  END OF FILE
 * 
 * 
 */