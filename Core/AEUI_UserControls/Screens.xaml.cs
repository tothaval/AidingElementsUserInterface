/* Aiding Elements User Interface
 *      Screens element
 * 
 * basic screen overview element, switch to target screen via click
 * shows element count, canvas name and canvas background on a button, one button per screen
 * 
 * init:        2024|02|24
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.AEUI_SystemControls;
using AidingElementsUserInterface.Core.Auxiliaries;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AidingElementsUserInterface.Core.AEUI_UserControls
{
    /// <summary>
    /// Interaktionslogik für Screens.xaml
    /// </summary>
    public partial class Screens : UserControl
    {
        private System.Windows.Threading.DispatcherTimer _timer = new System.Windows.Threading.DispatcherTimer();
        private ObservableCollection<Button> buttons = new ObservableCollection<Button>();
        

        public Screens()
        {
            InitializeComponent();

            _timer.Tick += _timer_Tick;
            _timer.Interval = TimeSpan.FromMilliseconds(100);
            _timer.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            build();
            update();

            _timer.Tick -= _timer_Tick;

            _timer.Tick += _update_Tick;
            _timer.Interval = TimeSpan.FromMilliseconds(500);
            _timer.Start();
        }

        private void _update_Tick(object sender, EventArgs e)
        {
            update();
        }



        private void build()
        {
            Button SYSTEM_button = new Button()
            {
                Name = $"button_{0}",
                Width = 200,
                Height = 100
            };

            SYSTEM_button.Click += Button_Click;

            buttons.Add(SYSTEM_button);

            CoreCanvasSwitch coreCanvasSwitch = new SharedLogic().GetMainWindow().CORE_CANVAS_SWITCH;

            int i = 1;

            StackPanel stackPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };

            foreach (CoreCanvas item in coreCanvasSwitch.Get_coreCanvasScreens)
            {
                string screenData = $"{item.getCanvasData().canvasName}\n" +
                    $"{item.canvas.Children.Count} elements";

                Button button = new Button()
                {
                    Name = $"button_{i}",
                    Width = 100,
                    Height = 100
                };

                button.Click += Button_Click;

                buttons.Add(button);

                stackPanel.Children.Add(button);

                if (i % 2 == 0)
                {
                    SP_screens.Children.Add(stackPanel);

                    stackPanel = new StackPanel()
                    { Orientation = Orientation.Horizontal };
                }

                i++;
            }

            SP_screens.Children.Add(SYSTEM_button);
        }

        private void update()
        {
            int i = 0;
            double megabyte = 1024 * 1024;

            IO_Handler iO_Handler = new IO_Handler();

            foreach (Button item in buttons)
            {
                if (i == 0)
                {
                    if (item.Name.Equals($"button_{i}"))
                    {
                        long size = iO_Handler.check_folder_size(iO_Handler.SYSTEM_folder);
                        double size_in_megabyts = size / megabyte;

                        CoreCanvas systemScreen = new SharedLogic().GetMainWindow().Get_SYTEM_CANVAS.Get_SYSTEM_CANVAS;
                        string SYSTEM_screenData = $"{systemScreen.getCanvasData().canvasName}\n" +
                            $"{systemScreen.canvas.Children.Count} elements\n" +
                            $"{size_in_megabyts:N2} MB";

                        Label label = new Label();
                        label.Background = new SolidColorBrush(Color.FromArgb(77, 0, 0, 0));
                        label.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                        label.HorizontalAlignment = HorizontalAlignment.Stretch;
                        label.VerticalAlignment = VerticalAlignment.Center;
                        label.FontSize = 9;
                        label.FontFamily = new FontFamily("Verdana");
                        label.Content = SYSTEM_screenData;

                        item.Content = label;
                        item.Background = systemScreen.getCanvasData().background.GetBrush();

                        if (new SharedLogic().GetMainWindow().Get_SYSTEM_ACTIVE_FLAG)
                        {
                            item.BorderBrush = new SolidColorBrush(Colors.LightGreen);
                            item.BorderThickness = new Thickness(3, 3, 3, 3);
                        }
                        else
                        {
                            item.BorderBrush = new SolidColorBrush(Colors.DarkSlateGray);
                            item.BorderThickness = new Thickness(1);
                        }
                    }
                }
                else
                {
                    if (item.Name.Equals($"button_{i}"))
                    {
                        long size = iO_Handler.check_folder_size($"{iO_Handler.UserSpace_folder}screen_{i}\\");
                        double size_in_megabyts = size / megabyte;

                        CoreCanvasSwitch coreCanvasSwitch = new SharedLogic().GetMainWindow().CORE_CANVAS_SWITCH;

                        string screenData = $"{coreCanvasSwitch.Get_coreCanvasScreens[i - 1].getCanvasData().canvasName}\n" +
                            $"{coreCanvasSwitch.Get_coreCanvasScreens[i - 1].canvas.Children.Count} elements\n" +
                            $"{size_in_megabyts:N2} MB";

                        Label label = new Label();
                        label.Background = new SolidColorBrush(Color.FromArgb(77, 0, 0, 0));
                        label.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                        label.HorizontalAlignment = HorizontalAlignment.Stretch;
                        label.VerticalAlignment = VerticalAlignment.Center;
                        label.FontSize = 9;
                        label.FontFamily = new FontFamily("Verdana");
                        label.Content = screenData;

                        item.Content = label;
                        item.Background = coreCanvasSwitch.Get_coreCanvasScreens[i - 1].getCanvasData().background.GetBrush();

                        if (new SharedLogic().GetMainWindow().Get_SYSTEM_ACTIVE_FLAG == false)
                        {
                            if (coreCanvasSwitch.Get_ACTIVE_CANVAS().getCanvasData().canvasID == i)
                            {
                                item.BorderBrush = new SolidColorBrush(Colors.LightGreen);
                                item.BorderThickness = new Thickness(3, 3, 3, 3);
                            }
                            else
                            {
                                item.BorderBrush = new SolidColorBrush(Colors.DarkSlateGray);
                                item.BorderThickness = new Thickness(1);
                            }
                        }

                    }
                }

                i++;
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            int id = Int32.Parse(button.Name.Substring(7));

            if (id == 0)
            {
                new SharedLogic().GetMainWindow().set_SYSTEM_CANVAS();
            }
            else
            {
                if (new SharedLogic().GetMainWindow().Get_SYSTEM_ACTIVE_FLAG)
                {
                    new SharedLogic().GetMainWindow().unset_SYSTEM_CANVAS();
                }

                new SharedLogic().GetMainWindow().CORE_CANVAS_SWITCH.jumpToScreen(id - 1);
            }

            e.Handled = true;
        }
    }
}
/*  END OF FILE
 * 
 * 
 */