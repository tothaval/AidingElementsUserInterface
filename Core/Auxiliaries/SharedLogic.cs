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
using AidingElementsUserInterface.Core.AEUI_Logic;
using AidingElementsUserInterface.Elements.FlatShareCC;
using AidingElementsUserInterface.Texts;

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using Color = System.Windows.Media.Color;
using Point = System.Windows.Point;

namespace AidingElementsUserInterface.Core.Auxiliaries
{
    internal class SharedLogic
    {
        internal Point point = new Point(125, 25);

        public SharedLogic()
        {

        }


        internal void executeLink(string link)
        {
            try
            {
                ProcessStartInfo start = new ProcessStartInfo(link)
                {
                    UseShellExecute = true
                };
                Process.Start(start);
            }
            catch
            {
                //if (btn.ToolTip.ToString() != null)
                //{
                //    MessageBox.Show(text.pathExcetptionMessage().ToString());
                //}
            }
        }

        internal Data_Handler GetDataHandler()
        {
            return GetMainWindow().data_Handler;
        }

        internal ElementHandler GetElementHandler()
        {
            return GetMainWindow().element_handler;
        }

        internal FlatShareCC GetFlatShareCC()
        {
            FlatShareCC select = new FlatShareCC();

            select = (FlatShareCC)GetMainWindow().element_handler.returnFlatShareCC();

            return select;
        }

        internal MainWindow GetMainWindow()
        {
            return (MainWindow)System.Windows.Application.Current.MainWindow;
        }


        internal Microsoft.Win32.OpenFileDialog? openDialog()
        {
            Microsoft.Win32.OpenFileDialog setPath = new Microsoft.Win32.OpenFileDialog();
            setPath.InitialDirectory = Environment.GetEnvironmentVariable("userdir");
            setPath.Filter = "files (*.*)|*.*";
            setPath.FilterIndex = 2;
            setPath.RestoreDirectory = true;

            if (setPath.ShowDialog() == true)
            {
                return setPath;
            }

            return null;
        }

        internal FolderBrowserDialog? openFolder()
        {
            FolderBrowserDialog openFileDlg = new FolderBrowserDialog();

            var result = openFileDlg.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                return openFileDlg;
            }

            return null;
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

        internal Point ParsePoint(string parsed_xml) 
        {
            double x, y;
            string[] split = parsed_xml.Split(';');

            x = double.Parse(split[0]);
            y = double.Parse(split[1]);

            return new Point(x, y);
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

        internal void QuitApplicationCommand()
        {
            TXT_0 txt = new TXT_0("english");

            string question = txt.quitQuestion();
            string title = txt.quitTitle();

            MessageBoxResult result = System.Windows.MessageBox.Show(question, title, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                GetMainWindow().quitAEUI();
            }
        }

        internal void ShutdownCommand()
        {
            TXT_0 text = new TXT_0("english");

            string question = text.shutdownQuestion().ToString();
            string title = text.shutdownTitle().ToString();

            MessageBoxResult result = System.Windows.MessageBox.Show(question, title, MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                string command = "/C shutdown /p";
                Process.Start("cmd.exe", command);
            }
        }

       
        internal static ImageSource ToImageSource(Icon icon)
        {
            // thx to: https://stackoverflow.com/questions/1127647/convert-system-drawing-icon-to-system-media-imagesource

            ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return imageSource;
        }
    }
}
/*  END OF FILE
 * 
 * 
 */