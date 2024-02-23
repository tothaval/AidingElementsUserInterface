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
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Color = System.Windows.Media.Color;
using ColorConverter = System.Windows.Media.ColorConverter;

namespace AidingElementsUserInterface.Core.AEUI_UserControls
{
    /// <summary>
    /// Interaktionslogik für BrushSetup.xaml
    /// </summary>
    public partial class BrushSetup : UserControl
    {
        /*
         * current problems:
         * 
         * data will not be saved to container data, therefor it will not be saved or loaded correctly
         * linear and radial gradient brushes atm disrupt the data structure 
         * 
         * 
         * 
         */


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

        private void adjustSliderValues(Color color)
        {
            unregisterSliderEvents();

            if (color.R == color.B && color.B == color.G)
            {
                S_Alpha.Value = color.A;
                alpha = color.A;

                S_Blue.Value = color.R;
                S_Green.Value = color.R;
                S_Red.Value = color.R;

                S_Grey.Value = color.R;
                grey = color.R;

                buildColor(true);
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

                buildColor();
            }

            registerSliderEvents();
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

            CVC_ColorStringInput.setIdentifier("color value");
            CVC_ColorStringInput.setText($"{colorData.color1_string}");

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

            CVC_GradientOrigin.setIdentifier($"origin point");
            CVC_GradientStart.setIdentifier($"start point");
            CVC_GradientStop.setIdentifier($"end point");

            CVC_GradientOrigin.setText($"{colorData.gradientStartPoint}");
            CVC_GradientStart.setText($"{colorData.gradientEndPoint}");
            CVC_GradientStop.setText($"{colorData.gradientOrigin}");

            CB_ResetColorData.setContent("reset brush");
            CB_SaveColorData.setContent("save brush");

            BrushTypeSelection(CB_SolidColorBrush);

            colorData.brushtype = "SolidColorBrush";
            active_gradient_color = 1;

            SP_GradientArea.Visibility = Visibility.Collapsed;

            adjustSliderValues(new SharedLogic().ParseColor(colorData.color1_string));

            gradientPointCVCs();
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
        }

        private void gradientPointCVCs()
        {
            if (colorData.brushtype.Equals("RadialGradientBrush"))
            {
                CVC_GradientOrigin.Visibility = Visibility.Visible;

                CVC_GradientStart.Visibility = Visibility.Collapsed;
                CVC_GradientStop.Visibility = Visibility.Collapsed;
            }
            else if (colorData.brushtype.Equals("LinearGradientBrush"))
            {
                CVC_GradientStart.Visibility = Visibility.Visible;
                CVC_GradientStop.Visibility = Visibility.Visible;

                CVC_GradientOrigin.Visibility = Visibility.Collapsed;
            }
            else
            {
                CVC_GradientOrigin.Visibility = Visibility.Collapsed;
                CVC_GradientStart.Visibility = Visibility.Collapsed;
                CVC_GradientStop.Visibility = Visibility.Collapsed;
            }
        }

        private void registerEvents()
        {
            CVC_ColorStringInput.CTB_Value.KeyDown += CVC_ColorStringInput_KeyDown;
            CVC_ColorStringInput.CTB_Value.KeyUp += CVC_ColorStringInput_KeyUp;

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

        private void setColor(Color color)
        {
            ColorField.Children.Clear();

            ColorField.Background = new SolidColorBrush(color);

            UniformGrid grid = new UniformGrid()
            {
                Width = 150,
                Height = 150
            };

            Label label = new Label();
            label.Content = color.ToString();
            label.Background = new SolidColorBrush(Color.FromArgb(154, 00, 00, 00));
            label.Foreground = new SolidColorBrush(Color.FromArgb(255, 231, 154, 154));

            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Center;

            grid.Children.Add(label);

            ColorField.Children.Add(grid);

            switch (active_gradient_color)
            {
                case 1:
                    colorData.color1_string = color.ToString();
                    CVC_Gradient1.setIdentifier(color.ToString());
                    break;
                case 2:
                    colorData.color2_string = color.ToString();
                    CVC_Gradient2.setIdentifier(color.ToString());
                    break;
                case 3:
                    colorData.color3_string = color.ToString();
                    CVC_Gradient3.setIdentifier(color.ToString());
                    break;
                case 4:
                    colorData.color4_string = color.ToString();
                    CVC_Gradient4.setIdentifier(color.ToString());
                    break;
                case 5:
                    colorData.color5_string = color.ToString();
                    CVC_Gradient5.setIdentifier(color.ToString());
                    break;
                case 6:
                    colorData.color6_string = color.ToString();

                    CVC_Gradient6.setIdentifier(color.ToString());
                    break;

                default:
                    colorData.color1_string = color.ToString();
                    CVC_Gradient1.setIdentifier(color.ToString());
                    break;
            }

            CVC_ColorStringInput.setText(color.ToString());

            updateGradientBrush();
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

        private async void updateGradientBrush()
        {
            if (SP_GradientArea.Visibility == Visibility.Visible)
            {
                SharedLogic logic = new SharedLogic();

                colorData.color1_string = CVC_Gradient1.getIdentifier();
                colorData.color2_string = CVC_Gradient2.getIdentifier();
                colorData.color3_string = CVC_Gradient3.getIdentifier();
                colorData.color4_string = CVC_Gradient4.getIdentifier();
                colorData.color5_string = CVC_Gradient5.getIdentifier();
                colorData.color6_string = CVC_Gradient6.getIdentifier();

                await Task.Delay(4);

                ObservableCollection<double> offsets = new ObservableCollection<double>()
                {
                    Double.Parse(CVC_Gradient1.Value, CultureInfo.InvariantCulture),
                    Double.Parse(CVC_Gradient2.Value, CultureInfo.InvariantCulture),
                    Double.Parse(CVC_Gradient3.Value, CultureInfo.InvariantCulture),
                    Double.Parse(CVC_Gradient4.Value, CultureInfo.InvariantCulture),
                    Double.Parse(CVC_Gradient5.Value, CultureInfo.InvariantCulture),
                    Double.Parse(CVC_Gradient6.Value, CultureInfo.InvariantCulture)
                };

                colorData.setOffsets(offsets);

                await Task.Delay(4);

                if (colorData.brushtype.Equals("LinearGradientBrush"))
                {
                    string[] split_Start = CVC_GradientStart.Value.Split(';');
                    string[] split_End = CVC_GradientStop.Value.Split(';');

                    System.Windows.Point start_point = new System.Windows.Point(
                        Double.Parse(split_Start[0]),
                        Double.Parse(split_Start[1])
                        );
                    System.Windows.Point end_point = new System.Windows.Point(
                        Double.Parse(split_End[0]),
                        Double.Parse(split_End[1])
                        );

                    colorData.gradientStartPoint = start_point;
                    colorData.gradientEndPoint = end_point;

                    LinearGradientBrush linearGradientBrush = new LinearGradientBrush()
                    {
                        StartPoint = start_point,
                        EndPoint = end_point
                    };

                    linearGradientBrush.GradientStops.Add(new GradientStop(logic.ParseColor(colorData.color1_string), offsets[0]));
                    linearGradientBrush.GradientStops.Add(new GradientStop(logic.ParseColor(colorData.color2_string), offsets[1]));
                    linearGradientBrush.GradientStops.Add(new GradientStop(logic.ParseColor(colorData.color3_string), offsets[2]));
                    linearGradientBrush.GradientStops.Add(new GradientStop(logic.ParseColor(colorData.color4_string), offsets[3]));
                    linearGradientBrush.GradientStops.Add(new GradientStop(logic.ParseColor(colorData.color5_string), offsets[4]));
                    linearGradientBrush.GradientStops.Add(new GradientStop(logic.ParseColor(colorData.color6_string), offsets[5]));


                    RECT_GradientArea.Fill = linearGradientBrush;
                }
                else if (colorData.brushtype.Equals("RadialGradientBrush"))
                {
                    string[] split_Origin = CVC_GradientOrigin.Value.Split(';');

                    System.Windows.Point origin_point = new System.Windows.Point(
                        Double.Parse(split_Origin[0]),
                        Double.Parse(split_Origin[1])
                    );


                    colorData.gradientOrigin = origin_point;

                    RadialGradientBrush radialGradientBrush = new RadialGradientBrush()
                    {
                        GradientOrigin = origin_point
                    };

                    radialGradientBrush.GradientStops.Add(new GradientStop(logic.ParseColor(colorData.color1_string), offsets[0]));
                    radialGradientBrush.GradientStops.Add(new GradientStop(logic.ParseColor(colorData.color2_string), offsets[1]));
                    radialGradientBrush.GradientStops.Add(new GradientStop(logic.ParseColor(colorData.color3_string), offsets[2]));
                    radialGradientBrush.GradientStops.Add(new GradientStop(logic.ParseColor(colorData.color4_string), offsets[3]));
                    radialGradientBrush.GradientStops.Add(new GradientStop(logic.ParseColor(colorData.color5_string), offsets[4]));
                    radialGradientBrush.GradientStops.Add(new GradientStop(logic.ParseColor(colorData.color6_string), offsets[5]));

                    RECT_GradientArea.Fill = radialGradientBrush;
                }
            }
        }

        // CB events CB_Color1 - CB_Color6
        #region CB events CB_Color1 - CB_Color6
        private async void CB_Color1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            active_gradient_color = 1;

            Task.Delay(1);

            adjustSliderValues((Color)ColorConverter.ConvertFromString(CVC_Gradient1.getIdentifier()));

            e.Handled = true;

        }
        private void CB_Color2_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            active_gradient_color = 2;
            adjustSliderValues(new SharedLogic().ParseColor(CVC_Gradient2.getIdentifier()));

            e.Handled = true;
        }
        private void CB_Color3_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            active_gradient_color = 3;
            adjustSliderValues(new SharedLogic().ParseColor(CVC_Gradient3.getIdentifier()));

            e.Handled = true;
        }
        private void CB_Color4_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            active_gradient_color = 4;
            adjustSliderValues(new SharedLogic().ParseColor(CVC_Gradient4.getIdentifier()));

            e.Handled = true;
        }
        private void CB_Color5_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            active_gradient_color = 5;
            adjustSliderValues(new SharedLogic().ParseColor(CVC_Gradient5.getIdentifier()));

            e.Handled = true;
        }
        private void CB_Color6_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            active_gradient_color = 6;
            adjustSliderValues(new SharedLogic().ParseColor(CVC_Gradient6.getIdentifier()));

            e.Handled = true;
        }
        #endregion CB events CB_Color1 - CB_Color6

        // CB events CB_ResetColorData, CB_SaveColorData
        #region CB events CB_ResetColorData, CB_SaveColorData
        private void CB_ResetColorData_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            colorData = new ColorData();
            resetCaller();

            e.Handled = true;
        }

        private async void CB_SaveColorData_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CVC_Gradient1.setIdentifier(colorData.color1_string);

            CVC_Gradient2.setIdentifier(colorData.color2_string);

            CVC_Gradient3.setIdentifier(colorData.color3_string);

            CVC_Gradient4.setIdentifier(colorData.color4_string);

            CVC_Gradient5.setIdentifier(colorData.color5_string);

            CVC_Gradient6.setIdentifier(colorData.color6_string);

            Task.Delay(4);

            updateGradientBrush();

            Task.Delay(4);

            updateCaller();

            e.Handled = true;
        }
        #endregion CB events CB_ResetColorData, CB_SaveColorData

        // CB events CB Brushselection
        #region CB events CB Brushselection
        private void CB_SolidColorBrush_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            BrushTypeSelection(CB_SolidColorBrush);

            colorData.brushtype = "SolidColorBrush";
            active_gradient_color = 1;

            SP_GradientArea.Visibility = Visibility.Collapsed;

            adjustSliderValues(new SharedLogic().ParseColor(colorData.color1_string));

            gradientPointCVCs();

            e.Handled = true;
        }

        private void CB_RadialGradientBrush_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            BrushTypeSelection(CB_RadialGradientBrush);

            colorData.brushtype = "RadialGradientBrush";
            active_gradient_color = 1;

            SP_GradientArea.Visibility = Visibility.Visible;

            adjustSliderValues(new SharedLogic().ParseColor(colorData.color1_string));

            gradientPointCVCs();

            e.Handled = true;
        }

        private void CB_LinearGradientBrush_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            BrushTypeSelection(CB_LinearGradientBrush);

            colorData.brushtype = "LinearGradientBrush";

            active_gradient_color = 1;

            SP_GradientArea.Visibility = Visibility.Visible;

            adjustSliderValues(new SharedLogic().ParseColor(colorData.color1_string));

            gradientPointCVCs();

            e.Handled = true;
        }

        private void CB_ImageBrush_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            BrushTypeSelection(CB_ImageBrush);

            SP_GradientArea.Visibility = Visibility.Collapsed;

            colorData.brushtype = "ImageBrush";

            OpenFileDialog file = new SharedLogic().openDialog();

            if (file != null)
            {
                if (File.Exists(file.FileName))
                {
                    colorData.brushpath = file.FileName;

                    ColorField.Children.Clear();
                    ColorField.Background = colorData.GetBrush();
                }
                else
                {
                    colorData.brushtype = " SolidColorBrush";
                }
            }

            gradientPointCVCs();

            e.Handled = true;
        }

        private void CB_DrawingBrush_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            BrushTypeSelection(CB_DrawingBrush);

            //colorData.brushtype = "DrawingBrush";

            SP_GradientArea.Visibility = Visibility.Collapsed;

            ColorField.Children.Clear();

            UniformGrid grid = new UniformGrid()
            {
                Width = 150,
                Height = 150
            };

            Label label = new Label()
            {
                Content = "not yet implemented",
                Background = new SolidColorBrush(Color.FromArgb(154, 00, 00, 00)),
                Foreground = new SolidColorBrush(Color.FromArgb(255, 231, 154, 154)),

                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            grid.Children.Add(label);
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            grid.VerticalAlignment = VerticalAlignment.Stretch;

            ColorField.Children.Add(grid);

            gradientPointCVCs();

            e.Handled = true;
        }

        private void CB_VisualBrush_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            BrushTypeSelection(CB_VisualBrush);

            //colorData.brushtype = "VisualBrush";

            SP_GradientArea.Visibility = Visibility.Collapsed;

            ColorField.Children.Clear();

            UniformGrid grid = new UniformGrid()
            {
                Width = 150,
                Height = 150
            };

            Label label = new Label()
            {
                Content = "not yet implemented",
                Background = new SolidColorBrush(Color.FromArgb(154, 00, 00, 00)),
                Foreground = new SolidColorBrush(Color.FromArgb(255, 231, 154, 154)),

                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            grid.Children.Add(label);
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            grid.VerticalAlignment = VerticalAlignment.Stretch;

            ColorField.Children.Add(grid);

            gradientPointCVCs();

            e.Handled = true;
        }
        #endregion CB events CB Brushselection

        // CVC_ColorStringInput textbox events
        #region CVC_ColorStringInput textbox events
        private async void CVC_ColorStringInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {

            }
        }

        private void CVC_ColorStringInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                try
                {
                    Color color = new SharedLogic().ParseColor(CVC_ColorStringInput.Value);

                    adjustSliderValues(color);
                }
                catch (Exception)
                {
                    MessageBox.Show("wrong input format\ncolor not recognized");

                    adjustSliderValues(new Color());
                }
            }

            e.Handled = true;
        }
        #endregion CVC_ColorStringInput textbox events

        // Slider events
        #region Slider events
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
        #endregion Slider events
    }
}
/*  END OF FILE
 * 
 * 
 */