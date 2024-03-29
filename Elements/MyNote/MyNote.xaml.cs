﻿/* Aiding Elements User Interface
 *      MyNote element 
 * 
 * enables the user to deactivate the application
 * or shutdown the computer entirely
 * 
 * init:        2023|11|30
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 * 
 * origin:      MyNote_2023_11_01 (former MainWindow.xaml & MainWindow.xaml.cs)
 */
using AidingElementsUserInterface.Core.Auxiliaries;
using AidingElementsUserInterface.Core.MyNote_Data;

using System.Windows;
using System.Windows.Controls;

namespace AidingElementsUserInterface.Elements.MyNote
{
    /// <summary>
    /// Interaktionslogik für MyNote.xaml
    /// </summary>
    public partial class MyNote : UserControl
    {
        internal LogTab Tab_log;
        internal NotesTab Tab_notes;
        internal NoteMatrixTab Tab_matrix;
        internal ActivityTab Tab_history;

        public MyNote()
        {
            InitializeComponent();

            Tab_log = new LogTab(this);
            Tab_notes = new NotesTab(this);
            Tab_matrix = new NoteMatrixTab(this);
            Tab_history = new ActivityTab(this);

            Tab_history.history_entry("loading complete");

            build();
        }

        private void build()
        {

            TI_log.Content = Tab_log;

            TI_notes.Content = Tab_notes;

            TI_matrix.Content = Tab_matrix;

            TI_history.Content = Tab_history;
        }


        private void BTN_options_Click(object sender, RoutedEventArgs e)
        {
            if (SP_options.Visibility == Visibility.Collapsed)
            {
                SP_options.Visibility = Visibility.Visible;
            }
            else
            {
                SP_options.Visibility = Visibility.Collapsed;
            }

            e.Handled = true;
        }

        private void __MyNote_LostFocus(object sender, RoutedEventArgs e)
        {
            Tab_matrix.save_matrix();

            XML_Handler handler = new XML_Handler(this);

            handler.MyNote_save_log(Tab_log.getNoteData());

            handler = new XML_Handler(this);

            handler.MyNote_save_notes(Tab_notes.get_notes());

            handler = new XML_Handler(this);

            handler.MyNote_save_history(Tab_history.application_closing());
        }
    }
}
/*  END OF FILE
 * 
 * 
 */