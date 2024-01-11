/* Aiding Elements User Interface
 *      MatrixElement element 
 * 
 * creates a line of note elements for NoteMatrixTab
 * 
 * init:        2023|11|27
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace AidingElementsUserInterface.Elements.MyNote
{
    /// <summary>
    /// Interaktionslogik für MatrixElement.xaml
    /// </summary>
    public partial class MatrixElement : UserControl
    {
        public int id { get; set; }

        private MyNote origin_element;

        public MatrixElement(MyNote myNote)
        {
            this.origin_element = myNote;

            InitializeComponent();            
        }

        public ObservableCollection<Note> extract()
        {
            ObservableCollection<Note> notes = new ObservableCollection<Note>();

            foreach (Note item in SP.Children)
            {
                notes.Add(item);
            }

            return notes;
        }

        public void fill(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                insert();
            }
        }

        public Note insert()
        {
            Note note = new Note(origin_element);

            SP.Children.Add(note);

            return note;
        }

        public Note insert(Note note)
        {
            SP.Children.Add(note);

            return note;
        }

        public void remove_last_note()
        {
            if (CKX_Delete.IsChecked == true)
            {
                int index = SP.Children.Count - 1;

                if (index > -1)
                {
                    SP.Children.RemoveAt(index);
                }
            }
        }


        private void CKX_Select_Checked(object sender, RoutedEventArgs e)
        {
            CKX_Delete.Visibility = Visibility.Visible;
            CKX_Delete.IsEnabled = true;

            BTN_minus.Visibility = Visibility.Visible;
            BTN_minus.IsEnabled = true;

            BTN_plus.Visibility = Visibility.Visible;
            BTN_plus.IsEnabled = true;

        }

        private void CKX_Select_Unchecked(object sender, RoutedEventArgs e)
        {
            CKX_Delete.Visibility = Visibility.Collapsed;
            CKX_Delete.IsEnabled = false;

            BTN_minus.Visibility = Visibility.Collapsed;
            BTN_minus.IsEnabled = false;

            BTN_plus.Visibility = Visibility.Collapsed;
            BTN_plus.IsEnabled = false;
        }

        private void BTN_minus_Click(object sender, RoutedEventArgs e)
        {
            remove_last_note();
        }

        private void BTN_plus_Click(object sender, RoutedEventArgs e)
        {
            insert();
        }
    }
}
/*  END OF FILE
 * 
 * 
 */