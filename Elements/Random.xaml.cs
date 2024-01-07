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

            CB_Base10.button.Click += CB_Base10_Click;
            CB_Base6.button.Click += CB_Base6_Click;
        }

        private void CB_Base10_Click(object sender, RoutedEventArgs e)
        {
            if (CB_Base10.isSelected)
            {
                CB_Base10.deselect();
            }
            else
            {
                border_RandomDisplay.Child = new Rectangle() { Width = 100, Height = 200, Fill = new SolidColorBrush(Colors.Red) };

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

        private void __Random_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
/*  END OF FILE
 * 
 * 
 */