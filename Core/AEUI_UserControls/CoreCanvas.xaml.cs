/* Aiding Elements User Interface
 *      CoreCanvas element 
 * 
 * basic surface element for placement of elements
 * and for scetching, drawing, et cetera
 * 
 * init:        2023|11|27
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.AEUI_UserControls;
using AidingElementsUserInterface.Core.Auxiliaries;
using AidingElementsUserInterface.Elements;
using AidingElementsUserInterface.Elements.FlatShareCC;
using AidingElementsUserInterface.Elements.MyNote;

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Runtime.InteropServices.JavaScript.JSType;
using DragEventArgs = System.Windows.DragEventArgs;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using UserControl = System.Windows.Controls.UserControl;

namespace AidingElementsUserInterface.Core
{
    /// <summary>
    /// Interaktionslogik für CoreCanvas.xaml
    /// </summary>
    public partial class CoreCanvas : UserControl
    {
        private CanvasData config;
        private System.Windows.Point selection_point;
        private System.Windows.Shapes.Rectangle selection_rectangle;

        private ObservableCollection<CoreContainer> selected_items = new ObservableCollection<CoreContainer>();

        public CoreCanvas()
        {
            InitializeComponent();

            build();
        }


        internal void add_element_to_canvas(UserControl content)
        {
            SharedLogic logic = new SharedLogic();

            CoreContainer coreContainer = logic.GetElementHandler().instantiate(content, ref __CoreCanvas);

            if (coreContainer != null)
            {
                PositionElement(coreContainer, logic.point);
                canvas.Children.Add(coreContainer);
            }
        }

        internal void add_element_to_canvas(CoreContainer content, MouseButtonEventArgs e)
        {
            SharedLogic logic = new SharedLogic();

            CoreContainer coreContainer = logic.GetElementHandler().instantiate(content, ref __CoreCanvas);

            if (coreContainer != null)
            {
                PositionElement(coreContainer, e.GetPosition(canvas));
                canvas.Children.Add(coreContainer);
            }
        }

        internal void add_element_to_canvas(CoreContainer container, System.Windows.Point point)
        {
            PositionElement(container, point);

            new SharedLogic().GetElementHandler().addElement(container, __CoreCanvas);

            canvas.Children.Add(container);
        }

        internal void add_selected_item(CoreContainer coreContainer)
        {
            selected_items.Add(coreContainer);
        }

        internal void _backgroundImage()
        {
            if (config.imageFilePath != null)
            {
                if (File.Exists(config.imageFilePath))
                {
                    canvas.Background = new ImageBrush(new BitmapImage(new Uri(config.imageFilePath)));
                }
                else
                {
                    canvas.Background = config.background.GetBrush();
                }
            }
        }

        private void build()
        {
            Data_Handler data_Handler = new SharedLogic().GetDataHandler();


            config = data_Handler.LoadCanvasData();

            if (config == null)
            {
                config = new CanvasData();
            }

            data_Handler.AddCanvasData(config);

            _backgroundImage();

            __CoreCanvas.BorderBrush = config.borderbrush.GetBrush();
            __CoreCanvas.BorderThickness = config.thickness;

            __CoreCanvas.Name = config.canvasName;

            load();

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

        }

        internal void delete_selected_items()
        {
            foreach (CoreContainer item in selected_items)
            {
                item.remove_Container();
            }

            selected_items.Clear();
        }

        private void deselect_selected_containers()
        {
            foreach (CoreContainer item in selected_items)
            {
                item.deselect(false);
            }

            selected_items.Clear();
        }

        internal CanvasData getCanvasData()
        {
            return config;
        }


        internal System.Windows.Point GetElementPosition(CoreContainer container)
        {
            System.Windows.Point position = new System.Windows.Point(Canvas.GetLeft(container), Canvas.GetTop(container));

            return position;
        }

        internal void group_selected_items(bool horizontal)
        {
            int iteration = 1;
            int displacement = config.grouping_displacement;

            foreach (CoreContainer item in selected_items)
            {
                if (horizontal)
                {
                    Canvas.SetLeft(item, iteration * displacement);
                    Canvas.SetTop(item, displacement);

                    //System.Windows.MessageBox.Show($"{iteration}\n{displacement}\n{iteration * displacement}");
                }
                else
                {
                    Canvas.SetLeft(item, displacement);
                    Canvas.SetTop(item, iteration * displacement);
                }

                Canvas.SetZIndex(item, item.GetContainerData().z_position);

                iteration++;
            }
        }

        private void load()
        {
            XML_Handler xml_Handler = new XML_Handler(new SharedLogic().GetDataHandler().GetCoreData());

            foreach (CoreContainer item in xml_Handler.Container_load())
            {
                item.setCanvas(ref __CoreCanvas);

                add_element_to_canvas(item, item.get_dragPoint());

                item.load_Container();
            }
        }


        internal void MapSelection(
            CoreContainer event_sender,
            System.Windows.Point sender_origin)
        {
            double x0, y0, x_diff, y_diff;

            foreach (CoreContainer item in selected_items)
            {
                if (item != event_sender)
                {
                    x0 = Canvas.GetLeft(item);
                    y0 = Canvas.GetTop(item);

                    x_diff = x0 - sender_origin.X;
                    y_diff = y0 - sender_origin.Y;

                    item.setPosition(new Point(x_diff, y_diff));
                }
            }
        }


        internal void MoveSelection(
            CoreContainer event_sender,
            System.Windows.Point sender_origin,
            System.Windows.Point new_point)
        {
            double x1, y1;

            foreach (CoreContainer item in selected_items)
            {
                if (item != event_sender)
                {
                    x1 = new_point.X + item.get_dragPoint().X;
                    y1 = new_point.Y + item.get_dragPoint().Y;

                    Canvas.SetLeft(item, x1);
                    Canvas.SetTop(item, y1);
                }
            }
        }

        internal void PositionElement(CoreContainer container, System.Windows.Point point)
        {
            Canvas.SetLeft(container, point.X);
            Canvas.SetTop(container, point.Y);
        }

        internal void PositionElement(CoreContainer container, MouseButtonEventArgs e)
        {
            Canvas.SetLeft(container, e.GetPosition(canvas).X);
            Canvas.SetTop(container, e.GetPosition(canvas).Y);
        }
        internal void RemoveFlatShareCC()
        {
            foreach (CoreContainer container in canvas.Children)
            {
                if (container.GetContainerData().GetElement().GetType() == typeof(FlatShareCC))
                {
                    new SharedLogic().GetElementHandler().removeElement(container);

                    canvas.Children.Remove(container);
                    break;
                }
            }
        }

        internal void RemoveMyNote()
        {
            foreach (CoreContainer container in canvas.Children)
            {
                if (container.GetContainerData().GetElement().GetType() == typeof(MyNote))
                {
                    new SharedLogic().GetElementHandler().removeElement(container);

                    canvas.Children.Remove(container);
                    break;
                }
            }
        }

        internal void removeFromSelectedItems(CoreContainer coreContainer)
        {
            selected_items.Remove(coreContainer);
        }

        private void select_containers()
        {
            var rectangleGeometry = selection_rectangle.RenderedGeometry as RectangleGeometry;
            var hitTestParams = new GeometryHitTestParameters(rectangleGeometry);

            var resultCallback = new HitTestResultCallback(
                result => HitTestResultBehavior.Continue);

            var filterCallback = new HitTestFilterCallback(
                element =>
                {
                    if (VisualTreeHelper.GetParent(element) == canvas)
                    {
                        if (element != null && element.GetType() != selection_rectangle.GetType())
                        {
                            CoreContainer coreContainer = element as CoreContainer;

                            if (coreContainer != null)
                            {
                                coreContainer.select();

                                if (!selected_items.Contains(coreContainer))
                                {
                                    selected_items.Add(coreContainer);
                                }
                            }
                        }
                    }
                    return HitTestFilterBehavior.Continue;
                });

            VisualTreeHelper.HitTest(
                canvas, filterCallback, resultCallback, hitTestParams);
        }

        internal void selectAll()
        {
            foreach (CoreContainer item in canvas.Children)
            {
                item.select();
            }
        }

        internal void selectContainer(int containerId)
        {
            foreach (CoreContainer item in canvas.Children)
            {
                if (item.GetContainerData().ContainerDataFilename.Equals($"{containerId}.xml"))
                {
                    item.select();
                }
            }
        }

        internal void selectType(Type type)
        {
            foreach (CoreContainer item in canvas.Children)
            {
                if (item.GetContainerData().GetElement().GetType() == type)
                {
                    item.select();
                }
            }
        }

        internal void setCanvasData(CanvasData data)
        {
            this.config = data;
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
                deselect_selected_containers();

                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    canvas.Children.Remove(selection_rectangle);

                    selection_point = new Point();
                    selection_point = e.GetPosition(canvas);

                    selection_rectangle = null;

                    selection_rectangle = new System.Windows.Shapes.Rectangle();
                    Canvas.SetZIndex(selection_rectangle, config.dragLevel);

                    selection_rectangle.Stroke = config.borderbrush.GetBrush();
                    selection_rectangle.StrokeDashArray = new DoubleCollection() { 2, 1, 4, 1 };
                    selection_rectangle.StrokeThickness = 7;
                    selection_rectangle.Fill = new SolidColorBrush(Colors.Transparent);

                    Canvas.SetLeft(selection_rectangle, selection_point.X);
                    Canvas.SetTop(selection_rectangle, selection_point.Y);

                    canvas.Children.Add(selection_rectangle);

                    e.Handled = true;
                }
            }

            else if (e.ChangedButton == MouseButton.Right)
            {
                add_element_to_canvas(new RightClickChoice(), e);

                e.Handled = true;
            }

        }

        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if ((Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)) && e.LeftButton == MouseButtonState.Pressed)
            {
                System.Windows.Point pos = e.GetPosition(canvas);

                double x = Math.Min(pos.X, selection_point.X);
                double y = Math.Min(pos.Y, selection_point.Y);

                double w = Math.Max(pos.X, selection_point.X) - x;
                double h = Math.Max(pos.Y, selection_point.Y) - y;

                //dragWidth = w;
                //dragHeight = h;

                if (selection_rectangle != null)
                {
                    selection_rectangle.Width = w;
                    selection_rectangle.Height = h;

                    Canvas.SetLeft(selection_rectangle, x);
                    Canvas.SetTop(selection_rectangle, y);
                }

            }
            else
            {
                canvas.Children.Remove(selection_rectangle);
                //selection_rectangle = null;
            }
        }

        private void canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    select_containers();

                    canvas.Children.Remove(selection_rectangle);
                }

                e.Handled = true;
            }
        }

        private void canvas_PreviewDragOver(object sender, DragEventArgs e)
        {

        }


        #endregion events

    }
}
/*  END OF FILE
 * 
 * 
 */