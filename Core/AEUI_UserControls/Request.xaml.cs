/* Aiding Elements User Interface
 *      Request element
 * 
 * element combined of command element and logstat element
 * 
 * it displays commands, the time of input etc.
 * 
 * the plan is to develop this towards a useful system tool,
 * once the rest of the project has progressed further, currently
 * planned is an additional configurable infobitpanel, where
 * system stats and other system informations can be added to
 * a separate area within the Request element, right or atop
 * of logstat. 
 * 
 * init:        2024|01|19
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        //private void MI_LocalProcesses_IB_Click(object sender, RoutedEventArgs e)
        //{
        //    StringBuilder processOverview = new StringBuilder();

        //    // pretty ugly, because there are many processes, therefor it is not a good idea to 
        //    // make an infobit for all processes, but it is suitable for a system element

        //    foreach (Process process in Process.GetProcesses())
        //    {
        //        processOverview.AppendLine(
        //            $"{process.Id} {process.ProcessName} {process.BasePriority} {process.MachineName}"
        //            );

        //        // tested:
        //        //  -> working        {process.MachineName} {process.BasePriority} {process.Id} {process.ProcessName} 
        //        //  -> access denied  {process.UserProcessorTime} {process.ProcessorAffinity}
        //    }


        //    new CoreToolTip(processOverview.ToString());

        //    e.Handled = true;
        //}



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
/*  END OF FILE
 * 
 * 
 */