/* Aiding Elements User Interface
 *      CoreToolTip element
 *  
 * basic tooltip element with timer
 * 
 * init:        2024|02|01
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.Auxiliaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaktionslogik für CoreToolTip.xaml
    /// </summary>
    public partial class CoreToolTip : UserControl
    {
        private System.Windows.Threading.DispatcherTimer _timer = new System.Windows.Threading.DispatcherTimer();

        private DateTime start = DateTime.UtcNow;

        private string content = "";

        public CoreToolTip(string content)
        {
            InitializeComponent(); 

            this.content = content;

            P_CoreToolTip.PlacementTarget = new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS;

            _timer.Tick += _timer_Tick;
            _timer.Interval = TimeSpan.FromMilliseconds(1);
            _timer.Start();
        }


        private void _timer_Tick(object sender, EventArgs e)
        {
            if (DateTime.UtcNow - start < TimeSpan.FromSeconds(5.2))
            {
                if (content.Equals("SessionRuntime"))
                {
                    CL_ToolTip.setText(new SharedLogic().GetSystemPulseTimer().GetSessionRuntime());
                }
                else
                {
                    CL_ToolTip.setText(content);
                }
            }
            else
            {
                P_CoreToolTip.IsOpen = false;
            }
        }
    }
}
/*  END OF FILE
 * 
 * 
 */