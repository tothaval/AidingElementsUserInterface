/* Aiding Elements User Interface
 *      CoreButton element 
 * 
 * basic configurable fileLinkElement element
 * 
 * init:        2023|11|27
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.Auxiliaries;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AidingElementsUserInterface.Core
{
    /// <summary>
    /// basic configurable fileLinkElement element
    /// <para>:CoreButton(UserControl) { 0 empty; 1 string;} </para> 
    /// <para>?!public </para> 
    /// <para> </para> 
    /// <para>.isSelected(bool) ?!internal > bool . </para> 
    /// <para> </para> 
    /// <para>;_disabled(void) ?!internal {} ; </para> 
    /// <para>;deselect(void) ?!internal {} ; </para> 
    /// <para>;enabled(void) ?!internal {} ; </para> 
    /// <para>;select(void) ?!internal {} ; </para> 
    /// <para>;setContent(void) ?!internal { 0 string  1 Icon } ; </para> 
    /// <para>;setTooltip(void) ?!internal { 0 string } ; </para> 
    /// <para>:CoreButton </para> 
    /// </summary>
    public partial class CoreButton : UserControl
    { 
        internal ButtonData config;
        private bool selected = false;
        internal bool isSelected => selected;

        // constructors
        #region constructors
        public CoreButton()
        {
            InitializeComponent();

            build();
        }

        public CoreButton(string content)
        {
            InitializeComponent();

            button.Content = content;

            build();
        }
        #endregion constructors


        //private static readonly Action EmptyDelegate = delegate { };
        //public static void Refresh(this UIElement uiElement)
        //{
        //    uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        //}



        private async void build()
        {
            Data_Handler data_Handler = new SharedLogic().GetDataHandler();
        
            config = data_Handler.LoadButtonData();

            //await Task.Delay(10);

            if (config == null)
            {
                config = new ButtonData();
            }

            data_Handler.AddData(config);

            //await Task.Delay(12);
            
            //this.Resources.Remove("buttonColor");
            //this.Resources.Remove("forecolor");
            //this.Resources.Remove("highlight");
            //this.Resources.Remove("radius");

            this.Resources.Add("buttonColor", new SolidColorBrush(config.background));
            this.Resources.Add("forecolor", new SolidColorBrush(config.foreground));
            this.Resources.Add("highlight", new SolidColorBrush(config.highlight));
            this.Resources.Add("radius", config.cornerRadius);

            _backgroundImage();

            border.BorderBrush = new SolidColorBrush(config.borderbrush);
            border.CornerRadius = config.cornerRadius;
            border.BorderThickness = config.thickness;

            border.HorizontalAlignment = HorizontalAlignment.Center;
            border.VerticalAlignment = VerticalAlignment.Center;

            button.MaxWidth = config.width * 2;
            button.MinWidth = config.width;

            button.MaxHeight = config.height * 2;
            button.MinHeight = config.height;

            //fileLinkElement.Background = config.return_TransparentSolidColorBrush();
            button.Foreground = new SolidColorBrush(config.foreground);

            button.VerticalContentAlignment = VerticalAlignment.Center;
            button.HorizontalContentAlignment = HorizontalAlignment.Center;

            //fileLinkElement.SetResourceReference(Control.StyleProperty, "buttonStyle");

            Style style = this.FindResource("buttonStyle") as Style;
            button.Style = style;
        }

        internal void _backgroundImage()
        {
            if (config.imageFilePath != null)
            {
                if (File.Exists(config.imageFilePath))
                {
                    border.Background = new ImageBrush(new BitmapImage(new Uri(config.imageFilePath)));
                    this.Resources.Remove("buttonColor");
                }
                else
                {
                    border.Background = new SolidColorBrush(config.background);
                }
            }
        }

        internal void _disabled()
        {
            button.IsEnabled = false;
        }

        internal void deselect()
        {
            

            this.Resources.Remove("buttonColor");
            this.Resources.Remove("highlight");

            this.Resources.Add("buttonColor", new SolidColorBrush(config.background));
            this.Resources.Add("highlight", new SolidColorBrush(config.highlight));

            Style style = this.FindResource("buttonStyle") as Style;
            button.Style = style;

            selected = false;
        }

        internal void _enabled()
        {
            button.IsEnabled = true;
        }

        internal void select()
        {
            this.Resources.Remove("buttonColor");
            this.Resources.Remove("highlight");

            this.Resources.Add("buttonColor", new SolidColorBrush(config.highlight));
            this.Resources.Add("highlight", new SolidColorBrush(config.background));

            Style style = this.FindResource("buttonStyle") as Style;
            button.Style = style;

            selected = true;
        }

        internal void setContent(string content)
        {
            button.Content = content;
        }

        internal void setContent(Icon icon)
        {
            System.Windows.Controls.Image image = new System.Windows.Controls.Image();

            ImageSource imso = SharedLogic.ToImageSource(icon);
            image.Source = imso;

            button.Content = image;
        }

        internal void setShapeAsContent(Shape shape)
        {
            config = new ButtonData(new SharedLogic().GetDataHandler().GetCoreData());

            //shape.Fill = config.btnBackColor;
            //shape.Stroke = config.btnForeColor;
            shape.StrokeThickness = 3;

            button.Content = shape;
        }

        internal void setTooltip(string tooltip)
        {
            this.ToolTip = tooltip;
        }

        // element events
        #region element events
        // CORE_Button
        #region CORE_Button
        private void CORE_Button_Loaded(object sender, RoutedEventArgs e)
        {

        }
        #endregion CORE_Button
        #endregion element events


    }
}
/*  END OF FILE
 * 
 * 
 */