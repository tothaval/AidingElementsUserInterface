﻿/* Aiding Elements User Interface
 *      Clock element 
 * 
 * setupable timer element
 * 
 * init:        2024|01|31
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core;
using AidingElementsUserInterface.Core.AEUI_HelperClasses;
using AidingElementsUserInterface.Core.Auxiliaries;
using System;
using System.Windows.Controls;

namespace AidingElementsUserInterface.Elements
{
    /// <summary>
    /// Interaktionslogik für Clock.xaml
    /// </summary>
    public partial class Clock : UserControl
    {
        SystemPulseTimer systemPulse;

        public Clock()
        {
            InitializeComponent();       

            systemPulse = new SystemPulseTimer();

            L_Clock.Content = "Clock";
            systemPulse.GET_timer.Tick += GET_timer_Tick;
            systemPulse.GET_timer.Interval = TimeSpan.FromMilliseconds(281);
            systemPulse.GET_timer.Start();
        }

        private void GET_timer_Tick(object? sender, System.EventArgs e)
        {
            CL_DateTimeNow_Date.setText(DateTime.UtcNow.ToLongDateString());
            CL_DateTimeNow_Time.setText(DateTime.UtcNow.ToLongTimeString());
            CL_SessionRuntime.setText(systemPulse.GetSessionRuntime());
            CL_SessionRuntime.ToolTip = "clock uptime";
        }
    }
}
/*  END OF FILE
 * 
 * 
 */