/* Aiding Elements User Interface
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
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
    /// Interaktionslogik für MyNote.xaml
    /// </summary>
    public partial class MyNote : UserControl
    {
        internal LogTab Tab_log = new LogTab();
        internal NotesTab Tab_notes = new NotesTab();
        internal NoteMatrixTab Tab_matrix = new NoteMatrixTab();
        internal ActivityTab Tab_history = new ActivityTab();

        public MyNote()
        {
            InitializeComponent(); Tab_history.history_entry("loading complete");

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

            XML_Handler handler = new XML_Handler();

            handler.save_log_to_xml(Tab_log.getNoteData());
            handler.save_notes_to_xml(Tab_notes.get_notes());
            handler.save_history_to_xml(Tab_history.application_closing());
        }
    }
}
