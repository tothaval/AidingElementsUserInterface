﻿/* Aiding Elements User Interface
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
using AidingElementsUserInterface.Core.AEUI_Data;

namespace AidingElementsUserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal CoreCanvas canvas;
        internal MainWindowData mainWindowData;

        internal Data_Handler data_Handler = new Data_Handler();

        internal ElementHandler handler = new ElementHandler();


        public MainWindow()
        {
            InitializeComponent();

            build();
        }


        private void build()
        {
            load_MainWindowData();
        }


        // processing
        #region processing
        private void add_MyNote()
        {
            if (MI_MyNote.IsChecked)
            {
                coreCanvas.add_element_to_canvas(new MyNote());
            }
            else
            {
                coreCanvas.RemoveMyNote();
            }
        }

        private void add_FlatShareCC()
        {
            if (MI_FlatShareCC.IsChecked)
            {
                coreCanvas.add_element_to_canvas(new FlatShareCC());
            }
            else
            {
                coreCanvas.RemoveFlatShareCC();
            }
        }

        private void load_MainWindowData()
        {
            mainWindowData = data_Handler.LoadMainWindowData();

            if (mainWindowData == null)
            {
                mainWindowData = new MainWindowData();
            }

            //MessageBox.Show($"{coreData.mainWindowWidth}\n{coreData.fontSize}\n{coreData.highlight}" +
            //    $"\n{coreData.brushtype}" +
            //    $"\n{coreData.cornerRadius}");


            data_Handler.AddData(mainWindowData);

            load_borderDefaults();
        }

        private void load_borderDefaults()
        {
            if (mainWindowData.brushtype.Equals("SolidColorBrush"))
            {
                border.Background = new SolidColorBrush(mainWindowData.background);
                border.BorderBrush = new SolidColorBrush(mainWindowData.borderbrush);
            }

            border.CornerRadius = mainWindowData.cornerRadius;
            border.BorderThickness = mainWindowData.thickness;

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
            if (mainWindowData != null)
            {
                XML_Handler xml_handler = new XML_Handler(mainWindowData);

                xml_handler.ButtonData_save();
                xml_handler.CanvasData_save();
                xml_handler.CoreData_save();
                xml_handler.MainWindowData_save();
                xml_handler.TextBoxData_save();

                if (handler != null)
                {
<<<<<<< Updated upstream
                    handler.save_state(xml_handler);
                }                
=======
                    element_handler.save_state(xml_handler);
                }
>>>>>>> Stashed changes
            }

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
            if (e.Key == Key.X)
            {
                coreCanvas.delete_selected_items();
            }

            if (e.Key == Key.F1)
            {
<<<<<<< Updated upstream
                canvas.add_element_to_canvas(
                    handler.instantiate_Manual(canvas)
                    );
=======
                coreCanvas.add_element_to_canvas(new Manual());
>>>>>>> Stashed changes
            }

            if (e.Key == Key.F2)
            {
<<<<<<< Updated upstream

                canvas.add_element_to_canvas(
                    handler.instantiate_MyNote(canvas)
                    );
=======
                if (MI_MyNote.IsChecked == false)
                {
                    MI_MyNote.IsChecked = true;
                }
                else
                {
                    MI_MyNote.IsChecked = false;
                }
                add_MyNote();
>>>>>>> Stashed changes
            }

            if (e.Key == Key.F3)
            {
<<<<<<< Updated upstream
                canvas.add_element_to_canvas(
                    handler.instantiate_FlatShareCC(canvas)
                    );

=======
                if (MI_FlatShareCC.IsChecked == false)
                {
                    MI_FlatShareCC.IsChecked = true;
                }
                else
                {
                    MI_FlatShareCC.IsChecked = false;
                }
                add_FlatShareCC();
>>>>>>> Stashed changes
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
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
        private void MI_Coordinates_Click(object sender, RoutedEventArgs e)
        {
            coreCanvas.add_element_to_canvas(new Coordinates());
        }



        private void MI_manual_Click(object sender, RoutedEventArgs e)
        {
            coreCanvas.add_element_to_canvas(new Manual());
        }

        private void MI_MyNote_Click(object sender, RoutedEventArgs e)
        {
            add_MyNote();
        }

        private void MI_FlatShareCC_Click(object sender, RoutedEventArgs e)
        {
            add_FlatShareCC();
        }

        private void MI_quit_Click(object sender, RoutedEventArgs e)
        {
            logic.QuitApplicationCommand();
        }

        private void MI_shutdown_Click(object sender, RoutedEventArgs e)
        {
            logic.ShutdownCommand();
        }

        private void MI_RightClickChoice_Click(object sender, RoutedEventArgs e)
        {
            coreCanvas.add_element_to_canvas(new RightClickChoice());
        }
    }
}
/*  END OF FILE
 * 
 * 
 */