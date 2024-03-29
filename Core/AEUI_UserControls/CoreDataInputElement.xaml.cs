﻿/* Aiding Elements User Interface
 *      CoreDataInputElement element 
 * 
 * basic element for entering and display of linkData
 * 
 * init:        2023|12|03
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 * 
 * origin:      FlatShareCostCalculator_2023_11_18(MIT-license) https://github.com/tothaval/Flat-Share-CC
 */
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

/*  END OF FILE
 * 
 * 
 */
namespace AidingElementsUserInterface.Core
{
    /// <summary>
    /// Interaktionslogik für CoreDataInputElement.xaml
    /// </summary>
    public partial class CoreDataInputElement : UserControl
    {
        internal int id { get; set; }
        internal object value { get; set; }

        internal bool _datetime { get; set; }
        internal bool _double { get; set; }        
        internal bool _integer { get; set; }
        internal bool _string { get; set; }

        public CoreDataInputElement()
        {
            InitializeComponent();

            _datetime = false;
            _double = false;
            _integer = false;
            _string = false;

            id = -1;

            TB_L.Text = "left";
            TX.Text = "0.0";
            TX.IsReadOnly = false;
            TB_R.Text = "right";
        }


        internal void configurate(string tb_left, bool tx_readonly, string value, string tb_right)
        {
            TB_L.Text = tb_left;

            TX.IsReadOnly = tx_readonly;
            TX.Text = value;

            TB_R.Text = tb_right;
        }

        internal void configurate(string tb_left, bool tx_readonly, string tb_right)
        {
            TB_L.Text = tb_left;

            TX.IsReadOnly = tx_readonly;

            TB_R.Text = tb_right;
        }


        internal void focus_and_select()
        {
            TX.SelectAll();
            TX.Focus();
        }

        internal void insert_value<T>(T insert, int decimal_places)
        {
            if (insert is double is_double)
            {
                value = is_double;

                TX.Text = is_double.ToString();

                if (decimal_places > -1)
                {
                    TX.Text = is_double.ToString($"F{decimal_places}");
                }
            }
        }
        internal void insert_value<T>(T insert, bool is_currency, bool timeframe_hours)
        {
            if (insert is double is_double)
            {
                value = is_double;

                if (is_currency)
                {
                    TX.Text = $"{is_double:F2}";
                }
            }

            if (insert is DateTime is_datetime)
            {
                value = is_datetime;

                if (timeframe_hours)
                {
                    TX.Text = $"{value}";
                }
                else
                {
                    TX.Text = $"{value:d}";
                }

            }
        }

        internal void insert_value<T>(T insert)
        {

            if (insert is string is_string)
            {
                value = is_string;
                TX.Text = is_string;
            }

            else if (insert is int is_int)
            {
                value = is_int;
                TX.Text = $"{is_int}";
            }

            else if (insert is DateTime is_datetime)
            {
                value = is_datetime;

                TX.Text = $"{value}";
            }

            else if (insert is double is_double)
            {
                value = is_double;

                TX.Text = $"{is_double}";
            }
        }

        internal void parse_value()
        {
            if (_datetime)
            {
                if (DateTime.TryParse(TX.Text, out DateTime is_datetime))
                {
                    value = is_datetime;
                }
                else
                {
                    MessageBox.Show("DateTime expected");
                    TX.Text = $"{DateTime.Now:d}";

                    parse_value();
                }
            }
            else if (_double)
            {
                if (Double.TryParse(TX.Text, out double is_double))
                {
                    value = is_double;
                }
                else if (Int32.TryParse(TX.Text, out int _int_while_double))
                {
                    value = (double)_int_while_double;
                }
                else
                {
                    MessageBox.Show("Double expected");
                    TX.Text = "0";

                    parse_value();
                }
            }
            else if (_integer)
            {
                if (Int32.TryParse(TX.Text, out int is_integer))
                {
                    value = is_integer;
                }
                else
                {
                    MessageBox.Show("Integer expected");
                    TX.Text = "0";

                    parse_value();
                }

            }
            else if (_string)
            {
                value = TX.Text;
            }

        }

        internal void set_tb_left_color(SolidColorBrush tb_left_color)
        {
            TB_L.Foreground = tb_left_color;
        }

        internal void set_tb_right_color(SolidColorBrush tb_right_color)
        {
            TB_R.Foreground = tb_right_color;
        }

        internal void set_tx_color(SolidColorBrush tx_color)
        {
            TX.Foreground = tx_color;
        }

        internal void set_tx_fontweight(FontWeight fontWeight)
        {
            TX.FontWeight = fontWeight;
        }

        internal void set_input_value_type<T>(T value_type)
        {
            _datetime = false;
            _double = false;
            _integer = false;
            _string = false;

            if (value_type is DateTime is_datetime)
            {
                _datetime = true;
            }
            else if (value_type is double is_double)
            {
                _double = true;
            }
            else if (value_type is int is_int)
            {
                _integer = true;
            }
            else if (value_type is string is_string)
            {
                _string = true;
            }
            else
            {
                TX.IsReadOnly = true;
                TX.Text = "TYPE ERROR";
            }

            parse_value();
        }


        private void TX_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                parse_value();
            }
        }

        private void TX_GotFocus(object sender, RoutedEventArgs e)
        {
            TX.SelectAll();

            e.Handled = true;
        }

        private void TX_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TX.SelectAll();

            e.Handled = true;
        }

        private void TX_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                TX.SelectAll();

                e.Handled = true;
            }
        }

        private void TX_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                e.Handled = true;
            }
        }
    }
}
/*  END OF FILE
 * 
 * 
 */