/* Aiding Elements User Interface
 *      Image element 
 * 
 * displays an image
 * 
 * init:        2024|01|03
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.AEUI_Data;
using System.Windows;
using System.Windows.Controls;

namespace AidingElementsUserInterface.Elements
{
    /// <summary>
    /// Interaktionslogik für Image.xaml
    /// </summary>
    public partial class Image : UserControl
    {

        // use scale transform to zoom in or out using mousewheel
        public Image()
        {
            InitializeComponent();
        }

        private void __Image_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
/*  END OF FILE
 * 
 * 
 */