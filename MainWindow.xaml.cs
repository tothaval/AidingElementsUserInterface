/* Aiding Elements User Interface
 *      MainWindow 
 * 
 * main frame for the application
 * 
 * init:        2023|11|27
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
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

namespace AidingElementsUserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal CoreCanvas coreCanvas;
        internal MainWindowData mainWindowData;

        internal Data_Handler data_Handler = new Data_Handler();

        internal ElementHandler element_handler = new ElementHandler();

        internal SharedLogic logic = new SharedLogic();        


        public MainWindow()
        {
            InitializeComponent();

            build();
        }


        private void build()
        {
            load_MainWindowData();

            _backgroundImage();

            load_CoreCanvas();
        }


        // processing
        #region processing
        private void add_MyNote()
        {
            if (MI_MyNote.IsChecked)
            {
                coreCanvas.GetCentral().ExecuteCommandRequest($">MyNote");
            }
            else
            {
                coreCanvas.RemoveMyNote();
            }
        }

        private void add_FlatShareCC()
        {
            // disabled until fixed
            //if (MI_FlatShareCC.IsChecked)
            //{
            //coreCanvas.GetCentral().ExecuteCommandRequest($">FlatShareCC");
            //}
            //else
            //{
            //    coreCanvas.RemoveFlatShareCC();
            //}
        }
        internal void _backgroundImage()
        {
            if (mainWindowData.imageFilePath != null)
            {
                if (File.Exists(mainWindowData.imageFilePath))
                {
                    border.Background = new ImageBrush(new BitmapImage(new Uri(mainWindowData.imageFilePath)));
                }
            }
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

        private void load_CoreCanvas()
        {
            coreCanvas = new CoreCanvas();
            coreCanvas.Name = "mainWindowCanvas";

            canvas_border.Child = coreCanvas;

            Keyboard.Focus(coreCanvas);
        }

        public void quitAEUI()
        {
            if (mainWindowData != null)
            {
                mainWindowData.initialPosition = new Point(this.Left, this.Top);

                XML_Handler xml_handler = new XML_Handler(mainWindowData);

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
                coreCanvas.GetCentral().ExecuteCommandRequest($">Manual");
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
            coreCanvas.GetCentral().ExecuteCommandRequest($">{((MenuItem)sender).Header}");
        }

        private void MI_FileLink_Click(object sender, RoutedEventArgs e)
        {
            coreCanvas.GetCentral().ExecuteCommandRequest($">{((MenuItem)sender).Header}");
        }

        private void MI_Image_Click(object sender, RoutedEventArgs e)
        {
            coreCanvas.GetCentral().ExecuteCommandRequest($">{((MenuItem)sender).Header}");
        }

        private void MI_Link_Click(object sender, RoutedEventArgs e)
        {
            coreCanvas.GetCentral().ExecuteCommandRequest($">{((MenuItem)sender).Header}");
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
        private void MI_manual_Click(object sender, RoutedEventArgs e)
        {
            coreCanvas.GetCentral().ExecuteCommandRequest($">{((MenuItem)sender).Header}");
        }
        #endregion about menu item clicks

        #region control menu item clicks
        private void MI_command_Click(object sender, RoutedEventArgs e)
        {
            coreCanvas.GetCentral().ExecuteCommandRequest($">{((MenuItem)sender).Header}");
        }
        private void MI_levelShifter_Click(object sender, RoutedEventArgs e)
        {
            coreCanvas.GetCentral().ExecuteCommandRequest($">{((MenuItem)sender).Header}");
        }

        #region control options menu item clicks
        private void MI_OPTIONS_Click(object sender, RoutedEventArgs e)
        {
            coreCanvas.GetCentral().ExecuteCommandRequest($">{((MenuItem)sender).Header}");
        }

    
        #endregion control options menu item clicks

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
        private void MI_LocalDrives_Click(object sender, RoutedEventArgs e)
        {
            coreCanvas.GetCentral().ExecuteCommandRequest($">{((MenuItem)sender).Header}");
        }

        private void MI_Random_Click(object sender, RoutedEventArgs e)
        {
            coreCanvas.GetCentral().ExecuteCommandRequest($">{((MenuItem)sender).Header}");
        }

        private void MI_RightClickChoice_Click(object sender, RoutedEventArgs e)
        {
            coreCanvas.GetCentral().ExecuteCommandRequest($">{((MenuItem)sender).Header}");
        }
        #endregion tools menu item clicks

        #region selection control menu item clicks
        private void MI_group_to_line_Click(object sender, RoutedEventArgs e)
        {
            coreCanvas.group_selected_items(true);
        }
        private void MI_group_to_row_Click(object sender, RoutedEventArgs e)
        {
            coreCanvas.group_selected_items(false);
        }
        private void MI_delete_Click(object sender, RoutedEventArgs e)
        {
            coreCanvas.delete_selected_items();
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
                        coreCanvas.updateLocalDrives();                        
                        //updateDriveInfo(); // this is where you do your magic
                        break;
                    case UsbNotification.DbtDevicearrival:
                        coreCanvas.updateLocalDrives();
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