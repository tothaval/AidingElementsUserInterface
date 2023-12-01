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

namespace AidingElementsUserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool loaded = false;

        internal CoreCanvas canvas;

        internal CoreData coreData;

        internal ElementHandler handler;


        public MainWindow()
        {
            handler = new ElementHandler();

            InitializeComponent();

            build();
        }


        private async void build()
        {
            loaded = true;

            int delayValue = 1;

            await Task.Delay(delayValue);

            load_CoreData();

            load_borderDefaults();

            load_CoreCanvas();
        }



        // element instantiation
        private void instantiate_Manual()
        {
            Manual manual = new Manual();

            CoreContainer myNoteElement = new CoreContainer(manual, canvas);

            canvas.PositionElement(myNoteElement);

            handler.addElement(myNoteElement, canvas);

            canvas.canvas.Children.Add(myNoteElement);

        }

        private void instantiate_MyNote()
        {
            MyNote note = new MyNote();

            if (handler.checkElement(note))
            {
                CoreContainer myNoteElement = new CoreContainer(note, canvas);

                canvas.PositionElement(myNoteElement);

                handler.addElement(myNoteElement, canvas);

                canvas.canvas.Children.Add(myNoteElement);
            }
        }



        // processing
        #region processing
        private void load_borderDefaults()
        {
            border.Background = new SolidColorBrush(coreData.background);

            border.BorderBrush = new SolidColorBrush(coreData.borderbrush);

            border.CornerRadius = coreData.cornerRadius;

            border.BorderThickness = coreData.thickness;

        }


        private void load_CoreCanvas()
        {
            canvas = new CoreCanvas();

            canvas_border.Child = canvas;

            Keyboard.Focus(canvas);
        }

        private void load_CoreData()
        {
            // if default_file not found
            coreData = new CoreData();
        }


        public void quitAEUI()
        {
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
                instantiate_Manual();
            }

            if (e.Key == Key.F2)
            {
                instantiate_MyNote();
            }

        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {

            e.Handled = true;
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
