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
using System.Drawing;
using AidingElementsUserInterface.Core.AEUI_UserControls;

namespace AidingElementsUserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal CoreCanvas coreCanvas;
        internal MainWindowData mainWindowData;

        internal Data_Handler data_Handler = new Data_Handler();

        internal ElementHandler element_handler = new ElementHandler();

        internal SharedLogic logic = new SharedLogic();


        public MainWindow()
        {
            InitializeComponent();


            build();
        }


        private void build()
        {
            load_MainWindowData();

            load_borderDefaults();

            load_CoreCanvas();
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
            // disabled until fixed
            //if (MI_FlatShareCC.IsChecked)
            //{
            //    coreCanvas.add_element_to_canvas(new FlatShareCC());
            //}
            //else
            //{
            //    coreCanvas.RemoveFlatShareCC();
            //}
        }

        private void load_MainWindowData()
        {
            mainWindowData = data_Handler.LoadMainWindowData();

            if (mainWindowData == null)
            {
                mainWindowData = new MainWindowData();
            }

            __MainWindow.Left = mainWindowData.initialPosition.X;
            __MainWindow.Top = mainWindowData.initialPosition.Y;

            __MainWindow.Width = mainWindowData.mainWindowWidth;
            __MainWindow.Height = mainWindowData.mainWindowHeight;

            //MessageBox.Show($"{coreData.mainWindowWidth}\n{coreData.fontSize}\n{coreData.highlight}" +
            //    $"\n{coreData.brushtype}" +
            //    $"\n{coreData.cornerRadius}");


            data_Handler.AddData(mainWindowData);
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
        }


        private void load_CoreCanvas()
        {
            coreCanvas = new CoreCanvas();
            coreCanvas.Name = "mainWindowCanvas";

            canvas_border.Child = coreCanvas;

            Keyboard.Focus(coreCanvas);
        }

        public void quitAEUI()
        {
            if (mainWindowData != null)
            {
                mainWindowData.initialPosition = new Point(this.Left, this.Top);

                XML_Handler xml_handler = new XML_Handler(mainWindowData);

                xml_handler.ButtonData_save();
                xml_handler.CanvasData_save();
                xml_handler.CoreData_save();
                xml_handler.MainWindowData_save();
                xml_handler.TextBoxData_save();

                if (element_handler != null)
                {
                    element_handler.save_state(xml_handler);
                }
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
                coreCanvas.add_element_to_canvas(new Manual());
            }

            if (e.Key == Key.F2)
            {
                if (MI_MyNote.IsChecked == false)
                {
                    MI_MyNote.IsChecked = true;
                }
                else
                {
                    MI_MyNote.IsChecked = false;
                }
                add_MyNote();
            }

            if (e.Key == Key.F3)
            {
                if (MI_FlatShareCC.IsChecked == false)
                {
                    MI_FlatShareCC.IsChecked = true;
                }
                else
                {
                    MI_FlatShareCC.IsChecked = false;
                }
                add_FlatShareCC();
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
            System.Windows.Size size = e.NewSize;

            mainWindowData.mainWindowWidth = (int)size.Width;
            mainWindowData.mainWindowHeight = (int)size.Height;

            e.Handled = true;
        }

        #endregion events

        private void MI_Coordinates_Click(object sender, RoutedEventArgs e)
        {
            coreCanvas.add_element_to_canvas(new Elements.Coordinates());
        }

        private void MI_FileLink_Click(object sender, RoutedEventArgs e)
        {
            coreCanvas.add_element_to_canvas(new Elements.FileLink());
        }

        private void MI_Image_Click(object sender, RoutedEventArgs e)
        {
            coreCanvas.add_element_to_canvas(new Elements.Image());
        }

        private void MI_Link_Click(object sender, RoutedEventArgs e)
        {
            coreCanvas.add_element_to_canvas(new Elements.Link());
        }


        private void MI_manual_Click(object sender, RoutedEventArgs e)
        {
            coreCanvas.add_element_to_canvas(new Elements.Manual());
        }

        private void MI_MyNote_Click(object sender, RoutedEventArgs e)
        {
            add_MyNote();
        }

        private void MI_FlatShareCC_Click(object sender, RoutedEventArgs e)
        {
            add_FlatShareCC();
        }

        private void MI_command_Click(object sender, RoutedEventArgs e)
        {
            coreCanvas.add_element_to_canvas(new CoreCommand());
        }

        private void MI_quit_Click(object sender, RoutedEventArgs e)
        {
            logic.QuitApplicationCommand();
        }

        private void MI_shutdown_Click(object sender, RoutedEventArgs e)
        {
            logic.ShutdownCommand();
        }

        private void MI_Random_Click(object sender, RoutedEventArgs e)
        {
            coreCanvas.add_element_to_canvas(new Elements.Random());
        }

        private void MI_RightClickChoice_Click(object sender, RoutedEventArgs e)
        {
            coreCanvas.add_element_to_canvas(new Elements.RightClickChoice());
        }
    }
}
/*  END OF FILE
 * 
 * 
 */