/* Aiding Elements User Interface
 *      MainWindow 
 * 
 * main frame for the application
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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Collections;
using System.Diagnostics;
using System.IO;

using Point = System.Windows.Point;
using AidingElementsUserInterface.Core;
using AidingElementsUserInterface.Texts;
using AidingElementsUserInterface.Core.Auxiliaries;
using AidingElementsUserInterface.Elements.MyNote;
using AidingElementsUserInterface.Elements;
using AidingElementsUserInterface.Elements.FlatShareCC;

namespace AidingElementsUserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal CoreCanvas canvas;
        internal CoreData coreData;

        internal Data_Handler data_Handler = new Data_Handler();

        internal ElementHandler handler = new ElementHandler();

        public MainWindow()
        {
            InitializeComponent();

            build();
        }


        private void build()
        {            
            load_coreData();
        }


        // processing
        #region processing
        private async void load_coreData()
        {
            coreData = data_Handler.LoadCoreData();

            //await Task.Delay(10);

            if (coreData == null)
            {
                MessageBox.Show("hi");

                coreData = new CoreData();
            }

            //MessageBox.Show($"{coreData.mainWindowWidth}\n{coreData.fontSize}\n{coreData.highlight}" +
            //    $"\n{coreData.brushtype}" +
            //    $"\n{coreData.cornerRadius}");


            data_Handler.AddCoreData(coreData);

            load_borderDefaults();
        }

        private void load_borderDefaults()
        {
            if (coreData.brushtype.Equals("SolidColorBrush"))
            {
                border.Background = new SolidColorBrush(coreData.background);
                border.BorderBrush = new SolidColorBrush(coreData.borderbrush);
            }

            border.CornerRadius = coreData.cornerRadius;
            border.BorderThickness = coreData.thickness;

            load_CoreCanvas();
        }


        private void load_CoreCanvas()
        {
            canvas = new CoreCanvas();

            canvas_border.Child = canvas;

            Keyboard.Focus(canvas);
        }

        public void quitAEUI()
        {
            new XML_Handler(coreData).CoreData_save();

            Application.Current.Shutdown();
        }

        #endregion processing




        // events
        #region events
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            quitAEUI();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                canvas.add_element_to_canvas(
                    handler.instantiate_Manual(canvas)
                    );
            }

            if (e.Key == Key.F2)
            {

                canvas.add_element_to_canvas(
                    handler.instantiate_MyNote(canvas)
                    );
            }

            if (e.Key == Key.F3)
            {
                canvas.add_element_to_canvas(
                    handler.instantiate_FlatShareCC(canvas)
                    );

            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {

                e.Handled = true;
            }

            if (e.Key == Key.F2)
            {

                e.Handled = true;
            }

            if (e.Key == Key.F3)
            {

                e.Handled = true;

            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }

            else if (e.ChangedButton == MouseButton.Right)
            {

            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        #endregion events
    }
}
/*  END OF FILE
 * 
 * 
 */