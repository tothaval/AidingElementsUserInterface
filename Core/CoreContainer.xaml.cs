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
using System.DirectoryServices.ActiveDirectory;
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
    /// Interaktionslogik für CoreContainer.xaml
    /// </summary>
    public partial class CoreContainer : UserControl
    {
        private CoreButton leftExpanderButton = new CoreButton(true);
        private CoreButton rightExpanderButton = new CoreButton(true);
        public CorePanel rightExpander;
        public bool rightExpanderLoadValue = true;

        private CoreCanvas canvas;
        private ContainerData containerData;

        private Point dragPoint = new Point();
        private bool elementDrag = false;

        #region constructors
        public CoreContainer(UserControl element, CoreCanvas canvas)
        {
            containerData = new ContainerData(element);

            this.canvas = canvas;

            InitializeComponent();

            initialize_container();

        }

        private void _loadtimer_Tick(object sender, EventArgs e)
        {
            //loadTickCounter++;
            //
            //if (loadTickCounter == 1 || loadTickCounter < 2)
            //{
            //    rightExpander = new CorePanel( CORE_ContainerElement);
            //    rightExpander.createElementOptionsPanel();
            //    rightExpander.DVIB_amount_and_thickness.Visibility = Visibility.Collapsed;
            //    rightExpander.element_wrapper.Orientation = Orientation.Vertical;
            //
            //    if (rightExpander != null)
            //    {
            //        rightExpander.ui_eventHandlingChoice.IsChecked = rightExpanderLoadValue;
            //    }
            //    alterFontValuesOnContainerElement();
            //
            //    alterAppearance();
            //
            //    _loadtimer.Stop();
            //}
        }
        #endregion constructors

        private void build()
        {
            __CoreContainer.FontSize = containerData.fontSize;
            __CoreContainer.FontFamily = containerData.fontFamiliy;

            rightExpander = new CorePanel();
            rightExpander.createElementOptionsPanel();
            rightExpander.wrapper.Orientation = Orientation.Vertical;

            // element arrangement
            #region element arrangement
            ui_border_ExpansionArea_right.Visibility = Visibility.Collapsed;
            ui_border_ExpansionArea_right.Background = new SolidColorBrush(Colors.Transparent);
            ui_border_ExpansionArea_right.BorderBrush = new SolidColorBrush(Colors.Transparent);

            ui_border_rightExpander.Margin = new Thickness(7, 0, 7, 0);

            ui_border_ExpansionArea_right.Child = rightExpander;
            #endregion element arrangement

            Background = new SolidColorBrush(Colors.Transparent);
            Foreground = new SolidColorBrush(containerData.foreground);

            element_border.CornerRadius = containerData.cornerRadius;
            element_border.BorderThickness = containerData.thickness;

            element_border.Background = new SolidColorBrush(containerData.background);
            element_border.BorderBrush = new SolidColorBrush(containerData.borderbrush);

            rightExpander.ui_eventHandlingChoice.Background = new SolidColorBrush(containerData.foreground);

            configure_CORE_ContainerElement();
        }


        private void configure_CORE_ContainerElement()
        {
            element_border.MouseLeftButtonDown += Element_border_MouseLeftButtonDown;

            //content_border.MouseDown += ui_border_ElementContainer_MouseDown;
            //content_border.MouseMove += ui_border_ElementContainer_MouseMove;
            //content_border.MouseUp += ui_border_ElementContainer_MouseUp;

            element_border.MouseDown += Element_border_MouseDown;
            element_border.MouseMove += Element_border_MouseMove;
            element_border.MouseUp += Element_border_MouseUp;


            //rightExpanderButton.ui_button.Click += Ui_button_rightExpander_Click;

            ui_border_rightExpander.Child = rightExpanderButton;

            leftExpanderButton._expander(true);
            rightExpanderButton._expander(true);

            //if (config.showPanels && !invisibleContainer)
            //{
            //    showPanelExpanders();
            //}
            //else
            //{
            //    hideSidePanels();
            //}
            //
            //if (config.showTooltips)
            //{
            //    showTooltips();
            //}
            //else
            //{
            //    hideTooltips();
            //}
            //
            //if (config.containerImageFilePath.Length > 4)
            //{
            //    element_border.Background = YS_CLASS_ConfigData.Return_ImageBrush(config.containerImageFilePath);
            //}
        }

        internal ContainerData GetContainerData()
        {
            return containerData;
        }

        private void initialize_container()
        {
            if (containerData.getContent() == null)
            {
                containerData.setContent(new UserControl());
            }

            content_border.Child = containerData.getContent();

            build();
        }

        //public void reloadConfig()
        //{
        //    config = new YS_CLASS_ConfigData();
        //
        //    CORE_ContainerElement.FontFamily = config.font;
        //
        //    dragLevel = config.dragLevel;
        //    element_border.CornerRadius = new CornerRadius(config.borderRadius);
        //    element_border.BorderThickness = new Thickness(config.borderThickness);
        //
        //    color1_string = config.backColor.ToString();
        //    color2_string = config.foreColor.ToString();
        //
        //    setColors();
        //
        //    finishSetup();
        //}

        private void setColors()
        {
            //imageFilepath = "-";
            //imageIsBackground = false;

            ContainerLogic.ApplyColorOnBorder(
                ref element_border,
                ref containerData.brushType,
                ref containerData.brushOrientation,
                ref containerData.color1_string,
                ref containerData.color2_string,
                ref containerData.color3_string,
                ref containerData.color4_string
                );
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
                    containerData.z_position,
                    containerData.dragLevel,
                    ref dragPoint,
                    ref canvas
                    );

                //mainWindow.writing = false;
                ////Keyboard.ClearFocus();
                //Keyboard.Focus(mainWindow.focusTarget);

                e.Handled = true;
            }


            if (e.ChangedButton == MouseButton.Right)
            {
                canvas.canvas.Children.Remove(__CoreContainer);

                e.Handled = true;
            }
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
                    containerData.z_position
                    );

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


        private void Element_border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        #endregion element mouse events
    }
}
