/* Aiding Elements User Interface
 *      FileLink element 
 * 
 * link any file to a button
 * 
 * init:        2024|01|03
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
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

using Microsoft.Win32;
using System;
using System.Diagnostics;
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
        private bool loaded = false;
        private bool linked = false;

        internal LinkData data;
        private SharedLogic logic = new SharedLogic();

        public FileLink()
        {
            InitializeComponent();

            build();

            loaded = true;
        }

        private void build()
        {
            CB_FileLink.setContent("link file");
            CB_FileLink.button.Click += CB_FileLink_Click;

            CTB_LinkText.setText("linktext");

            CB_LinkButton.setContent("setup\nlink");
            CB_LinkButton.button.Click += CB_LinkButton_Click;
        }

        internal void load(LinkData linkData)
        {
            SP_Choice.Visibility = Visibility.Collapsed;

            CB_FileLink.setContent(linkData.GetLink);

            CB_LinkButton.setTooltip(linkData.GetLink);
            CB_LinkButton.setContent(linkData.GetLinkText);

            data = linkData;

            linked = true;
        }

        internal void initiate_link()
        {
            OpenFileDialog file = logic.openDialog();

            if (file != null)
            {
                data = new LinkData();
                data.SetLink(file.FileName);
                
                CB_FileLink.setContent(file.FileName);
                CTB_LinkText.setText(file.SafeFileName);
            }
        }

        //public ImageSource GetIcon(string fileName)
        //{
        //    try
        //    {
        //        System.Drawing.Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(fileName);
        //        return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
        //            icon.Handle,
        //            new Int32Rect(),
        //            BitmapSizeOptions.FromEmptyOptions()
        //            );
        //    }
        //    catch (Exception)
        //    {

        //        //MessageBox.Show("empty path");
        //    }

        //    return null;
        //}


        private void reset()
        {
            SP_Choice.Visibility = Visibility.Visible;

            CB_LinkButton.setContent("setup\nlink");

            linked = false;
        }

        private void setup_choice()
        {
            data.SetLinkText(CTB_LinkText.getText());

            CB_LinkButton.setContent(data.GetLinkText);

            SP_Choice.Visibility = Visibility.Collapsed;

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
                        logic.executeLink(data.GetLink);
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