/* Aiding Elements User Interface
 *      Image element 
 * 
 * displays an imageBrush
 * 
 * init:        2024|01|03
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core;
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.AEUI_UserControls;
using AidingElementsUserInterface.Core.Auxiliaries;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using static System.Net.Mime.MediaTypeNames;
using Point = System.Windows.Point;

namespace AidingElementsUserInterface.Elements
{
    /// <summary>
    /// Interaktionslogik für Image.xaml
    /// </summary>
    public partial class Image : UserControl
    {
        // thx to https://www.codeproject.com/Articles/168176/Zooming-and-panning-in-WPF-with-fixed-focus for mousewheel zooming code
        // license: https://www.codeproject.com/info/cpol10.aspx

        private bool linked = false;
        private bool loaded = false;

        private ContentData contentData = new ContentData();

        private LinkData linkData;
        private SharedLogic logic = new SharedLogic();

        private Point image_offset;
        private Point mouse_position;

        // use scale transform to zoom in or out using mousewheel
        public Image()
        {
            contentData.settings = logic.GetDataHandler().GetCoreData();

            if (contentData.settings == null)
            {
                contentData.settings = new CoreData();
            }

            InitializeComponent();

            build();
            registerEvents();

            loaded = true;
        }

        internal Image(ContentData contentData)
        {
            this.contentData = contentData;

            InitializeComponent();

            if (contentData != null)
            {
                if (contentData.GetValue("Link") != null && contentData.GetValue("LinkText") != null)
                {
                    if (contentData.GetValue("Width") != null && contentData.GetValue("Height") != null)
                    {
                        load(contentData);
                    }
                }
            }
            else
            {
                build();
            }
            
            registerEvents();

            loaded = true;
        }
        private void registerEvents()
        {
            CB_ImageLink.button.Click += CB_ImageLink_Click;
            CB_LinkButton.button.Click += CB_LinkButton_Click;

            image.MouseLeftButtonDown += Image_MouseLeftButtonDown;
            image.MouseLeftButtonUp += Image_MouseLeftButtonUp;
            image.MouseMove += Image_MouseMove;
        }

        private void build()
        {
            CB_ImageLink.setContent("image");

            CTB_LinkText.setText("linktext");

            CVC_width.setIdentifier("width");
            CVC_height.setIdentifier("height");

            CVC_width.setText(contentData.settings.width.ToString());
            CVC_height.setText(contentData.settings.height.ToString());

            CB_LinkButton.setContent("setup\nimage");

            //L_LinkText.FontSize = new SharedLogic().GetDataHandler().GetCoreData().fontSize;
            L_LinkText.FontSize = 9;
            L_LinkText.FontFamily = contentData.settings.fontFamily;
        }


        internal ContentData GetContentData()
        {
            return contentData;
        }

        internal void initiate_link()
        {
            OpenFileDialog file = logic.openDialog();

            if (file != null)
            {
                linkData = new LinkData();
                linkData.SetLink(file.FileName);

                CB_ImageLink.setContent(file.FileName);
                CB_ImageLink.setTooltip(file.FileName);

                CTB_LinkText.setText(file.SafeFileName);

                CB_LinkButton.Visibility = Visibility.Visible;
                CTB_LinkText.Visibility = Visibility.Visible;
                CVC_width.Visibility = Visibility.Visible;
                CVC_height.Visibility = Visibility.Visible;

                L_LinkText.Visibility = Visibility.Visible;
                L_LinkText.Content = file.SafeFileName;
            }
        }

        internal void load(ContentData data)
        {
            string link = (string)data.GetValue("Link");
            string linkText = (string)data.GetValue("LinkText");
            double width = Double.Parse(data.GetValue("Width").ToString());
            double height = Double.Parse(data.GetValue("Height").ToString());

            this.ToolTip = link + "\n" + linkText;

            linkData = new LinkData(link, linkText);

            CB_ImageLink.setContent(linkData.GetLink);

            CB_LinkButton.setTooltip(linkData.GetLink);
            CB_LinkButton.setContent(linkData.GetLinkText);

            ImageSource? imageSource = new BitmapImage(new Uri(linkData.GetLink));
            if (imageSource != null)
            {
                image.Source = imageSource;
            }

            //border.Width = width;
            //border.Height = height;

            image.Stretch = Stretch.Fill;
            image.Width = width;
            image.Height = height;

            image.Visibility = Visibility.Visible;

            contentData.Clear();

            contentData.AddValue("Link", linkData.GetLink);
            contentData.AddValue("LinkText", linkData.GetLinkText);
            contentData.AddValue("Width", width);
            contentData.AddValue("Height", height);

            this.contentData = data;

            SP_Choice.Visibility = Visibility.Collapsed;
            CB_LinkButton.Visibility = Visibility.Collapsed;

            L_LinkText.Visibility = Visibility.Visible;
            L_LinkText.Content = linkData.GetLinkText;

            linked = true;
        }


        private double parseCVC(CoreValueChange coreValueChange)
        {
            double value = 0;

            try
            {
                value = Double.Parse(coreValueChange.value);
            }
            catch (Exception)
            {
                value = contentData.settings.width;
                MessageBox.Show($"could not resolve value, value set to: {contentData.settings.width}");
                coreValueChange.setText(contentData.settings.width.ToString());
            }

            return value;
        }

        private void reset()
        {
            image.Visibility = Visibility.Collapsed;

            SP_Choice.Visibility = Visibility.Visible;

            CB_LinkButton.setContent("setup\nimage");

            CB_ImageLink.Visibility = Visibility.Visible;
            CB_LinkButton.Visibility = Visibility.Visible;
            CTB_LinkText.Visibility = Visibility.Visible;
            L_LinkText.Visibility = Visibility.Visible;

            CVC_width.Visibility = Visibility.Visible;
            CVC_height.Visibility = Visibility.Visible;

            linked = false;
        }

        private void setup_choice()
        {
            double width, height;

            linkData.SetLinkText(CTB_LinkText.getText());

            width = parseCVC(CVC_width);
            height = parseCVC(CVC_height);

            if (linkData.GetLink != null)
            {
                ImageSource? imageSource = new BitmapImage(new Uri(linkData.GetLink));
                if (imageSource != null)
                {
                    image.Source = imageSource;
                }

                //border.MaxWidth = width;
                //border.MaxHeight = height;

                //border.MinWidth = width;
                //border.MinHeight = height;


                //border.Width = width;
                //border.Height = height;

                image.Stretch = Stretch.Fill;
                image.Width = width;
                image.Height = height;

                image.Visibility = Visibility.Visible;

                contentData.Clear();

                contentData.AddValue("Link", linkData.GetLink);
                contentData.AddValue("LinkText", linkData.GetLinkText);
                contentData.AddValue("Width", width);
                contentData.AddValue("Height", height);

                CB_LinkButton.setContent(linkData.GetLinkText);
                CB_LinkButton.setTooltip(linkData.GetLink);

                SP_Choice.Visibility = Visibility.Collapsed;
                CB_LinkButton.Visibility = Visibility.Collapsed;

                L_LinkText.Content = linkData.GetLinkText;
                L_LinkText.Visibility = Visibility.Visible;

                linked = true;
            }

        }

        private void CB_ImageLink_Click(object sender, RoutedEventArgs e)
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
        private void __Image_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    reset();

                }
            }

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                image.CaptureMouse();

                mouse_position = e.GetPosition(border);
                image_offset.X = image.RenderTransform.Value.OffsetX;
                image_offset.Y = image.RenderTransform.Value.OffsetY;

            }
            if (image.IsMouseCaptured)
            {
                return;
            }

        }
        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                image.ReleaseMouseCapture();

                //e.Handled = true;
            }
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (!image.IsMouseCaptured) return;

            if (image.IsMouseCaptured)
            {
                Point p = e.MouseDevice.GetPosition(border);

                Matrix m = image.RenderTransform.Value;
                m.OffsetX = image_offset.X + (p.X - mouse_position.X);
                m.OffsetY = image_offset.Y + (p.Y - mouse_position.Y);

                image.RenderTransform = new MatrixTransform(m);

            }
        }

        private void image_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Point p = e.MouseDevice.GetPosition(image);

            Matrix m = image.RenderTransform.Value;
            if (e.Delta > 0)
            {
                m.ScaleAtPrepend(1.2, 1.2, p.X, p.Y);
            }
            else
            {
                if (image.RenderTransform.Value.M11 > 0.095 && image.RenderTransform.Value.M22 > 0.095)
                {
                    m.ScaleAtPrepend(1 / 1.2, 1 / 1.2, p.X, p.Y);
                }
            }

            image.RenderTransform = new MatrixTransform(m);

            e.Handled = true;
        }

        private void scrollviewer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            image.RenderTransform = new MatrixTransform(1, 0, 0, 1, 0, 0);

            e.Handled = true;
        }
    }
}
/*  END OF FILE
 * 
 * 
 */