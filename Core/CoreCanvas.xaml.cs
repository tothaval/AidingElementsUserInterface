using AidingElementsUserInterface.Elements;
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

namespace AidingElementsUserInterface.Core
{
    /// <summary>
    /// Interaktionslogik für CoreCanvas.xaml
    /// </summary>
    public partial class CoreCanvas : UserControl
    {
        public CoreCanvas()
        {
            InitializeComponent();
        }

        // move to corecanvas usercontrol v
        private void canvasDesign()
        {
            //Height = config.mainWindowHeight;
            //Width = config.mainWindowWidth;

            this.Background = new SolidColorBrush(Colors.Transparent);
            //this.FontFamily = config.font;
            //
            //border.Background = config.backColor;
            //border.BorderThickness = new Thickness(config.borderThickness);
            //border.CornerRadius = new CornerRadius(config.borderRadius);

            //imageIsBackground = config.imageBackgroundOnQuit;
            //colorsAreBackground = config.colorsAreBackground;

            //if (imageIsBackground == false)
            //{
            //    if (colorsAreBackground == true)
            //    {
            //        config.changeToBackgroundColors(MainWindowCanvas, true);

            //        colorsAreBackground = true;
            //    }
            //    else
            //    {
            //        border.Background = config.backColor;
            //        border.BorderBrush = config.foreColor;

            //        MainWindowCanvas.Background = config.canvasColor;

            //        colorsAreBackground = false;
            //    }
            //}
            //else if (imageIsBackground == true)
            //{
            //    if (config.imageFilePath != "-" || config.imageFilePath != "")
            //    {
            //        try
            //        {
            //            MainWindowCanvas.Background = new ImageBrush(new BitmapImage(new Uri(config.imageFilePath)));

            //            colorsAreBackground = false;
            //            imageIsBackground = true;
            //        }
            //        catch (Exception)
            //        {
            //            MainWindowCanvas.Background = config.canvasColor;
            //            border.Background = new LinearGradientBrush(config.backColor.Color, config.foreColor.Color, 0);

            //            colorsAreBackground = false;
            //        }
            //    }
            //}
        }

        public void clearCanvasElements()
        {
            //UIE_ModuleCreator.list_YS_CORE_ContainerElement.Clear();
            //lines.Clear();
            //points.Clear();

        }


        // events
        #region events

        private void canvas_Drop(object sender, DragEventArgs e)
        {

        }

        private void canvas_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void canvas_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void canvas_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                //canvas.DragMove();
            }

            else if (e.ChangedButton == MouseButton.Right)
            {
                CoreContainer rightClickElement = new CoreContainer((new RightClickChoice()));

                Canvas.SetLeft(rightClickElement, e.GetPosition(canvas).X);
                Canvas.SetTop(rightClickElement, e.GetPosition(canvas).Y);

                canvas.Children.Add(rightClickElement);
            }
        }

        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void canvas_PreviewDragOver(object sender, DragEventArgs e)
        {

        }


        #endregion events

    }
}
