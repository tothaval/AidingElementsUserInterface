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
using AidingElementsUserInterface.Core.Auxiliaries;
using AidingElementsUserInterface.Elements;
using AidingElementsUserInterface.Elements.FlatShareCC;
using AidingElementsUserInterface.Elements.MyNote;
using AidingElementsUserInterface.Texts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
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
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

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

        private Link_Handler link_Handler = new Link_Handler();

        public CoreCanvas()
        {
            InitializeComponent();

            build();
        }


        internal void add_element_to_canvas(UserControl content)
        {
            SharedLogic logic = new SharedLogic();

            CoreContainer coreContainer = logic.GetElementHandler().instantiate(content, __CoreCanvas);

            if (coreContainer != null)
            {
                PositionElement(coreContainer, logic.point);
                canvas.Children.Add(coreContainer);
            }
        }

        internal void add_element_to_canvas(UserControl content, MouseButtonEventArgs e)
        {
            SharedLogic logic = new SharedLogic();

            CoreContainer coreContainer = logic.GetElementHandler().instantiate(content, __CoreCanvas);

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
                config = new CanvasData();
            }

            data_Handler.AddData(config);

            __CoreCanvas.Background = new SolidColorBrush(config.background);

            __CoreCanvas.BorderBrush = new SolidColorBrush(config.borderbrush);
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

        private void load()
        {
            XML_Handler xml_Handler = new XML_Handler(new SharedLogic().GetDataHandler().GetCoreData());

            foreach (CoreContainer item in xml_Handler.Container_load())
            {
                item.setCanvas(this);

                add_element_to_canvas(item, item.get_dragPoint());

                item.load_Container();
            }
        }

        internal async void MoveSelection(
            CoreContainer event_sender,
            System.Windows.Point sender_origin,
            System.Windows.Point new_point)
        {
            double x0, y0, x_diff, y_diff, x1, y1;

            foreach (CoreContainer item in selected_items)
            {
                if (item != event_sender)
                {
                    x0 = Canvas.GetLeft(item);
                    y0 = Canvas.GetTop(item);

                    x_diff = sender_origin.X - x0;
                    y_diff = sender_origin.Y - y0;

                    x1 = new_point.X + x_diff;
                    y1 = new_point.Y + y_diff;

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
                if (container.GetContainerData().getContent().GetType() == typeof(FlatShareCC))
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
                if (container.GetContainerData().getContent().GetType() == typeof(MyNote))
                {
                    new SharedLogic().GetElementHandler().removeElement(container);

                    canvas.Children.Remove(container);
                    break;
                }
            }
        }

        internal void removeSelectedItem(CoreContainer coreContainer)
        {
            selected_items.Remove(coreContainer);
        }

        private void select_containers()
        {
            if (canvas.Children.Contains(selection_rectangle))
            {

                // how to do the hittest? 
                // i need to determine whether a single set of coordinates 
                // of one object is part of the coordinates set of the other
                // or i use an already existing function like hittest, but i don't
                // know how this is done.


                foreach (object item in canvas.Children)
                {
                    if (item is CoreContainer container)
                    {

                    }
                }

                canvas.Children.Remove(selection_rectangle);
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
                    selection_point = e.GetPosition(canvas);

                    selection_rectangle = new System.Windows.Shapes.Rectangle();

                    selection_rectangle.Stroke = new SolidColorBrush(config.highlight);
                    selection_rectangle.StrokeDashArray = new DoubleCollection() { 2, 1 };
                    selection_rectangle.StrokeThickness = 3;
                    //selection_rectangle.Fill = new SolidColorBrush(Colors.Transparent);

                    Canvas.SetLeft(selection_rectangle, selection_point.X);
                    Canvas.SetTop(selection_rectangle, selection_point.Y);

                    canvas.Children.Add(selection_rectangle);

                    e.Handled = true;
                }
            }

            else if (e.ChangedButton == MouseButton.Right)
            {
                add_element_to_canvas(new RightClickChoice(), e);
            }
        }

        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
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

        private void canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                select_containers();

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