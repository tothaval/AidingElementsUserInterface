﻿/* Aiding Elements User Interface
 *      Coordinates element 
 * 
 * shows element and mouse coordinates on canvas
 * 
 * init:        2023|12|27
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core;
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.AEUI_UserControls;
using AidingElementsUserInterface.Core.Auxiliaries;

using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AidingElementsUserInterface.Elements
{
    /// <summary>
    /// Interaktionslogik für Coordinates.xaml
    /// </summary>
    public partial class Coordinates : UserControl
    {
        CoreContainer coreContainer;

        // constructors
        #region constructors
        public Coordinates()
        {
            InitializeComponent();

            detectCoreContainer();

            build();
        }

        #endregion constructors

        private async void detectCoreContainer()
        {
            await Task.Delay(25);

            if (new SharedLogic().GetMainWindow().Get_SYSTEM_ACTIVE_FLAG)
            {

                foreach (CoreContainer item in new SharedLogic().GetMainWindow().Get_SYTEM_CANVAS.Get_SYSTEM_CANVAS.canvas.Children)
                {
                    if (item.GetContainerData().GetElement() == __Coordinates)
                    {
                        coreContainer = item;

                        registerMouseMove(new SharedLogic().GetMainWindow().Get_SYTEM_CANVAS.Get_SYSTEM_CANVAS);

                        break;
                    }

                }
            }
            else 
            {
                if (new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS != null)
                {
                    foreach (CoreContainer item in new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS.canvas.Children)
                    {
                        if (item.GetContainerData().GetElement() == __Coordinates)
                        {
                            coreContainer = item;

                            registerMouseMove(new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS);

                            break;
                        }

                    }
                }            
            }
        }

        private void build()
        {
            CoreData textBoxData = new SharedLogic().GetDataHandler().GetTextBoxData();

            if (textBoxData != null)
            {
                uie_textblockseparator.FontSize = textBoxData.fontSize;
                uie_textblockseparator.FontFamily = textBoxData.fontFamily;
                uie_textblockseparator.Background = textBoxData.background.GetBrush();
                uie_textblockseparator.Foreground = textBoxData.foreground.GetBrush();
            }

        }


        internal void registerMouseMove(CoreCanvas coreCanvas)
        {
            if (coreCanvas != null)
            {
                coreCanvas.MouseMove += CoreCanvas_MouseMove;
            }
        }

        private void CoreCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            updateElementCoordinates();
            updateMouseCoordinates(e);
        }

        public void updateElementCoordinates()
        {           
            if (coreContainer != null)
            {
                CL_ElementCoordinates.setText($"{(int)Canvas.GetLeft(coreContainer):D4} : {(int)Canvas.GetTop(coreContainer):D4}");
            }
        }

        public void updateMouseCoordinates(MouseEventArgs e)
        {
            System.Windows.Point position = e.GetPosition(coreContainer.GetCanvas());

            CL_MouseCoordinates.setText($"{(int)position.X:D4} : {(int)position.Y:D4}");
        }
        

        // element events
        #region element events
        private void __Coordinates_Loaded(object sender, RoutedEventArgs e)
        {            

        }
        #endregion element events

        private void __Coordinates_Initialized(object sender, EventArgs e)
        {

        }
    }
}
/*  END OF FILE
 * 
 * 
 */