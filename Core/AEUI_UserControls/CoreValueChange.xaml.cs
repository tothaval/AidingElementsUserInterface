/* Aiding Elements User Interface
 *      CoreValueChange user control
 * 
 * basic input element with a textblock and a textbox
 * 
 * init:        2024|01|11
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AidingElementsUserInterface.Core.AEUI_UserControls
{
    /// <summary>
    /// Interaktionslogik für CoreValueChange.xaml
    /// </summary>
    public partial class CoreValueChange : UserControl
    {
        internal string value;
        internal string Value => value;

        public CoreValueChange()
        {
            InitializeComponent();
        }

        public CoreValueChange(string value_identifier)
        {
            InitializeComponent();

            TB_value_identifier.Text = value_identifier;
        }

        internal void setIdentifier(string value_identifier)
        {
            TB_value_identifier.Text = value_identifier;
        }

        internal void setText(string text)
        {
            textbox.Text = text;

            value = text;
        }


        private void textbox_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void textbox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {

        }

        private void textbox_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                value = textbox.Text;
            }
        }

        private void textbox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
/*  END OF FILE
 * 
 * 
 */