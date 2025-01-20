using AidingElementsUserInterface.Core.AEUI_HelperClasses;
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
    /// Interaktionslogik für SessionClock.xaml
    /// </summary>
    public partial class SessionClock : UserControl
    {
        SystemPulseTimer systemPulse;

        public SessionClock()
        {
            InitializeComponent();
            systemPulse = new SystemPulseTimer();

            L_SessionClock.Content = "Session Clock";
            systemPulse.GET_timer.Tick += GET_timer_Tick;
            systemPulse.GET_timer.Interval = TimeSpan.FromMilliseconds(281);
            systemPulse.GET_timer.Start();
        }

        private void GET_timer_Tick(object? sender, System.EventArgs e)
        {
            long tickCountMs = Environment.TickCount64;

            var uptime = TimeSpan.FromMilliseconds(tickCountMs);


            CL_DateTimeNow_Date.setText(DateTime.UtcNow.ToLongDateString());
            CL_DateTimeNow_Time.setText(DateTime.UtcNow.ToLongTimeString());
            CL_SessionRuntime.setText($"{uptime.Hours:D2}:{uptime.Minutes:D2}:{uptime.Seconds:D2}");
            CL_SessionRuntime.ToolTip = "session uptime";
        }
    }
}
