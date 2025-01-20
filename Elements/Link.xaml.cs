/* Aiding Elements User Interface
 *      Manual element 
 * 
 * link local paths or URLs to a fileLinkElement
 * 
 * init:        2024|01|03
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.Auxiliaries;

using Microsoft.Win32;

using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AidingElementsUserInterface.Elements
{
    /// <summary>
    /// Interaktionslogik für Link.xaml
    /// </summary>
    public partial class Link : UserControl
    {        
        private bool loaded = false;
        private ContentData contentData = new ContentData();

        private LinkData linkData;
        private SharedLogic logic = new SharedLogic();


        public Link()
        {
            InitializeComponent();

            build();
            registerEvents();

            loaded = true;
        }

        internal Link(ContentData contentData)
        {
            this.contentData = contentData;

            InitializeComponent();

            load(contentData);
            registerEvents();

            loaded = true;
        }

        private void registerEvents()
        {
            CB_FileLink.button.Click += CB_file_Click;
            CB_local.button.Click += CB_local_Click;
            CB_web.button.Click += CB_web_Click;
            CB_LinkButton.button.Click += CB_LinkButton_Click;
        }

        private void build()
        {
            CB_FileLink.setContent("file");
            CB_local.setContent("local");
            CB_web.setContent("web");

            CTB_Link.setText("link");
            CTB_LinkText.setText("linktext");
            CB_LinkButton.setContent("setup\nlink");
        }

        internal ContentData GetContentData()
        {
            return contentData;
        }

        internal void initiate_link()
        {
            linkData = new LinkData(CTB_Link.getText(), CTB_LinkText.getText());

            if (Uri.IsWellFormedUriString(linkData.GetLink, UriKind.RelativeOrAbsolute)
                || Directory.Exists(linkData.GetLink) || File.Exists(linkData.GetLink))
            {
                setup();
            }            
            else
            {
                linkData = null;

                MessageBox.Show("could not resolve link");
            }
        }
        internal void load(ContentData data)
        {
            string link = (string)data.GetValue("Link");
            string linkText = (string)data.GetValue("LinkText");

            load_setup();

            this.ToolTip = link + "\n" + linkText;

            linkData = new LinkData(link, linkText);

            CTB_Link.setText(link);
            CTB_LinkText.setText(linkText);

            initiate_link();
        }

        private void load_setup()
        {
            SP_Choice.Visibility = Visibility.Collapsed;

            CTB_Link.Visibility = Visibility.Collapsed;
            CTB_LinkText.Visibility = Visibility.Collapsed;

            CB_LinkButton.Visibility = Visibility.Visible;
        }

        private void reset()
        {
            SP_Choice.Visibility = Visibility.Visible;

            CTB_Link.Visibility = Visibility.Collapsed;
                        
            CTB_LinkText.Visibility = Visibility.Collapsed;

            CB_LinkButton.setContent("setup\nlink");

            CB_LinkButton.Visibility = Visibility.Collapsed;

            //L_LinkText.Visibility = Visibility.Collapsed;

            linkData = null;
        }

        private void setup()
        {
            CTB_Link.Visibility = Visibility.Collapsed;
            CTB_LinkText.Visibility = Visibility.Collapsed;

            CB_LinkButton.setContent(linkData.GetLinkText);

            __Link.ToolTip = linkData.GetLink;

            if (linkData.GetLink != null)
            {
                contentData.Clear();

                contentData.AddValue("Link", linkData.GetLink);
                contentData.AddValue("LinkText", linkData.GetLinkText);

                //MessageBox.Show(contentData.GetValuesDictionary().Count.ToString()
                //    + "\n" + contentData.GetValue("Link")
                //     + "\n" + contentData.GetValue("LinkText")
                //     + "\n" + contentData.GetValuesDictionary()["Link"].ToString());

                if (Uri.IsWellFormedUriString(linkData.GetLink, UriKind.RelativeOrAbsolute))
                {
                    //thx to https://www.brad-smith.info/blog/archives/164 for IconTools.cs
                    Icon icon = IconTools.GetIconForExtension(".html", ShellIconSize.LargeIcon);

                    if (icon != null)
                    {
                        CB_LinkButton.setContent(icon);
                    }
                }
                //else if (File.Exists(linkData.GetLink))
                //{
                //    Icon icon = IconTools.GetIconForFile(linkData.GetLink, ShellIconSize.LargeIcon);

                //    if (icon != null)
                //    {
                //        CB_LinkButton.SetElement(icon);                        
                //    }
                //}
                else
                {
                    Icon icon = IconTools.GetIconForFile(linkData.GetLink, ShellIconSize.LargeIcon);

                    if (icon != null)
                    {
                        CB_LinkButton.setContent(icon);
                    }
                }
            }

            //L_LinkText.Content = linkData.GetLinkText;
            //L_LinkText.Visibility = Visibility.Visible;
        }

        private void setup_choice()
        {
            SP_Choice.Visibility = Visibility.Collapsed;

            CTB_Link.Visibility = Visibility.Visible;
            CTB_LinkText.Visibility = Visibility.Visible;
            CB_LinkButton.Visibility = Visibility.Visible;
        }
        
        private void CB_file_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = logic.openDialog();

            if (file != null)
            {
                CB_FileLink.setContent(file.FileName);

                CTB_Link.setText(file.FileName);
                CTB_LinkText.setText(file.SafeFileName);

                setup_choice();
            }
        }

        private void CB_local_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderDialog = logic.openFolder();

            if (folderDialog != null)
            {                
                CTB_Link.setText(folderDialog.SelectedPath);
                CTB_LinkText.setText(folderDialog.SelectedPath);

                setup_choice();
            }           
        }
        private void CB_web_Click(object sender, RoutedEventArgs e)
        {
            setup_choice();
        }

        private void CB_LinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (loaded)
            {
                if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                {
                    reset();                
                }
                else
                {
                    if (linkData != null)
                    {
                        logic.executeLink(linkData.GetLink);
                    }
                    else
                    {
                        initiate_link();
                    }
                }                    
            }

            e.Handled = true;
        }

        private void CTB_Link_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void CTB_LinkText_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void __Link_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
/*  END OF FILE
 * 
 * 
 */