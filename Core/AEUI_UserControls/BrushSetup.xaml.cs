/* Aiding Elements User Interface
 *      BrushSetup element 
 * 
 * basic element to setup brushes for data resources
 * 
 * init:        2024|01|28
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.AEUI_Data;
using System;
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
    /// Interaktionslogik für BrushSetup.xaml
    /// </summary>
    public partial class BrushSetup : UserControl
    {
        object data;

        public BrushSetup()
        {
            this.data = new CoreData();

            InitializeComponent();

            build();
            registerEvents();

            DataResources();
        }

        internal BrushSetup(object data)
        {
            this.data = data;

            InitializeComponent();

            build();
            registerEvents();

            DataResources();
        }


        private void BrushTypeSelection(CoreButton core)
        {
            if (core.isSelected)
            {
                core.deselect();
            }
            else
            {
                core.select();
            }
        }

        private void build()
        {
            CB_SolidColorBrush.setContent("solid");
            CB_RadialGradientBrush.setContent("radial");
            CB_LinearGradientBrush.setContent("linear");
            CB_ImageBrush.setContent("image");
            CB_DrawingBrush.setContent("drawing");
            CB_VisualBrush.setContent("visual");
        }

        private void DataResources()
        {
            __BrushSetup.MinHeight = ((CoreData)data).height;
            __BrushSetup.MinWidth = ((CoreData)data).width;

            ColorField.Fill = ((CoreData)data).foreground.GetBrush();
        }

        private void registerEvents()
        {
            CB_SolidColorBrush.button.Click += CB_SolidColorBrush_Click;
            CB_RadialGradientBrush.button.Click += CB_RadialGradientBrush_Click;
            CB_LinearGradientBrush.button.Click += CB_LinearGradientBrush_Click;
            CB_ImageBrush.button.Click += CB_ImageBrush_Click;
            CB_DrawingBrush.button.Click += CB_DrawingBrush_Click;
            CB_VisualBrush.button.Click += CB_VisualBrush_Click;
        }

        internal void SetData(object data)
        {
            if (data != null)
            {
                this.data = data;
            }
            else
            {
                data = new CoreData();
            }

        }


        private void CB_SolidColorBrush_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            BrushTypeSelection(CB_SolidColorBrush);

            e.Handled = true;
        }
        private void CB_RadialGradientBrush_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            BrushTypeSelection(CB_RadialGradientBrush);

            e.Handled = true;
        }
        private void CB_LinearGradientBrush_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            BrushTypeSelection(CB_LinearGradientBrush);

            e.Handled = true;
        }

        private void CB_ImageBrush_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            BrushTypeSelection(CB_ImageBrush);

            e.Handled = true;
        }

        private void CB_DrawingBrush_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            BrushTypeSelection(CB_DrawingBrush);

            e.Handled = true;
        }
        private void CB_VisualBrush_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            BrushTypeSelection(CB_VisualBrush);

            e.Handled = true;
        }
   
    }
}
/*  END OF FILE
 * 
 * 
 */