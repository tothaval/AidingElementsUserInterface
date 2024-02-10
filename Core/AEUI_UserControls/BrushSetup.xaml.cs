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
using AidingElementsUserInterface.Core.Auxiliaries;
using Microsoft.Win32;
using System;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace AidingElementsUserInterface.Core.AEUI_UserControls
{
    /// <summary>
    /// Interaktionslogik für BrushSetup.xaml
    /// </summary>
    public partial class BrushSetup : UserControl
    {
        ColorData colorData = new ColorData();

        byte alpha = 255, red = 255, green = 255, blue = 255, brightness = 100, grey = 255;

        internal CoreValueChange callerCVC;
        internal LevelBar callerLB;

        public BrushSetup()
        {
            InitializeComponent();

            build();
            registerEvents();
        }

        internal BrushSetup(ref CoreValueChange CVC)
        {
            this.callerCVC = CVC;

            InitializeComponent();

            build();
            registerEvents();
        }



        internal BrushSetup(ref LevelBar LB)
        {
            this.callerLB = LB;

            InitializeComponent();

            CB_ResetColorData.Visibility = Visibility.Visible;

            build();
            registerEvents();
        }

        private void BrushTypeSelection(CoreButton core)
        {
            colorData = new ColorData();

            if (core.isSelected)
            {
                core.deselect();
            }
            else
            {
                CB_SolidColorBrush.deselect();
                CB_RadialGradientBrush.deselect();
                CB_LinearGradientBrush.deselect();
                CB_ImageBrush.deselect();
                CB_DrawingBrush.deselect();
                CB_VisualBrush.deselect();

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

            CB_AddGradient.setContent("add gradient");

            CB_ResetColorData.setContent("reset brush");
            CB_SaveColorData.setContent("save brush");
        }

        private void buildColor(bool isGrayscale = false)
        {
            colorData = new ColorData();

            if (!isGrayscale)
            {
                colorData.color1_string = Color.FromArgb(
                    alpha,
                    red,
                    green,
                    blue).ToString();
            }
            else
            {
                colorData.color1_string = Color.FromArgb(
                    alpha,
                    grey,
                    grey,
                    grey).ToString();
            }


            ColorField.ToolTip = colorData.color1_string;

            ColorField.Fill = colorData.GetBrush();               
            
            //CT_ArgbhexString.setText(argb.ToString());
            //CT_ArgbString.setText($"{(int)alpha_value},{(int)red_value},{(int)green_value},{(int)blue_value}");
            
        }

        private void registerEvents()
        {
            CB_SolidColorBrush.button.Click += CB_SolidColorBrush_Click;
            CB_RadialGradientBrush.button.Click += CB_RadialGradientBrush_Click;
            CB_LinearGradientBrush.button.Click += CB_LinearGradientBrush_Click;
            CB_ImageBrush.button.Click += CB_ImageBrush_Click;
            CB_DrawingBrush.button.Click += CB_DrawingBrush_Click;
            CB_VisualBrush.button.Click += CB_VisualBrush_Click;

            CB_AddGradient.button.Click += CB_AddGradient_Click;
            CB_ResetColorData.button.Click += CB_ResetColorData_Click;
            CB_SaveColorData.button.Click += CB_SaveColorData_Click;
        }

        private void CB_AddGradient_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // add leveldata line in stackpanel, store all of them in colordata, alter colordata accordingly
        }

        private void CB_ResetColorData_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            resetCaller();
        }


        private void CB_SaveColorData_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            updateCaller();
        }

        private void resetCaller()
        {
            if (callerCVC != null)
            {
                callerCVC.setObject(new ColorData());
            }
            if (callerLB != null)
            {
                callerLB.ChangeLevelBackground(null);
            }
        }

        private void updateCaller()
        {
            if (callerCVC != null)
            {
                    callerCVC.setObject(colorData);                                             
            }
            if (callerLB != null)
            {
                callerLB.ChangeLevelBackground(colorData);
            }
        }

        private void CB_SolidColorBrush_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SP_GradientArea.Visibility = Visibility.Collapsed;

            BrushTypeSelection(CB_SolidColorBrush);

            colorData.brushtype = "SolidColorBrush";
            colorData.color1_string = "#BB00FFDD";

            ColorField.Fill = colorData.GetBrush();

            e.Handled = true;
        }
        private void CB_RadialGradientBrush_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SP_GradientArea.Visibility = Visibility.Visible;

            SP_gradientSteps.Children.Clear();

            BrushTypeSelection(CB_RadialGradientBrush);

            colorData.brushtype = "RadialGradientBrush";

            e.Handled = true;
        }
        private void CB_LinearGradientBrush_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SP_GradientArea.Visibility = Visibility.Visible;

            SP_gradientSteps.Children.Clear();

            BrushTypeSelection(CB_LinearGradientBrush);

            colorData.brushtype = "LinearGradientBrush";

            e.Handled = true;
        }

        private void CB_ImageBrush_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SP_GradientArea.Visibility = Visibility.Visible;

            BrushTypeSelection(CB_ImageBrush);

            OpenFileDialog file = new SharedLogic().openDialog();

            if (file != null)
            {
                if (File.Exists(file.FileName))
                {
                    colorData.brushtype = "ImageBrush";
                    colorData.brushpath = file.FileName;
                    
                    ColorField.Fill = colorData.GetBrush();
                }
            }

            e.Handled = true;
        }

        private void CB_DrawingBrush_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SP_GradientArea.Visibility = Visibility.Visible;

            BrushTypeSelection(CB_DrawingBrush);

            colorData.brushtype = "DrawingBrush";

            e.Handled = true;
        }
        private void CB_VisualBrush_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SP_GradientArea.Visibility = Visibility.Visible;

            BrushTypeSelection(CB_VisualBrush);

            colorData.brushtype = "VisualBrush";

            e.Handled = true;
        }

        private void S_Alpha_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            alpha = (byte)e.NewValue;
            if (alpha < 1)
            {
                alpha = 0;
            }
            if (alpha > 254)
            {
                alpha = 255;
            }

            buildColor();

            e.Handled = true;
        }

        private void S_Red_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            red = (byte)e.NewValue;
            if (red < 1)
            {
                red = 0;
            }
            if (red > 254)
            {
                red = 255;
            }

            buildColor();

            e.Handled = true;

        }

        private void S_Green_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            green = (byte)e.NewValue;
            if (green < 1)
            {
                green = 0;
            }
            if (green > 254)
            {
                green = 255;
            }

            buildColor();

            e.Handled = true;

        }

        private void S_Blue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            blue = (byte)e.NewValue;
            if (blue < 1)
            {
                blue = 0;
            }
            if (blue > 254)
            {
                blue = 255;
            }

            buildColor();

            e.Handled = true;

        }

        private void S_Brightness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void S_Grey_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            grey = (byte)e.NewValue;
            if (grey < 1)
            {
                grey = 0;
            }
            if (grey > 254)
            {
                grey = 255;
            }

            buildColor();

            e.Handled = true;

        }
    }
}
/*  END OF FILE
 * 
 * 
 */