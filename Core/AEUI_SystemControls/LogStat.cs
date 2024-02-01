/* Aiding Elements User Interface
 *      LogStat element
 *  
 * serves as a monitor for Command request feedback and other protocol, technical or os/system data
 * 
 * init:        2024|01|31
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.AEUI_HelperClasses;
using AidingElementsUserInterface.Core.AEUI_UserControls;
using AidingElementsUserInterface.Core.Auxiliaries;
using AidingElementsUserInterface.Core.MyNote_Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AidingElementsUserInterface.Core.AEUI_SystemControls
{
    internal class LogStat : CoreContainer
    {
        CoreTextBox CTB_Log = new CoreTextBox(true, $"{DateTime.UtcNow} log initiated\n\n");

        ObservableCollection<NoteData> command_logs = new ObservableCollection<NoteData>();

        public LogStat()
        {
            build();
        }

        private void build()
        {
            hideContainerNesting(this);

            content_border.Child = CTB_Log;
        }


        internal void update_log(Command command)
        {
            StringBuilder commandText = new StringBuilder();
            commandText.Append(command.GetCoreTextBox().getText());

            NoteData logEntry = new NoteData()
            {
                id = command_logs.Count,
                dateTime = DateTime.UtcNow,

                title = "command"
            };

            logEntry.content = commandText;
            
            command_logs.Add(logEntry);

            int screen_index = new SharedLogic().GetMainWindow().CORE_CANVAS_SWITCH.Get_coreCanvasScreens.IndexOf(
                new SharedLogic().GetMainWindow().CORE_CANVAS_SWITCH.Get_ACTIVE_CANVAS()
                );

            SystemPulseTimer systemPulse = new SharedLogic().GetMainWindow().CORE_CANVAS_SWITCH.Get_systemPulseTimers[screen_index];

            CTB_Log.appendText(
                $"\n{logEntry.id}: {logEntry.dateTime}\t{systemPulse.GetSessionRuntime()}\t {logEntry.title} {logEntry.content}"
                );
        }

        internal ObservableCollection<NoteData> getCommandLogs()
        {
            return command_logs;
        }

        //private void protocol_changed()
        //{
        //    origin_element.Tab_history.history_entry(
        //        "protocol changed"
        //    );
        //}

    }
}
/*  END OF FILE
 * 
 * 
 */