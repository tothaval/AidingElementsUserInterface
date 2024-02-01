/* Aiding Elements User Interface
 *      SystemPulseTimer class
 *      
 * provides time functionalities for corecanvas screens
 * 
 * init:        2024|02|01
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.Auxiliaries;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AidingElementsUserInterface.Core.AEUI_HelperClasses
{
    internal class SystemPulseTimer
    {
        private int screen_index = -100;

        private System.Windows.Threading.DispatcherTimer _timer = new System.Windows.Threading.DispatcherTimer();

        private bool breakState = false;

        private DateTime initiation_time = new DateTime();

        public SystemPulseTimer(int screen_index)
        {
            this.screen_index = screen_index;
            initiation_time = DateTime.UtcNow;

            _timer.Tick += _timer_Tick;
            _timer.Interval = TimeSpan.FromMilliseconds(1);
            _timer.Start();

            //tj.start_stopwatch();
        }

        // tick and sessionswitch
        #region tick and sessionswitch
        //public void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        //{
        //    if (e.Reason == SessionSwitchReason.SessionLock)
        //    {
        //        handleBreak();
        //        timeLog();
        //    }

        //    else if (e.Reason == SessionSwitchReason.SessionUnlock)
        //    {
        //        handleBreak();
        //        timeLog();
        //    }
        //}


        internal string GetSessionRuntime()
        {
            return $"{DateTime.UtcNow - initiation_time}";
        }
            
            

        private void _timer_Tick(object sender, EventArgs e)
        {
            CoreCanvas active_canvas = new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS;

            if (active_canvas.get_CANVAS_NAME().Contains(screen_index.ToString()))
            {
                active_canvas.SetCanvasToolTip(GetSessionRuntime());
            }
            

        }
        #endregion tick and sessionswitch

    }
}
/*  END OF FILE
 * 
 * 
 */