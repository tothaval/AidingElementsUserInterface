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

namespace AidingElementsUserInterface.Core.AEUI_SystemControls
{
    /// <summary>
    /// Interaktionslogik für UserLicenseLogin.xaml
    /// 
    /// mockup
    /// </summary>
    public partial class UserLicenseLogin : UserControl
    {
        public UserLicenseLogin()
        {
            InitializeComponent();

            build();
        }

        private void build()
        {
            CB_LogOut.setContent("LogOut");
        }

        private void MI_Abonoments_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MI_Extensions_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MI_Themes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MI_Own_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
