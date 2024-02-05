/* Aiding Elements User Interface
 *      Text element
 * 
 * basic textbox editor, 
 * element deletion must be captured with a dialogresult or alike feature,
 * f.e. .<Text delete? deletes the appropriate .xml
 *      !.<Text force delete the appropriate .xml
 *      >Text create new text element
 *      .> load text element from screen\containerdata folder, incrementally increasing, if all are loaded, create new and save
 * 
 * init:        2024|02|02
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

namespace AidingElementsUserInterface.Core.AEUI_UserControls
{
    /// <summary>
    /// Interaktionslogik für Text.xaml
    /// </summary>
    public partial class Text : UserControl
    {
        public Text()
        {
            InitializeComponent();
        }
    }
}
/*  END OF FILE
 * 
 * 
 */