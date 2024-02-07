/* Aiding Elements User Interface
 *      LevelBar 
 *  
 * 
 * 
 * init:        2024|01|28
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.AEUI_Logic;
using AidingElementsUserInterface.Core.Auxiliaries;
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

namespace AidingElementsUserInterface.Core.AEUI_UserControls
{
    /// <summary>
    /// Interaktionslogik für LevelBar.xaml
    /// </summary>
    public partial class LevelBar : UserControl
    {
        public LevelBar()
        {
            InitializeComponent();

            build();
            registerEvents();
        }

        private void build()
        {
            CB_First.setContent("|<<");
            CB_Prev.setContent("<");

            CB_Zero.setContent("zero");

            CB_Next.setContent(">");
            CB_Last.setContent(">>|");

        }

        private void registerEvents()
        {
            CB_First.button.Click += CB_FirstClick;
            CB_Prev.button.Click += CB_Prev_Click;
            CB_Zero.button.Click += CB_Zero_Click;
            CB_Next.button.Click += CB_Next_Click;
            CB_Last.button.Click += CB_Last_Click;

            CL_LevelName.textblock.MouseLeftButtonDown += CL_LevelName_MouseLeftButtonDown;
            CL_LevelName.textblock.MouseLeftButtonUp += CL_LevelName_MouseLeftButtonUp;

            CL_LevelDescription.textblock.MouseLeftButtonDown += CL_LevelDescription_MouseLeftButtonDown;
            CL_LevelDescription.textblock.MouseLeftButtonUp += CL_LevelDescription_MouseLeftButtonUp;

            CTB_LevelName.textbox.KeyUp += CTB_LevelName_KeyUp;
            CTB_LevelDescription.textbox.KeyUp += CTB_LevelDescription_KeyUp;
        }

        private void LevelDescriptionSwitch()
        {
            if (border_LevelDescCL.Visibility == Visibility.Visible)
            {

                border_LevelDescCL.Visibility = Visibility.Collapsed;
                border_LevelDescCTB.Visibility = Visibility.Visible;
            }
            else
            {
                border_LevelDescCL.Visibility = Visibility.Visible;
                border_LevelDescCTB.Visibility = Visibility.Collapsed;
            }


            string newDescription = CTB_LevelDescription.getText();

            GetLevelSystem().Get_CURRENT_LEVEL().SetDescription(newDescription);

            CL_LevelDescription.setText(newDescription);
        }


        private void LevelNameSwitch()
        {
            if (border_LevelNameCL.Visibility == Visibility.Visible)
            {

                border_LevelNameCL.Visibility = Visibility.Collapsed;
                border_LevelNameCTB.Visibility = Visibility.Visible;
            }
            else
            {
                border_LevelNameCL.Visibility = Visibility.Visible;
                border_LevelNameCTB.Visibility = Visibility.Collapsed;
            }


            string newName = CTB_LevelName.getText();

            GetLevelSystem().Get_CURRENT_LEVEL().SetName(newName);
            CL_LevelName.setText(newName);
        }


        private void CTB_LevelLogin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
            }
        }

        private void CTB_LevelDescription_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                LevelDescriptionSwitch();
            }
        }

        private void CTB_LevelName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                LevelNameSwitch();
            }
        }

        private void CL_LevelLogin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                
            }
        }
        private void CL_LevelLogin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void CL_LevelDescription_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                LevelDescriptionSwitch();
            }
        }


        private void CL_LevelDescription_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void CL_LevelName_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                LevelNameSwitch();
            }
        }

        private void CL_LevelName_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        internal void update(LevelData levelData)
        {
            CL_LevelId.setText(levelData.LEVEL.ToString());

            CL_LevelName.setText(levelData.NAME.ToString());
            CL_LevelDescription.setText(levelData.DESCRIPTION.ToString());

            CL_LevelLogin.setText(levelData.LOGIN_FLAG.ToString());

            CL_LevelSecurityPanel.setText(levelData.SECURITY_FLAG.ToString());

            // fix this later, intended display is element count per level, not per screen
            if (new SharedLogic().GetMainWindow().Get_SYSTEM_ACTIVE_FLAG)
            {
                CL_LevelElementCount.setText(new SharedLogic().GetMainWindow().Get_SYTEM_CANVAS.Get_SYSTEM_CANVAS.canvas.Children.Count.ToString());
            }
            else
            {
                if (new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS != null)
                {
                    CL_LevelElementCount.setText(new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS.canvas.Children.Count.ToString());
                }
            }

            GetLevelSystem().LevelChange();
        }

        private LevelSystem GetLevelSystem()
        {
            if (new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS != null)
            {
                return new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS.GetLevelSystem();
            }

            if (new SharedLogic().GetMainWindow().Get_SYSTEM_ACTIVE_FLAG)
            {
                return new SharedLogic().GetMainWindow().Get_SYTEM_CANVAS.Get_SYSTEM_CANVAS.GetLevelSystem();
            }

            return new LevelSystem();
        }


        private void CB_FirstClick(object sender, RoutedEventArgs e)
        {
            update(GetLevelSystem().First());
            // level system show first level (lowest, meaning 100)
        }

        private void CB_Prev_Click(object sender, RoutedEventArgs e)
        {
            update(GetLevelSystem().Prev());
        }
        private void CB_Zero_Click(object sender, RoutedEventArgs e)
        {
            update(GetLevelSystem().Get_ZERO_LEVEL());
        }

        private void CB_Next_Click(object sender, RoutedEventArgs e)
        {
            update(GetLevelSystem().Next());
        }

        private void CB_Last_Click(object sender, RoutedEventArgs e)
        {
            update(GetLevelSystem().Last());
        }
    }
}
/*  END OF FILE
 * 
 * 
 */