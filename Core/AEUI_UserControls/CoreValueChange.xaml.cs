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
        private bool has_button = false;
        private CoreButton coreButton;

        internal string value;
        internal string Value => value;

        public CoreValueChange()
        {
            InitializeComponent();

            build();
        }

        public CoreValueChange(string value_identifier)
        {
            InitializeComponent();

            TB_value_identifier.Text = value_identifier;
            //CTB_Value.textbox.HorizontalContentAlignment = HorizontalAlignment.Right;
            CTB_Value.textbox.TextAlignment = TextAlignment.Right;

            build();
        }

        public CoreValueChange(string value_identifier, bool has_button)
        {
            this.has_button = has_button;

            InitializeComponent();            

            if (has_button)
            {
                CTB_Value.Visibility = Visibility.Collapsed;

                coreButton = new CoreButton("-");
                TextBoxData textBoxData = new SharedLogic().GetDataHandler().GetTextBoxData();

                if (textBoxData != null)
                {
                    // so it blends in with the surrounding textboxes
                    coreButton.button.MaxWidth = textBoxData.width;
                    coreButton.button.MaxHeight = textBoxData.height;

                    coreButton.config.imageFilePath = textBoxData.imageFilePath;
                    coreButton._backgroundImage();
                }

                SP_ValueChangeElement.Children.Add(coreButton);
            }                        

            TB_value_identifier.Text = value_identifier;

            build();
        }

        private void build()
        {
            if (has_button)
            {
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

        internal void setIdentifier(string value_identifier)
        {
            TB_value_identifier.Text = value_identifier;
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
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.FileName = text;

                if (openFileDialog.CheckPathExists)
                {
                    coreButton.setContent(openFileDialog.SafeFileName);
                    coreButton.setTooltip(text);
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
    }
}
/*  END OF FILE
 * 
 * 
 */