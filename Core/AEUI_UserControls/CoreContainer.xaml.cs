/* Aiding Elements User Interface
 *      CoreContainer element 
 * 
 * basic frame for content elements
 * 
 * init:        2023|11|27
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
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
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.AEUI_HelperClasses;
using AidingElementsUserInterface.Core.Auxiliaries;

namespace AidingElementsUserInterface.Core.AEUI_UserControls
{
    /// <summary>
    /// Interaktionslogik für CoreContainer.xaml
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
            InitializeComponent();
        }

        internal CoreContainer(ContainerData containerData)
        {
            this.containerData = containerData;

            InitializeComponent();
        }

        internal CoreContainer(UserControl element, CoreCanvas canvas)
        {
            InitializeComponent();

            this.canvas = canvas;

            containerData = new ContainerData(new SharedLogic().GetDataHandler().LoadCoreData(), element);

            initialize_container();
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

        private void build()
        {
            __CoreContainer.FontSize = containerData.fontSize;
            __CoreContainer.FontFamily = containerData.fontFamily;

            Column_left.Width = containerData.element_spacing;
            Column_right.Width = containerData.element_spacing;

            Row_top.Height = containerData.element_spacing;
            Row_bottom.Height = containerData.element_spacing;

            Background = new SolidColorBrush(Colors.Transparent);
            Foreground = new SolidColorBrush(containerData.foreground);

            element_border.CornerRadius = containerData.cornerRadius;
            element_border.BorderThickness = containerData.thickness;

            element_border.Background = new SolidColorBrush(containerData.background);
            element_border.BorderBrush = new SolidColorBrush(containerData.borderbrush);

            configure_CORE_ContainerElement();
        }


        private void configure_CORE_ContainerElement()
        {
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

            if (containerData.getContent() == null)
            {
                containerData.setContent(new UserControl());
            }

            content_border.Child = null;

            content_border.Child = containerData.getContent();

            build();
        }

        internal void load_Container()
        {
            initialize_container();
        }

        internal void deselect(bool remove_from_list = true)
        {
            container_is_selected = false;

            element_border.BorderBrush = new SolidColorBrush(containerData.borderbrush);

            if (remove_from_list)
            {
                canvas.removeSelectedItem(this);
            }
        }

        internal void remove_Container()
        {
            new SharedLogic().GetElementHandler().removeElement((CoreContainer)__CoreContainer);
            canvas.canvas.Children.Remove(__CoreContainer);

            content_border.Child = null;
        }

        internal void select()
        {
            container_is_selected = true;
            element_border.BorderBrush = new SolidColorBrush(containerData.highlight);

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

        internal void setCanvas(CoreCanvas coreCanvas)
        {
            this.canvas = coreCanvas;
        }

        private void setColors()
        {
            //imageFilepath = "-";
            //imageIsBackground = false;

            ContainerLogic.ApplyColorOnBorder(
                element_border,
                containerData.GetColorData().brushtype,
                containerData.GetColorData().brushOrientation,
                containerData.GetColorData().color1_string,
                containerData.GetColorData().color2_string,
                containerData.GetColorData().color3_string,
                containerData.GetColorData().color4_string
                );
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
                    __CoreContainer,
                    ref containerData.z_position,
                    ref containerData.dragLevel,
                    ref dragPoint,
                    ref canvas
                    );

                //mainWindow.writing = false;
                ////Keyboard.ClearFocus();
                Keyboard.Focus(new SharedLogic().GetMainWindow().focusTarget);

                e.Handled = true;
            }


            if (e.ChangedButton == MouseButton.Right)
            {
                remove_Container();

                e.Handled = true;
            }
        }

        private void Element_border_MouseMove(object sender, MouseEventArgs e)
        {
            ContainerLogic.DragMove(
                ref elementDrag,
                (CoreContainer)__CoreContainer,
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
                    (CoreContainer)__CoreContainer,
                    ref containerData.z_position
                    );

                dragPoint = new Point(Canvas.GetLeft(this), Canvas.GetTop(this));

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