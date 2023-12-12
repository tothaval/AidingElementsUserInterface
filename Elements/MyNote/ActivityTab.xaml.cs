/* Aiding Elements User Interface
 *      ActivityTab element 
 * 
 * element for logging of MyNote user activity
 * currently mostly intended and needed for debugging
 * 
 * 
 * init:        2023|11|30
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.Auxiliaries;
using AidingElementsUserInterface.Core.MyNote_Data;
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

namespace AidingElementsUserInterface.Elements.MyNote
{
    /// <summary>
    /// Interaktionslogik für ActivityTab.xaml
    /// </summary>
    public partial class ActivityTab : UserControl
    {
        private StringBuilder history = new StringBuilder();


        private MyNote origin_element;


        public ActivityTab(MyNote myNote)
        {
            origin_element = myNote;

            InitializeComponent();

            loadHistory();

            history.Insert(0, "\n");
            history_entry("MyNote activation");
        }

        public StringBuilder application_closing()
        {
            history_entry("saving complete");
            history_entry("MyNote deactivation");

            return history;
        }

        private void history_clear()
        {
            history.Clear();

            history_entry("history cleared");
        }

        public void history_entry(string entry)
        {
            history.Insert(0, $"{DateTime.Now} {entry}\n");

            //history.AppendLine($"{DateTime.Now} {entry}");

            TB_History.Text = history.ToString();
        }

        public void loadHistory()
        {
            history.Clear();

            history = new XML_Handler(origin_element).MyNote_load_history();

        }

        private void BT_ClearHistory_Click(object sender, RoutedEventArgs e)
        {
            history_clear();
        }
    }
}
/*  END OF FILE
 * 
 * 
 */