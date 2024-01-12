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

        internal LinkData data;
        private SharedLogic logic = new SharedLogic();


        public Link()
        {
            InitializeComponent();

            build();

            loaded = true;

            //L_LinkText.FontSize = new SharedLogic().GetDataHandler().GetCoreData().fontSize;
            L_LinkText.FontSize = 9;
            L_LinkText.FontFamily = new SharedLogic().GetMainWindow().mainWindowData.fontFamily;
        }

        private void build()
        {
            CB_FileLink.setContent("file");
            CB_local.setContent("local");
            CB_web.setContent("web");

            CB_FileLink.button.Click += CB_file_Click;
            CB_local.button.Click += CB_local_Click;
            CB_web.button.Click += CB_web_Click;

            CTB_Link.setText("link");
            CTB_LinkText.setText("linktext");
            CB_LinkButton.setContent("setup\nlink");

            CB_LinkButton.button.Click += CB_LinkButton_Click;
        }

        internal void initiate_link()
        {
            data = new LinkData(CTB_Link.getText(), CTB_LinkText.getText());

            if (Uri.IsWellFormedUriString(data.GetLink, UriKind.RelativeOrAbsolute)
                || Directory.Exists(data.GetLink) || File.Exists(data.GetLink))
            {
                setup();
            }            
            else
            {
                data = null;

                MessageBox.Show("could not resolve link");
            }
        }
        internal void load(LinkData linkData)
        {
            load_setup();

            CTB_Link.setText(linkData.GetLink);
            CTB_LinkText.setText(linkData.GetLinkText);

            data = linkData;

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

            L_LinkText.Visibility = Visibility.Collapsed;

            data = null;
        }

        private void setup()
        {
            CTB_Link.Visibility = Visibility.Collapsed;
            CTB_LinkText.Visibility = Visibility.Collapsed;

            CB_LinkButton.setContent(data.GetLinkText);

            __Link.ToolTip = data.GetLink;

            if (data.GetLink != null)
            {
                if (Uri.IsWellFormedUriString(data.GetLink, UriKind.RelativeOrAbsolute))
                {
                    //thx to https://www.brad-smith.info/blog/archives/164 for IconTools.cs
                    Icon icon = IconTools.GetIconForExtension(".html", ShellIconSize.LargeIcon);

                    if (icon != null)
                    {
                        CB_LinkButton.setContent(icon);
                    }
                }
                //else if (File.Exists(data.GetLink))
                //{
                //    Icon icon = IconTools.GetIconForFile(data.GetLink, ShellIconSize.LargeIcon);

                //    if (icon != null)
                //    {
                //        CB_LinkButton.setContent(icon);                        
                //    }
                //}
                else
                {
                    Icon icon = IconTools.GetIconForFile(data.GetLink, ShellIconSize.LargeIcon);

                    if (icon != null)
                    {
                        CB_LinkButton.setContent(icon);
                    }
                }
            }

            L_LinkText.Content = data.GetLinkText;
            L_LinkText.Visibility = Visibility.Visible;
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
                    if (data != null)
                    {
                        logic.executeLink(data.GetLink);
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