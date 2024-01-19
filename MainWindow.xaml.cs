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
 *  MainWindow frame
 *      CoreCanvasSwitch (license: hardcoded amount of 11 CoreCanvas elements,
 *      accessible via switch element, each protectable via security layer with password request
 *      1 CoreCanvas is reserved for the aeui core and system display stuff, the system canvas
 *      is placed in a separate border within MainWindow xaml, access via Visibility change
 *      
 *      CoreCanvasSwitch loads the current canvas (should it hold the data or discard it(save canvas and remove every object))
 *      CoreCanvasSwitch responds to 2 buttons right and left of the mainwindow border
 * 
 *      also hardcoded are: LevelCap 201 (201 accessible z-layer per canvas, 1 for CanvasSystem stuff, unchangeable)
 *                          Canvas Max Size 10.000 pixels, a wider canvas than the display should be accessible with a
 *                          scrollviewer and zoom in/zoom out code from image
 *                          
 *                          the caps could be raised with a new license or an expansion right
 *                          
 *                          maybe offer a product called (individually compiled license version to hardcode user settings in the .exe)
 * 
 * 
 *  to do
 *      LevelSystem class
 *      LevelSystemDisplay usercontrol
 *      LevelShift
 *      LevelData
 *      
 *      further develop and upgrade Canvas save and load functionality, to accompany for the recent changes
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

namespace AidingElementsUserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CoreCanvas ACTIVE_CANVAS;

        internal MainWindowData mainWindowData;

        internal Data_Handler data_Handler = new Data_Handler();
        internal ElementHandler element_handler = new ElementHandler();

        internal SharedLogic logic = new SharedLogic();

        
        internal CoreCanvas Get_ACTIVE_CANVAS => ACTIVE_CANVAS;


        public MainWindow()
        {
            InitializeComponent();

            build();
        }


        private void build()
        {
            load_MainWindowData();

            MainWindowResources();
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
            if (mainWindowData != null)
            {
                mainWindowData.initialPosition = new Point(this.Left, this.Top);

                XML_Handler xml_handler = new XML_Handler();

                xml_handler.ButtonData_save();
                xml_handler.CanvasData_save();
                xml_handler.CoreData_save();
                xml_handler.MainWindowData_save();
                xml_handler.TextBoxData_save();

                if (element_handler != null)
                {
                    element_handler.save_state(xml_handler);
                }
            }

            Application.Current.Shutdown();
        }


        private void SYSTEM_CANVAS_SWITCH()
        {
            if (border_SYSTEM_CANVAS.Visibility == Visibility.Collapsed)
            {
                border_CORE_CANVAS_SWITCH.Visibility = Visibility.Collapsed;
                border_SYSTEM_CANVAS.Visibility = Visibility.Visible;

                ACTIVE_CANVAS = SYSTEM_CANVAS.Get_SYSTEM_CANVAS;
            }
            else
            {
                border_CORE_CANVAS_SWITCH.Visibility = Visibility.Visible;
                border_SYSTEM_CANVAS.Visibility = Visibility.Collapsed;

                ACTIVE_CANVAS = CORE_CANVAS_SWITCH.Get_ACTIVE_CANVAS();
            }
        }

        #endregion processing




        // events
        #region events
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            quitAEUI();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.F1)
            {
                if (ACTIVE_CANVAS != null)
                {
                    ACTIVE_CANVAS.GetCentral().ExecuteCommandRequest($">Manual");
                }
            }

            if (e.Key == Key.F2)
            {
                if (MI_MyNote.IsChecked == false)
                {
                    MI_MyNote.IsChecked = true;
                }
                else
                {
                    MI_MyNote.IsChecked = false;
                }
                add_MyNote();
            }

            if (e.Key == Key.F3)
            {
                if (MI_FlatShareCC.IsChecked == false)
                {
                    MI_FlatShareCC.IsChecked = true;
                }
                else
                {
                    MI_FlatShareCC.IsChecked = false;
                }
                add_FlatShareCC();
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
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
            if (ACTIVE_CANVAS != null)
            {
                ACTIVE_CANVAS.GetCentral().ExecuteCommandRequest($">{((MenuItem)sender).Header}");
            }
        }

        private void MI_FileLink_Click(object sender, RoutedEventArgs e)
        {
            if (ACTIVE_CANVAS != null)
            {
                ACTIVE_CANVAS.GetCentral().ExecuteCommandRequest($">{((MenuItem)sender).Header}");
            }
        }

        private void MI_Image_Click(object sender, RoutedEventArgs e)
        {
            if (ACTIVE_CANVAS != null)
            {
                ACTIVE_CANVAS.GetCentral().ExecuteCommandRequest($">{((MenuItem)sender).Header}");
            }
        }

        private void MI_Link_Click(object sender, RoutedEventArgs e)
        {
            if (ACTIVE_CANVAS != null)
            {
                ACTIVE_CANVAS.GetCentral().ExecuteCommandRequest($">{((MenuItem)sender).Header}");
            }
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
            if (ACTIVE_CANVAS != null)
            {
                ACTIVE_CANVAS.GetCentral().ExecuteCommandRequest($">{((MenuItem)sender).Header}");
            }
        }
        #endregion about menu item clicks

        #region control menu item clicks
        private void MI_Command_Click(object sender, RoutedEventArgs e)
        {
            if (ACTIVE_CANVAS != null)
            {
                ACTIVE_CANVAS.GetCentral().ExecuteCommandRequest($">{((MenuItem)sender).Header}");
            }
        }

        private void MI_LevelShift_Click(object sender, RoutedEventArgs e)
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

        private void MI_SYSTEM_Click(object sender, RoutedEventArgs e)
        {
            SYSTEM_CANVAS_SWITCH();           
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


        #region tools menu item clicks

        private void MI_Adjust_Click(object sender, RoutedEventArgs e)
        {
            if (ACTIVE_CANVAS != null)
            {
                ACTIVE_CANVAS.GetCentral().ExecuteCommandRequest($">{((MenuItem)sender).Header}");
            }
        }

        private void MI_LocalDrives_Click(object sender, RoutedEventArgs e)
        {
            if (ACTIVE_CANVAS != null)
            {
                ACTIVE_CANVAS.GetCentral().ExecuteCommandRequest($">{((MenuItem)sender).Header}");
            }
        }

        private void MI_Random_Click(object sender, RoutedEventArgs e)
        {
            if (ACTIVE_CANVAS != null)
            {
                ACTIVE_CANVAS.GetCentral().ExecuteCommandRequest($">{((MenuItem)sender).Header}");
            }
        }

        private void MI_Request_Click(object sender, RoutedEventArgs e)
        {
            if (ACTIVE_CANVAS != null)
            {
                ACTIVE_CANVAS.GetCentral().ExecuteCommandRequest($">{((MenuItem)sender).Header}");
            }
        }

        private void MI_RightClickChoice_Click(object sender, RoutedEventArgs e)
        {
            if (ACTIVE_CANVAS != null)
            {
                ACTIVE_CANVAS.GetCentral().ExecuteCommandRequest($">{((MenuItem)sender).Header}");
            }
        }
        #endregion tools menu item clicks

        #region selection control menu item clicks
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