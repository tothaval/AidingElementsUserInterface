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
            CB_FileLink.button.Click += Button_Click;
        }

        internal void load(LinkData linkData)
        {
            CB_FileLink.ToolTip = linkData.GetLink;
            CB_FileLink.button.Content = linkData.GetLinkText;
            
            data = linkData;
        }

        internal void initiate_link()
        {
            OpenFileDialog file = logic.openDialog();

            if (file != null) 
            {
                load(new LinkData(file.FileName, file.SafeFileName));
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



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (loaded)
            {
                if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                {
                    initiate_link();
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

        private void __FileLink_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
