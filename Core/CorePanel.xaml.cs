/* Aiding Elements User Interface
 *      CorePanel 
 * 
 * basic wrappanel element, used f.e. for expander panel content
 * 
 * init:         2023|11|29
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


namespace AidingElementsUserInterface.Core
{
    /// <summary>
    /// Interaktionslogik für CorePanel.xaml
    /// </summary>
    public partial class CorePanel : UserControl
    {        // global classes, properties and variables

        #region global classes, properties and variables

        // element options panel
        public CheckBox ui_eventHandlingChoice;

        //public YS_UI_DoubleValueIntakeButton DVIB_amount_and_thickness = new YS_UI_DoubleValueIntakeButton(false);
        //public YS_UI_DoubleValueIntakeButton DVIB_canvas_width_and_height = new YS_UI_DoubleValueIntakeButton(false);
        //public YS_UI_DoubleValueIntakeButton DVIB_fontfamily_and_fontsize = new YS_UI_DoubleValueIntakeButton(false);
        //public YS_UI_DoubleValueIntakeButton DVIB_x_axis_and_y_axis = new YS_UI_DoubleValueIntakeButton(false);
        //public YS_UI_DoubleValueIntakeButton DVIB_z_index_and_angle = new YS_UI_DoubleValueIntakeButton(false);
        //public YS_UI_DoubleValueIntakeButton DVIB_width_and_height = new YS_UI_DoubleValueIntakeButton(false);

        private UserControl initiatingElement;

        #endregion global classes, properties and variables


        // constructors
        #region constructors
        public CorePanel()
        {
            InitializeComponent();
        }
        #endregion constructors

        // create ElementOptionsPanel
        #region create ElementOptionsPanel
        public void createElementOptionsPanel()
        {
            ButtonData config = new ButtonData();

            wrapper.Margin = new Thickness(3, 3, 3, 3);

            Border border = new Border();
            border.BorderBrush = new SolidColorBrush(config.borderbrush);
            border.CornerRadius = config.cornerRadius;
            border.BorderThickness = config.thickness;

            ui_eventHandlingChoice = new CheckBox()
            {
                IsChecked = true,
                Content = "interactive",
                Background = new SolidColorBrush(config.background),
            Foreground = new SolidColorBrush(config.foreground),
            BorderBrush = new SolidColorBrush(config.borderbrush),
            HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5)
            };

            Border border_ui_eventHandlingCheckbox = new Border()
            {
                Child = ui_eventHandlingChoice,

                BorderBrush = new SolidColorBrush(config.borderbrush),
                CornerRadius = config.cornerRadius,
                BorderThickness = config.thickness,
            };

            Label label = new Label();
            label.Content = "element options";
            label.HorizontalContentAlignment = HorizontalAlignment.Center;
            label.VerticalContentAlignment = VerticalAlignment.Center;
            label.FontFamily = config.fontFamiliy;
            label.FontSize = config.fontSize;
            label.Background = new SolidColorBrush(Colors.Transparent);
            label.Foreground = new SolidColorBrush(config.foreground);

            border.Child = label;

            wrapper.Children.Add(border);
            wrapper.Children.Add(border_ui_eventHandlingCheckbox);
            //wrapper.Children.Add(create_DVIB_fontfamily_and_fontsize());
            //wrapper.Children.Add(create_DVIB_x_axis_and_y_axis());
            //wrapper.Children.Add(create_DVIB_z_index_and_angle());

            //updateOptionPanelDVIBs(initiatingElement);
        }
        #endregion create ElementOptionsPanel

    }
}
