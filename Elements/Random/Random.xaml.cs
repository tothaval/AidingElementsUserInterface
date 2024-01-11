/* Aiding Elements User Interface
 *      Manual element 
 * 
 * create random numbers, choose between dices(base 6)
 * or random number display (base 10)
 * 
 * init:        2024|01|04
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core;
using AidingElementsUserInterface.Core.AEUI_UserControls;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AidingElementsUserInterface.Elements
{
    /// <summary>
    /// Interaktionslogik für Random.xaml
    /// </summary>
    public partial class Random : UserControl
    {
        public Random()
        {
            InitializeComponent();

            build();
        }

        private void build()
        {
            CB_Base10.setContent("base 10");
            CB_Base6.setContent("base 6");
            CB_Run.setContent("generate");
            CB_Print.setContent("print");

            CB_Base10.button.Click += CB_Base10_Click;
            CB_Base6.button.Click += CB_Base6_Click;

            CB_Run.button.Click += CB_Run_Click;
            CB_Print.button.Click += CB_Print_Click;
        }



        private void CB_Base10_Click(object sender, RoutedEventArgs e)
        {
            if (CB_Base10.isSelected)
            {
                CB_Base10.deselect();
            }
            else
            {
                border_RandomDisplay.Child = new CoreMatrix(typeof(CoreTextBox));

                CB_Base10.select();
                CB_Base6.deselect();
            }
        }

        private void CB_Base6_Click(object sender, RoutedEventArgs e)
        {
            if (CB_Base6.isSelected)
            {
                CB_Base6.deselect();
            }
            else
            {
                border_RandomDisplay.Child = new Ellipse() { Width = 100, Height = 200, Fill = new SolidColorBrush(Colors.Blue) };

                CB_Base6.select();
                CB_Base10.deselect();
            }
        }


        private void CB_Run_Click(object sender, RoutedEventArgs e)
        {
            if (border_RandomDisplay.Child.GetType() == typeof(CoreMatrix))
            {
                CoreMatrix cm = border_RandomDisplay.Child as CoreMatrix;

                foreach (CoreMatrixLine item in cm.GetCoreMatrixLines())
                {
                    foreach (UserControl uc in item.extract())
                    {
                        if (uc.GetType() == typeof(CoreTextBox))
                        {
                            CoreTextBox ctb = uc as CoreTextBox;
                            
                            System.Random random_Number = new System.Random(); ;
                            
                            ctb.setText(random_Number.Next().ToString());
                        }

                    }                    
                }
            }

        }

        private void CB_Print_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder stringBuilder= new StringBuilder();

            if (border_RandomDisplay.Child.GetType() == typeof(CoreMatrix))
            {
                CoreMatrix cm = border_RandomDisplay.Child as CoreMatrix;

                foreach (CoreMatrixLine item in cm.GetCoreMatrixLines())
                {
                    foreach (UserControl uc in item.extract())
                    {
                        if (uc.GetType() == typeof(CoreTextBox))
                        {
                            CoreTextBox ctb = uc as CoreTextBox;

                            stringBuilder.AppendLine(ctb.getText());
                        }

                    }
                }
            }

            MessageBox.Show(stringBuilder.ToString());
        }
        private void __Random_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
/*  END OF FILE
 * 
 * 
 */