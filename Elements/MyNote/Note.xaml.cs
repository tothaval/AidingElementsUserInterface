/* Aiding Elements User Interface
 *      Note element 
 * 
 * a text input user control mainly for MyNote element
 * 
 * init:        2023|11|27
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

namespace AidingElementsUserInterface.Elements.MyNote
{
    /// <summary>
    /// Interaktionslogik für Note.xaml
    /// </summary>
    public partial class Note : UserControl
    {
        private NoteData data;

        internal Note()
        {
            data = new NoteData();

            InitializeComponent();

            updateDataOutput();
        }

        internal Note(NoteData data)
        {
            this.data = data;

            InitializeComponent();

            updateDataOutput();
        }

        internal NoteData getNoteData()
        {
            return data;
        }

        private void inform_on_update()
        {
            //if (SharedLogic.GetMainWindow().Tab_notes != null)
            //{
            //    SharedLogic.GetMainWindow().Tab_notes.update_output();
            //}
        }

        internal void loadNoteData(NoteData noteData)
        {
            data = noteData;

            updateDataOutput();
        }

        private void note_title_changed(object sender)
        {
            TextBox tx = (TextBox)sender;

            //if (SharedLogic.GetMainWindow().Tab_history != null)
            //{
            //    SharedLogic.GetMainWindow().Tab_history.history_entry(
            //        $"changed note '{data.title}' >>>> '{tx.Text}'");
            //}

            saveNoteData();

            inform_on_update();
        }

        internal void saveData()
        {
            data.content.Clear();
            data.content.Append(TX_Note.Text);

            data.title = TX_Title.Text;
        }


        internal void saveNoteData()
        {
            data.content.Clear();
            data.content.Append(TX_Note.Text);

            data.title = TX_Title.Text;

            inform_on_update();
        }


        private void updateDataOutput()
        {
            TX_Time.Text = data.dateTime.ToString();
            TX_Title.Text = data.title;
            TX_Note.Text = data.content.ToString();
        }

        private void TX_Note_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                saveNoteData();
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
                note_title_changed(sender);
            }
        }

        private void TX_Title_LostFocus(object sender, RoutedEventArgs e)
        {
            saveNoteData();
        }

        private void TX_Time_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void TX_Time_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void TX_Time_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                TX_Time.Text = DateTime.Now.ToString();

                e.Handled = true;
            }
        }

        private void TX_Title_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
