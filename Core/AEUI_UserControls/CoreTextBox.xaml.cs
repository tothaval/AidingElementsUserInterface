/* Aiding Elements User Interface
 *      CoreTextBox element 
 * 
 * basic configurable textbox element
 * 
 * init:        2023|12|01
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

namespace AidingElementsUserInterface.Core
{
    /// <summary>
    /// Interaktionslogik für CoreTextBox.xaml
    /// </summary>
    public partial class CoreTextBox : UserControl
    {
        private TextBoxData config;

        public CoreTextBox()
        {
            InitializeComponent();

            build();
        }

        public CoreTextBox(string content)
        {
            InitializeComponent();

            textbox.Text = content;

            build();
        }

        private void build()
        {
            Data_Handler data_Handler = new SharedLogic().GetDataHandler();

            config = data_Handler.LoadTextBoxData();

            border.Background = new SolidColorBrush(config.background);
            border.BorderBrush = new SolidColorBrush(config.borderbrush);
            border.CornerRadius = config.cornerRadius;
            border.BorderThickness = config.thickness;

            textbox.Background = new SolidColorBrush(Colors.Transparent);
            textbox.Foreground = new SolidColorBrush(config.foreground);

            textbox.CaretBrush = new SolidColorBrush(config.foreground);            

            textbox.Padding = new Thickness(7, 3, 7, 3);
        }


        // element design and functionality
        #region element design and functionality
        public void _acceptsReturn()
        {
            textbox.AcceptsReturn = true;
        }

        public void _acceptsTab()
        {
            textbox.AcceptsTab = true;
        }

        public void _disabled()
        {
            textbox.IsEnabled = false;
        }

        public void _enabled()
        {
            textbox.IsEnabled = true;
        }

        public void _fontfamily()
        {
            textbox.FontFamily = FontFamily;
        }

        public void _fontsize()
        {
            textbox.FontSize = FontSize;
        }

        public void _heightLimitationBasedOnMainWindow(UserControl initiator, double limitationValue)
        {

            //uie_textbox.MaxWidth = mainWindow.MainWindowCanvas.RenderSize.Width / 4;  //mainWindow.last_width;
            textbox.MaxHeight = new SharedLogic().GetMainWindow().RenderSize.Height * limitationValue; //mainWindow.last_height;
        }

        public void _heightLimitationBasedOnUserControl(UserControl usc, double limitationValue)
        {

            //uie_textbox.MaxWidth = mainWindow.MainWindowCanvas.RenderSize.Width / 4;  //mainWindow.last_width;
            textbox.MaxHeight = usc.RenderSize.Height * limitationValue; //mainWindow.last_height;
        }

        public void _no_borders()
        {
            border.BorderThickness = new Thickness(0);
            //border.BorderBrush = config.return_TransparentSolidColorBrush();

            textbox.BorderThickness = new Thickness(0);
            //textbox.BorderBrush = config.return_TransparentSolidColorBrush();
        }

        public void _readonly_no_caret()
        {
            textbox.IsReadOnly = true;
            textbox.IsReadOnlyCaretVisible = false;
        }

        public void _readonly_caret()
        {
            textbox.IsReadOnly = true;
            textbox.IsReadOnlyCaretVisible = true;
        }

        public void _scrolling()
        {
            textbox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            textbox.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
        }

        public void _widthLimitationBasedOnMainWindow(UserControl initiator, double limitationValue)
        {
            //uie_textbox.MaxWidth = mainWindow.MainWindowCanvas.RenderSize.Width / 4;  //mainWindow.last_width;
            textbox.MaxWidth = new SharedLogic().GetMainWindow().RenderSize.Width * limitationValue; //mainWindow.last_height;
        }

        public void _widthLimitationBasedOnUserControl(UserControl usc, double limitationValue)
        {

            //uie_textbox.MaxWidth = mainWindow.MainWindowCanvas.RenderSize.Width / 4;  //mainWindow.last_width;
            textbox.MaxWidth = usc.RenderSize.Width * limitationValue; //mainWindow.last_height;
        }

        public void _writeable()
        {
            textbox.IsReadOnly = false;
            textbox.IsReadOnlyCaretVisible = true;
        }

        public void appendText(string text)
        {
            textbox.AppendText(text);
        }

        public void clearText()
        {
            textbox.Clear();
        }

        public string getText()
        {
            if (textbox.SelectionLength > 0)
            {
                return textbox.SelectedText;
            }
            else
            {
                return textbox.Text;
            }
        }

        public void setChar(char c)
        {
            textbox.Text = c.ToString();
        }

        public void setText(string text)
        {
            textbox.Text = text;
        }
        #endregion element design and functionality


        // element events
        #region element events
        // CORE_TextBox
        #region CORE_TextBox
        private void CORE_TextBox_Loaded(object sender, RoutedEventArgs e)
        {

        }
        #endregion CORE_TextBox
        #endregion element events


    }
}
/*  END OF FILE
 * 
 * 
 */