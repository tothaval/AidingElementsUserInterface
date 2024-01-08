/* Aiding Elements User Interface
 *      Manual element 
 * 
 * manual and handling instructions element
 * 
 * init:        2024|01|07
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.Auxiliaries;
using AidingElementsUserInterface.Core;
using AidingElementsUserInterface.Texts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using AidingElementsUserInterface.Core.AEUI_UserControls;
using System.Windows.Controls;

namespace AidingElementsUserInterface.Elements
{
    public class Manual : CoreContainer
    {
        // global classes, properties and variables
        #region global classes, properties and variables            
        CoreTextBox textbox;
        private TXT_Manual info = new TXT_Manual();

        private List<StringBuilder> content_pages = new List<StringBuilder>();

        private int page_counter = 0;
        #endregion global classes, properties and variables

        // constructors
        #region constructors
        public Manual()
        {
            InitializeComponent();

            build();
        }
        #endregion constructors


        // element design and functionality
        #region element design and functionality
        private void build()
        {
            Data_Handler data_Handler = new SharedLogic().GetDataHandler();
            CoreData config = data_Handler.LoadCoreData();

            loadInfoTextStrings();

            CoreButton CB_PageLeft = new CoreButton("<");
            CoreButton CB_PageRight = new CoreButton(">");
            CB_PageLeft.button.Click += CB_PageLeft_Click;
            CB_PageRight.button.Click += CB_PageRight_Click;

            WrapPanel horizontalWrapPanel = new WrapPanel() { Margin = new Thickness(1)};
            horizontalWrapPanel.Children.Add(CB_PageLeft);
            horizontalWrapPanel.Children.Add(CB_PageRight);

            textbox = new CoreTextBox() { Margin = new Thickness(1)};
            textbox.setText(content_pages[0].ToString());
            textbox._readonly_no_caret();
            textbox._scrolling();

            WrapPanel verticalWrapPanel = new WrapPanel() { Orientation = Orientation.Vertical};
            verticalWrapPanel.Children.Add(horizontalWrapPanel);
            verticalWrapPanel.Children.Add(textbox);

            this.FontFamily = config.fontFamily;
            this.FontSize = config.fontSize;

            Background = new SolidColorBrush(Colors.Transparent);
            Foreground = new SolidColorBrush(config.foreground);

            content_border.Child = verticalWrapPanel;

            showManualPage();
        }

        // maybe work with lists to enlist textboxes and texts, which could simplify the page turning
        // and reduce the code to one instruction via iterationg through one list and using the values
        // of the other on page count up or down. not that high a priority atm.

        private void loadInfoTextStrings()
        {
            content_pages.Add(info.tb_NoticesAndHandlingInstructions());
            content_pages.Add(info.tb_NonAlphabetKeys());
            content_pages.Add(info.tb_AlphabetKeys_A_M());
            content_pages.Add(info.tb_AlphabetKeys_N_Z());
            content_pages.Add(info.tbVersion());
            content_pages.Add(info.tb_LicenseTerms());
            //content_pages.Add(info.);
            //content_pages.Add();
        }

        private void managePageCounter()
        {
            if (page_counter < 0)
            {
                page_counter = content_pages.Count - 1;
            }

            else if (page_counter > content_pages.Count - 1)
            {
                page_counter = 0;
            }
        }

        private void showManualPage()
        {
            textbox.setText(content_pages[page_counter].ToString());
        }
        #endregion element design and functionality


        // element events
        #region element events
        // UI_InfoElement
        #region UI_InfoElement
        private void UI_InfoElement_Loaded(object sender, RoutedEventArgs e)
        {
            showManualPage();
        }
        #endregion UI_InfoElement

        private void CB_PageLeft_Click(object sender, RoutedEventArgs e)
        {
            page_counter--;

            managePageCounter();
            showManualPage();
        }

        private void CB_PageRight_Click(object sender, RoutedEventArgs e)
        {
            page_counter++;

            managePageCounter();
            showManualPage();
        }
        #endregion element events


    }
}
/*  END OF FILE
 * 
 * 
 */