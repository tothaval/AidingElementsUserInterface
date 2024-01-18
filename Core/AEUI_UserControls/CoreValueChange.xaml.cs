/* Aiding Elements User Interface
 *      CoreValueChange user control
 * 
 * basic input element with a textblock and a textbox
 * 
 * init:        2024|01|11
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.Auxiliaries;
using AidingElementsUserInterface.Elements;
using Microsoft.VisualBasic.Logging;
using Microsoft.Win32;
using System.Drawing;
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
        internal CoreButton coreButton;

        private bool has_button = false;
        private bool prevent_default_click = false;

        internal object _object;
        internal string value;

        internal object _Object => _object;
        internal string Value => value;

        public CoreValueChange()
        {
            InitializeComponent();

            build();
        }

        public CoreValueChange(string value_identifier)
        {
            InitializeComponent();

            TB_value_identifier.setText(value_identifier);
            //CTB_Value.textbox.HorizontalContentAlignment = HorizontalAlignment.Right;
            CTB_Value.textbox.TextAlignment = TextAlignment.Right;

            build();
        }

        public CoreValueChange(bool has_button, bool prevent_default_click, bool no_limits, string value_identifier)
        {
            this.has_button = has_button;
            this.prevent_default_click = prevent_default_click;

            InitializeComponent();

            if (has_button)
            {
                CTB_Value.Visibility = Visibility.Collapsed;

                if (no_limits)
                {
                    coreButton = new CoreButton(true);
                    // so it blends in with the surrounding textboxes
                    coreButton.button.Width = coreButton.config.width * 3;
                    coreButton.button.MaxHeight = coreButton.config.height * 3;
                    //coreButton.button.Width = coreButton.config.width * 3;
                    //coreButton.button.Height = coreButton.config.height * 3;
                }
                else
                {
                    coreButton = new CoreButton(false);

                    CoreData textBoxData = new SharedLogic().GetDataHandler().GetTextBoxData();
                    if (textBoxData != null)
                    {
                        // so it blends in with the surrounding textboxes
                        coreButton.button.MaxWidth = textBoxData.width;
                        coreButton.button.MaxHeight = textBoxData.height;
                    }
                }

                coreButton.Visibility = Visibility.Visible;
                coreButton.setContent("-");
            }

            TB_value_identifier.setText(value_identifier);

            build();
        }

        public CoreValueChange(string value_identifier, bool has_button, bool prevent_default_click = false)
        {
            this.has_button = has_button;
            this.prevent_default_click = prevent_default_click;

            InitializeComponent();

            if (has_button)
            {
                coreButton = new CoreButton(false);

                CoreData textBoxData = new SharedLogic().GetDataHandler().GetTextBoxData();
                if (textBoxData != null)
                {
                    // so it blends in with the surrounding textboxes
                    coreButton.button.MaxWidth = textBoxData.width;
                    coreButton.button.MaxHeight = textBoxData.height;
                }

                CTB_Value.Visibility = Visibility.Collapsed;

                coreButton.Visibility = Visibility.Visible;

                coreButton.setContent("-");

            }

            TB_value_identifier.setText(value_identifier);

            build();
        }

        private void build()
        {
            if (has_button)
            {
                border_CB.Child = coreButton;

                if (prevent_default_click) return;

                coreButton.button.Click += Button_Click;

            }
            else
            {
                CTB_Value.textbox.GotFocus += textbox_GotFocus;
                CTB_Value.textbox.GotKeyboardFocus += textbox_GotKeyboardFocus;
                CTB_Value.textbox.KeyDown += textbox_KeyDown;
                CTB_Value.textbox.KeyUp += textbox_KeyUp;
                CTB_Value.textbox.MouseDoubleClick += textbox_MouseDoubleClick;
            }
        }


        internal void setButtonConfiguration(string value_identifier, bool has_button, bool prevent_default_click)
        {
            this.has_button = has_button;
            this.prevent_default_click = prevent_default_click;

            if (has_button)
            {
                coreButton = new CoreButton(false);

                CoreData textBoxData = new SharedLogic().GetDataHandler().GetTextBoxData();
                if (textBoxData != null)
                {
                    // so it blends in with the surrounding textboxes
                    coreButton.button.MaxWidth = textBoxData.width;
                    coreButton.button.MaxHeight = textBoxData.height;
                }

                CTB_Value.Visibility = Visibility.Collapsed;

                coreButton.Visibility = Visibility.Visible;

                coreButton.setContent("-");

            }

            TB_value_identifier.setText(value_identifier);

            build();

        }

        internal void setIdentifier(string value_identifier)
        {
            TB_value_identifier.setText(value_identifier);
        }

        internal void setObject(object _object)
        {
            this._object = _object;
        }

        internal void setText(string text)
        {
            if (!has_button)
            {
                CTB_Value.setText(text);

                value = text;
            }
            else
            {
                if (prevent_default_click)
                {
                    coreButton.setTooltip(text);
                    coreButton.setContent(text);

                    return;
                }

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.FileName = text;

                if (openFileDialog.CheckPathExists)
                {
                    coreButton.setContent(openFileDialog.SafeFileName);
                    coreButton.setTooltip(text);

                    value = text;
                }
                else
                {
                    coreButton.setTooltip(text);
                    coreButton.setContent(text);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new SharedLogic().openDialog();

            if (file != null)
            {
                coreButton.setContent(file.SafeFileName);
                coreButton.setTooltip(file.FileName);

                value = file.FileName;
            }
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
                value = CTB_Value.getText();
            }
        }

        private void textbox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void UserControl_Initialized(object sender, System.EventArgs e)
        {

        }
    }
}
/*  END OF FILE
 * 
 * 
 */