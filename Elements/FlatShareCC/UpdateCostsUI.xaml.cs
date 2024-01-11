/* Aiding Elements User Interface
 *      FlatShareCC element 
 *          UpdateCostsUI user control
 * 
 * init:        2023|12|03
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 * 
 * origin:      FlatShareCostCalculator_2023_11_18(MIT-license) https://github.com/tothaval/Flat-Share-CC
 */
using AidingElementsUserInterface.Core.FlatShareCC_Data;

using System;
using System.Windows;
using System.Windows.Controls;

namespace AidingElementsUserInterface.Elements.FlatShareCC
{
    /// <summary>
    /// Interaktionslogik für UpdateCostsUI.xaml
    /// </summary>
    public partial class UpdateCostsUI : UserControl
    {
        public UpdateCostsUI()
        {
            InitializeComponent();


        }

        internal void add_UpdateCosts()
        {
            UpdateCosts updateCosts = new UpdateCosts("cost update", DateTime.Now)
            {
                FontSize = 16,
                FontWeight = FontWeights.Bold
            };

            TabItem tabItem = new TabItem();

            tabItem.Content = updateCosts;

            MainTabControl.Items.Insert(0, tabItem);
            tabItem.Focus();
            tabItem.Header = updateCosts.header_string();

            if (BTN_remove_active_cost_update.Visibility == Visibility.Hidden)
            {
                BTN_remove_active_cost_update.Visibility = Visibility.Visible;
            }
        }

        internal void add_UpdateCosts(CostUpdateData costUpdateData)
        {
            UpdateCosts updateCosts = new UpdateCosts(costUpdateData)
            {
                FontSize = 16,
                FontWeight = FontWeights.Bold
            };


            TabItem tabItem = new TabItem();

            tabItem.Content = updateCosts;

            MainTabControl.Items.Insert(0, tabItem);
            updateCosts.Focus();
            tabItem.Header = updateCosts.header_string();

            if (BTN_remove_active_cost_update.Visibility == Visibility.Hidden)
            {
                BTN_remove_active_cost_update.Visibility = Visibility.Visible;
            }
        }



        private void BTN_add_cost_update_Click(object sender, RoutedEventArgs e)
        {
            add_UpdateCosts();
        }

        private void BTN_remove_active_cost_update_Click(object sender, RoutedEventArgs e)
        {


            if (MainTabControl.Items.Count > 0 && MainTabControl.SelectedIndex > -1)
            {
                int index = MainTabControl.SelectedIndex;

                string question = "do you want to remove the\ncurrently active cost update tab?";
                string title = "removal of cost update tab";


                MessageBoxResult result = MessageBox.Show(question, title, MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    CostUpdateData costUpdate = ((UpdateCosts)MainTabControl.SelectedContent).costUpdateData;
                    costUpdate.origin_element.removeCostUpdate(costUpdate);
                    costUpdate.origin_element.ToolTip = costUpdate.origin_element.getCostUpdates().Count.ToString();
                    
                    MainTabControl.Items.RemoveAt(index);

                    if (MainTabControl.Items.Count == 0)
                    {
                        BTN_remove_active_cost_update.Visibility = Visibility.Hidden;
                    }
                }
            }
        }
    }
}
/*  END OF FILE
 * 
 * 
 */