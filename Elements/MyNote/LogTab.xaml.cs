/* Aiding Elements User Interface
 *      LogTab element 
 * 
 * single Note element configurated to serve as a protocol
 * within MyNote element
 * 
 * init:        2023|11|30
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 * 
 * origin:      MyNote_2023_11_01
 */
using AidingElementsUserInterface.Core.Auxiliaries;
using AidingElementsUserInterface.Core;
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
using System.Windows.Threading;

namespace AidingElementsUserInterface.Elements.MyNote
{
    /// <summary>
    /// Interaktionslogik für LogTab.xaml
    /// </summary>
    public partial class LogTab : UserControl
    {
        private NoteData data;

        private DispatcherTimer _timer = new DispatcherTimer();

        public LogTab()
        {
            InitializeComponent();

            loadNoteData();

            updateDataOutput();

            _timer.Tick += _timer_Tick;
            _timer.Interval = TimeSpan.FromSeconds(0.28);
            _timer.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            data.dateTime = DateTime.Now;

            TX_Time.Content = data.dateTime.ToString();
        }

        private void append_to_log()
        {
            data.dateTime = DateTime.Now;

            data.title = TX_Title.Text;

            StringBuilder log = new StringBuilder();

            log.Append($"{data.dateTime}\n{data.title}\n\n{TX_Note.Text}");

            data.content.Clear();
            data.content.Append(log);

            updateDataOutput();
        }

        internal NoteData getNoteData()
        {
            return data;
        }

        internal void loadNoteData()
        {
            data = new XML_Handler().load_log_from_xml();

            data.dateTime = DateTime.Now;

            updateDataOutput();
        }

        private void protocol_changed()
        {
            //SharedLogic.GetMainWindow().Tab_history.history_entry(
            //    "protocol changed"
            //);
        }

        private void saveNoteData()
        {
            data.content.Clear();
            data.content.Append(TX_Note.Text);

            data.title = TX_Title.Text;
        }

        private void updateDataOutput()
        {
            TX_Time.Content = data.dateTime.ToString();
            TX_Title.Text = data.title;
            TX_Note.Text = data.content.ToString();
        }

        private void TX_Note_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                saveNoteData();
                protocol_changed();
            }
        }

        private void TX_Note_LostFocus(object sender, RoutedEventArgs e)
        {
            saveNoteData();
        }

        private void TX_Note_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            saveNoteData();
        }

        private void TX_Note_LostMouseCapture(object sender, MouseEventArgs e)
        {
            saveNoteData();
        }

        private void TX_Title_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                append_to_log();
                protocol_changed();
            }
        }

        private void TX_Title_LostFocus(object sender, RoutedEventArgs e)
        {
            saveNoteData();
        }

        private void TX_Title_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TX_Time_Click(object sender, RoutedEventArgs e)
        {
            append_to_log();
            protocol_changed();
        }

        private void TX_Title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void TX_Title_MouseEnter(object sender, MouseEventArgs e)
        {

            TX_Title.SelectAll();
        }

        private void TX_Title_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            TX_Title.SelectAll();
        }
    }
}
