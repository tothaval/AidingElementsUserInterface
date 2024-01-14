/* Aiding Elements User Interface
 *      ColorData class
 * 
 * color properties class
 * 
 * init:        2023|12|14
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */

using AidingElementsUserInterface.Core.Auxiliaries;
using System;
using System.Windows;
using System.Windows.Media;

namespace AidingElementsUserInterface.Core.AEUI_Data
{
    internal class ColorData
    {
        internal string brushtype { get; set; }

        //LinearGradientBrush
        internal Point gradiantEndPoint { get; set; }
        internal Point gradiantStartPoint { get; set; }

        // RadialGradientBrush
        internal Point gradiantOrigin { get; set; }

        // color linkData
        internal string color1_string { get; set; }
        internal string color2_string { get; set; }
        internal string color3_string { get; set; }
        internal string color4_string { get; set; }


        internal ColorData(bool load)
        {
            if (load)
            {

            }
        }

        internal ColorData()
        {
            brushtype = "SolidColorBrush";

            gradiantOrigin = new Point(0.5, 0.5);

            gradiantStartPoint = new Point(0.5, 1);
            gradiantEndPoint = new Point(0.5, 0);

            color1_string = "#FF000000";
            color2_string = "#FF000000";
            color3_string = "#FFFFFFFF";
            color4_string = "#FFEEEE00";
        }

        internal Brush? GetBrush()
        {
            Brush _brush = null;
            SharedLogic logic = new SharedLogic();

            if (brushtype != null)
            {
                if (brushtype.Equals("SolidColorBrush"))
                {
                    SolidColorBrush brush = new SolidColorBrush();
                    brush.Color = logic.ParseColor(color1_string);

                    return brush;
                }
                else if (brushtype.Equals("RadialGradientBrush"))
                {
                    RadialGradientBrush brush = new RadialGradientBrush();
                    brush.GradientOrigin = gradiantOrigin;

                    // Create four gradient stops.
                    brush.GradientStops.Add(new GradientStop(logic.ParseColor(color1_string), 0.0));
                    brush.GradientStops.Add(new GradientStop(logic.ParseColor(color1_string), 0.25));
                    brush.GradientStops.Add(new GradientStop(logic.ParseColor(color1_string), 0.75));
                    brush.GradientStops.Add(new GradientStop(logic.ParseColor(color1_string), 1.0));

                    return brush;
                }
                else if (brushtype.Equals("LinearGradientBrush"))
                {
                    LinearGradientBrush brush = new LinearGradientBrush();

                    brush.StartPoint = gradiantStartPoint;
                    brush.EndPoint = gradiantEndPoint;
                    brush.GradientStops.Add(
                        new GradientStop(logic.ParseColor(color1_string), 0.2));
                    brush.GradientStops.Add(
                        new GradientStop(logic.ParseColor(color2_string), 0.4));
                    brush.GradientStops.Add(
                        new GradientStop(logic.ParseColor(color3_string), 0.6));
                    brush.GradientStops.Add(
                        new GradientStop(logic.ParseColor(color4_string), 0.8));

                    return brush;
                }
                else if (brushtype.Equals("DrawingBrush"))
                {
                    DrawingBrush brush = new DrawingBrush();

                    return brush;
                }
                else if (brushtype.Equals("VisualBrush"))
                {
                    VisualBrush brush = new VisualBrush();

                    return brush;
                }
            }
            else
            {
                SolidColorBrush brush = new SolidColorBrush(logic.ParseColor(color1_string));

                return brush;
            }

            return _brush;
        }

    }
}
/*  END OF FILE
 * 
 * 
 */