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
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

        private Point dragPoint = new Point();        

        private bool elementDrag = false;
        private bool container_is_selected = false;

        #region constructors
        public CoreContainer()
        {
            ContainerDataResources();

            InitializeComponent();
        }

        internal CoreContainer(ContainerData containerData)
        {
            this.containerData = containerData;

            ContainerDataResources();

            InitializeComponent();
        }

        internal CoreContainer(UserControl element, ref CoreCanvas canvas)
        {
            ContainerDataResources();

            InitializeComponent();

            this.canvas = canvas;

            //containerData = new ContainerData(canvas.getCanvasData(), element);
            containerData = new ContainerData(new SharedLogic().GetDataHandler().LoadCoreData(), element);

            containerData.SetCanvasName(canvas.getCanvasData().canvasName);

            containerData.SetContainerDataFilename($"{canvas.canvas.Children.Count}.xml");

            initialize_container();
        }

        internal void ContainerDataResources(ContainerData containerData = null)
        {
            if (containerData == null)
            {
                containerData = new ContainerData(new CoreData());
            }
            else
            {
                if (containerData.settings == null)
                {
                    containerData.settings = new CoreData();
                }
            }

            this.Resources["ContainerData_background"] = containerData.settings.background.GetBrush();
            this.Resources["ContainerData_borderbrush"] = containerData.settings.borderbrush.GetBrush();
            this.Resources["ContainerData_foreground"] = containerData.settings.foreground.GetBrush();
            this.Resources["ContainerData_highlight"] = containerData.settings.highlight.GetBrush();
                            
            this.Resources["ContainerData_cornerRadius"] = containerData.settings.cornerRadius;
            this.Resources["ContainerData_thickness"] = containerData.settings.thickness;
                            
            this.Resources["ContainerData_fontSize"] = (double)containerData.settings.fontSize;
            this.Resources["ContainerData_fontFamily"] = containerData.settings.fontFamily;
                            
            this.Resources["ContainerData_width"] = containerData.settings.width;
            this.Resources["ContainerData_height"] = containerData.settings.height;
                            
            this.Resources["ContainerData_CanvasName"] = containerData.CanvasName;
                            
            this.Resources["ContainerData_ContainerDataFilename"] = containerData.ContainerDataFilename;
                            
            this.Resources["ContainerData_z_position"] = containerData.level;


            if (File.Exists(containerData.settings.imageFilePath))
            {
                this.Resources["ContainerData_image"] = new ImageBrush(new BitmapImage(new Uri(containerData.settings.imageFilePath)));
                this.Resources["ContainerData_background"] = this.Resources["ContainerData_image"];
            }
        }

        internal ref CoreCanvas GetCanvas()
        {
            return ref canvas;
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



        internal void setRotation(double angle)
        {
            this.RenderTransform = new RotateTransform(angle);
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

            coreContainer.ContainerDataResources();
        }

        private void _loadtimer_Tick(object sender, EventArgs e)
        {
            //loadTickCounter++;
            //
            //if (loadTickCounter == 1 || loadTickCounter < 2)
            //{
            //
            //    _loadtimer.Stop();
            //}
        }
        #endregion constructors
        internal void _backgroundImage()
        {
            if (containerData.settings.imageFilePath != null)
            {
                if (File.Exists(containerData.settings.imageFilePath))
                {
                    element_border.Background = new ImageBrush(new BitmapImage(new Uri(containerData.settings.imageFilePath)));
                }
                else
                {
                    element_border.Background = containerData.settings.background.GetBrush();
                }
            }
        }

        private void build()
        {
            Background = new SolidColorBrush(Colors.Transparent);

            register_events();
        }


        private void register_events()
        {
            element_border.MouseEnter += Element_border_MouseEnter;
            element_border.MouseLeave += Element_border_MouseLeave;

            element_border.MouseDown += Element_border_MouseDown;
            element_border.MouseMove += Element_border_MouseMove;
            element_border.MouseUp += Element_border_MouseUp;
        }


        internal ContainerData GetContainerData()
        {
            return containerData;
        }

        internal Point get_dragPoint()
        {
            return dragPoint;
        }

        internal bool get_containerIsSelected()
        {
            return container_is_selected;
        }

        internal Point get_Position()
        {
            return canvas.GetElementPosition(this);
        }

        private async void initialize_container()
        {
            await Task.Delay(2);

            if (containerData.GetElement() == null)
            {
                containerData.SetElement(new UserControl());
            }

            content_border.Child = null;

            content_border.Child = containerData.GetElement();

            build();
        }

        internal void load_Container()
        {
            initialize_container();
        }


        internal void deselect(bool remove_from_list = true)
        {
            container_is_selected = false;

            element_border.BorderBrush = containerData.settings.borderbrush.GetBrush();

            if (remove_from_list)
            {
                canvas.removeFromSelectedItems(this);
            }
        }

        internal void remove_Container()
        {
            new SharedLogic().GetElementHandler().removeElement(__CoreContainer);
            canvas.canvas.Children.Remove(__CoreContainer);

            content_border.Child = null;
        }

        internal void select()
        {
            container_is_selected = true;
            element_border.BorderBrush = containerData.settings.highlight.GetBrush();

            canvas.add_selected_item(this);
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
        }

        private void setColors()
        {
            //imageFilepath = "-";
            //imageIsBackground = false;

            //ContainerLogic.ApplyColorOnBorder(
            //    element_border,
            //    containerData.GetColorData().brushtype,
            //    containerData.GetColorData().brushOrientation,
            //    containerData.GetColorData().color1_string,
            //    containerData.GetColorData().color2_string,
            //    containerData.GetColorData().color3_string,
            //    containerData.GetColorData().color4_string
            //    );
        }

        internal void setContainerData(ContainerData containerData)
        {
            this.containerData = containerData;
        }

        internal void setPosition(Point point)
        {
            this.dragPoint = point;
        }

        private void Container_Loaded(object sender, RoutedEventArgs e)
        {

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
                    ref containerData.level,
                    ref dragPoint,
                    ref canvas
                    );

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
                    ref elementDrag,
                    ref __CoreContainer,
                    ref containerData.level
                    );

                dragPoint = new Point(Canvas.GetLeft(__CoreContainer), Canvas.GetTop(__CoreContainer));

                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    selected();
                }


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

                Canvas.SetZIndex(__CoreContainer, index + CoreCanvasSwitchData.Get_CORECANVAS_HOVER_LEVEL);


                //MessageBox.Show($"{Canvas.GetZIndex(this)}\n{containerData.level}\n{containerData.level + containerData.hoverLevel}");
            }

            e.Handled = true; 
        }

        private void Element_border_MouseLeave(object sender, MouseEventArgs e)
        {
            if (containerData != null)
            {
                Canvas.SetZIndex(__CoreContainer, containerData.level);
            }

            e.Handled = true;
        }
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