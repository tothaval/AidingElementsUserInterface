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
    /// Interaktionslogik für CoreContainer.xaml
    /// </summary>
    public partial class CoreContainer : UserControl
    {
        UserControl content;

        public CoreContainer(UserControl element)
        {
            content = element;

            InitializeComponent();

            initialize_container();
        }

        private void initialize_container()
        {
            if (content == null)
            {
                content = new UserControl();                
            }

            content_border.Child = content;
        }

        private void Container_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
