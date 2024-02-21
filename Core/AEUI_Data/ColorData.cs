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
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Brush = System.Windows.Media.Brush;
using Point = System.Windows.Point;

namespace AidingElementsUserInterface.Core.AEUI_Data
{
    internal class ColorData
    {
        internal string brushtype { get; set; }
        internal string brushpath { get; set; }

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
        internal string color5_string { get; set; }
        internal string color6_string { get; set; }

        internal double[] offsets = new double[6];

        internal ColorData(bool load)
        {
            if (load)
            {
                color5_string = "#FFFFFFFF";
                color6_string = "#FF000000";

            }
        }

        internal ColorData(int nr = 1)
        {

            brushtype = "SolidColorBrush";
            brushpath = "-";

            gradiantOrigin = new Point(0.5, 0.5);

            gradiantStartPoint = new Point(0.5, 1);
            gradiantEndPoint = new Point(0.5, 0);

            switch (nr)
            {
                case 1:
                    color1_string = "#FFFFEBCD";
                    break;
                case 2:
                    color1_string = "#FF000000";
                    break;
                case 3:
                    color1_string = "#FF2F4F4F";
                    break;
                case 4:
                    color1_string = "#FF0FFF0F";
                    break;

                default:
                    color1_string = "#FF000000";
                    break;

            }

            color2_string = "#FF000000";
            color3_string = "#EE2F4F4F";
            color4_string = "#FF0FFF0F";
            color5_string = "#FFFFFFFF";
            color6_string = "#FF000000";
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

                    brush.GradientStops.Add(new GradientStop(logic.ParseColor(color1_string), 0.00));
                    brush.GradientStops.Add(new GradientStop(logic.ParseColor(color2_string), 0.20));
                    brush.GradientStops.Add(new GradientStop(logic.ParseColor(color3_string), 0.40));
                    brush.GradientStops.Add(new GradientStop(logic.ParseColor(color4_string), 0.60));
                    brush.GradientStops.Add(new GradientStop(logic.ParseColor(color5_string), 0.80));
                    brush.GradientStops.Add(new GradientStop(logic.ParseColor(color6_string), 1.00));

                    return brush;
                }
                else if (brushtype.Equals("LinearGradientBrush"))
                {
                            LinearGradientBrush brush = new LinearGradientBrush()
                            {
                                StartPoint = gradiantStartPoint,
                                EndPoint = gradiantEndPoint
                            };

                            //brush.StartPoint = gradiantStartPoint;
                            //brush.EndPoint = gradiantEndPoint;

                            //foreach (GradientStop item in gradientStops)
                            //{
                            //brush.GradientStops.Add(new GradientStop(item.Color, item.Offset));
                            //}

                            // applying the GradientStops directly works, a LinearGradientBrush is drawn to any
                            // object as intended, but when assigning the GradientStops via loop they are
                            // assigned to the brush, but the brush will only draw first or last color
                            // i really do not get this, maybe some form of bug within the framework
                            // even the correct amount of stops was passed to the brush, doesn't matter, i will
                            // limit brush gradients to 6 for now, case closed.
                            // will have to test, whether to build in offset or not.

                            // MessageBox.Show(gradientStops.Count.ToString() + "\n" + brush.GradientStops.Count.ToString());
                            // i dont get it and i dont get IT, just useless junk.

                            brush.GradientStops.Add(new GradientStop(logic.ParseColor(color1_string), offsets[0]));
                            brush.GradientStops.Add(new GradientStop(logic.ParseColor(color2_string), offsets[1]));
                            brush.GradientStops.Add(new GradientStop(logic.ParseColor(color3_string), offsets[2]));
                            brush.GradientStops.Add(new GradientStop(logic.ParseColor(color4_string), offsets[3]));
                            brush.GradientStops.Add(new GradientStop(logic.ParseColor(color5_string), offsets[4]));
                            brush.GradientStops.Add(new GradientStop(logic.ParseColor(color6_string), offsets[5]));

                            return brush;
                  
                }
                else if (brushtype.Equals("ImageBrush"))
                {
                    if (!brushpath.Equals("-"))
                    {
                        if (File.Exists(brushpath))
                        {
                            ImageSource? imageSource = new BitmapImage(new Uri(brushpath));

                            if (imageSource != null)
                            {
                                ImageBrush brush = new ImageBrush(imageSource);

                                return brush;
                            }                            
                        }
                    }
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

        internal void setOffsets(double[] offsets)
        {
            if (offsets.Length == 6)
            {
                MessageBox.Show("six");
                this.offsets = offsets;
            }            
        }

    }
}
/*  END OF FILE
 * 
 * 
 */