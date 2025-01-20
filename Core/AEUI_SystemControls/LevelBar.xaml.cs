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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace AidingElementsUserInterface.Core.AEUI_UserControls
{
    /// <summary>
    /// Interaktionslogik für LevelBar.xaml
    /// </summary>
    public partial class LevelBar : UserControl
    {
        SharedLogic logic = new SharedLogic();
        string RB_State = "all";
        bool loading = true;

        public LevelBar()
        {
            InitializeComponent();

            build();
            registerEvents();

            loading = false;
        }

        private void build()
        {
            CB_First.setContent("|<<");
            CB_Prev.setContent("<");

            CB_Zero.setContent("zero");
            CB_LevelBackground.setContent("background");

            CB_Next.setContent(">");
            CB_Last.setContent(">>|");

            CVC_Min_Range.setIdentifier("lowest range");
            CVC_Min_Range.setText("1");

            CVC_Max_Range.setIdentifier("upmost range");
            CVC_Max_Range.setText($"{CoreCanvasSwitchData.Get_CORECANVAS_LEVEL_CAP - 1}");

            CL_LevelDescription.textblock.MaxWidth = 300;
            CL_LevelDescription.textblock.TextWrapping = TextWrapping.Wrap;
            CL_LevelDescription.ToolTip = CL_LevelDescription.textblock.Text;

            CTB_LevelDescription.textbox.MaxWidth = 300;
            CTB_LevelDescription.textbox.TextWrapping = TextWrapping.Wrap;
            CTB_LevelDescription.textbox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
        }

        private void registerEvents()
        {
            CB_First.button.Click += CB_FirstClick;
            CB_Prev.button.Click += CB_Prev_Click;
            CB_Zero.button.Click += CB_Zero_Click;
            CB_LevelBackground.button.Click += CB_LevelBackground_Click;
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
            if (GetLevelSystem().Get_CURRENT_LEVEL() != GetLevelSystem().Get_ZERO_LEVEL())
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
        }

        private void LevelBackgroundSwitch()
        {
            if (border_LevelBackground.Visibility == Visibility.Collapsed)
            {
                border_LevelBackground.Visibility = Visibility.Visible;

                border_LevelBackground.Child = new BrushSetup(ref __LevelBar);
            }
            else
            {
                border_LevelBackground.Visibility = Visibility.Collapsed;
                border_LevelBackground.Child = null;
            }
        }


        private void LevelNameSwitch()
        {
            if (GetLevelSystem().Get_CURRENT_LEVEL() != GetLevelSystem().Get_ZERO_LEVEL())
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
        }

        internal void ChangeLevelBackground(ColorData? colorData)
        {
            if (!loading)
            {
                LevelBackgroundSwitch();

                if (colorData == null)
                {
                    if (logic.GetMainWindow().Get_SYSTEM_ACTIVE_FLAG)
                    {
                        logic.GetMainWindow().Get_SYTEM_CANVAS.Get_SYSTEM_CANVAS.GetLevelSystem().Get_CURRENT_LEVEL().SetBackground(false, null);
                        logic.GetMainWindow().Get_SYTEM_CANVAS.Get_SYSTEM_CANVAS.GetLevelSystem().LevelChange();
                        logic.GetMainWindow().Get_SYTEM_CANVAS.Get_SYSTEM_CANVAS.NoLevelBackground();
                    }
                    else
                    {
                        if (logic.GetMainWindow().Get_ACTIVE_CANVAS != null)
                        {
                            logic.GetMainWindow().Get_ACTIVE_CANVAS.GetLevelSystem().Get_CURRENT_LEVEL().SetBackground(false, null);
                            logic.GetMainWindow().Get_ACTIVE_CANVAS.GetLevelSystem().LevelChange();
                            logic.GetMainWindow().Get_ACTIVE_CANVAS.NoLevelBackground();
                        }
                    }

                }
                else
                {
                    if (logic.GetMainWindow().Get_SYSTEM_ACTIVE_FLAG)
                    {
                        logic.GetMainWindow().Get_SYTEM_CANVAS.Get_SYSTEM_CANVAS.GetLevelSystem().Get_CURRENT_LEVEL().SetBackground(true, colorData);
                        logic.GetMainWindow().Get_SYTEM_CANVAS.Get_SYSTEM_CANVAS.GetLevelSystem().LevelChange();
                        logic.GetMainWindow().Get_SYTEM_CANVAS.Get_SYSTEM_CANVAS.SetBackground(colorData);
                    }
                    else
                    {
                        if (logic.GetMainWindow().Get_ACTIVE_CANVAS != null)
                        {
                            logic.GetMainWindow().Get_ACTIVE_CANVAS.GetLevelSystem().Get_CURRENT_LEVEL().SetBackground(true, colorData);
                            logic.GetMainWindow().Get_ACTIVE_CANVAS.GetLevelSystem().LevelChange();
                            logic.GetMainWindow().Get_ACTIVE_CANVAS.SetBackground(colorData);
                        }
                    }

                }
            }                        
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
            if (!loading)
            {
                CL_LevelId.setText(levelData.LEVEL.ToString());

                CL_LevelName.setText(levelData.NAME.ToString());
                CL_LevelDescription.setText(levelData.DESCRIPTION.ToString());

                CL_LevelLogin.setText(levelData.LOGIN_FLAG.ToString());

                CL_LevelSecurityPanel.setText(levelData.SECURITY_FLAG.ToString());

                // fix this later, intended display is element count per level, not per screen
                if (logic.GetMainWindow().Get_SYSTEM_ACTIVE_FLAG)
                {
                    CL_LevelElementCount.setText(logic.GetMainWindow().Get_SYTEM_CANVAS.Get_SYSTEM_CANVAS.canvas.Children.Count.ToString());
                }
                else
                {
                    if (logic.GetMainWindow().Get_ACTIVE_CANVAS != null)
                    {
                        CL_LevelElementCount.setText(logic.GetMainWindow().Get_ACTIVE_CANVAS.canvas.Children.Count.ToString());
                    }
                }

                if (GetLevelSystem() != null)
                {
                    GetLevelSystem().LevelChange();
                }
            }       
        }

        private LevelSystem? GetLevelSystem()
        {
            if (logic.GetMainWindow().Get_SYSTEM_ACTIVE_FLAG)
            {
                return logic.GetMainWindow().Get_SYTEM_CANVAS.Get_SYSTEM_CANVAS.GetLevelSystem();
            }
            else
            {

                if (logic.GetMainWindow().Get_ACTIVE_CANVAS != null)
                {
                    return logic.GetMainWindow().Get_ACTIVE_CANVAS.GetLevelSystem();
                }
            }

            return null;
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

        private void CB_LevelBackground_Click(object sender, RoutedEventArgs e)
        {
            LevelBackgroundSwitch();               
        }


        private void CB_Next_Click(object sender, RoutedEventArgs e)
        {
            update(GetLevelSystem().Next());
        }

        private void CB_Last_Click(object sender, RoutedEventArgs e)
        {
            update(GetLevelSystem().Last());
        }

        private void RB_Range_Checked(object sender, RoutedEventArgs e)
        {

            SP_Range_CVCs.Visibility = Visibility.Visible;

            RB_State = "range";

            int min_range;
            int max_range;


            try
            {
                min_range = Int32.Parse(CVC_Min_Range.Value);
                max_range = Int32.Parse(CVC_Max_Range.Value);
            }
            catch (Exception)
            {
                min_range = 1;
                max_range = CoreCanvasSwitchData.Get_CORECANVAS_LEVEL_CAP - 1;
            }

            LevelSystem currentLevelSystem = new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS.GetLevelSystem();
            currentLevelSystem.SetVisibilityMODE(RB_State);
            currentLevelSystem.SetRange(min_range, max_range);
            new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS.SetVisibility(currentLevelSystem.Get_LEVEL(), RB_State);

            e.Handled = true;
        }

        private void RB_Range_Unchecked(object sender, RoutedEventArgs e)
        {
            SP_Range_CVCs.Visibility = Visibility.Collapsed;
        }

        private void RB_Level_Checked(object sender, RoutedEventArgs e)
        {
            RB_State = "level";

            LevelSystem currentLevelSystem = new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS.GetLevelSystem();
            currentLevelSystem.SetVisibilityMODE(RB_State);
            new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS.SetVisibility(currentLevelSystem.Get_LEVEL(), RB_State);

            e.Handled = true;
        }

        private void RB_All_Checked(object sender, RoutedEventArgs e)
        {
            if (!loading)
            {
                RB_State = "all";

                LevelSystem currentLevelSystem = new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS.GetLevelSystem();
                currentLevelSystem.SetVisibilityMODE(RB_State);
                new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS.SetVisibility(currentLevelSystem.Get_LEVEL(), RB_State);

                e.Handled = true;
            }
        }
    }
}
/*  END OF FILE
 * 
 * 
 */