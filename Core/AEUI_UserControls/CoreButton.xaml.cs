﻿/* Aiding Elements User Interface
 *      CoreButton element 
 * 
 * basic configurable fileLinkElement element
 * 
 * init:        2023|11|27
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.Auxiliaries;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AidingElementsUserInterface.Core
{
    /// <summary>
    /// basic configurable fileLinkElement element
    /// <para>:CoreButton(UserControl) { 0 empty; 1 string;} </para> 
    /// <para>?!public </para> 
    /// <para> </para> 
    /// <para>.isSelected(bool) ?!internal > bool . </para> 
    /// <para> </para> 
    /// <para>;_disabled(void) ?!internal {} ; </para> 
    /// <para>;deselect(void) ?!internal {} ; </para> 
    /// <para>;enabled(void) ?!internal {} ; </para> 
    /// <para>;select(void) ?!internal {} ; </para> 
    /// <para>;SetElement(void) ?!internal { 0 string  1 Icon } ; </para> 
    /// <para>;setTooltip(void) ?!internal { 0 string } ; </para> 
    /// <para>:CoreButton </para> 
    /// </summary>
    public partial class CoreButton : UserControl
    { 
        private bool selected = false;
        internal bool isSelected => selected;

        internal CoreData config;

        // constructors
        #region constructors
        public CoreButton()
        {
            InitializeComponent();

            build();
        }

        public CoreButton(bool no_limits)
        {
            InitializeComponent();

            build(no_limits);
        }

        public CoreButton(string content)
        {
            InitializeComponent();

            button.Content = content;

            build();
        }
        #endregion constructors


        private async void build(bool no_limits = false)
        {
            Data_Handler data_Handler = new SharedLogic().GetDataHandler();
        
            config = data_Handler.LoadButtonData(); // über App.xaml und XML Data Provider die ButtonData.xml laden

            if (config == null)
            {
                config = new CoreData();
            }


            if (no_limits)
            {
                this.Resources["TextBoxData_MinWidth"] = config.width;
                this.Resources["TextBoxData_MinHeight"] = config.height;
            }
            else
            {
                this.Resources["TextBoxData_MinWidth"] = config.width;
                this.Resources["TextBoxData_MinHeight"] = config.height;

                this.Resources["TextBoxData_MaxWidth"] = config.width * 2;
                this.Resources["TextBoxData_MaxHeight"] = config.height;
            }
        }

        internal void _disabled()
        {
            button.IsEnabled = false;
        }

        internal void deselect()
        {
            this.Resources["ButtonData_highlight"] = config.highlight.GetBrush();

            selected = false;
        }

        internal void _enabled()
        {
            button.IsEnabled = true;
        }

        internal void select()
        {
            this.Resources["ButtonData_highlight"] = config.background.GetBrush();

            selected = true;
        }

        internal void setContent(string content)
        {
            button.Content = content;
        }

        internal void setContent(Icon icon)
        {
            System.Windows.Controls.Image image = new System.Windows.Controls.Image();
            ImageSource imso = SharedLogic.ToImageSource(icon);
            image.Source = imso;

            button.Content = image;
        }

        //internal void SetElement(string content, Icon icon)
        //{
        //    // after some research: function test method for reflection and learning

        //    StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Vertical };
        //    TextBlock textBlock = new TextBlock() { Text = content };
        //    System.Windows.Controls.Image image = new System.Windows.Controls.Image();
        //    ImageSource imso = SharedLogic.ToImageSource(icon);
        //    image.Source = imso;
        //    stackPanel.Children.Add(textBlock);
        //    stackPanel.Children.Add(image);

        //    button.Content = stackPanel;
        //}

        internal void setTooltip(string tooltip)
        {
            this.ToolTip = tooltip;
        }

        // element events
        #region element events
        // CORE_Button
        #region CORE_Button
        private void CORE_Button_Loaded(object sender, RoutedEventArgs e)
        {

        }
        #endregion CORE_Button
        #endregion element events


    }
}
/*  END OF FILE
 * 
 * 
 */