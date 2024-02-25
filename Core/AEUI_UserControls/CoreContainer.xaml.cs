/* Aiding Elements User Interface
 *      CoreContainer element 
 * 
 * basic frame for content elements
 * 
 * init:        2023|11|27
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.AEUI_HelperClasses;
using AidingElementsUserInterface.Core.AEUI_Logic;
using AidingElementsUserInterface.Core.Auxiliaries;

using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AidingElementsUserInterface.Core.AEUI_UserControls
{
    /// <summary>
    /// CoreContainer(UserControl_Inheritance){[0] empty; [1] ContainerData; [2] UserControl, CoreCanvas;}
    /// intended to serve as container element for other elements
    /// </summary>
    public partial class CoreContainer : UserControl_Inheritance
    {
        private CoreCanvas canvas;
        private ContainerData containerData;

        CoreData? buttonData;
        CoreData? labelData;
        CoreData? textBoxData;

        private object element { get; set; }

        internal object Element => element;

        private Point dragPoint = new Point();

        private bool elementDrag = false;
        private bool container_is_selected = false;

        // constructors
        #region constructors
        public CoreContainer()
        {
            InitializeComponent();

            containerData = new ContainerData();

            ContainerDataResources(containerData);
        }

        internal CoreContainer(ContainerData containerData, object element)
        {
            this.containerData = containerData;
            this.element = element;

            InitializeComponent();

            ContainerDataResources(containerData);

        }

        internal CoreContainer(Shape element, ContainerData containerData, ref CoreCanvas canvas)
        {
            InitializeComponent();

            this.canvas = canvas;
            this.element = element;

            content_border.Child = element;

            this.containerData = new ContainerData(new SharedLogic().GetDataHandler().LoadCoreData(), element, canvas.getCanvasData().canvasID);

            this.containerData.SetContainerDataFilename($"{canvas.canvas.Children.Count}.xml");

            //containerData = new ContainerData(canvas.getCanvasData(), element);

            ContainerDataResources(containerData);

            build();

            element_border.Background = new SolidColorBrush(Colors.Transparent);
            element_border.BorderBrush = new SolidColorBrush(Colors.Transparent);
        }

        internal CoreContainer(UserControl element, ref CoreCanvas canvas)
        {

            InitializeComponent();

            this.canvas = canvas;
            this.element = element;

            containerData = new ContainerData(new SharedLogic().GetDataHandler().LoadCoreData(), element, canvas.getCanvasData().canvasID);

            containerData.SetContainerDataFilename($"{canvas.canvas.Children.Count}.xml");

            ContainerDataResources(containerData);

            initialize_container();
        }
        #endregion constructors


        private void build()
        {
            Background = new SolidColorBrush(Colors.Transparent);

            register_events();
        }

        internal void setButtonData(CoreData buttonData)
        {
            // thought applying resources on surrounding context of button 
            // would overwrite application wide resource setting, but it does not   

            if (buttonData == null)
            {
                buttonData = new CoreData();
            }

            __CoreContainer.content_border.Resources["ButtonData_background"] = buttonData.background.GetBrush();
            __CoreContainer.content_border.Resources["ButtonData_borderbrush"] = buttonData.borderbrush.GetBrush();
            __CoreContainer.content_border.Resources["ButtonData_foreground"] = buttonData.foreground.GetBrush();
            __CoreContainer.content_border.Resources["ButtonData_highlight"] = buttonData.highlight.GetBrush();

            __CoreContainer.content_border.Resources["ButtonData_cornerRadius"] = buttonData.cornerRadius;
            __CoreContainer.content_border.Resources["ButtonData_thickness"] = buttonData.thickness;

            __CoreContainer.content_border.Resources["ButtonData_fontSize"] = (double)buttonData.fontSize;
            __CoreContainer.content_border.Resources["ButtonData_fontFamily"] = buttonData.fontFamily;

            __CoreContainer.content_border.Resources["ButtonData_width"] = buttonData.width;
            __CoreContainer.content_border.Resources["ButtonData_height"] = buttonData.height;

            if (File.Exists(buttonData.background.brushpath))
            {
                __CoreContainer.content_border.Resources["ButtonData_image"] = buttonData.background.GetBrush();
                __CoreContainer.content_border.Resources["ButtonData_background"] = buttonData.background.GetBrush();
            }

            this.buttonData = buttonData;
        }

        internal void setLabelData(CoreData labelData)
        {
            if (labelData == null)
            {
                labelData = new CoreData();
            }

            __CoreContainer.Resources["LabelData_background"] = labelData.background.GetBrush();
            __CoreContainer.Resources["LabelData_borderbrush"] = labelData.borderbrush.GetBrush();
            __CoreContainer.Resources["LabelData_foreground"] = labelData.foreground.GetBrush();
            __CoreContainer.Resources["LabelData_highlight"] = labelData.highlight.GetBrush();
            
            __CoreContainer.Resources["LabelData_cornerRadius"] = labelData.cornerRadius;
            __CoreContainer.Resources["LabelData_thickness"] = labelData.thickness;
            
            __CoreContainer.Resources["LabelData_fontSize"] = (double)labelData.fontSize;
            __CoreContainer.Resources["LabelData_fontFamily"] = labelData.fontFamily;
            
            __CoreContainer.Resources["LabelData_width"] = labelData.width;
            __CoreContainer.Resources["LabelData_height"] = labelData.height;

            if (File.Exists(labelData.background.brushpath))
            {
                __CoreContainer.Resources["LabelData_image"] = labelData.background.GetBrush();
                __CoreContainer.Resources["LabelData_background"] = labelData.background.GetBrush();
            }

            this.labelData = labelData;
        }

        internal void setTextBoxData(CoreData textBoxData)
        {
            if (textBoxData == null)
            {
                textBoxData = new CoreData();
            }

            __CoreContainer.Resources["TextBoxData_background"] = textBoxData.background.GetBrush();
            __CoreContainer.Resources["TextBoxData_borderbrush"] = textBoxData.borderbrush.GetBrush();
            __CoreContainer.Resources["TextBoxData_foreground"] = textBoxData.foreground.GetBrush();
            __CoreContainer.Resources["TextBoxData_highlight"] = textBoxData.highlight.GetBrush();

            __CoreContainer.Resources["TextBoxData_cornerRadius"] = textBoxData.cornerRadius;
            __CoreContainer.Resources["TextBoxData_thickness"] = textBoxData.thickness;

            __CoreContainer.Resources["TextBoxData_fontSize"] = (double)textBoxData.fontSize;
            __CoreContainer.Resources["TextBoxData_fontFamily"] = textBoxData.fontFamily;

            __CoreContainer.Resources["TextBoxData_width"] = textBoxData.width;
            __CoreContainer.Resources["TextBoxData_height"] = textBoxData.height;

            if (File.Exists(textBoxData.background.brushpath))
            {
                __CoreContainer.Resources["TextBoxData_image"] = textBoxData.background.GetBrush();
                __CoreContainer.Resources["TextBoxData_background"] = textBoxData.background.GetBrush();
            }

            this.textBoxData = textBoxData;
        }

        internal async void ContainerDataResources(ContainerData? containerData)
        {
            //object element = this.containerData.GetElement();

            //containerData.element = (UserControl)element;


            if (containerData == null)
            {
                containerData = new ContainerData(new CoreData(), GetContainerData().CanvasID);
            }
            else
            {
                if (containerData.settings == null)
                {
                    containerData.settings = new CoreData();
                }
            }

            containerData.SetContainerDataFilename(this.containerData.ContainerDataFilename);
            containerData.SetElement(this.containerData.GetElement());
            containerData.SetCanvasID(this.containerData.CanvasID);

            __CoreContainer.Resources["ContainerData_background"] = containerData.settings.background.GetBrush();
            __CoreContainer.Resources["ContainerData_borderbrush"] = containerData.settings.borderbrush.GetBrush();
            __CoreContainer.Resources["ContainerData_foreground"] = containerData.settings.foreground.GetBrush();
            __CoreContainer.Resources["ContainerData_highlight"] = containerData.settings.highlight.GetBrush();

            __CoreContainer.Resources["ContainerData_cornerRadius"] = containerData.settings.cornerRadius;
            __CoreContainer.Resources["ContainerData_thickness"] = containerData.settings.thickness;

            __CoreContainer.Resources["ContainerData_fontSize"] = (double)containerData.settings.fontSize;
            __CoreContainer.Resources["ContainerData_fontFamily"] = containerData.settings.fontFamily;

            __CoreContainer.Resources["ContainerData_width"] = containerData.settings.width;
            __CoreContainer.Resources["ContainerData_height"] = containerData.settings.height;

            __CoreContainer.Resources["ContainerData_CanvasID"] = containerData.CanvasID;

            __CoreContainer.Resources["ContainerData_ContainerDataFilename"] = containerData.ContainerDataFilename;

            __CoreContainer.Resources["ContainerData_z_position"] = containerData.level;


            if (File.Exists(containerData.settings.background.brushpath))
            {
                __CoreContainer.Resources["ContainerData_image"] = containerData.settings.background.GetBrush();
                __CoreContainer.Resources["ContainerData_background"] = containerData.settings.background.GetBrush();
            }


            if (content_border.Child != null)
            {
                if (content_border.Child.GetType().IsSubclassOf(typeof(Shape)))
                {
                    ((Shape)content_border.Child).Fill = containerData.settings.background.GetBrush();

                    if (File.Exists(containerData.settings.background.brushpath))
                    {
                        ((Shape)content_border.Child).Fill = containerData.settings.background.GetBrush();

                    }

                    if (container_is_selected)
                    {
                        ((Shape)content_border.Child).Stroke = containerData.settings.highlight.GetBrush();
                    }
                    else
                    {
                        ((Shape)content_border.Child).Stroke = containerData.settings.borderbrush.GetBrush();
                    }
                }
            }

            setRotation(containerData.rotation);

            await Task.Delay(4);

            this.containerData = containerData;
        }

        internal void deselect(bool remove_from_list = true)
        {
            container_is_selected = false;

            element_border.BorderBrush = containerData.settings.borderbrush.GetBrush();

            if (remove_from_list)
            {
                canvas.removeFromSelectedItems(this);
            }

            if (content_border.Child != null)
            {
                if (content_border.Child.GetType().IsSubclassOf(typeof(Shape)))
                {
                    ((Shape)content_border.Child).Fill = containerData.settings.background.GetBrush();
                    ((Shape)content_border.Child).Stroke = containerData.settings.borderbrush.GetBrush();
                    ((Shape)content_border.Child).StrokeThickness = containerData.settings.thickness.Left;

                    element_border.Background = new SolidColorBrush(Colors.Transparent);
                    element_border.BorderBrush = new SolidColorBrush(Colors.Transparent);
                    element_border.BorderThickness = new Thickness(0);
                }
            }
        }

        internal ref CoreCanvas GetCanvas()
        {
            return ref canvas;
        }

        internal ContainerData GetContainerData()
        {

            return containerData;


        }

        internal bool get_containerIsSelected()
        {
            return container_is_selected;
        }

        internal Point get_dragPoint()
        {
            return dragPoint;
        }

        internal Point get_Position()
        {
            return canvas.GetElementPosition(this);
        }

        internal double getRotation()
        {
            RotateTransform rotation = this.RenderTransform as RotateTransform;

            if (rotation != null)
            {
                double rotationInDegrees = rotation.Angle;

                return rotationInDegrees;
            }

            return 0;
        }

        internal void hideContainerNesting(CoreContainer coreContainer)
        {
            coreContainer.element_border.Background = new SolidColorBrush(Colors.Transparent);
            coreContainer.element_border.BorderBrush = new SolidColorBrush(Colors.Transparent);
            coreContainer.element_border.BorderThickness = new Thickness(0);

            coreContainer.ColumnLeft.Width = new GridLength(0);
            coreContainer.ColumnRight.Width = new GridLength(0);

            coreContainer.RowTop.Height = new GridLength(0);
            coreContainer.RowBottom.Height = new GridLength(0);

            //coreContainer.ContainerDataResources();
        }

        private async void initialize_container()
        {
            await Task.Delay(2);

            if (containerData.GetElement() == null)
            {
                containerData.SetElement(new UserControl());
            }

            content_border.Child = null;

            content_border.Child = (UIElement)containerData.GetElement();

            build();
        }

        internal void load_Container()
        {
            initialize_container();
        }

        private void register_events()
        {
            element_border.MouseEnter += Element_border_MouseEnter;
            element_border.MouseLeave += Element_border_MouseLeave;

            element_border.MouseDown += Element_border_MouseDown;
            element_border.MouseMove += Element_border_MouseMove;
            element_border.MouseUp += Element_border_MouseUp;
        }

        internal void remove_Container()
        {
            canvas.canvas.Children.Remove(__CoreContainer);

            content_border.Child = null;
        }

        internal void select()
        {
            container_is_selected = true;
            element_border.BorderBrush = containerData.settings.highlight.GetBrush();

            canvas.add_selected_item(this);

            if (content_border.Child != null)
            {
                if (content_border.Child.GetType().IsSubclassOf(typeof(Shape)))
                {
                    ((Shape)content_border.Child).Fill = containerData.settings.background.GetBrush();
                    ((Shape)content_border.Child).Stroke = containerData.settings.highlight.GetBrush();
                    ((Shape)content_border.Child).StrokeThickness = containerData.settings.thickness.Left;

                    element_border.Background = new SolidColorBrush(Colors.Transparent);
                    element_border.BorderBrush = new SolidColorBrush(Colors.Transparent);
                    element_border.BorderThickness = new Thickness(0);
                }
            }
        }

        private void selected()
        {
            if (container_is_selected)
            {
                deselect();
            }
            else
            {
                select();
            }
        }

        internal void setCanvas(ref CoreCanvas coreCanvas)
        {
            this.canvas = coreCanvas;

            containerData.SetCanvasID(canvas.getCanvasData().canvasID);
        }

        internal void setContainerData(ContainerData containerData)
        {
            this.containerData = containerData;
        }

        internal void setPosition(Point point)
        {
            containerData.position = point;
            this.dragPoint = point;
        }

        internal void setRotation(double angle)
        {
            containerData.rotation = angle;

            this.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);

            this.RenderTransform = new RotateTransform(angle);
        }

        internal void setSize(double width, double height)
        {
            // size change for now is limited to set minWidth and minHeight, 
            // with the consequence that all elements will remain useable
            // and never to small to display their contents

            containerData.settings.width = width;
            containerData.settings.height = height;

            __CoreContainer.Resources["ContainerData_width"] = containerData.settings.width;
            __CoreContainer.Resources["ContainerData_height"] = containerData.settings.height;

            OnChildDesiredSizeChanged(this);
        }


        // element mouse events
        #region element mouse events
        private void Element_border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                ContainerLogic.DragStart(
                    ref elementDrag,
                    ref __CoreContainer,
                    ref dragPoint,
                    ref canvas
                    );


                Panel.SetZIndex(__CoreContainer, CoreCanvasSwitchData.Get_CORECANVAS_DRAG_LEVEL);

                //mainWindow.writing = false;
                ////Keyboard.ClearFocus();
                Keyboard.Focus(new SharedLogic().GetMainWindow().focusTarget);
            }


            if (e.ChangedButton == MouseButton.Right)
            {
                remove_Container();
            }

            e.Handled = true;
        }

        private void Element_border_MouseMove(object sender, MouseEventArgs e)
        {
            ContainerLogic.DragMove(
                ref elementDrag,
                ref __CoreContainer,
                ref dragPoint,
                ref canvas
                );
        }

        private void Element_border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                ContainerLogic.DragStop(
                    ref elementDrag
                    );

                dragPoint = new Point(Canvas.GetLeft(__CoreContainer), Canvas.GetTop(__CoreContainer));

                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    selected();
                }

                Panel.SetZIndex(__CoreContainer, containerData.level);

                //int count = 0;
                //foreach (YS_CORE_ContainerElement item in mainWindow.UIE_ModuleCreator.list_YS_CORE_ContainerElement)
                //{
                //    if (item.Name.Contains("YS_UI_PlacementAndDesignElement"))
                //    {
                //        count++;
                //    }
                //}

                //if (count > 0)
                //{
                //    mainWindow.UIE_ModuleCreator.updateElementInfo(CORE_ContainerElement);
                //}

                e.Handled = true;
            }
        }
        private void Element_border_MouseEnter(object sender, MouseEventArgs e)
        {
            if (containerData != null)
            {
                int index = containerData.level;

                Panel.SetZIndex(__CoreContainer, index + CoreCanvasSwitchData.Get_CORECANVAS_HOVER_LEVEL);
            }

            e.Handled = true;


            ToolTip = Panel.GetZIndex(__CoreContainer);
        }

        private void Element_border_MouseLeave(object sender, MouseEventArgs e)
        {
            if (containerData != null)
            {
                Panel.SetZIndex(__CoreContainer, containerData.level);
            }

            ToolTip = Panel.GetZIndex(__CoreContainer);

            //System.Windows.Threading.DispatcherTimer _fadeOut = new System.Windows.Threading.DispatcherTimer();

            //_fadeOut.Tick += _fadeOut_Tick;
            //_fadeOut.Interval = TimeSpan.FromSeconds(3);
            //_fadeOut.Start();

            e.Handled = true;
        }

        //private void _fadeOut_Tick(object? sender, EventArgs e)
        //{
        //    ToolTip = null;
        //}
        #endregion element mouse events

        private void element_grid_MouseEnter(object sender, MouseEventArgs e)
        {


        }

        private void element_grid_MouseMove(object sender, MouseEventArgs e)
        {

            //this.ToolTip = $"{(int)e.GetPosition(canvas).X}:{(int)e.GetPosition(canvas).Y}";            
        }

        private void element_grid_MouseLeave(object sender, MouseEventArgs e)
        {

        }
    }
}
/*  END OF FILE
 * 
 * 
 */