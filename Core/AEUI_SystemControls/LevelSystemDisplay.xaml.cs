/* Aiding Elements User Interface
 *      LevelSystemDisplay user control
 * 
 * displays all levels in an overview, only visible on level 0
 * 
 * init:        2024|01|20
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.AEUI_Logic;
using AidingElementsUserInterface.Core.Auxiliaries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace AidingElementsUserInterface.Core.AEUI_SystemControls
{
    /// <summary>
    /// Interaktionslogik für LevelSystemDisplay.xaml
    /// </summary>
    public partial class LevelSystemDisplay : UserControl
    {
        private CoreCanvas screen;

        public LevelSystemDisplay()
        {
            InitializeComponent();
        }

        internal void clear()
        {
            SP_ScreenLevels.Children.Clear();
        }

        internal void update()
        {
            clear();

            StackPanel stackPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };

            ObservableCollection<LevelData> levels = screen.GetLevelSystem().getLevels();

            for (int i = 1; i < levels.Count; i++)
            {
                LevelData levelData = levels[i];

                StackPanel SP_LevelData = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                Width = 240,
                    Height = 40
                };

                TextBox TB_level_name = new TextBox()
                {
                    Name = $"TB_name_{i}",
                    Width = 80,
                    Text = $"{levelData.NAME}"
                };

                TextBox TB_level_desc = new TextBox()
                {
                    Name = $"TB_desc_{i}",
                    Width = 120,
                    Text = $"{levelData.DESCRIPTION}",
                    TextWrapping = TextWrapping.Wrap
                };

                Label label = new Label()
                {
                    Background = new SolidColorBrush(Color.FromArgb(77, 0, 0, 0)),
                    Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 9,
                    FontFamily = new FontFamily("Verdana"),
                    Content = $"Level\n{levelData.LEVEL}"

                };

                Button button = new Button()
                {
                    Name = $"button_{i}",
                    Width = 40,
                    Content = label,

                    Background = levelData.Background.GetBrush()
                };

                SP_LevelData.Children.Add(button);
                SP_LevelData.Children.Add(TB_level_name);
                SP_LevelData.Children.Add(TB_level_desc);

                button.Click += Button_Click;
                TB_level_name.KeyDown += TB_level_name_KeyDown;
                TB_level_name.KeyUp += TB_level_name_KeyUp;
                TB_level_desc.KeyDown += TB_level_desc_KeyDown;
                TB_level_desc.KeyUp += TB_level_desc_KeyUp;

                stackPanel.Children.Add(SP_LevelData);

                if (i % 5 == 0)
                {
                    SP_ScreenLevels.Children.Add(stackPanel);

                    stackPanel = new StackPanel()
                    { 
                        Orientation = Orientation.Horizontal
                    };
                }
            }
        }

        private void TB_level_desc_KeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void TB_level_desc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                TextBox textbox = (TextBox)sender;

                int id = Int32.Parse(textbox.Name.Substring(8));

                LevelSystem ls = screen.GetLevelSystem();

                ls.getLevels()[id].SetDescription(textbox.Text);
            }
        }

        private void TB_level_name_KeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void TB_level_name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                TextBox textbox = (TextBox)sender;

                int id = Int32.Parse(textbox.Name.Substring(8));

                LevelSystem ls = screen.GetLevelSystem();

                ls.getLevels()[id].SetName(textbox.Text);
            }
        }

        internal void setCanvas(ref CoreCanvas coreCanvas)
        {
            screen = coreCanvas;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            int id = Int32.Parse(button.Name.Substring(7));

            LevelSystem ls = screen.GetLevelSystem();

            ls.SetCurrentLevel(id);
            screen.GetLevelBar().update(ls.Get_CURRENT_LEVEL());

            e.Handled = true;
        }
    }
}
/*  END OF FILE
 * 
 * 
 */