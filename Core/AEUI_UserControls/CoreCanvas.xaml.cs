/* Aiding Elements User Interface
 *      CoreCanvas element 
 * 
 * basic surface element for placement of elements
 * and for scetching, drawing, et cetera
 * 
 * init:        2023|11|27
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 * 
 * nicht mal nachvollziehbar. so wenig kann ich tatsächlich.
 */
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.AEUI_Logic;
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
        private string canvas_name;

        private System.Windows.Point selection_point;
        private System.Windows.Shapes.Rectangle selection_rectangle;

        private ObservableCollection<CoreContainer> selected_items = new ObservableCollection<CoreContainer>();

        private CallCentral callCentral;

        public CoreCanvas()
        {
            this.canvas_name = "core canvas";

            InitializeComponent();

            build();

            callCentral = new CallCentral(ref __CoreCanvas);
        }

        public CoreCanvas(string canvas_name)
        {
            this.canvas_name = canvas_name;

            InitializeComponent();

            build();

            callCentral = new CallCentral(ref __CoreCanvas);
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

        private void build()
        {
            Data_Handler data_Handler = new SharedLogic().GetDataHandler();

            config = data_Handler.LoadCanvasData();

            if (config == null)
            {
                config = new CanvasData(canvas_name);
            }

            Application.Current.Resources["Level"] = 0;
            Application.Current.Resources["CurrentLevel"] = 0;

            Application.Current.Resources["LevelBarHeight"] = (double)75;

            Application.Current.Resources["LevelShiftOrientationOut"] = Orientation.Vertical;
            Application.Current.Resources["LevelShiftOrientationScroll"] = Orientation.Horizontal;
            Application.Current.Resources["LevelShiftOrientationInner"] = Orientation.Vertical;

            //Application.Current.Resources["LevelShiftOrientationOut"] = Orientation.Vertical;
            //Application.Current.Resources["LevelShiftOrientationInner"] = Orientation.Horizontal;

            data_Handler.AddCanvasData(config);

            canvas.ToolTip = canvas_name;

            CanvasDataResources();
        }

        private void CanvasDataResources()
        {
            CanvasData canvasData = config;

            if (canvasData == null)
            {
                canvasData = new CanvasData();
            }

            __CoreCanvas.Resources["CanvasData_background"] = canvasData.background.GetBrush();
            __CoreCanvas.Resources["CanvasData_borderbrush"] = canvasData.borderbrush.GetBrush();
            __CoreCanvas.Resources["CanvasData_foreground"] = canvasData.foreground.GetBrush();
            __CoreCanvas.Resources["CanvasData_highlight"] = canvasData.highlight.GetBrush();

            __CoreCanvas.Resources["CanvasData_cornerRadius"] = canvasData.cornerRadius;
            __CoreCanvas.Resources["CanvasData_thickness"] = canvasData.thickness;

            __CoreCanvas.Resources["CanvasData_fontSize"] = (double)canvasData.fontSize;
            __CoreCanvas.Resources["CanvasData_fontFamily"] = canvasData.fontFamily;

            __CoreCanvas.Resources["CanvasData_width"] = canvasData.width;
            __CoreCanvas.Resources["CanvasData_height"] = canvasData.height;

            __CoreCanvas.Resources["CanvasData_canvasName"] = canvasData.canvasName;
            __CoreCanvas.Resources["CanvasData_element_spacing"] = canvasData.element_spacing;

            __CoreCanvas.Resources["CanvasData_grouping_displacement"] = canvasData.grouping_displacement;


            if (File.Exists(canvasData.imageFilePath))
            {
                __CoreCanvas.Resources["CanvasData_image"] = new ImageBrush(new BitmapImage(new Uri(canvasData.imageFilePath)));
                __CoreCanvas.Resources["CanvasData_background"] = __CoreCanvas.Resources["CanvasData_image"];
            }
        }

        internal void ChangeSelectionData(ContainerData containerData)
        {
            foreach (CoreContainer item in selected_items)
            {
                item.ContainerDataResources(containerData);
            }

        }

        public void clearCanvasElements()
        {

        }

        internal void delete_selected_items()
        {
            foreach (object child in selected_items)
            {
                if (child.GetType() == typeof(CoreContainer))
                {
                    CoreContainer item = child as CoreContainer;

                    item.remove_Container();                    
                }
            }

            selected_items.Clear();
        }

        private void deselect_selected_containers()
        {
            foreach (object child in selected_items)
            {
                if (child.GetType() == typeof(CoreContainer))
                {
                    CoreContainer item = child as CoreContainer;
                    item.deselect(false);
                }
            }

            selected_items.Clear();
        }

        internal CanvasData getCanvasData()
        {
            return config;
        }   

        internal CallCentral GetCentral()
        {
            return callCentral;
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


            foreach (object child in selected_items)
            {
                if (child.GetType() == typeof(CoreContainer))
                {
                    CoreContainer item = child as CoreContainer;

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
        }

        internal void MapSelection(
            CoreContainer event_sender,
            System.Windows.Point sender_origin)
        {
            double x0, y0, x_diff, y_diff;

            foreach (object child in selected_items)
            {
                if (child.GetType() == typeof(CoreContainer))
                {
                    CoreContainer item = child as CoreContainer;
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
        }


        internal void MoveSelection(
            CoreContainer event_sender,
            System.Windows.Point sender_origin,
            System.Windows.Point new_point)
        {
            double x1, y1;


            foreach (object child in selected_items)
            {
                if (child.GetType() == typeof(CoreContainer))
                {
                    CoreContainer item = child as CoreContainer;

                    if (item != event_sender)
                    {
                        x1 = new_point.X + item.get_dragPoint().X;
                        y1 = new_point.Y + item.get_dragPoint().Y;

                        Canvas.SetLeft(item, x1);
                        Canvas.SetTop(item, y1);
                    }
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
            foreach (object child in canvas.Children)
            {
                if (child.GetType() == typeof(CoreContainer))
                {
                    CoreContainer item = child as CoreContainer;

                    if (item.GetContainerData().GetElement().GetType() == typeof(FlatShareCC))
                    {
                        new SharedLogic().GetElementHandler().removeElement(item);

                        canvas.Children.Remove(item);
                        break;
                    }
                }
            }
        }

        internal void RemoveMyNote()
        {
            foreach (object child in canvas.Children)
            {
                if (child.GetType() == typeof(CoreContainer))
                {
                    CoreContainer item = child as CoreContainer;

                    if (item.GetContainerData().GetElement().GetType() == typeof(MyNote))
                    {
                        new SharedLogic().GetElementHandler().removeElement(item);

                        canvas.Children.Remove(item);
                        break;
                    }
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
            foreach (object child in canvas.Children)
            {
                if (child.GetType() == typeof(CoreContainer))
                {
                    CoreContainer item = child as CoreContainer;

                    item.select();

                    selected_items.Add(item);
                }
            }
        }

        internal void selectContainer(int containerId)
        {
            foreach (object child in canvas.Children)
            {
                if (child.GetType() == typeof(CoreContainer))
                {
                    CoreContainer item = child as CoreContainer;

                    if (item.GetContainerData().ContainerDataFilename.Equals($"{containerId}.xml"))
                    {
                        item.select();

                        selected_items.Add(item);
                    }
                }
                
            }
        }

        internal void selectType(Type type)
        {
            foreach (object child in canvas.Children)
            {
                if (child.GetType() == typeof(CoreContainer))
                {
                    CoreContainer item = child as CoreContainer;

                    if (item.GetContainerData().GetElement().GetType() == type)
                    {
                        item.select();

                        selected_items.Add(item);
                    }
                }
            }
        }

        internal void setCanvasData(CanvasData data)
        {
            this.config = data;
        }

        internal void updateLocalDrives()
        {
            foreach (object child in canvas.Children)
            {
                if (child.GetType() == typeof(CoreContainer))
                {
                    CoreContainer item = child as CoreContainer;

                    if (item.GetContainerData().GetElement().GetType() == typeof(LocalDrives))
                    {
                        LocalDrives localDrives = item.GetContainerData().GetElement() as LocalDrives;
                        localDrives.updateDriveInfo();
                    }
                }
            }
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
                    Canvas.SetZIndex(selection_rectangle, CoreCanvasSwitchData.Get_CORECANVAS_DRAG_LEVEL);

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