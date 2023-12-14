/* Aiding Elements User Interface
 *      Manual element 
 * 
 * manual and handling instructions element
 * 
 * init:        2023|12|01
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core;
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.Auxiliaries;
using AidingElementsUserInterface.Texts;
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

namespace AidingElementsUserInterface.Elements
{
    /// <summary>
    /// Interaktionslogik für Manual.xaml
    /// </summary>
    public partial class Manual : UserControl
    {
        // global classes, properties and variables
        #region global classes, properties and variables   
        public int containerElementIndex = 0;

        private TXT_Manual info;

        private CoreButton CB_PageLeft = new CoreButton("<");
        private CoreButton CB_PageRight = new CoreButton(">");
        private CoreTextBox textbox = new CoreTextBox();
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

            horizontalWrapPanel.Children.Add(CB_PageLeft);
            horizontalWrapPanel.Children.Add(CB_PageRight);

            verticalWrapPanel.Children.Add(textbox);

            __Manual.FontFamily = config.fontFamily;
            __Manual.FontSize = config.fontSize;

            Background = new SolidColorBrush(Colors.Transparent);
            Foreground = new SolidColorBrush(config.foreground);

            loadInfoTextStrings();
        }


        private void configureInfoElement()
        {

            CB_PageLeft.button.Click += CB_PageLeft_Click;
            CB_PageRight.button.Click += CB_PageRight_Click;

            textbox.setText(content_pages[0].ToString());
            textbox._readonly_no_caret();
            textbox._heightLimitationBasedOnMainWindow(__Manual, 0.75);
            textbox._widthLimitationBasedOnMainWindow(__Manual, 0.75);
            textbox._scrolling();

            showManualPage();
        }

        // maybe work with lists to enlist textboxes and texts, which could simplify the page turning
        // and reduce the code to one instruction via iterationg through one list and using the values
        // of the other on page count up or down. not that high a priority atm.

        private void loadInfoTextStrings()
        {
            info = new TXT_Manual();

            content_pages.Add(info.tb_NoticesAndHandlingInstructions());
            content_pages.Add(info.tb_NonAlphabetKeys());
            content_pages.Add(info.tb_AlphabetKeys_A_M());
            content_pages.Add(info.tb_AlphabetKeys_N_Z());
            content_pages.Add(info.tbVersion());
            content_pages.Add(info.tb_LicenseTerms());
            //content_pages.Add(info.);
            //content_pages.Add();

            configureInfoElement();
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

            if (page_counter == 0)
            {
                textbox.setText(content_pages[0].ToString());
            }
            else if (page_counter == 1)
            {
                textbox.setText(content_pages[1].ToString());
            }
            else if (page_counter == 2)
            {
                textbox.setText(content_pages[2].ToString());
            }
            else if (page_counter == 3)
            {
                textbox.setText(content_pages[3].ToString());
            }
            else if (page_counter == 4)
            {
                textbox.setText(content_pages[4].ToString());
            }
            else if (page_counter == 5)
            {
                textbox.setText(content_pages[5].ToString());
            }
        }
        #endregion element design and functionality


        // element events
        #region element events
        // UI_InfoElement
        #region UI_InfoElement
        private void UI_InfoElement_Loaded(object sender, RoutedEventArgs e)
        {

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