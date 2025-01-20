/* Aiding Elements User Interface
 *      FileLink element 
 * 
 * link any file to a fileLinkElement
 * 
 * init:        2024|01|03
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Microsoft.Win32;
using System.Drawing;
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.Auxiliaries;

namespace AidingElementsUserInterface.Elements
{
    /// <summary>
    /// Interaktionslogik für FileLink.xaml
    /// </summary>
    public partial class FileLink : UserControl
    {
        private bool linked = false;
        private bool loaded = false;

        private ContentData contentData = new ContentData();

        private LinkData linkData;
        private SharedLogic logic = new SharedLogic();

        public FileLink()
        {
            InitializeComponent();

            build();
            registerEvents();

            loaded = true;

            //L_LinkText.FontSize = new SharedLogic().GetDataHandler().GetCoreData().fontSize;
            //L_LinkText.FontSize = 9;
            //L_LinkText.FontFamily = new SharedLogic().GetMainWindow().mainWindowData.fontFamily;
        }

        internal FileLink(ContentData contentData)
        {
            this.contentData = contentData;

            InitializeComponent();

            load(contentData);
            registerEvents();

            loaded = true;
        }

        private void registerEvents()
        {
            CB_FileLink.button.Click += CB_FileLink_Click;
            CB_LinkButton.button.Click += CB_LinkButton_Click;
        }

        private void build()
        {
            CB_FileLink.setContent("file");

            CTB_LinkText.setText("linktext");

            CB_LinkButton.setContent("setup\nlink");
        }

        internal ContentData GetContentData()
        {
            return contentData;
        }

        internal void load(ContentData data)
        {
            string link = (string)data.GetValue("Link");
            string linkText = (string)data.GetValue("LinkText");

            this.ToolTip = link + "\n" + linkText;

            linkData = new LinkData(link, linkText);

            SP_Choice.Visibility = Visibility.Collapsed;

            CB_FileLink.setContent(linkData.GetLink);            

            CB_LinkButton.setTooltip(linkData.GetLink);
            CB_LinkButton.setContent(linkData.GetLinkText);

            //thx to https://www.brad-smith.info/blog/archives/164 for IconTools.cs
            Icon icon = IconTools.GetIconForFile(linkData.GetLink, ShellIconSize.LargeIcon);

            if (icon != null)
            {
                CB_LinkButton.setContent(icon);
            }

            this.contentData = data;

            //L_LinkText.Content = linkData.GetLinkText;

            CB_LinkButton.Visibility = Visibility.Visible;
            CTB_LinkText.Visibility = Visibility.Visible;
            //L_LinkText.Visibility = Visibility.Visible;       

            linked = true;
        }

        internal void initiate_link()
        {
            OpenFileDialog file = logic.openDialog();

            if (file != null)
            {
                linkData = new LinkData();
                linkData.SetLink(file.FileName);

                CB_FileLink.setContent(file.FileName);
                CB_FileLink.setTooltip(file.FileName);

                CTB_LinkText.setText(file.SafeFileName);

                CB_LinkButton.Visibility = Visibility.Visible;
                CTB_LinkText.Visibility = Visibility.Visible;
            }
        }

        private void reset()
        {
            SP_Choice.Visibility = Visibility.Visible;

            CB_LinkButton.setContent("setup\nlink");

            CB_LinkButton.Visibility = Visibility.Collapsed;
            CTB_LinkText.Visibility = Visibility.Collapsed;
            //L_LinkText.Visibility = Visibility.Collapsed;

            linked = false;
        }

        private void setup_choice()
        {
            linkData.SetLinkText(CTB_LinkText.getText());

            if (linkData.GetLink != null)
            {
                contentData.Clear();

                contentData.AddValue("Link", linkData.GetLink);
                contentData.AddValue("LinkText", linkData.GetLinkText);

                CB_LinkButton.setContent(linkData.GetLinkText);
                CB_LinkButton.setTooltip(linkData.GetLink);

                //thx to https://www.brad-smith.info/blog/archives/164 for IconTools.cs
                Icon icon = IconTools.GetIconForFile(linkData.GetLink, ShellIconSize.LargeIcon);

                if (icon != null)
                {
                    CB_LinkButton.setContent(icon);
                }
            }

            SP_Choice.Visibility = Visibility.Collapsed;

            //L_LinkText.Content = linkData.GetLinkText;
            //L_LinkText.Visibility = Visibility.Visible;

            linked = true;
        }

        private void CB_FileLink_Click(object sender, RoutedEventArgs e)
        {
            if (loaded)
            {
                initiate_link();
            }

            e.Handled = true;
        }
        private void CB_LinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                reset();
            }
            else
            {
                if (loaded)
                {
                    if (linked)
                    {
                        logic.executeLink(linkData.GetLink);
                    }
                    else
                    {
                        setup_choice();
                    }
                }
            }

            e.Handled = true;
        }
        private void __FileLink_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
/*  END OF FILE
 * 
 * 
 */