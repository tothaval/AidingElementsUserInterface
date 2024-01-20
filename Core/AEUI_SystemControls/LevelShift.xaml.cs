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
        CanvasData config;

        public LevelShift()
        {
            InitializeComponent();

            build();
            registerEvents();
        }

        private void build()
        {
            Data_Handler data_Handler = new SharedLogic().GetDataHandler();
            config = data_Handler.LoadCanvasData();

            if (config == null)
            {
                config = new CanvasData();
            }

            CVC_Min_Level.setIdentifier("lowest level");
            CVC_Min_Level.setText((-1 * (int)(CoreCanvasSwitchData.Get_CORECANVAS_LEVEL_CAP/2)).ToString());
            CVC_Max_Level.setIdentifier("upmost level");
            CVC_Min_Level.setText(((int)(CoreCanvasSwitchData.Get_CORECANVAS_LEVEL_CAP / 2)).ToString());
            
            CVC_Min_Range.setIdentifier("lowest range");
            //CVC_Min_Level.setText(config.z_level_MIN.ToString());
            CVC_Min_Level.setText("-100");

            CVC_Max_Range.setIdentifier("upmost range");
            //CVC_Min_Level.setText(config.z_level_MIN.ToString());
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
            //int element_count = new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS.canvas.Children.Count - 1;

            //int value_range = 0;

            //int min_range = Int32.Parse(CVC_Min_Range.Value);
            //int max_range = Int32.Parse(CVC_Max_Range.Value);

            //value_range = config.z_level_MAX - config.z_level_MIN + 1;

            //CVC_Max_Range.setText(value_range.ToString());

            //double value = e.NewValue;

            //int new_level = (int)(value_range * value / 100);

            //e.Handled = true;

            //Application.Current.Resources["CurrentLevel"] = new_level;

            //CL_current_level.setText(new_level.ToString());

            //LevelBar levelBar = new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS._LevelBar;

            //string visible = "";

            //if (new_level < min_range || new_level > max_range) // add Range values, make LevelData class
            //{
            //    visible = "hidden";
            //}
            //else
            //{
            //    visible = "visible";
            //}

            //levelBar.update($"{new_level}", $"level {new_level}", "purpose: ", visible, $"{element_count}");
        }

        private void SB_LevelShift_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void RB_Range_Checked(object sender, RoutedEventArgs e)
        {
            SP_Range_CVCs.Visibility = Visibility.Visible;
        }

        private void RB_Range_Unchecked(object sender, RoutedEventArgs e)
        {
            SP_Range_CVCs.Visibility = Visibility.Collapsed;
        }

        private void RB_Level_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RB_All_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
/*  END OF FILE
 * 
 * 
 */