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
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AidingElementsUserInterface.Core
{
    /// <summary>
    /// Interaktionslogik für CoreTextBox.xaml
    /// </summary>
    public partial class CoreTextBox : UserControl
    {
        private CoreData config;

        public CoreTextBox()
        {
            InitializeComponent();

            build();
        }

        public CoreTextBox(bool no_limits)
        {
            InitializeComponent();

            build(no_limits);
        }

        public CoreTextBox(string content)
        {
            InitializeComponent();

            textbox.Text = content;

            build();
        }

        private void build(bool no_limits = false)
        {
            Data_Handler data_Handler = new SharedLogic().GetDataHandler();

            config = data_Handler.LoadTextBoxData(); 
            data_Handler.AddTextBoxData(config);

            if (config == null)
            {
                config = new CoreData();
            }


            if (no_limits)
            {
                this.Resources["TextBoxData_MinWidth"] = config.width;
                this.Resources["TextBoxData_MinHeight"] = config.height;
            }
            else
            {
                this.Resources["TextBoxData_MinWidth"] = config.width;
                this.Resources["TextBoxData_MinHeight"] = config.height;

                this.Resources["TextBoxData_MaxWidth"] = config.width * 2;
                this.Resources["TextBoxData_MaxHeight"] = config.height;
            }

            textbox.Padding = new Thickness(7, 3, 7, 3);

            TextBoxDataResources();
        }

        private void TextBoxDataResources()
        {
            CoreData textBoxData = config;

            if (textBoxData == null)
            {
                textBoxData = new CoreData();
            }

            __CoreTextBox.Resources["TextBoxData_background"] = textBoxData.background.GetBrush();
            __CoreTextBox.Resources["TextBoxData_borderbrush"] = textBoxData.borderbrush.GetBrush();
            __CoreTextBox.Resources["TextBoxData_foreground"] = textBoxData.foreground.GetBrush();
            __CoreTextBox.Resources["TextBoxData_highlight"] = textBoxData.highlight.GetBrush();
            
            __CoreTextBox.Resources["TextBoxData_cornerRadius"] = textBoxData.cornerRadius;
            __CoreTextBox.Resources["TextBoxData_thickness"] = textBoxData.thickness;
            
            __CoreTextBox.Resources["TextBoxData_fontSize"] = (double)textBoxData.fontSize;
            __CoreTextBox.Resources["TextBoxData_fontFamily"] = textBoxData.fontFamily;
            
            __CoreTextBox.Resources["TextBoxData_width"] = textBoxData.width;
            __CoreTextBox.Resources["TextBoxData_height"] = textBoxData.height;

            if (File.Exists(textBoxData.imageFilePath))
            {
                __CoreTextBox.Resources["TextBoxData_image"] = new ImageBrush(new BitmapImage(new Uri(textBoxData.imageFilePath)));
                __CoreTextBox.Resources["TextBoxData_background"] = __CoreTextBox.Resources["TextBoxData_image"];
            }
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
            bool test = double.TryParse(text, out double result);

            if (test)
            {
                textbox.Text = result.ToString("#.##");
            }

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