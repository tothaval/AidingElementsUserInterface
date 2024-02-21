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
using System.Drawing;
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
using Color = System.Windows.Media.Color;

namespace AidingElementsUserInterface.Core.AEUI_UserControls
{
    /// <summary>
    /// Interaktionslogik für BrushSetup.xaml
    /// </summary>
    public partial class BrushSetup : UserControl
    {
        ColorData colorData = new ColorData();

        byte alpha = 255, red = 255, green = 255, blue = 255, brightness = 100, grey = 255;

        int active_gradient_color = 1;

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

            CB_Color1.setContent("color 1");
            CB_Color2.setContent("color 2");
            CB_Color3.setContent("color 3");
            CB_Color4.setContent("color 4");
            CB_Color5.setContent("color 5");
            CB_Color6.setContent("color 6");

            CVC_Gradient1.setIdentifier($"{colorData.color1_string}");
            CVC_Gradient2.setIdentifier($"{colorData.color2_string}");
            CVC_Gradient3.setIdentifier($"{colorData.color3_string}");
            CVC_Gradient4.setIdentifier($"{colorData.color4_string}");
            CVC_Gradient5.setIdentifier($"{colorData.color5_string}");
            CVC_Gradient6.setIdentifier($"{colorData.color6_string}");

            CVC_Gradient1.setText("0.0");
            CVC_Gradient2.setText("0.2");
            CVC_Gradient3.setText("0.4");
            CVC_Gradient4.setText("0.6");
            CVC_Gradient5.setText("0.8");
            CVC_Gradient6.setText("1.0");

            CB_ResetColorData.setContent("reset brush");
            CB_SaveColorData.setContent("save brush");
        }

        private void setColor(Color color)
        {
            ColorField.Fill = new SolidColorBrush(color);

            switch (active_gradient_color)
            {
                case 1:
                    colorData.color1_string = color.ToString();
                    break;
                case 2:
                    colorData.color2_string = color.ToString();
                    break;
                case 3:
                    colorData.color3_string = color.ToString();
                    break;
                case 4:
                    colorData.color4_string = color.ToString();
                    break;
                case 5:
                    colorData.color5_string = color.ToString();
                    break;
                case 6:
                    colorData.color6_string = color.ToString();
                    break;

                default:
                    colorData.color1_string = color.ToString();
                    break;
            }
        }

        private void buildColor(bool isGrayscale = false)
        {
            if (!isGrayscale)
            {
                Color color = Color.FromArgb(
                    alpha,
                    red,
                    green,
                    blue);

                setColor(color);

            }
            else
            {
                Color color = Color.FromArgb(
                    alpha,
                    grey,
                    grey,
                    grey);

                setColor(color);
            }

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

            CB_Color1.button.Click += CB_Color1_Click;
            CB_Color2.button.Click += CB_Color2_Click;
            CB_Color3.button.Click += CB_Color3_Click;
            CB_Color4.button.Click += CB_Color4_Click;
            CB_Color5.button.Click += CB_Color5_Click;
            CB_Color6.button.Click += CB_Color6_Click;

            CB_ResetColorData.button.Click += CB_ResetColorData_Click;
            CB_SaveColorData.button.Click += CB_SaveColorData_Click;
        }

        private void registerSliderEvents()
        {
            S_Alpha.ValueChanged += S_Alpha_ValueChanged;
            S_Blue.ValueChanged += S_Blue_ValueChanged;
            S_Brightness.ValueChanged += S_Brightness_ValueChanged;
            S_Green.ValueChanged += S_Green_ValueChanged;
            S_Grey.ValueChanged += S_Grey_ValueChanged;
            S_Red.ValueChanged += S_Red_ValueChanged;
        }

        private void unregisterSliderEvents()
        {
            S_Alpha.ValueChanged -= S_Alpha_ValueChanged;
            S_Blue.ValueChanged -= S_Blue_ValueChanged;
            S_Brightness.ValueChanged -= S_Brightness_ValueChanged;
            S_Green.ValueChanged -= S_Green_ValueChanged;
            S_Grey.ValueChanged -= S_Grey_ValueChanged;
            S_Red.ValueChanged -= S_Red_ValueChanged;
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

        private void updateGradientBrush()
        {
            if (SP_GradientArea.Visibility == Visibility.Visible)
            {
                SharedLogic logic = new SharedLogic();

                double[] offsets = new double[6];

                offsets[0] = Double.Parse(CVC_Gradient1.Value);
                offsets[1] = Double.Parse(CVC_Gradient2.Value);
                offsets[2] = Double.Parse(CVC_Gradient3.Value);
                offsets[3] = Double.Parse(CVC_Gradient4.Value);
                offsets[4] = Double.Parse(CVC_Gradient5.Value);
                offsets[5] = Double.Parse(CVC_Gradient6.Value);

                colorData.setOffsets(offsets);

                LinearGradientBrush linearGradientBrush = new LinearGradientBrush()
                {
                    StartPoint = new System.Windows.Point(0.5, 0.5),

                    EndPoint = new System.Windows.Point(0.5, 1)
                };

                linearGradientBrush.GradientStops.Add(new GradientStop(logic.ParseColor(colorData.color1_string), offsets[0]));
                linearGradientBrush.GradientStops.Add(new GradientStop(logic.ParseColor(colorData.color2_string), offsets[1]));
                linearGradientBrush.GradientStops.Add(new GradientStop(logic.ParseColor(colorData.color3_string), offsets[2]));
                linearGradientBrush.GradientStops.Add(new GradientStop(logic.ParseColor(colorData.color4_string), offsets[3]));
                linearGradientBrush.GradientStops.Add(new GradientStop(logic.ParseColor(colorData.color5_string), offsets[4]));
                linearGradientBrush.GradientStops.Add(new GradientStop(logic.ParseColor(colorData.color6_string), offsets[5]));


                RECT_GradientArea.Fill = linearGradientBrush;

                //RECT_GradientArea.Fill = colorData.GetBrush();
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

        private void adjustSliderValues(Color color)
        {
            unregisterSliderEvents();

            if (color.R == color.B && color.B == color.G)
            {
                S_Alpha.Value = color.A;
                alpha = color.A;

                S_Grey.Value = color.R;
                grey = color.R;
            }
            else
            {
                S_Alpha.Value = color.A;
                alpha = color.A;

                S_Blue.Value = color.B;
                blue = color.B;

                S_Green.Value = color.G;
                green = color.G;

                S_Red.Value = color.R;
                red = color.R;

            }

            buildColor();

            registerSliderEvents();
        }


        private void CB_Color1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            adjustSliderValues(new SharedLogic().ParseColor(CVC_Gradient1.getIdentifier()));
            active_gradient_color = 1;
        }
        private void CB_Color2_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            adjustSliderValues(new SharedLogic().ParseColor(CVC_Gradient2.getIdentifier()));
            active_gradient_color = 2;
        }
        private void CB_Color3_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            adjustSliderValues(new SharedLogic().ParseColor(CVC_Gradient3.getIdentifier()));
            active_gradient_color = 3;
        }
        private void CB_Color4_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            adjustSliderValues(new SharedLogic().ParseColor(CVC_Gradient4.getIdentifier()));
            active_gradient_color = 4;
        }
        private void CB_Color5_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            adjustSliderValues(new SharedLogic().ParseColor(CVC_Gradient5.getIdentifier()));
            active_gradient_color = 5;
        }
        private void CB_Color6_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            adjustSliderValues(new SharedLogic().ParseColor(CVC_Gradient6.getIdentifier()));
            active_gradient_color = 6;
        }



        private void CB_ResetColorData_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            colorData = new ColorData();
            resetCaller();
        }


        private void CB_SaveColorData_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            switch (active_gradient_color)
            {
                case 1:
                    CVC_Gradient1.setIdentifier(colorData.color1_string);
                    break;

                case 2:
                    CVC_Gradient2.setIdentifier(colorData.color2_string);
                    break;

                case 3:
                    CVC_Gradient3.setIdentifier(colorData.color3_string);
                    break;

                case 4:
                    CVC_Gradient4.setIdentifier(colorData.color4_string);
                    break;

                case 5:
                    CVC_Gradient5.setIdentifier(colorData.color5_string);
                    break;

                case 6:
                    CVC_Gradient6.setIdentifier(colorData.color6_string);
                    break;

                default:
                    CVC_Gradient1.setIdentifier(colorData.color1_string);
                    break;

            }

            updateGradientBrush();

            updateCaller();
        }

        private void CB_SolidColorBrush_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SP_GradientArea.Visibility = Visibility.Collapsed;

            BrushTypeSelection(CB_SolidColorBrush);

            colorData.brushtype = "SolidColorBrush";
            active_gradient_color = 1;

            e.Handled = true;
        }
        private void CB_RadialGradientBrush_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SP_GradientArea.Visibility = Visibility.Visible;

            BrushTypeSelection(CB_RadialGradientBrush);

            colorData.brushtype = "RadialGradientBrush";

            e.Handled = true;
        }
        private void CB_LinearGradientBrush_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SP_GradientArea.Visibility = Visibility.Visible;

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

            buildColor(true);

            e.Handled = true;

        }
    }
}
/*  END OF FILE
 * 
 * 
 */