/* Aiding Elements User Interface
 *      CoreOptions element 
 * 
 * basic options element to configure properties
 * 
 * init:        2024|01|17
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.Auxiliaries;

using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.ComponentModel;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Runtime.ConstrainedExecution;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices.ObjectiveC;
using ComboBox = System.Windows.Controls.ComboBox;
using AidingElementsUserInterface.Core.MyNote_Data;

namespace AidingElementsUserInterface.Core.AEUI_UserControls
{
    internal class CoreOptions : CoreContainer
    {
        private Data_Handler handler; // move to SetupData, utilize xaml

        private StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Vertical };
        private WrapPanel wrapPanel = new WrapPanel() { Orientation = Orientation.Vertical };

        object data;

        private ComboBox CBX_dataTypeSelection = new ComboBox();


        #region CanvasData CVCs
        private CoreValueChange CVC_canvasName = new CoreValueChange("canvas name");
        private CoreValueChange CVC_element_spacing = new CoreValueChange("element spacing");
        private CoreValueChange CVC_grouping_displacement = new CoreValueChange("grouping displacement");
        private CoreValueChange CVC_dragLevel = new CoreValueChange("dragLevel");
        private CoreValueChange CVC_hoverLevel = new CoreValueChange("hoverLevel");
        private CoreValueChange CVC_z_level_MAX = new CoreValueChange("z level MAX");
        private CoreValueChange CVC_z_level_MIN = new CoreValueChange("z level MIN");
        #endregion CanvasData CVCs

        #region ContainerData CVCs
        private CoreValueChange CVC_ContainerDataFilename = new CoreValueChange("container filename");
        private CoreValueChange CVC_z_position = new CoreValueChange("level");
        #endregion ContainerData CVCs

        #region CoreData CVCs
        private CoreValueChange CVC_background = new CoreValueChange("background", true, true);
        private CoreValueChange CVC_borderbrush = new CoreValueChange("borderbrush", true, true);
        private CoreValueChange CVC_foreground = new CoreValueChange("foreground", true, true);
        private CoreValueChange CVC_highlight = new CoreValueChange("highlight", true, true);

        private CoreValueChange CVC_cornerRadius = new CoreValueChange("corner radius");
        private CoreValueChange CVC_thickness = new CoreValueChange("thickness");
        private CoreValueChange CVC_fontSize = new CoreValueChange("fontSize");
        private CoreValueChange CVC_fontFamily = new CoreValueChange("fontFamily");
        private CoreValueChange CVC_imageFilePath = new CoreValueChange("image", true);
        private CoreValueChange CVC_height = new CoreValueChange("height");
        private CoreValueChange CVC_width = new CoreValueChange("width");
        #endregion CoreData CVCs

        #region MainWindowData CVCs
        private CoreValueChange CVC_initialPosition = new CoreValueChange("initial position");
        private CoreValueChange CVC_language = new CoreValueChange("language");
        #endregion MainWindowData CVCs

        private CoreButton CB_saveChanges = new CoreButton("save changes");

        private enum AEUI_Data_Types
        {
            [Description("mainwindowdata.xml")]
            MainWindow,

            [Description("canvasdata.xml")]
            Canvas,

            [Description("buttondata.xml")]
            Button,

            [Description("labeldata.xml")]
            Label,

            [Description("textboxdata.xml")]
            TextBox,

            [Description("coredata.xml")]
            Core
            //,Paths
        }

        public CoreOptions()
        {
            InitializeComponent();

            build();
        }

        private void CONFIGURATION_CanvasData()
        {
            CanvasData canvasData = new SharedLogic().GetDataHandler().GetCanvasData();

            if (canvasData == null)
            {
                canvasData = new CanvasData();
            }

            wrapPanel.Children.Add(CVC_canvasName);
            wrapPanel.Children.Add(CVC_element_spacing);
            wrapPanel.Children.Add(CVC_grouping_displacement);
            wrapPanel.Children.Add(CVC_dragLevel);
            wrapPanel.Children.Add(CVC_hoverLevel);
            wrapPanel.Children.Add(CVC_z_level_MAX);
            wrapPanel.Children.Add(CVC_z_level_MIN);

            CVC_canvasName.setText(canvasData.canvasName.ToString());
            CVC_element_spacing.setText(canvasData.element_spacing.ToString());
            CVC_grouping_displacement.setText(canvasData.grouping_displacement.ToString());
            CVC_dragLevel.setText(canvasData.dragLevel.ToString());
            CVC_hoverLevel.setText(canvasData.hoverLevel.ToString());
            CVC_z_level_MAX.setText(canvasData.z_level_MAX.ToString());
            CVC_z_level_MIN.setText(canvasData.z_level_MIN.ToString());

            CONFIGURATION_CoreData(canvasData);

            data = canvasData;
        }

        private void CONFIGURATION_ContainerData() //???
        {
            ContainerData containerData = new ContainerData(new CoreData());

            wrapPanel.Children.Add(CVC_ContainerDataFilename);
            wrapPanel.Children.Add(CVC_z_position);

            CVC_ContainerDataFilename.setText(containerData.ContainerDataFilename);
            CVC_z_position.setText(containerData.z_position.ToString());

            CONFIGURATION_CoreData(containerData.settings);

            data = containerData;
        }

        private void CONFIGURATION_CoreData(CoreData _coreData = null)
        {
            CoreData coreData;

            if (_coreData == null)
            {
                coreData = new SharedLogic().GetDataHandler().GetCoreData();

                if (coreData == null)
                {
                    coreData = new CoreData();
                }
            }
            else
            {
                coreData = _coreData;
            }

            wrapPanel.Children.Add(CVC_background); // configure with buttons, each opens a color setup tool
            wrapPanel.Children.Add(CVC_borderbrush);
            wrapPanel.Children.Add(CVC_foreground);
            wrapPanel.Children.Add(CVC_highlight);

            wrapPanel.Children.Add(CVC_cornerRadius);
            wrapPanel.Children.Add(CVC_thickness);
            wrapPanel.Children.Add(CVC_fontSize);
            wrapPanel.Children.Add(CVC_fontFamily);
            wrapPanel.Children.Add(CVC_imageFilePath);
            wrapPanel.Children.Add(CVC_height);
            wrapPanel.Children.Add(CVC_width);

            CVC_background.setText("setup brush"); 
            CVC_background.coreButton.button.Background = coreData.background.GetBrush();
            CVC_background.setObject(coreData.background);

            CVC_borderbrush.setText("setup brush");
            CVC_borderbrush.coreButton.Background = coreData.borderbrush.GetBrush();
            CVC_borderbrush.setObject(coreData.borderbrush);

            CVC_foreground.setText("setup brush");
            CVC_foreground.coreButton.Background = coreData.foreground.GetBrush();
            CVC_foreground.setObject(coreData.foreground);

            CVC_highlight.setText("setup brush");
            CVC_highlight.coreButton.Background = coreData.highlight.GetBrush();
            CVC_highlight.setObject(coreData.highlight);

            CVC_cornerRadius.setText(coreData.cornerRadius.ToString());
            CVC_thickness.setText(coreData.thickness.ToString());
            CVC_fontSize.setText(coreData.fontSize.ToString());
            CVC_fontFamily.setText(coreData.fontFamily.ToString());
            CVC_imageFilePath.setText(coreData.imageFilePath.ToString());
            CVC_height.setText(coreData.height.ToString());
            CVC_width.setText(coreData.width.ToString());
        }

        private void CONFIGURATION_MainWindowData()
        {
            MainWindowData mainWindowData = new SharedLogic().GetDataHandler().GetMainWindowData();

            if (mainWindowData == null)
            {
                mainWindowData = new MainWindowData();
            }

            CONFIGURATION_CoreData(mainWindowData);

            wrapPanel.Children.Add(CVC_initialPosition);
            wrapPanel.Children.Add(CVC_language);

            CVC_initialPosition.setText(mainWindowData.initialPosition.ToString());
            CVC_language.setText(mainWindowData.language.ToString());

        }

        private void build()
        {
            CBX_dataTypeSelection.Items.Add(AEUI_Data_Types.MainWindow);
            CBX_dataTypeSelection.Items.Add(AEUI_Data_Types.Canvas);
            CBX_dataTypeSelection.Items.Add(AEUI_Data_Types.Button);
            CBX_dataTypeSelection.Items.Add(AEUI_Data_Types.Label);
            CBX_dataTypeSelection.Items.Add(AEUI_Data_Types.TextBox);
            CBX_dataTypeSelection.Items.Add(AEUI_Data_Types.Core);

            hideContainerNesting(this);

            handler = new SharedLogic().GetDataHandler();

            wrapPanel.Background = new SolidColorBrush(Colors.Transparent);

            stackPanel.Children.Add(CBX_dataTypeSelection);
            stackPanel.Children.Add(wrapPanel);
            stackPanel.Children.Add(CB_saveChanges);

            content_border.Child = stackPanel;

            content_border.Background = new SolidColorBrush(Colors.Transparent);
            content_border.BorderThickness = new Thickness(0);

            registerEvents();
        }

        private void registerEvents()
        {
            CB_saveChanges.button.Click += CB_saveChanges_Click;

            CBX_dataTypeSelection.SelectionChanged += CBX_dataTypeSelection_SelectionChanged;
        }

        private void selectionChange(object sender)
        {
            int value = ((ComboBox)sender).SelectedIndex;

            wrapPanel.Children.Clear();

            switch (value)
            {
                case (int)AEUI_Data_Types.MainWindow:
                    CONFIGURATION_MainWindowData();

                    break;

                case (int)AEUI_Data_Types.Canvas:
                    CONFIGURATION_CanvasData();
                    break;

                case (int)AEUI_Data_Types.Button:
                    CONFIGURATION_CoreData(new SharedLogic().GetDataHandler().GetButtonData());
                    break;

                case (int)AEUI_Data_Types.Label:
                    CONFIGURATION_CoreData(new SharedLogic().GetDataHandler().GetLabelData());
                    break;

                case (int)AEUI_Data_Types.TextBox:
                    CONFIGURATION_CoreData(new SharedLogic().GetDataHandler().GetTextBoxData());
                    break;

                case (int)AEUI_Data_Types.Core:     
                    CONFIGURATION_CoreData(new SharedLogic().GetDataHandler().GetCoreData());
                    break;

                default:
                    break;
            }
        }

        private void CBX_dataTypeSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectionChange(sender);
        }

        private CoreData ParseCoreDataCVCs()
        {
            CoreData coreData = new CoreData();

            coreData.background = (ColorData)CVC_background._Object;
            coreData.borderbrush = (ColorData)CVC_borderbrush._Object;
            coreData.foreground = (ColorData)CVC_foreground._Object;
            coreData.highlight = (ColorData)CVC_highlight._Object;

            string splitter = CVC_cornerRadius.value;
            string[] split = splitter.Split(',');

            double a, b, c, d;

            a = double.Parse(split[0]);
            b = double.Parse(split[1]);
            c = double.Parse(split[2]);
            d = double.Parse(split[3]);

            coreData.cornerRadius = new CornerRadius(a, b, c, d);

            splitter = CVC_thickness.value;
            split = splitter.Split(',');

            a = double.Parse(split[0]);
            b = double.Parse(split[1]);
            c = double.Parse(split[2]);
            d = double.Parse(split[3]);

            coreData.thickness = new Thickness(a, b, c, d);
            coreData.fontSize = int.Parse(CVC_fontSize.Value);
            coreData.fontFamily = new System.Windows.Media.FontFamily(CVC_fontFamily.Value);
            coreData.imageFilePath = CVC_imageFilePath.Value;
            coreData.height = double.Parse(CVC_height.Value);
            coreData.width = double.Parse(CVC_width.Value);

            return coreData;
        }

        private void Save_MainWindowData()
        {

        }

        private void Save_CanvasData()
        {

        }

        private void Save_ButtonData()
        {
            CoreData coreData = ParseCoreDataCVCs();

            Application.Current.Resources["ButtonData_background"] = coreData.background.GetBrush();
            Application.Current.Resources["ButtonData_borderbrush"] = coreData.borderbrush.GetBrush();
            Application.Current.Resources["ButtonData_foreground"] = coreData.foreground.GetBrush();
            Application.Current.Resources["ButtonData_highlight"] = coreData.highlight.GetBrush();
                                           
            Application.Current.Resources["ButtonData_cornerRadius"] = coreData.cornerRadius;
            Application.Current.Resources["ButtonData_thickness"] = coreData.thickness;
                                           
            Application.Current.Resources["ButtonData_fontSize"] = (double)coreData.fontSize;
            Application.Current.Resources["ButtonData_fontFamily"] = coreData.fontFamily;
                                           
            Application.Current.Resources["ButtonData_width"] = coreData.width;
            Application.Current.Resources["ButtonData_height"] = coreData.height;

            if (coreData.imageFilePath != null)
            {
                if (File.Exists(coreData.imageFilePath))
                {
                    Application.Current.Resources["ButtonData_image"] = new ImageBrush(new BitmapImage(new Uri(coreData.imageFilePath)));
                    Application.Current.Resources["ButtonData_background"] = Application.Current.Resources["ButtonData_image"];
                }
            }

            handler.SetButtonData(coreData);
        }
        private void Save_LabelData()
        {
            CoreData coreData = ParseCoreDataCVCs();

            Application.Current.Resources["LabelData_background"] = coreData.background.GetBrush();
            Application.Current.Resources["LabelData_borderbrush"] = coreData.borderbrush.GetBrush();
            Application.Current.Resources["LabelData_foreground"] = coreData.foreground.GetBrush();
            Application.Current.Resources["LabelData_highlight"] = coreData.highlight.GetBrush();
                                           
            Application.Current.Resources["LabelData_cornerRadius"] = coreData.cornerRadius;
            Application.Current.Resources["LabelData_thickness"] = coreData.thickness;
                                           
            Application.Current.Resources["LabelData_fontSize"] = (double)coreData.fontSize;
            Application.Current.Resources["LabelData_fontFamily"] = coreData.fontFamily;
                                           
            Application.Current.Resources["LabelData_width"] = coreData.width;
            Application.Current.Resources["LabelData_height"] = coreData.height;

            if (coreData.imageFilePath != null)
            {
                if (File.Exists(coreData.imageFilePath))
                {
                    Application.Current.Resources["LabelData_image"] = new ImageBrush(new BitmapImage(new Uri(coreData.imageFilePath)));
                    Application.Current.Resources["LabelData_background"] = Application.Current.Resources["LabelData_image"];
                }
            }

            handler.SetLabelData(coreData);
        }
        private void Save_TextBoxData()
        {
            CoreData coreData = ParseCoreDataCVCs();

            Application.Current.Resources["TextBoxData_background"] = coreData.background.GetBrush();
            Application.Current.Resources["TextBoxData_borderbrush"] = coreData.borderbrush.GetBrush();
            Application.Current.Resources["TextBoxData_foreground"] = coreData.foreground.GetBrush();
            Application.Current.Resources["TextBoxData_highlight"] = coreData.highlight.GetBrush();
                                           
            Application.Current.Resources["TextBoxData_cornerRadius"] = coreData.cornerRadius;
            Application.Current.Resources["TextBoxData_thickness"] = coreData.thickness;
                                           
            Application.Current.Resources["TextBoxData_fontSize"] = (double)coreData.fontSize;
            Application.Current.Resources["TextBoxData_fontFamily"] = coreData.fontFamily;
                                           
            Application.Current.Resources["TextBoxData_width"] = coreData.width;
            Application.Current.Resources["TextBoxData_height"] = coreData.height;

            if (coreData.imageFilePath != null)
            {
                if (File.Exists(coreData.imageFilePath))
                {
                    Application.Current.Resources["TextBoxData_image"] = new ImageBrush(new BitmapImage(new Uri(coreData.imageFilePath)));
                    Application.Current.Resources["TextBoxData_background"] = Application.Current.Resources["TextBoxData_image"];
                }
            }

            handler.SetTextBoxData(coreData);
        }
        private void Save_CoreData()
        {
            CoreData coreData = ParseCoreDataCVCs();

            Application.Current.Resources["CoreData_background"] = coreData.background.GetBrush();
            Application.Current.Resources["CoreData_borderbrush"] = coreData.borderbrush.GetBrush();
            Application.Current.Resources["CoreData_foreground"] = coreData.foreground.GetBrush();
            Application.Current.Resources["CoreData_highlight"] = coreData.highlight.GetBrush();
                                           
            Application.Current.Resources["CoreData_cornerRadius"] = coreData.cornerRadius;
            Application.Current.Resources["CoreData_thickness"] = coreData.thickness;
                                           
            Application.Current.Resources["CoreData_fontSize"] = (double)coreData.fontSize;
            Application.Current.Resources["CoreData_fontFamily"] = coreData.fontFamily;
                                           
            Application.Current.Resources["CoreData_width"] = coreData.width;
            Application.Current.Resources["CoreData_height"] = coreData.height;

            if (coreData.imageFilePath != null)
            {
                if (File.Exists(coreData.imageFilePath))
                {
                    Application.Current.Resources["CoreData_image"] = new ImageBrush(new BitmapImage(new Uri(coreData.imageFilePath)));
                    Application.Current.Resources["CoreData_background"] = Application.Current.Resources["CoreData_image"];
                }
            }

            handler.SetCoreData(coreData);
        }


        private void CB_saveChanges_Click(object sender, RoutedEventArgs e)
        {
            switch (CBX_dataTypeSelection.SelectedIndex)
            {
                case (int)AEUI_Data_Types.MainWindow:
                    Save_MainWindowData();

                    break;

                case (int)AEUI_Data_Types.Canvas:
                    Save_CanvasData();
                    break;

                case (int)AEUI_Data_Types.Button:
                    Save_ButtonData();
                    break;

                case (int)AEUI_Data_Types.Label:
                    Save_LabelData();
                    break;

                case (int)AEUI_Data_Types.TextBox:
                    Save_TextBoxData();
                    break;

                case (int)AEUI_Data_Types.Core:
                    Save_CoreData();
                    break;

                default:
                    break;
            }



            //ideas:
            //make new function, build in type check, either here, ore in CoreValueChange by passing in
            // an int type, check string using this function, and using get..()


        }
    }
}
/*  END OF FILE
 * 
 * 
 */