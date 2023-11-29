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

namespace AidingElementsUserInterface.Core
{
    /// <summary>
    /// Interaktionslogik für CoreButton.xaml
    /// </summary>
    public partial class CoreButton : UserControl
    {        
        // global classes, properties and variables
        #region global classes, properties and variables     
        private bool expander = false;

        ButtonData config;
        #endregion global classes, properties and variables


        // constructors
        #region constructors
        public CoreButton()
        {
            InitializeComponent();

            build();
        }


        public CoreButton(bool expander_)
        {
            expander = expander_;

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


        // element design and functionality
        #region element design and functionality
        private async void build()
        {
            await Task.Delay(12);

            config = new ButtonData();

            config.background = Colors.DarkGreen;
            config.foreground = Colors.Yellow;
            config.thickness = new Thickness(5);
            config.cornerRadius = new CornerRadius(25);
            config.highlight = Colors.Tomato;
            config.borderbrush = Colors.Tan;


            this.Resources.Remove("buttonColor");
            this.Resources.Remove("forecolor");
            this.Resources.Remove("highlight");
            this.Resources.Remove("radius");

            this.Resources.Add("buttonColor", new SolidColorBrush(config.background));
            this.Resources.Add("forecolor", new SolidColorBrush(config.foreground));
            this.Resources.Add("highlight", new SolidColorBrush(config.highlight));
            this.Resources.Add("radius", config.cornerRadius);

            Style style = this.FindResource("buttonStyle") as Style;

            //if (config.buttonImageFilePath.Length > 4)
            //{
            //    border.Background = config.Return_ImageBrush(config.buttonImageFilePath);
            //}
            //else
            //{
            //    border.Background = config.btnBackColor;
            //}

            border.BorderBrush = new SolidColorBrush(config.borderbrush);
            border.CornerRadius = config.cornerRadius;
            border.BorderThickness = config.thickness;

            border.HorizontalAlignment = HorizontalAlignment.Stretch;
            border.VerticalAlignment = VerticalAlignment.Stretch;

            if (expander == false)
            {
                button.MinWidth = config.width;
                button.MinHeight = config.height;
            }

            //button.Background = config.return_TransparentSolidColorBrush();
            button.Foreground = new SolidColorBrush(config.foreground);

            button.VerticalContentAlignment = VerticalAlignment.Stretch;
            button.HorizontalContentAlignment = HorizontalAlignment.Stretch;

            button.Style = style;
        }


        public void _disabled()
        {
            button.IsEnabled = false;
        }

        public void _enabled()
        {
            button.IsEnabled = true;
        }

        public void _point_expander()
        {
            //config = new CoreData();
            //
            //button.Content = "";
            //
            //button.Width = config.expanderSize;
            //button.Height = config.expanderSize;
        }

        public void _expander(bool vertical)
        {
            //config = new CoreData();
            //
            //button.Content = "";
            //
            //if (vertical == true)
            //{
            //    button.Width = config.expanderSize;
            //}
            //else if (vertical == false)
            //{
            //    button.Height = config.expanderSize;
            //}
        }

        public void setContent(string content)
        {
            button.Content = content;
        }

        public void setShapeAsContent(Shape shape)
        {
            config = new ButtonData();

            //shape.Fill = config.btnBackColor;
            //shape.Stroke = config.btnForeColor;
            shape.StrokeThickness = 3;

            button.Content = shape;
        }

        public void setTooltip(string tooltip)
        {
            this.ToolTip = tooltip;
        }
        #endregion element design and functionality


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