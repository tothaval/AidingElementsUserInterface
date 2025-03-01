﻿/* Aiding Elements User Interface
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
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static AidingElementsUserInterface.Core.AEUI_UserControls.Adjust;
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
        private CanvasData config { get; set; }

        private LevelSystem levelSystem { get; set; }

        private System.Windows.Point selection_point;
        private System.Windows.Shapes.Rectangle selection_rectangle;

        private System.Windows.Threading.DispatcherTimer _timer = new System.Windows.Threading.DispatcherTimer();

        private ObservableCollection<CoreContainer> selected_items = new ObservableCollection<CoreContainer>();

        private CallCentral callCentral;

        private bool SYSTEM_CANVAS_FLAG = false;

        private Point image_offset;
        private Point mouse_position;

        internal CoreCanvas(CanvasData canvasData)
        {
            InitializeComponent();

            this.config = canvasData;

            CanvasDataResources(config);

            build();

            callCentral = new CallCentral(ref __CoreCanvas);

            loadElements(config.canvasID);

            _timer.Tick += _timer_Tick;
            _timer.Interval = TimeSpan.FromMilliseconds(1);
            _timer.Start();

        }
        private void _timer_Tick(object sender, EventArgs e)
        {
            NoLevelBackground();
            MainWindowData mainWindowData = new XML_Handler().MainWindowData_load();

            if (SYSTEM_CANVAS_FLAG)
            {
                SYSTEM_xml xml = new SYSTEM_xml();

                LevelSystem levelSystem = xml.SYSTEM_Level_load($"{xml.SYSTEM_folder}{xml.LevelData_file}", config.level_id);

                if (levelSystem != null)
                {
                    _LevelBar.update(levelSystem.Get_CURRENT_LEVEL());

                    if (levelSystem.Get_CURRENT_LEVEL().HASBACKGROUND)
                    {
                        if (levelSystem.Get_CURRENT_LEVEL().Background != null)
                        {
                            SetBackground(levelSystem.Get_CURRENT_LEVEL().Background);
                        }
                        else
                        {
                            levelSystem.Get_CURRENT_LEVEL().SetBackground(false, null);
                        }
                    }

                    this.levelSystem = levelSystem;
                }
            }
            else
            {
                UserSpace_xml xml = new UserSpace_xml();

                LevelSystem levelSystem = xml.UserSpace_Level_load($"{xml.UserSpace_folder}screen_{config.canvasID}\\{xml.LevelData_file}", config.canvasID, config.level_id);

                if (levelSystem != null)
                {

                    _LevelBar.update(levelSystem.Get_CURRENT_LEVEL());


                    if (levelSystem.Get_CURRENT_LEVEL().HASBACKGROUND)
                    {
                        if (levelSystem.Get_CURRENT_LEVEL().Background != null)
                        {
                            SetBackground(levelSystem.Get_CURRENT_LEVEL().Background);
                        }
                    }

                    this.levelSystem = levelSystem;
                }
            }

            if (levelSystem == null)
            {
                levelSystem = new LevelSystem(config.canvasID);
            }




            LSD.setCanvas(ref __CoreCanvas);

            _timer.Stop();
        }



        internal void ADJUST_GATE(ADJUST_DATA_STRUCTURE data)
        {
            foreach (CoreContainer item in selected_items)
            {
                Canvas.SetLeft(item, data.x);
                Canvas.SetTop(item, data.y);
                item.setRotation(data.rotation);
                
                item.setSize(data.width, data.height);

                item.setLevel(data.level);
            }
        }


        internal void add_element_to_canvas(UserControl content)
        {
            SharedLogic logic = new SharedLogic();

            CoreContainer coreContainer;

            //if (SYSTEM_CANVAS_FLAG)
            //{
                coreContainer = instantiate(content, ref __CoreCanvas);

                if (coreContainer != null)
                {
                    PositionElement(coreContainer, logic.point);
                    SetLevel(coreContainer);
                    canvas.Children.Add(coreContainer);
                }
            //}
            //else
            //{
            //    coreContainer = instantiate(content, ref __CoreCanvas);
            //    if (coreContainer != null)
            //    {
            //        PositionElement(coreContainer, logic.point);
            //        SetLevel(coreContainer);
            //        canvas.Children.Add(coreContainer);
            //    }
            //}
        }

        internal void add_element_to_canvas(CoreContainer content, MouseButtonEventArgs e)
        {
            SharedLogic logic = new SharedLogic();

            CoreContainer coreContainer;

            //if (SYSTEM_CANVAS_FLAG)
            //{
                coreContainer = instantiate(content, ref __CoreCanvas);

                if (coreContainer != null)
                {
                    PositionElement(coreContainer, e.GetPosition(canvas));
                    SetLevel(coreContainer);

                    canvas.Children.Add(coreContainer);
                }
            //}
            //else
            //{
            //    coreContainer = instantiate(content, ref __CoreCanvas);

            //    if (coreContainer != null)
            //    {
            //        PositionElement(coreContainer, e.GetPosition(canvas));
            //        SetLevel(coreContainer);

            //        canvas.Children.Add(coreContainer);
            //    }
            //}


        }

        internal void add_element_to_canvas(CoreContainer container, System.Windows.Point point)
        {
            //if (SYSTEM_CANVAS_FLAG)
            //{
                PositionElement(container, point);
                SetLevel(container);

                canvas.Children.Add(container);
            //}
            //else
            //{
            //    PositionElement(container, point);
            //    SetLevel(container);

            //    canvas.Children.Add(container);
            //}

        }

        internal void add_selected_item(CoreContainer coreContainer)
        {
            selected_items.Add(coreContainer);
        }

        private void build()
        {
            Resources["Level"] = 0;
            Resources["CurrentLevel"] = 0;

            Resources["LevelBarHeight"] = (double)75;

            Resources["LevelShiftOrientationOut"] = Orientation.Vertical;
            Resources["LevelShiftOrientationScroll"] = Orientation.Horizontal;
            Resources["LevelShiftOrientationInner"] = Orientation.Vertical;

            //Application.Current.Resources["LevelShiftOrientationOut"] = Orientation.Vertical;
            //Application.Current.Resources["LevelShiftOrientationInner"] = Orientation.Horizontal;

            CanvasDataResources(config);
        }

        internal void CanvasDataResources(CanvasData canvasData)
        {
            if (canvasData == null)
            {
                canvasData = new CanvasData(true);
            }

            CoreCanvasBorder.Resources["CanvasData_background"] = canvasData.background.GetBrush();
            CoreCanvasBorder.Resources["CanvasData_borderbrush"] = canvasData.borderbrush.GetBrush();
            CoreCanvasBorder.Resources["CanvasData_foreground"] = canvasData.foreground.GetBrush();
            CoreCanvasBorder.Resources["CanvasData_highlight"] = canvasData.highlight.GetBrush();

            CoreCanvasBorder.Resources["CanvasData_cornerRadius"] = canvasData.cornerRadius;
            CoreCanvasBorder.Resources["CanvasData_thickness"] = canvasData.thickness;

            CoreCanvasBorder.Resources["CanvasData_fontSize"] = (double)canvasData.fontSize;
            CoreCanvasBorder.Resources["CanvasData_fontFamily"] = canvasData.fontFamily;

            CoreCanvasBorder.Resources["CanvasData_width"] = canvasData.width;
            CoreCanvasBorder.Resources["CanvasData_height"] = canvasData.height;

            CoreCanvasBorder.Resources["CanvasData_canvasName"] = canvasData.canvasName;
            CoreCanvasBorder.Resources["CanvasData_element_spacing"] = canvasData.element_spacing;

            CoreCanvasBorder.Resources["CanvasData_grouping_displacement"] = canvasData.grouping_displacement;


            if (File.Exists(canvasData.background.brushpath))
            {
                CoreCanvasBorder.Resources["CanvasData_image"] = canvasData.background.GetBrush();
                CoreCanvasBorder.Resources["CanvasData_background"] = canvasData.background.GetBrush();
            }
            CoreCanvasBorder.Background = canvasData.background.GetBrush();

            this.config = canvasData;
        }

        internal void ChangeSelectionButtonData(CoreData buttonData)
        {
            foreach (CoreContainer item in selected_items)
            {
                item.setButtonData(buttonData);
            }
        }


        internal void ChangeSelectionLabelData(CoreData labelData)
        {
            foreach (CoreContainer item in selected_items)
            {
                item.setLabelData(labelData);
            }
        }
        internal void ChangeSelectionTextBoxData(CoreData labelData)
        {
            foreach (CoreContainer item in selected_items)
            {
                item.setTextBoxData(labelData);
            }
        }

        internal void ChangeSelectionData(ContainerData containerData)
        {
            foreach (CoreContainer item in selected_items)
            {
                item.ContainerDataResources(containerData);
            }
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

        internal CoreContainer GetCoreContainer(UserControl content)
        {
            return new CoreContainer(content, ref __CoreCanvas);
        }

        internal LevelBar GetLevelBar()
        {
            return _LevelBar;
        }

        internal LevelSystem GetLevelSystem()
        {
            return levelSystem;
        }

        internal ObservableCollection<CoreContainer> get_selected_items()
        {
            return selected_items;
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

                    Panel.SetZIndex(item, item.GetContainerData().level);

                    iteration++;
                }
            }
        }

        internal CoreContainer? instantiate(UserControl content, ref CoreCanvas target)
        {
            if (content != null)
            {
                CoreContainer coreContainer = GetCoreContainer(content);

                coreContainer.GetContainerData().CanvasID = target.getCanvasData().canvasID;

                return coreContainer;
            }

            return null;
        }

        private async void loadElements(int i)
        {
            if (i == 0)
            {
                SYSTEM_xml sys_xml = new SYSTEM_xml();

                ObservableCollection<CoreContainer> coreContainers = sys_xml.SYSTEM_Container_load();

                foreach (CoreContainer item in coreContainers)
                {
                    item.setCanvas(ref __CoreCanvas);

                    add_element_to_canvas(item, item.get_dragPoint());

                    item.load_Container();
                }

            }
            else
            {
                UserSpace_xml userSpace_Xml = new UserSpace_xml();
                ObservableCollection<CoreContainer> coreContainers = userSpace_Xml.Container_load(i);

                foreach (CoreContainer item in coreContainers)
                {
                    item.setCanvas(ref __CoreCanvas);

                    add_element_to_canvas(item, item.get_dragPoint());

                    item.load_Container();
                }
            }
        }

        internal void LevelShiftSelection(int target_level)
        {
            foreach (CoreContainer item in selected_items)
            {
                bool visibility = levelSystem.Get_CURRENT_LEVEL().VISIBILITY_FLAG;

                Panel.SetZIndex(item, target_level);

                item.GetContainerData().level = target_level;

                if (visibility)
                {
                    item.Visibility = Visibility.Visible;
                }
                else
                {
                    item.Visibility = Visibility.Hidden;
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

        internal void NoLevelBackground()
        {
            CoreCanvasBorder.Background = config.background.GetBrush();
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

        internal void RemoveMyNote()
        {
            foreach (object child in canvas.Children)
            {
                if (child.GetType() == typeof(CoreContainer))
                {
                    CoreContainer item = child as CoreContainer;

                    if (item.GetContainerData().GetElement().GetType() == typeof(MyNote))
                    {
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

        internal void SetBackground(ColorData colorData)
        {
            if (colorData != null)
            {
                CoreCanvasBorder.Background = colorData.GetBrush();
            }
        }

        internal void setCanvasData(CanvasData data)
        {
            this.config = data;

            CanvasDataResources(data);
        }

        internal void set_SYSTEM_CANVAS_FLAG(bool is_SYSTEM_CANVAS)
        {
            this.SYSTEM_CANVAS_FLAG = is_SYSTEM_CANVAS;
        }

        internal void SetLevel(CoreContainer coreContainer)
        {
            if (levelSystem != null)
            {
                coreContainer.setLevel(levelSystem.Get_LEVEL());


                //MessageBox.Show(coreContainer.GetContainerData().level.ToString());
            }
        }

        internal void SetVisibility(int newLevel, string state)
        {
            foreach (object child in canvas.Children)
            {
                if (child.GetType() == typeof(CoreContainer))
                {
                    CoreContainer element = child as CoreContainer;

                    if (element.GetContainerData().element != null)
                    {


                        if (element.GetContainerData().element.GetType() != typeof(LevelShift))
                        {
                            if (state.Equals("all"))
                            {
                                element.Visibility = Visibility.Visible;
                            }
                            if (state.Equals("range"))
                            {
                                if (Panel.GetZIndex(element) < levelSystem.min_range || Panel.GetZIndex(element) > levelSystem.max_range)
                                {
                                    element.Visibility = Visibility.Collapsed;
                                }
                                else if (Panel.GetZIndex(element) > levelSystem.min_range || Panel.GetZIndex(element) < levelSystem.max_range)
                                {
                                    element.Visibility = Visibility.Visible;
                                }
                            }
                            if (state.Equals("level"))
                            {
                                if (Panel.GetZIndex(element) == newLevel && state.Equals("level"))
                                {
                                    element.Visibility = Visibility.Visible;
                                }
                                else
                                {
                                    element.Visibility = Visibility.Collapsed;
                                }
                            }
                        }
                    }
                }
            }
        }

        internal void unsetLSD()
        {
            //LSD.Visibility = Visibility.Collapsed;
            //LSD.clear();
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
            //foreach (UIElement item in canvas.Children)
            //{
            //    if (item is CoreContainer coreCon)
            //    {
            //        AdornerLayer.GetAdornerLayer(canvas)?.Add(new ResizeAdorner(coreCon.content_border));
            //    }
            //}
                      

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

                SetCanvasToolTip(canvas.ActualWidth.ToString() + "\n" + canvas.RenderSize.Height.ToString());

                e.Handled = true;
            }

        }

        internal void SetCanvasToolTip(string tip)
        {
            canvas.ToolTip = tip;
        }

        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                canvas.CaptureMouse();

                mouse_position = e.GetPosition(CoreCanvasBorder);
                image_offset.X = canvas.RenderTransform.Value.OffsetX;
                image_offset.Y = canvas.RenderTransform.Value.OffsetY;

            }
            if (canvas.IsMouseCaptured)
            {
                return;
            }

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

            if (!canvas.IsMouseCaptured) return;

            if (canvas.IsMouseCaptured)
            {
                Point p = e.MouseDevice.GetPosition(CoreCanvasBorder);

                Matrix m = canvas.RenderTransform.Value;
                m.OffsetX = image_offset.X + (p.X - mouse_position.X);
                m.OffsetY = image_offset.Y + (p.Y - mouse_position.Y);

                canvas.RenderTransform = new MatrixTransform(m);

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

        private void SV_canvasZoom_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            canvas.RenderTransform = new MatrixTransform(1, 0, 0, 1, 0, 0);
            canvas.Background = new SolidColorBrush(Colors.Transparent);

            e.Handled = true;
        }

        private void canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Point p = e.MouseDevice.GetPosition(canvas);

            Matrix m = canvas.RenderTransform.Value;
            if (e.Delta > 0)
            {
                m.ScaleAtPrepend(1.2, 1.2, p.X, p.Y);
            }
            else
            {
                if (canvas.RenderTransform.Value.M11 > 0.095 && canvas.RenderTransform.Value.M22 > 0.095)
                {
                    m.ScaleAtPrepend(1 / 1.2, 1 / 1.2, p.X, p.Y);
                }
            }

            canvas.RenderTransform = new MatrixTransform(m);

            canvas.Background = new SolidColorBrush(Color.FromArgb(77,255,255,255));

            e.Handled = true;

        }

        private void canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                canvas.ReleaseMouseCapture();

                //e.Handled = true;
            }
        }
    }
}
/*  END OF FILE
 * 
 * 
 */