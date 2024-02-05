/* Aiding Elements User Interface
 *      LevelShift element 
 * 
 * basic element to set z-index and shift containers to a specific z-index
 * as well as z-index visibility
 * 
 * init:        2024|01|16
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
    /// Interaktionslogik für LevelShift.xaml
    /// </summary>
    public partial class LevelShift : UserControl
    {
        string RB_State = "all";

        public LevelShift()
        {
            InitializeComponent();

            build();
            registerEvents();
        }

        private void build()
        {
            Data_Handler data_Handler = new SharedLogic().GetDataHandler();


            CVC_Min_Level.setIdentifier("lowest level");
            CVC_Min_Level.setText((-1 * (CoreCanvasSwitchData.Get_CORECANVAS_LEVEL_CAP / 2)).ToString());
            CVC_Max_Level.setIdentifier("upmost level");
            CVC_Min_Level.setText((CoreCanvasSwitchData.Get_CORECANVAS_LEVEL_CAP / 2).ToString());

            CVC_Min_Range.setIdentifier("lowest range");

            CVC_Min_Level.setText("-100");

            CVC_Max_Range.setIdentifier("upmost range");

            CVC_Min_Level.setText("100");

            CB_ApplyToSelection.setContent("change selection");

            CB_SaveChanges.setContent("save changes");

        }

        private void CB_ApplyToSelection_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CB_SaveChange_Click(object sender, RoutedEventArgs e)
        {
        }


        private void registerEvents()
        {
            CB_ApplyToSelection.button.Click += CB_ApplyToSelection_Click;
            CB_SaveChanges.button.Click += CB_SaveChange_Click;

        }


        private void SB_LevelShift_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {
            LevelSystem currentLevelSystem = new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS.GetLevelSystem();

            int new_level = 0;
            double value = e.NewValue;

            if (value >= 0)
            {
                new_level = (int)(((CoreCanvasSwitchData.Get_CORECANVAS_LEVEL_CAP - 101) * value) / 100);
            }
            else 
            {
                new_level = (int)(((CoreCanvasSwitchData.Get_CORECANVAS_LEVEL_CAP - 101) * value) * 1 / 100);
                new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS.SetVisibility(new_level, RB_State);
            }            

            CL_current_level.setText(new_level.ToString());
            currentLevelSystem.SetCurrentLevel(new_level);

            new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS.GetLevelBar().update(currentLevelSystem.Get_CURRENT_LEVEL());
        }

        private void SB_LevelShift_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void RB_Range_Checked(object sender, RoutedEventArgs e)
        {

            SP_Range_CVCs.Visibility = Visibility.Visible;

            RB_State = "range";

            LevelSystem currentLevelSystem = new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS.GetLevelSystem();
            currentLevelSystem.SetVisibilityMODE(RB_State);
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
            RB_State = "all";

            LevelSystem currentLevelSystem = new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS.GetLevelSystem();
            currentLevelSystem.SetVisibilityMODE(RB_State);
            new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS.SetVisibility(currentLevelSystem.Get_LEVEL(), RB_State);

            e.Handled = true;
        }
    }
}
/*  END OF FILE
 * 
 * 
 */