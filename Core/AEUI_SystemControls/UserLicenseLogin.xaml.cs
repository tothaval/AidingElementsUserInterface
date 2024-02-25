/* Aiding Elements User Interface
 *      UserLicenseLogin user control
 * 
 * mockup for much later, once licensing is more relevant and
 * login and alike functions are already implemented, users
 * can or must login using the login control to access their
 * stuff or the full featureset of the surface system
 * 
 * init:        2024|01|31
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using System.Windows;
using System.Windows.Controls;

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
/*  END OF FILE
 * 
 * 
 */