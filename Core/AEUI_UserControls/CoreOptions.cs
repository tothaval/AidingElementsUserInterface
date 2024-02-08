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
using AidingElementsUserInterface.Core.AEUI_Logic;

namespace AidingElementsUserInterface.Core.AEUI_UserControls
{
    // noch ein grid einbauen, um dort die BrushSetup usercontrols aufzurufen, damit trennung vom element klar ist,
    // im element  noch buttons und eine combobox einbauen, um datentypen gezielt verändern zu können.

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
        private CoreValueChange CVC_height = new CoreValueChange("height");
        private CoreValueChange CVC_width = new CoreValueChange("width");
        #endregion CoreData CVCs

        #region MainWindowData CVCs
        private CoreValueChange CVC_initialPosition = new CoreValueChange("initial position");
        private CoreValueChange CVC_language = new CoreValueChange("language");
        #endregion MainWindowData CVCs

        private CoreButton CB_changeSelection = new CoreButton("change selection");
        private CoreButton CB_saveChanges = new CoreButton("save changes");

        private enum AEUI_Data_Types
        {
            [Description("mainwindowdata.xml")]
            MainWindow,

            [Description("canvasdata.xml")]
            Canvas,

            [Description("containerdata.xml")]
            Container,

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

        private void ApplySelectionChange()
        {
            if (CBX_dataTypeSelection.SelectedIndex == (int)AEUI_Data_Types.Container)
            {
                if (new SharedLogic().GetMainWindow().Get_SYSTEM_ACTIVE_FLAG)
                {
                    new SharedLogic().GetMainWindow().Get_SYTEM_CANVAS.Get_SYSTEM_CANVAS.ChangeSelectionData(ParseContainerDataCVCs());
                }
                else
                {
                    if (new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS != null)
                    {
                        new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS.ChangeSelectionData(ParseContainerDataCVCs());
                    }
                }
            }
        }


        private void CONFIGURATION_CanvasData()
        {
            CanvasData canvasData;

            if (new SharedLogic().GetMainWindow().Get_SYSTEM_ACTIVE_FLAG)
            {
                canvasData = new SharedLogic().GetMainWindow().Get_SYTEM_CANVAS.Get_SYSTEM_CANVAS.getCanvasData();
            }
            else
            {
                canvasData = new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS.getCanvasData();
            }

            if (canvasData == null)
            {
                if (GetContainerData().CanvasID < 1)
                {
                    canvasData = new CanvasData($"userscreen_1", 1);
                }
            }

            wrapPanel.Children.Add(CVC_canvasName);
            wrapPanel.Children.Add(CVC_element_spacing);
            wrapPanel.Children.Add(CVC_grouping_displacement);

            CVC_canvasName.setText(canvasData.canvasName.ToString());
            CVC_element_spacing.setText(canvasData.element_spacing.ToString());
            CVC_grouping_displacement.setText(canvasData.grouping_displacement.ToString());

            CONFIGURATION_CoreData(canvasData);

            data = canvasData;
        }

        private void CONFIGURATION_ContainerData() //???
        {
            ContainerData containerData = new ContainerData(new CoreData(), GetContainerData().CanvasID);

            wrapPanel.Children.Add(CVC_ContainerDataFilename);
            wrapPanel.Children.Add(CVC_z_position);

            CVC_ContainerDataFilename.setText(containerData.ContainerDataFilename);
            CVC_z_position.setText(containerData.level.ToString());

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
            CBX_dataTypeSelection.Items.Add(AEUI_Data_Types.Container);
            CBX_dataTypeSelection.Items.Add(AEUI_Data_Types.Button);
            CBX_dataTypeSelection.Items.Add(AEUI_Data_Types.Label);
            CBX_dataTypeSelection.Items.Add(AEUI_Data_Types.TextBox);
            CBX_dataTypeSelection.Items.Add(AEUI_Data_Types.Core);

            hideContainerNesting(this);

            handler = new SharedLogic().GetDataHandler();

            wrapPanel.Background = new SolidColorBrush(Colors.Transparent);

            stackPanel.Children.Add(CBX_dataTypeSelection);
            stackPanel.Children.Add(wrapPanel);

            #region buttonPanel
            StackPanel buttonPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
            };

            buttonPanel.Children.Add(CB_saveChanges);
            buttonPanel.Children.Add(CB_changeSelection);
            #endregion buttonPanel

            stackPanel.Children.Add(buttonPanel);

            content_border.Child = stackPanel;

            content_border.Background = new SolidColorBrush(Colors.Transparent);
            content_border.BorderThickness = new Thickness(0);

            registerEvents();
        }

        private void registerEvents()
        {
            CB_changeSelection.button.Click += CB_changeSelection_Click;
            CB_saveChanges.button.Click += CB_saveChanges_Click;

            CBX_dataTypeSelection.SelectionChanged += CBX_dataTypeSelection_SelectionChanged;

            CVC_background.coreButton.button.Click += CVC_background_Click;
            CVC_borderbrush.coreButton.button.Click += CVC_borderbrush_Click;
            CVC_foreground.coreButton.button.Click += CVC_foreground_Click;
            CVC_highlight.coreButton.button.Click += CVC_highlight_Click;


        }

        private void CVC_background_Click(object sender, RoutedEventArgs e)
        {
            if (options_border.Child == null)
            {
                options_border.Child = new BrushSetup(ref CVC_background);
            }
            else
            {
                if (options_border.Child.GetType() == typeof(BrushSetup))
                {
                    BrushSetup setup = options_border.Child as BrushSetup;

                    if (setup.callerCVC == CVC_background)
                    {
                        options_border.Child = null;
                    }
                    else
                    {
                        options_border.Child = new BrushSetup(ref CVC_background);
                    }
                }
            }
        }
        private void CVC_borderbrush_Click(object sender, RoutedEventArgs e)
        {
            if (options_border.Child == null)
            {
                options_border.Child = new BrushSetup(ref CVC_borderbrush);
            }
            else
            {
                if (options_border.Child.GetType() == typeof(BrushSetup))
                {
                    BrushSetup setup = options_border.Child as BrushSetup;

                    if (setup.callerCVC == CVC_borderbrush)
                    {
                        options_border.Child = null;
                    }
                    else
                    {
                        options_border.Child = new BrushSetup(ref CVC_borderbrush);
                    }
                }
            }
        }

        private void CVC_foreground_Click(object sender, RoutedEventArgs e)
        {
            if (options_border.Child == null)
            {
                options_border.Child = new BrushSetup(ref CVC_foreground);
            }
            else
            {
                if (options_border.Child.GetType() == typeof(BrushSetup))
                {
                    BrushSetup setup = options_border.Child as BrushSetup;

                    if (setup.callerCVC == CVC_foreground)
                    {
                        options_border.Child = null;
                    }
                    else
                    {
                        options_border.Child = new BrushSetup(ref CVC_foreground);
                    }
                }
            }
        }
        private void CVC_highlight_Click(object sender, RoutedEventArgs e)
        {
            if (options_border.Child == null)
            {
                options_border.Child = new BrushSetup(ref CVC_highlight);
            }
            else
            {
                if (options_border.Child.GetType() == typeof(BrushSetup))
                {
                    BrushSetup setup = options_border.Child as BrushSetup;

                    if (setup.callerCVC == CVC_highlight)
                    {
                        options_border.Child = null;
                    }
                    else
                    {
                        options_border.Child = new BrushSetup(ref CVC_highlight);
                    }
                }
            }
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

                case (int)AEUI_Data_Types.Container:
                    CONFIGURATION_ContainerData();
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

        private CanvasData ParseCanvasDataCVCs(CanvasData canvasData)
        {
            canvasData.canvasName = CVC_canvasName.value;
            canvasData.grouping_displacement = Int32.Parse(CVC_grouping_displacement.value);
            canvasData.element_spacing = new GridLength(Int32.Parse(CVC_element_spacing.value));

            return canvasData;
        }

        private ContainerData ParseContainerDataCVCs()
        {
            ContainerData containerData = new ContainerData(ParseCoreDataCVCs(), GetContainerData().CanvasID);

            containerData.ContainerDataFilename = CVC_ContainerDataFilename.value;
            containerData.level = int.Parse(CVC_z_position.Value);

            return containerData;
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
            coreData.height = double.Parse(CVC_height.Value);
            coreData.width = double.Parse(CVC_width.Value);

            return coreData;
        }

        private void Save_MainWindowData()
        {
            MainWindowData mainWindowData = new MainWindowData(ParseCoreDataCVCs());

            mainWindowData.initialPosition = new SharedLogic().ParsePoint(CVC_initialPosition.value);
            mainWindowData.language = CVC_language.value;

            Application.Current.MainWindow.Resources["MainWindowData_background"] = mainWindowData.background.GetBrush();
            Application.Current.MainWindow.Resources["MainWindowData_borderbrush"] = mainWindowData.borderbrush.GetBrush();
            Application.Current.MainWindow.Resources["MainWindowData_foreground"] = mainWindowData.foreground.GetBrush();
            Application.Current.MainWindow.Resources["MainWindowData_highlight"] = mainWindowData.highlight.GetBrush();

            Application.Current.MainWindow.Resources["MainWindowData_cornerRadius"] = mainWindowData.cornerRadius;
            Application.Current.MainWindow.Resources["MainWindowData_thickness"] = mainWindowData.thickness;

            Application.Current.MainWindow.Resources["MainWindowData_fontSize"] = (double)mainWindowData.fontSize;
            Application.Current.MainWindow.Resources["MainWindowData_fontFamily"] = mainWindowData.fontFamily;

            Application.Current.MainWindow.Resources["MainWindowData_width"] = mainWindowData.width;
            Application.Current.MainWindow.Resources["MainWindowData_height"] = mainWindowData.height;

            if (File.Exists(mainWindowData.background.brushpath))
            {
                Application.Current.MainWindow.Resources["MainWindowData_image"] = mainWindowData.background.GetBrush();
                Application.Current.MainWindow.Resources["MainWindowData_background"] = mainWindowData.background.GetBrush();                
            }

            new SharedLogic().GetMainWindow().border.Background = mainWindowData.background.GetBrush();

            handler.SetMainWindowData(mainWindowData);
        }

        private void Save_CanvasData()
        {
            CanvasData canvasData = new CanvasData(ParseCoreDataCVCs());
            SharedLogic logic = new SharedLogic();

            if (logic.GetMainWindow().Get_SYSTEM_ACTIVE_FLAG)
            {
                CoreCanvas coreCanvas = logic.GetMainWindow().Get_SYTEM_CANVAS.Get_SYSTEM_CANVAS;

                canvasData.canvasID = coreCanvas.getCanvasData().canvasID;
                canvasData = ParseCanvasDataCVCs(canvasData);

                coreCanvas.CanvasDataResources(canvasData);
                coreCanvas.setCanvasData(canvasData);
            }
            else
            {
                CoreCanvas coreCanvas = logic.GetMainWindow().Get_ACTIVE_CANVAS;

                canvasData.canvasID = coreCanvas.getCanvasData().canvasID;
                canvasData = ParseCanvasDataCVCs(canvasData);

                coreCanvas.CanvasDataResources(canvasData);
                coreCanvas.setCanvasData(canvasData);
            }
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


            if (File.Exists(coreData.background.brushpath))
            {
                Application.Current.Resources["ButtonData_image"] = coreData.background.GetBrush();
                Application.Current.Resources["ButtonData_background"] = coreData.background.GetBrush();
            }


            handler.SetButtonData(coreData);
        }

        private void Save_ContainerData()
        {
            ContainerData containerData = new ContainerData(ParseCoreDataCVCs(), GetContainerData().CanvasID);

            handler.SetContainerData(containerData);
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


            if (File.Exists(coreData.background.brushpath))
            {
                Application.Current.Resources["LabelData_image"] = coreData.background.GetBrush();
                Application.Current.Resources["LabelData_background"] = coreData.background.GetBrush();
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

            if (File.Exists(coreData.background.brushpath))
            {
                Application.Current.Resources["TextBoxData_image"] = coreData.background.GetBrush();
                Application.Current.Resources["TextBoxData_background"] = coreData.background.GetBrush();
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


            if (File.Exists(coreData.background.brushpath))
            {
                Application.Current.Resources["CoreData_image"] = coreData.background.GetBrush();
                Application.Current.Resources["CoreData_background"] = coreData.background.GetBrush();
            }


            handler.SetCoreData(coreData);
        }

        private void CB_changeSelection_Click(object sender, RoutedEventArgs e)
        {
            ApplySelectionChange();
        }


        // update to compensate for the recent changes to data and storgare logic
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

                case (int)AEUI_Data_Types.Container:
                    Save_ContainerData();
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
        private void CBX_dataTypeSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectionChange(sender);
        }
    }
}
/*  END OF FILE
 * 
 * 
 */