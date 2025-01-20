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
using System.Windows;

namespace AidingElementsUserInterface.Core.AEUI_HelperClasses
{
    internal class SystemPulseTimer
    {
        private System.Windows.Threading.DispatcherTimer _timer = new System.Windows.Threading.DispatcherTimer();

        private DateTime initiation_time = new DateTime();

        internal System.Windows.Threading.DispatcherTimer GET_timer => _timer;

        public SystemPulseTimer()
        {
            initiation_time = DateTime.UtcNow;
        }


        internal string GetSessionRuntime()
        {
            return $"{DateTime.UtcNow - initiation_time}";
        }

    }
}
/*  END OF FILE
 * 
 * 
 */