using AidingElementsUserInterface.Core;
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.Auxiliaries;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
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
    /// Interaktionslogik für Link.xaml
    /// </summary>
    public partial class Link : UserControl
    {
        private bool loaded = false;

        internal LinkData data;
        private SharedLogic logic = new SharedLogic();

        private CoreTextBox CTB_Link;
        private CoreTextBox CTB_LinkText;

        private CoreButton CB_LinkButton;


        public Link()
        {
            InitializeComponent();

            build();

            loaded = true;
        }

        private void build()
        {
            CTB_Link= new CoreTextBox("link");
            CTB_LinkText= new CoreTextBox("linktext");
            CB_LinkButton = new CoreButton("setup\nlink");

            CB_LinkButton.button.Click += CB_LinkButton_Click;

            StackPanel stackPanel = new StackPanel();

            stackPanel.Children.Add(CTB_Link);
            stackPanel.Children.Add(CTB_LinkText);
            stackPanel.Children.Add(CB_LinkButton);

            border.Child = stackPanel;
        }
        internal void initiate_link()
        {
            data = new LinkData(CTB_Link.getText(), CTB_LinkText.getText());

            if (Directory.Exists(data.GetLink))
            {
                setup();
            }
            else if (Uri.IsWellFormedUriString(data.GetLink, UriKind.RelativeOrAbsolute))
            {
                setup();
            }
            else
            {
                data = null;

                MessageBox.Show("could not resolve link");
            }
        }

        private void reset()
        {
            CTB_Link.Visibility = Visibility.Visible;
            CTB_LinkText.Visibility = Visibility.Visible;

            CB_LinkButton.setContent("setup\nlink");

            data = null;
        }

        private void setup()
        {
            CTB_Link.Visibility = Visibility.Collapsed;
            CTB_LinkText.Visibility = Visibility.Collapsed;

            CB_LinkButton.setContent(data.GetLinkText);

            __Link.ToolTip = data.GetLink;
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

        internal void load(LinkData linkData)
        {
            CTB_Link.setText(linkData.GetLink);
            CTB_LinkText.setText(linkData.GetLinkText);

            data = linkData;

            initiate_link();
        }
        private void __Link_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
