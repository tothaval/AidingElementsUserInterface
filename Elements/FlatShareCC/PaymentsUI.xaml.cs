/* Aiding Elements User Interface
 *      FlatShareCC element 
 *          PaymentsUI user control
 * 
 * init:        2023|12|03
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 * 
 * origin:      FlatShareCostCalculator_2023_11_18(MIT-license) https://github.com/tothaval/Flat-Share-CC
 */
using AidingElementsUserInterface.Core;
using AidingElementsUserInterface.Core.Auxiliaries;
using AidingElementsUserInterface.Core.FlatShareCC_Data;

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AidingElementsUserInterface.Elements.FlatShareCC
{
    /// <summary>
    /// Interaktionslogik für PaymentsUI.xaml
    /// </summary>
    public partial class PaymentsUI : UserControl
    {
        internal List<int> months { get; set; }
        internal List<double> payment { get; set; }
        List<StackPanel> payment_lines { get; set; }

        private System.Windows.Threading.DispatcherTimer _timer = new System.Windows.Threading.DispatcherTimer();
        internal BillingPeriodData billingPeriodData { get; set; }

        public PaymentsUI()
        {

        }

        internal PaymentsUI(BillingPeriodData periodData)
        {
            months = new List<int>();
            payment = new List<double>();

            payment_lines = new List<StackPanel>();

            billingPeriodData = periodData;

            InitializeComponent();

            MainStackPanel.Children.Add(add_payment_line());

            LL_room_name.Content = $"{billingPeriodData.room.name} - {billingPeriodData.room.area}{Units.area}";

            //if (loading)
            //{
            //    _timer.Tick += _timer_Tick;
            //    _timer.Interval = TimeSpan.FromSeconds(0.28);
            //    _timer.Start();
            //}
        }


        // generate and configure UserControlBars

        private void _timer_Tick(object sender, EventArgs e)
        {
            MainStackPanel.Children.Remove(MainStackPanel.Children[1]);

            //load_payments();

            _timer.Stop();
        }

        private StackPanel add_payment_line()
        {
            StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Horizontal };

            CoreDataInputElement DIE_months = new CoreDataInputElement();
            CoreDataInputElement DIE_payment = new CoreDataInputElement();

            DIE_months.configurate("months", false, "12", "x");
            DIE_payment.configurate("payment", false, "0", Units.currency);

            int x = 0;
            DIE_months.set_input_value_type(x);
            DIE_payment.set_input_value_type(0.0);

            DIE_months.KeyUp += DIE_months_KeyUp;
            DIE_payment.KeyUp += DIE_payment_KeyUp;

            stackPanel.Children.Add(DIE_months);
            stackPanel.Children.Add(DIE_payment);

            payment_lines.Add(stackPanel);

            return stackPanel;
        }

        internal void load_payments(BillingPeriodData data)
        {
            MainStackPanel.Children.Clear();

            List<double> o = new List<double>();
            List<int> u = new List<int>();

            int c = 0;

            foreach (double payment in data.monthly_payments)
            {

                if (!o.Contains(payment))
                {
                    c = 0;

                    o.Add(payment);
                    u.Add(c);
                }

                if (o.Contains(payment))
                {
                    u[o.IndexOf(payment)] += 1;
                }
                c++;
            }

            foreach (double d in o)
            {
                StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Horizontal };

                CoreDataInputElement DIE_months = new CoreDataInputElement();
                CoreDataInputElement DIE_payment = new CoreDataInputElement();

                DIE_months.configurate("months", false, "12", "x");
                DIE_payment.configurate("payment", false, "0", Units.currency);

                int x = 0;
                DIE_months.set_input_value_type(x);
                DIE_payment.set_input_value_type(0.0);

                DIE_months.KeyUp += DIE_months_KeyUp;
                DIE_payment.KeyUp += DIE_payment_KeyUp;

                DIE_months.insert_value(u[o.IndexOf(d)]);

                DIE_payment.insert_value(d, true, false);

                stackPanel.Children.Add(DIE_months);
                stackPanel.Children.Add(DIE_payment);

                payment_lines.Add(stackPanel);
                MainStackPanel.Children.Add(stackPanel);
            }
        }

        public void change_room_name(string room_name)
        {
            LL_room_name.Content = room_name;
        }

        public void save_payments()
        {
            billingPeriodData.monthly_payments.Clear();

            foreach (StackPanel payment_line in payment_lines)
            {
                CoreDataInputElement months = (CoreDataInputElement)payment_line.Children[0];
                CoreDataInputElement payment = (CoreDataInputElement)payment_line.Children[1];

                for (int i = 0; i < (int)months.value; i++)
                {
                    billingPeriodData.monthly_payments.Add((double)payment.value);
                }
            }
        }

        private void DIE_payment_KeyUp(object sender, KeyEventArgs e)
        {
            CoreDataInputElement payment_die = (CoreDataInputElement)sender;

            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                payment.Add((double)payment_die.value);
            }
        }

        private void DIE_months_KeyUp(object sender, KeyEventArgs e)
        {
            CoreDataInputElement months_die = (CoreDataInputElement)sender;

            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                int check = 0;

                months.Add((int)months_die.value);

                foreach (int value in months)
                {
                    check += value;
                }


                if (check < 12)
                {
                    int diff = 12 - check;

                    MainStackPanel.Children.Add(add_payment_line());

                    ((CoreDataInputElement)payment_lines[payment_lines.Count - 1].Children[0]).insert_value(diff);

                    months_die.TX.IsEnabled = false;

                }
                else if (check > 12)
                {
                    MessageBox.Show($"{check}\nmax value = 12");

                    months.Remove((int)months_die.value);
                }
                else
                {
                    months_die.TX.IsEnabled = false;
                }


                StackPanel parent = months_die.Parent as StackPanel;

                ((CoreDataInputElement)parent.Children[1]).TX.Focus();

                e.Handled = true;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

    }
}
/*  END OF FILE
 * 
 * 
 */