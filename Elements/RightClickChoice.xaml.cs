﻿/* Aiding Elements User Interface
 *      RightClickChoice element 
 * 
 * enables the user to deactivate the application
 * or shutdown the computer entirely
 * 
 * init:        2023|11|27
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core;
using AidingElementsUserInterface.Core.Auxiliaries;
using AidingElementsUserInterface.Texts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace AidingElementsUserInterface.Elements
{
    /// <summary>
    /// Interaktionslogik für RightClickChoice.xaml
    /// </summary>
    public partial class RightClickChoice : UserControl
    {
        // global classes, properties and variables
        #region global classes, properties and variables
        //private CoreButton CB_GraphicsTest = new CoreButton("graphics test");

        private CoreButton CB_QuitButton = new CoreButton("quit\nys ui");

        private CoreButton CB_ShutdownButton = new CoreButton("shut\ndown");
        #endregion global classes, properties and variables

        public RightClickChoice()
        {
            InitializeComponent();

            build();
        }


        private async void build()
        {
            await Task.Delay(12);

            //CB_GraphicsTest.button.Click += CB_GraphicsTest_Click;
            CB_QuitButton.button.Click += CB_QuitButton_Click;
            CB_ShutdownButton.button.Click += CB_ShutdownButton_Click;

            CB_QuitButton.Margin = new Thickness(0, 0, 0, 7);
            CB_ShutdownButton.Margin = new Thickness(0, 0, 0, 7);

            //wrap_panel.Children.Add(CB_GraphicsTest);
            wrap_panel.Children.Add(CB_QuitButton);
            wrap_panel.Children.Add(CB_ShutdownButton);
        }


        // element events
        #region element events
        // CORE_RightClickChoiceElement
        #region CORE_RightClickChoiceElement
        private void RCC_Loaded(object sender, RoutedEventArgs e)
        {

        }
        #endregion CORE_RightClickChoiceElement

        // clicks
        #region button clicks
        //private void CB_GraphicsTest_Click(object sender, RoutedEventArgs e)
        
        //{
            //MainWindow mainWindow = YS_CLASS_ConfigData.Return_MainWindow();

            //Polyline poly = new Polyline();
            //poly.VerticalAlignment = VerticalAlignment.Center;
            //poly.Stroke = System.Windows.SystemColors.WindowTextBrush;
            //poly.StrokeThickness = 2;

            ////Content = poly;

            ////for (int i = 0; i < 2000; i++)
            ////{
            ////    poly.Points.Add(new System.Windows.Point(i, 96 * (1 - Math.Sin(i * Math.PI / 192))));
            ////}


            //for (int i = 0; i < 628; i += 4)
            //{

            //    poly.Points.Add(new System.Windows.Point(0, 250));
            //    poly.Points.Add(new System.Windows.Point(i, 250 + 100 * Math.Sin(i / 100)));
            //}

            //mainWindow.MainWindowCanvas.Children.Add(poly);
        //}


        private void CB_QuitButton_Click(object sender, RoutedEventArgs e)
        {
            new SharedLogic().QuitApplicationCommand();
        }

        private void CB_ShutdownButton_Click(object sender, RoutedEventArgs e)
        {
            new SharedLogic().ShutdownCommand();
        }
        #endregion button clicks
        #endregion element events


    }
}
/*  END OF FILE
 * 
 * 
 */