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
using AidingElementsUserInterface.Core.AEUI_SystemControls;
using AidingElementsUserInterface.Core.AEUI_UserControls;
using System;
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
        string mode = string.Empty;




        public Random()
        {
            InitializeComponent();

            build();
        }

        private void build()
        {   
            CB_Base6.setContent("base 6");
            CB_Base10.setContent("base 10");
            CB_BaseRange.setContent("range");

            CVC_MinRange.setIdentifier("min range (integer)");
            CVC_MaxRange.setIdentifier("max range (integer)");

            CB_Run.setContent("generate");
            CB_Print.setContent("print");

            CB_BaseRange.button.Click += CB_BaseRange_Click;
            CB_Base10.button.Click += CB_Base10_Click;
            CB_Base6.button.Click += CB_Base6_Click;

            CB_Run.button.Click += CB_Run_Click;
            CB_Print.button.Click += CB_Print_Click;
        }

        private void MODE_Base6()
        {
            mode = "Base6";

            SP_RandomRange.Visibility = Visibility.Collapsed;
        }

        private void MODE_Base10()
        {
            mode = "Base10";

            SP_RandomRange.Visibility = Visibility.Collapsed;
        }

        private void MODE_BaseRange()
        {
            mode = "BaseRange";

            SP_RandomRange.Visibility = Visibility.Visible;
        }

        private int Randomize()
        {
            System.Random random_Number = new System.Random();

            if (mode.Equals("Base6"))
            {
                return random_Number.Next(1, 7);
            }
            else if (mode.Equals("Base10"))
            {
                return random_Number.Next(1, 11);
            }
            else
            {
                int min = Int32.Parse(CVC_MinRange.Value);
                int max = Int32.Parse(CVC_MaxRange.Value);

                return random_Number.Next(min, max + 1);
            }
        }


        private void CB_BaseRange_Click(object sender, RoutedEventArgs e)
        {
            if (CB_BaseRange.isSelected)
            {
                CB_BaseRange.deselect();
            }
            else
            {
                border_RandomDisplay.Child = new CoreMatrix(typeof(CoreTextBox));

                CB_BaseRange.select();
                MODE_BaseRange();

                CB_Base10.deselect();
                CB_Base6.deselect();
            }
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
                MODE_Base10();

                CB_Base6.deselect();
                CB_BaseRange.deselect();
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
                border_RandomDisplay.Child = new CoreMatrix(typeof(CoreTextBox));

                CB_Base6.select();
                MODE_Base6();

                CB_Base10.deselect();
                CB_BaseRange.deselect();
            }
        }


        private void CB_Run_Click(object sender, RoutedEventArgs e)
        {
            CoreMatrix cm = border_RandomDisplay.Child as CoreMatrix;

            if (border_RandomDisplay.Child.GetType() == typeof(CoreMatrix))
            {
                foreach (CoreMatrixLine item in cm.GetCoreMatrixLines())
                {
                    foreach (UserControl uc in item.extract())
                    {
                        if (uc.GetType() == typeof(CoreTextBox))
                        {
                            CoreTextBox ctb = uc as CoreTextBox;

                            ctb.setText(Randomize().ToString());
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