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

            CTB_Log.appendText(
                $"\n{logEntry.id}: {logEntry.dateTime}\t{logEntry.title} {logEntry.content}"
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
