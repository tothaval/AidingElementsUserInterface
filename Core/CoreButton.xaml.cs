/* Aiding Elements User Interface
 *      CoreButton element 
 * 
 * basic configurable button element
 * 
 * init:        2023|11|27
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.Auxiliaries;
using AidingElementsUserInterface.Core.MyNote_Data;
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
        private ButtonData config;

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

            this.Resources.Remove("buttonColor");
            this.Resources.Remove("forecolor");
            this.Resources.Remove("highlight");
            this.Resources.Remove("radius");

            this.Resources.Add("buttonColor", new SolidColorBrush(config.background));
            this.Resources.Add("forecolor", new SolidColorBrush(config.foreground));
            this.Resources.Add("highlight", new SolidColorBrush(config.highlight));
            this.Resources.Add("radius", config.cornerRadius);

            Style style = this.FindResource("buttonStyle") as Style;

            if (config.imageFilePath != null)
            {
                //border.Background = config.Return_ImageBrush(config.buttonImageFilePath);
            }
            else
            {
                border.Background = new SolidColorBrush(config.background);
            }

            border.BorderBrush = new SolidColorBrush(config.borderbrush);
            border.CornerRadius = config.cornerRadius;
            border.BorderThickness = config.thickness;

            border.HorizontalAlignment = HorizontalAlignment.Stretch;
            border.VerticalAlignment = VerticalAlignment.Stretch;

            button.MinWidth = config.width;
            button.MinHeight = config.height;

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

        public void setContent(string content)
        {
            button.Content = content;
        }

        public void setShapeAsContent(Shape shape)
        {
            config = new ButtonData(new SharedLogic().GetDataHandler().GetCoreData());

            //shape.Fill = config.btnBackColor;
            //shape.Stroke = config.btnForeColor;
            shape.StrokeThickness = 3;

            button.Content = shape;
        }

        public void setTooltip(string tooltip)
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