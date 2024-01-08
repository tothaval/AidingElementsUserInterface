/* Aiding Elements User Interface
 *      CoreContainerOptions 
 * 
 * container options element, used for the configuration of
 * already instantiated and selected containers
 * 
 * init:        2023|12|14
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
    /// Interaktionslogik für CoreContainerOptions.xaml
    /// </summary>
    public partial class CoreContainerOptions : UserControl
    {
        // set position values for all selected containers
        // --> x, y, z coordinates on the canvas

        // set background image or specify appearance via brush
        // and color configuration for all selected containers

        // specify fontsize, fontcolor, fontfamily and fontweight
        // of all selected containers, apply onto content elements

        // specify whether all selected containers can be deleted
        // via rightclick or ignore right click events, 
        // specify option to ask whether to be deleted upon right click

        // specify whether all selected containers can be moved
        // via leftclick

        // either use double value intakes or data input element
        // use checkboxes or radioboxes for binary(yes, no) options


        public CoreContainerOptions()
        {
            InitializeComponent();
        }
    }
}
