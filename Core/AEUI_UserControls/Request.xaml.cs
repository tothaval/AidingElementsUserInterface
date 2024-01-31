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
    /// Interaktionslogik für Request.xaml
    /// </summary>
    public partial class Request : UserControl
    {
        public Request()
        {
            InitializeComponent();

            build();
            registerEvents();
        }

        private void build()
        {     
        }

        private void registerEvents()
        {
            C_Console.GetCoreTextBox().textbox.KeyDown += Textbox_KeyDown;
            C_Console.GetCoreTextBox().textbox.KeyUp += Textbox_KeyUp; ;

        }

        private void Textbox_KeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void Textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                LS_Protocol.update_log(C_Console);
            }            
        }

    }
}
