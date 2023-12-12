/* Aiding Elements User Interface
 *      SharedLogic class
 * 
 * basic methods
 * 
 * init:        2023|11|30
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 * 
 * origin:      MyNote_2023_11_01
 */
using AidingElementsUserInterface.Elements.FlatShareCC;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AidingElementsUserInterface.Core.Auxiliaries
{
    internal class SharedLogic
    {
        public SharedLogic()
        {

        }

        internal Data_Handler GetDataHandler()
        {
            return GetMainWindow().data_Handler;
        }

        internal ElementHandler GetElementHandler()
        {
            return GetMainWindow().handler;
        }

        internal FlatShareCC GetFlatShareCC()
        {
            FlatShareCC select = new FlatShareCC();

            select = (FlatShareCC)GetMainWindow().handler.returnElement(select);

            return select;
        }

        internal MainWindow GetMainWindow()
        {
            return (MainWindow)System.Windows.Application.Current.MainWindow;
        }

        internal Color ParseColor(string colorstring)
        {
            Color color;
            byte a, r, g, b;

            colorstring.Trim();

            a = byte.Parse(colorstring.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
            r = byte.Parse(colorstring.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
            g = byte.Parse(colorstring.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);
            b = byte.Parse(colorstring.Substring(7, 2), System.Globalization.NumberStyles.HexNumber);

            color = Color.FromArgb(a, r, g, b);

            return color;
        }


        internal CornerRadius ParseCornerRadius(string parsed_xml)
        {
            CornerRadius cornerRadius;
            byte a, r, g, b;

            string[] strings = parsed_xml.Split(",");

            a = byte.Parse(strings[0]);

            cornerRadius = new CornerRadius(a);

            if (strings.Length == 4)
            {
                r = byte.Parse(strings[1]);
                g = byte.Parse(strings[2]);
                b = byte.Parse(strings[3]);

                cornerRadius = new CornerRadius(a, r, g, b);
            }

            return cornerRadius;
        }

        internal Thickness ParseThickness(string parsed_xml)
        {
            Thickness thickness;
            byte a, r, g, b;

            string[] strings = parsed_xml.Split(",");

            a = byte.Parse(strings[0]);

            thickness = new Thickness(a);

            if (strings.Length == 4)
            {
                r = byte.Parse(strings[1]);
                g = byte.Parse(strings[2]);
                b = byte.Parse(strings[3]);

                thickness = new Thickness(a, r, g, b);
            }

            return thickness;
        }
    }
}
/*  END OF FILE
 * 
 * 
 */