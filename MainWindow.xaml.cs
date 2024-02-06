/* Aiding Elements User Interface
 *      MainWindow 
 * 
 * main frame for the application
 * 
 * init:        2023|11|27
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 * 
 * 
 * AEUI dev goals:
 * 
 * 
 *  mind fuck stuff: probably my keyboard, but left alt can only be executed via ctrl+ left alt
 * 
 *  to do
 *      LevelShift
 *      LevelData
 *      
 *      further develop and upgrade Canvas save and load functionality, to accompany for the recent changes
 *      
 *      further work on concept stack and pop items from the stack
 */
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

using Point = System.Windows.Point;

using AidingElementsUserInterface.Core;
using AidingElementsUserInterface.Core.Auxiliaries;
using AidingElementsUserInterface.Elements.MyNote;
using AidingElementsUserInterface.Elements;
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.AEUI_UserControls;
using System.Windows.Media.Imaging;
using System;
using System.IO;
using System.Windows.Interop;
using AidingElementsUserInterface.Core.AEUI_Logic;
using System.Windows.Controls;
using AidingElementsUserInterface.Core.AEUI_SystemControls;
using System.Xml.Serialization;
using System.Windows.Controls.Primitives;

namespace AidingElementsUserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CoreCanvas ACTIVE_CANVAS;
        private bool SYSTEM_ACTIVE_FLAG = false;

        internal MainWindowData mainWindowData;

        internal Data_Handler data_Handler = new Data_Handler();

        internal SharedLogic logic = new SharedLogic();

        private LevelData SYSTEM_LEVEL = new LevelData(0, "SYSTEM LEVEL", true, true); // 'system canvas'  'level system level'

        internal CoreCanvas Get_ACTIVE_CANVAS => ACTIVE_CANVAS;

        internal SystemCanvas Get_SYTEM_CANVAS => SYSTEM_CANVAS;

        internal bool Get_SYSTEM_ACTIVE_FLAG => SYSTEM_ACTIVE_FLAG;

        internal void set_ACTIVE_CANVAS(CoreCanvas activeCanvas)
        {
            this.ACTIVE_CANVAS = activeCanvas;
            ACTIVE_CANVAS._LevelBar.Visibility = Visibility.Collapsed;
        }


        public MainWindow()
        {
            InitializeComponent();       

            build();
        }


        private void build()
        {
            load_MainWindowData();

            MainWindowResources();

            if (ACTIVE_CANVAS != null)
            {
                ACTIVE_CANVAS._LevelBar.Visibility = Visibility.Collapsed;
            }
        }


        // processing
        #region processing
        private void add_MyNote()
        {
            if (MI_MyNote.IsChecked)
            {
                CORE_CANVAS_SWITCH.Get_ACTIVE_CANVAS().GetCentral().ExecuteCommandRequest($">MyNote");
            }
            else
            {
                CORE_CANVAS_SWITCH.Get_ACTIVE_CANVAS().RemoveMyNote();
            }
        }

        private void add_FlatShareCC()
        {
            // disabled until fixed
            //if (MI_FlatShareCC.IsChecked)
            //{
            //ACTIVE_CANVAS.GetCentral().ExecuteCommandRequest($">FlatShareCC");
            //}
            //else
            //{
            //    ACTIVE_CANVAS.RemoveFlatShareCC();
            //}
        }

        private void load_MainWindowData()
        {
            mainWindowData = data_Handler.LoadMainWindowData();

            if (mainWindowData == null)
            {
                mainWindowData = new MainWindowData();
            }

            data_Handler.AddMainWindowData(mainWindowData);
        }

        private void MainWindowResources()
        {
            MainWindowData mainWindowData = this.mainWindowData;

            if (mainWindowData == null)
            {
                mainWindowData = new MainWindowData();
            }

            __MainWindow.Resources["MainWindow_background"] = mainWindowData.background.GetBrush();
            __MainWindow.Resources["MainWindow_borderbrush"] = mainWindowData.borderbrush.GetBrush();
            __MainWindow.Resources["MainWindow_foreground"] = mainWindowData.foreground.GetBrush();
            __MainWindow.Resources["MainWindow_highlight"] = mainWindowData.highlight.GetBrush();

            __MainWindow.Resources["MainWindow_cornerRadius"] = mainWindowData.cornerRadius;
            __MainWindow.Resources["MainWindow_thickness"] = mainWindowData.thickness;

            __MainWindow.Resources["MainWindow_fontSize"] = (double)mainWindowData.fontSize;
            __MainWindow.Resources["MainWindow_fontFamily"] = mainWindowData.fontFamily;

            __MainWindow.Resources["MainWindow_width"] = mainWindowData.width;
            __MainWindow.Resources["MainWindow_height"] = mainWindowData.height;

            __MainWindow.Resources["MainWindow_initialPosition_X"] = mainWindowData.initialPosition.X;
            __MainWindow.Resources["MainWindow_initialPosition_Y"] = mainWindowData.initialPosition.Y;

            __MainWindow.Resources["MainWindow_language"] = mainWindowData.language;

            if (File.Exists(mainWindowData.imageFilePath))
            {
                __MainWindow.Resources["MainWindow_image"] = new ImageBrush(new BitmapImage(new Uri(mainWindowData.imageFilePath)));
                __MainWindow.Resources["MainWindow_background"] = __MainWindow.Resources["MainWindow_image"];
            }
        }

        public void quitAEUI()
        {
            SYSTEM_CANVAS.save();

            if (mainWindowData != null)
            {
                mainWindowData.initialPosition = new Point(this.Left, this.Top);
            }

            XML_Handler xml_handler = new XML_Handler();

            xml_handler.ButtonData_save();
            xml_handler.CoreData_save();
            xml_handler.MainWindowData_save();
            xml_handler.TextBoxData_save();

            CORE_CANVAS_SWITCH.save_Screens();

            Application.Current.Shutdown();
        }


        private void SYSTEM_CANVAS_SWITCH()
        {
            if (border_SYSTEM_CANVAS.Visibility == Visibility.Collapsed)
            {
                border_CORE_CANVAS_SWITCH.Visibility = Visibility.Collapsed;
                border_SYSTEM_CANVAS.Visibility = Visibility.Visible;

                SYSTEM_ACTIVE_FLAG = true;
            }
            else
            {
                border_CORE_CANVAS_SWITCH.Visibility = Visibility.Visible;
                border_SYSTEM_CANVAS.Visibility = Visibility.Collapsed;

                ACTIVE_CANVAS = CORE_CANVAS_SWITCH.Get_ACTIVE_CANVAS();
                SYSTEM_ACTIVE_FLAG = false;
            }
        }

        #endregion processing

        private void summonElement(object sender)
        {
            if (SYSTEM_ACTIVE_FLAG)
            {
                SYSTEM_CANVAS.Get_SYSTEM_CANVAS.GetCentral().ExecuteCommandRequest($">{((MenuItem)sender).Header}");
            }
            else
            {
                if (ACTIVE_CANVAS != null)
                {
                    ACTIVE_CANVAS.GetCentral().ExecuteCommandRequest($">{((MenuItem)sender).Header}");
                }
            }

        }


        // events
        #region events
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            quitAEUI();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.LeftAlt || e.Key == Key.RightAlt)
            {
                if (ACTIVE_CANVAS._LevelBar.Visibility == Visibility.Collapsed)
                {
                    ACTIVE_CANVAS._LevelBar.Visibility = Visibility.Visible;
                }
                else
                {
                    ACTIVE_CANVAS._LevelBar.Visibility = Visibility.Collapsed;
                }
            }


            if (e.Key == Key.F1)
            {
                if (!MI_AEUI_CTRL.IsSubmenuOpen)
                {
                    MI_AEUI_CTRL.IsSubmenuOpen = true;
                }
                else
                {
                    MI_AEUI_CTRL.IsSubmenuOpen = false;
                }
            }

            if (e.Key == Key.F2)
            {
                if (!MI_SelectionControl.IsSubmenuOpen)
                {
                    MI_SelectionControl.IsSubmenuOpen = true;
                }
                else
                {
                    MI_SelectionControl.IsSubmenuOpen = false;
                }
            }

            if (e.Key == Key.F3)
            {
                if (!MI_elements.IsSubmenuOpen)
                {
                    MI_elements.IsSubmenuOpen = true;
                }
                else
                {
                    MI_elements.IsSubmenuOpen = false;
                }
            }

            if (e.Key == Key.F4)
            {
                if (!MI_tools.IsSubmenuOpen)
                {
                    MI_tools.IsSubmenuOpen = true;
                }
                else
                {
                    MI_tools.IsSubmenuOpen = false;
                }
            }

            if (e.Key == Key.F5)
            {
                if (!MI_infobits.IsSubmenuOpen)
                {
                    MI_infobits.IsSubmenuOpen = true;
                }
                else
                {
                    MI_infobits.IsSubmenuOpen = false;
                }
            }

            if (e.Key == Key.F6)
            {
                if (!MI_about.IsSubmenuOpen)
                {
                    MI_about.IsSubmenuOpen = true;
                }
                else
                {
                    MI_about.IsSubmenuOpen = false;
                }
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftAlt || e.Key == Key.RightAlt)
            {

                e.Handled = true;
            }

            if (e.Key == Key.F1)
            {

                e.Handled = true;
            }

            if (e.Key == Key.F2)
            {

                e.Handled = true;
            }

            if (e.Key == Key.F3)
            {

                e.Handled = true;

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }

            else if (e.ChangedButton == MouseButton.Right)
            {

            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            System.Windows.Size size = e.NewSize;

            mainWindowData.width = (int)size.Width;
            mainWindowData.height = (int)size.Height;

            data_Handler.SetMainWindowData(mainWindowData);

            e.Handled = true;
        }

        #endregion events


        #region elements menu item clicks
        private void MI_Coordinates_Click(object sender, RoutedEventArgs e)
        {
            summonElement(sender);
        }

        private void MI_Document_Click(object sender, RoutedEventArgs e)
        {
            summonElement(sender);
        }
        private void MI_Text_Click(object sender, RoutedEventArgs e)
        {
            summonElement(sender);
        }
        private void MI_FileLink_Click(object sender, RoutedEventArgs e)
        {
            summonElement(sender);
        }

        private void MI_Image_Click(object sender, RoutedEventArgs e)
        {
            summonElement(sender);
        }

        private void MI_Link_Click(object sender, RoutedEventArgs e)
        {
            summonElement(sender);
        }

        private void MI_MyNote_Click(object sender, RoutedEventArgs e)
        {
            add_MyNote();
        }

        private void MI_FlatShareCC_Click(object sender, RoutedEventArgs e)
        {
            add_FlatShareCC();
        }
        #endregion elements menu item clicks

        #region about menu item clicks
        private void MI_Manual_Click(object sender, RoutedEventArgs e)
        {
            summonElement(sender);
        }


        private void MI_Version_Click(object sender, RoutedEventArgs e)
        {
            summonElement(sender);
        }

        private void MI_License_Click(object sender, RoutedEventArgs e)
        {
            summonElement(sender);
        }

        private void MI_Documentation_Click(object sender, RoutedEventArgs e)
        {
            summonElement(sender);
        }
        #endregion about menu item clicks

        #region control menu item clicks
        private void MI_Command_Click(object sender, RoutedEventArgs e)
        {
            summonElement(sender);
        }

        private void MI_CoreOptions_Click(object sender, RoutedEventArgs e)
        {
            summonElement(sender);
        }



        private void MI_SYSTEM_Click(object sender, RoutedEventArgs e)
        {
            SYSTEM_CANVAS_SWITCH();
        }


        private void MI_LEVELSYSTEM_Click(object sender, RoutedEventArgs e)
        {
            if (ACTIVE_CANVAS != null)
            {
                if (ACTIVE_CANVAS._LevelBar.Visibility == Visibility.Collapsed)
                {
                    ACTIVE_CANVAS._LevelBar.Visibility = Visibility.Visible;
                }
                else
                {
                    ACTIVE_CANVAS._LevelBar.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void MI_quit_Click(object sender, RoutedEventArgs e)
        {
            logic.QuitApplicationCommand();
        }

        private void MI_shutdown_Click(object sender, RoutedEventArgs e)
        {
            logic.ShutdownCommand();
        }
        #endregion control menu item clicks

        #region infobits menu item clicks
        private void MI_SessionRuntime_IB_Click(object sender, RoutedEventArgs e)
        {
            new CoreToolTip("SessionRuntime");

            e.Handled = true;
        }
        #endregion infobits menu item clicks

        #region tools menu item clicks

        private void MI_Adjust_Click(object sender, RoutedEventArgs e)
        {
            summonElement(sender);
        }

        private void MI_Copy_Click(object sender, RoutedEventArgs e)
        {
            summonElement(sender);
        }

        private void MI_Paste_Click(object sender, RoutedEventArgs e)
        {
            summonElement(sender);
        }

        private void MI_Move_Click(object sender, RoutedEventArgs e)
        {
            summonElement(sender);
        }

        private void MI_LevelShift_Click(object sender, RoutedEventArgs e)
        {
            summonElement(sender);
        }

        private void MI_LocalDrives_Click(object sender, RoutedEventArgs e)
        {
            summonElement(sender);
        }


        private void MI_Random_Click(object sender, RoutedEventArgs e)
        {
            summonElement(sender);
        }

        private void MI_Request_Click(object sender, RoutedEventArgs e)
        {
            summonElement(sender);
        }


        private void MI_Stopwatch_Click(object sender, RoutedEventArgs e)
        {
            summonElement(sender);
        }

        private void MI_Clock_Click(object sender, RoutedEventArgs e)
        {
            summonElement(sender);
        }

        private void MI_RightClickChoice_Click(object sender, RoutedEventArgs e)
        {
            summonElement(sender);
        }
        #endregion tools menu item clicks

        #region selection control menu item clicks
        private void MI_copy_selection_Click(object sender, RoutedEventArgs e)
        {

            if (CORE_CANVAS_SWITCH.Get_ACTIVE_CANVAS().get_selected_items() != null)
            {
                int selection_count = CORE_CANVAS_SWITCH.Get_ACTIVE_CANVAS().get_selected_items().Count;

                if (selection_count > 0)
                {
                    CORE_CANVAS_SWITCH.copy();
                }
            }
        }

        private void MI_paste_selection_Click(object sender, RoutedEventArgs e)
        {
            if (CORE_CANVAS_SWITCH.Get_ACTIVE_CANVAS() != null)
            {
                CORE_CANVAS_SWITCH.paste();
            }
        }

        private void MI_move_selection_Click(object sender, RoutedEventArgs e)
        {
            if (CORE_CANVAS_SWITCH.Get_ACTIVE_CANVAS() != null
                && !SYSTEM_ACTIVE_FLAG)
            {
                CORE_CANVAS_SWITCH.move();
            }
        }


        private void MI_group_to_line_Click(object sender, RoutedEventArgs e)
        {
            if (ACTIVE_CANVAS != null)
            {
                ACTIVE_CANVAS.group_selected_items(true);
            }
        }
        private void MI_group_to_row_Click(object sender, RoutedEventArgs e)
        {
            if (ACTIVE_CANVAS != null)
            {
                ACTIVE_CANVAS.group_selected_items(false);
            }
        }
        private void MI_delete_Click(object sender, RoutedEventArgs e)
        {
            if (ACTIVE_CANVAS != null)
            {
                ACTIVE_CANVAS.delete_selected_items();
            }
        }
        #endregion selection control menu item clicks

        private void MI_minimize_Click(object sender, RoutedEventArgs e)
        {
            __MainWindow.WindowState = WindowState.Minimized;
        }

        private void MI_maximize_Click(object sender, RoutedEventArgs e)
        {
            if (__MainWindow.WindowState == WindowState.Maximized)
            {
                __MainWindow.WindowState = WindowState.Normal;
                MI_maximize.IsChecked = false;
            }
            else
            {
                __MainWindow.WindowState = WindowState.Maximized;
            }
        }

        // origin:      https://stackoverflow.com/questions/16245706/check-for-device-change-add-remove-events
        private IntPtr HwndHandler(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam, ref bool handled)
        {
            if (msg == UsbNotification.WmDevicechange)
            {
                switch ((int)wparam)
                {
                    case UsbNotification.DbtDeviceremovecomplete:
                        ACTIVE_CANVAS.updateLocalDrives();
                        //updateDriveInfo(); // this is where you do your magic
                        break;
                    case UsbNotification.DbtDevicearrival:
                        ACTIVE_CANVAS.updateLocalDrives();
                        //updateDriveInfo(); // this is where you do your magic
                        break;
                }
            }

            handled = false;
            return IntPtr.Zero;
        }

        private void __MainWindow_SourceInitialized(object sender, EventArgs e)
        {
            // origin:      https://stackoverflow.com/questions/16245706/check-for-device-change-add-remove-events
            // Adds the windows message processing hook and registers USB device add/removal notification.
            HwndSource source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
            if (source != null)
            {
                nint windowHandle = source.Handle;
                source.AddHook(HwndHandler);
                UsbNotification.RegisterUsbDeviceNotification(windowHandle);
            }
        }
    }
}
/*  END OF FILE
 * 
 * 
 */