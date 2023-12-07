using AidingElementsUserInterface.Core;
using AidingElementsUserInterface.Core.Auxiliaries;
using AidingElementsUserInterface.Core.FlatShareCC_Data;
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

namespace AidingElementsUserInterface.Elements.FlatShareCC
{
    /// <summary>
    /// Interaktionslogik für InitialCostsUI.xaml
    /// </summary>
    public partial class InitialCostsUI : UserControl
    {
        internal FlatCosts flatCosts = new FlatCosts();

        public InitialCostsUI()
        {
            InitializeComponent();

            setup_ui();
        }

        private void fresh_state()
        {
            BTN_reset.IsEnabled = false;
            BTN_reset.Visibility = Visibility.Collapsed;
            SP_initial_costs_result.Children.Clear();
        }

        private void show_costs()
        {
            FlatData flatData = SharedLogic.GetFlatShareCC().getFlatData();

            SP_initial_costs_result.Children.Clear();

            int counter = 0;

            foreach (RoomData room in flatData.rooms)
            {
                RentOutputUI outputUI = new RentOutputUI(room.id, flatData, flatCosts);

                if (counter % 2 == 0)
                {
                    outputUI.Background = new SolidColorBrush(Colors.AntiqueWhite);
                    outputUI.Foreground = new SolidColorBrush(Colors.SlateGray);
                }
                else
                {
                    outputUI.Background = new SolidColorBrush(Colors.SlateGray);
                    outputUI.Foreground = new SolidColorBrush(Colors.AntiqueWhite);
                }

                SP_initial_costs_result.Children.Add(outputUI);

                counter++;
            }
        }

        internal void focus()
        {
            FlatShareCC origin = SharedLogic.GetFlatShareCC();

            origin.Tab_InitialCosts.Focus();

            DIE_cold_rent.focus_and_select();
        }

        internal void load_data(FlatCosts costs)
        {
            this.flatCosts = costs;

            setup_ui();

            save_state();
        }

        private void save_state()
        {
            BTN_reset.IsEnabled = true;
            BTN_reset.Visibility = Visibility.Visible;

            show_costs();
        }

        private void setup_ui()
        {
            DIE_cold_rent.configurate("rent", false, $"{flatCosts.cold_rent:#.00}", $"{Units.currency}");
            DIE_cold_rent.set_input_value_type(0.0);

            DIE_cold_rent.KeyDown += DIE_cold_rent_KeyDown; ;

            DIE_extra_costs_advance.configurate("costs", false, $"{flatCosts.extra_costs_advance:#.00}", $"{Units.currency}");
            DIE_extra_costs_advance.set_input_value_type(0.0);

            DIE_extra_costs_advance.KeyDown += DIE_extra_costs_advance_KeyDown; ;
        }

        private void BTN_reset_Click(object sender, RoutedEventArgs e)
        {
            fresh_state();
        }


        private void BTN_save_data_Click(object sender, RoutedEventArgs e)
        {
            FlatShareCC origin = SharedLogic.GetFlatShareCC();

            origin.save_data(flatCosts);

            save_state();
        }

        private void DIE_cold_rent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                flatCosts.cold_rent = (double)((CoreDataInputElement)sender).value;

                DIE_cold_rent.ToolTip = flatCosts.cold_rent.ToString();

                DIE_extra_costs_advance.focus_and_select();
            }
        }

        private void DIE_extra_costs_advance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                flatCosts.extra_costs_advance = (double)((CoreDataInputElement)sender).value;

                DIE_cold_rent.ToolTip = flatCosts.extra_costs_advance.ToString();

                BTN_save_data.Focus();
            }
        }
    }
}
