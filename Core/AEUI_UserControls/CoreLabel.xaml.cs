/* Aiding Elements User Interface
 *      CoreLabel element 
 * 
 * basic configurable textblock element
 * 
 * init:        2023|12|01
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.Auxiliaries;
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

namespace AidingElementsUserInterface.Core.AEUI_UserControls
{
    /// <summary>
    /// Interaktionslogik für CoreLabel.xaml
    /// </summary>
    public partial class CoreLabel : UserControl
    {
        private CoreData config;

        public CoreLabel()
        {
            InitializeComponent();

            build();
        }

        public CoreLabel(bool no_limits)
        {
            InitializeComponent();

            build(no_limits);
        }

        public CoreLabel(string content)
        {
            InitializeComponent();

            textblock.Text = content;

            build();
        }
        private void build(bool no_limits = false)
        {
            Data_Handler data_Handler = new SharedLogic().GetDataHandler();

            config = data_Handler.LoadLabelData();
            data_Handler.AddLabelData(config);

            if (config == null)
            {
                config = new CoreData();
            }

            this.Resources["LabelData_MinWidth"] = config.width;
            this.Resources["LabelData_MinHeight"] = config.height;

            //if (no_limits)
            //{
            //    this.Resources["TextBoxData_MinWidth"] = config.width;
            //    this.Resources["TextBoxData_MinHeight"] = config.height;
            //}
            //else
            //{
            //    this.Resources["TextBoxData_MinWidth"] = config.width;
            //    this.Resources["TextBoxData_MinHeight"] = config.height;

            //    this.Resources["TextBoxData_MaxWidth"] = config.width * 2;
            //    this.Resources["TextBoxData_MaxHeight"] = config.height;
            //}

            textblock.Padding = new Thickness(7, 3, 7, 3);

        }
        public void setText(string text)
        {
            textblock.Text = text;
        }
    }
}
/*  END OF FILE
 * 
 * 
 */