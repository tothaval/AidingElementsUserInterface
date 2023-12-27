using AidingElementsUserInterface.Core;
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.Auxiliaries;
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

namespace AidingElementsUserInterface.Elements
{
    /// <summary>
    /// Interaktionslogik für Coordinates.xaml
    /// </summary>
    public partial class Coordinates : UserControl
    {
        CoreCanvas coreCanvas;
        CoreContainer coreContainer;

        // constructors
        #region constructors
        public Coordinates()
        {
            InitializeComponent();

            build();
        }

        public Coordinates(CoreCanvas coreCanvas)
        {
            InitializeComponent();

            this.coreCanvas = coreCanvas;

            build();
        }
        #endregion constructors

        internal void addCoreCanvas(CoreCanvas coreCanvas) 
        {
            this.coreCanvas = coreCanvas;
        }

        private async void detectCoreContainer()
        {
            await Task.Delay(5);

            foreach (CoreContainer item in new SharedLogic().GetElementHandler().GetContainerContentsDict().Keys)
            {
                if (item.GetContainerData().getContent() == __Coordinates)
                {
                    coreContainer = item;

                    break;
                }

            }
        }

        private void build()
        {
            TextBoxData textBoxData = new SharedLogic().GetDataHandler().GetTextBoxData();

            CTB_ElementCoordinates._readonly_caret();
            CTB_MouseCoordinates._readonly_caret();

            if (textBoxData != null)
            {
                uie_textblockseparator.FontSize = textBoxData.fontSize;
                uie_textblockseparator.FontFamily = textBoxData.fontFamily;
                uie_textblockseparator.Background = new SolidColorBrush(textBoxData.background);
                uie_textblockseparator.Foreground = new SolidColorBrush(textBoxData.foreground);
            }

            detectCoreContainer();

            registerMouseMove();
        }

        private void getCoreCanvas()
        {          
            coreCanvas = new SharedLogic().GetMainWindow().coreCanvas;
        }

        private async void registerMouseMove()
        {
            await Task.Delay(5);

            if (coreCanvas == null)
            {
                getCoreCanvas();
            }

            coreCanvas.MouseMove += CoreCanvas_MouseMove;
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
                CTB_ElementCoordinates.setText($"{(int)Canvas.GetLeft(coreContainer):D4} : {(int)Canvas.GetTop(coreContainer):D4}");
            }
        }

        public void updateMouseCoordinates(MouseEventArgs e)
        {
            System.Windows.Point position = e.GetPosition(coreCanvas);

            CTB_MouseCoordinates.setText($"{(int)position.X:D4} : {(int)position.Y:D4}");
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
