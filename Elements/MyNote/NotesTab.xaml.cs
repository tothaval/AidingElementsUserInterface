/* Aiding Elements User Interface
 *      NotesTab element 
 * 
 * presents and manages an observable collection of Note elements
 * within MyNote element
 * 
 * 
 * init:        2023|11|30
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 * 
 * origin:      MyNote_2023_11_01
 */
using AidingElementsUserInterface.Core.Auxiliaries;
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
using System.Collections.ObjectModel;
using AidingElementsUserInterface.Core.MyNote_Data;

namespace AidingElementsUserInterface.Elements.MyNote
{
    /// <summary>
    /// Interaktionslogik für NotesTab.xaml
    /// </summary>
    public partial class NotesTab : UserControl
    {
        private ObservableCollection<Note> notes = new ObservableCollection<Note>();
        private int active_note = 0;


        private MyNote origin_element;


        public NotesTab(MyNote myNote)
        {
            origin_element = myNote;

            InitializeComponent();

            loadNotes();
        }

        internal Note add_note(Note note)
        {
            if (!notes.Contains(note))
            {
                notes.Add(note);

                add_usc_to_panel(note);

                active_note = notes.Count;

                update_output();

                origin_element.Tab_history.history_entry(
                $"note '{note.getNoteData().dateTime}' '{note.getNoteData().title}' created"
                );

            }

            return note;
        }

        internal void add_usc_to_panel(UserControl userControl)
        {
            MainWrapPanel.Children.Clear();

            MainWrapPanel.Children.Add(userControl);
        }

        internal ObservableCollection<Note> get_notes()
        {
            return notes;
        }

        private void add_note_click()
        {
            add_note(new_note());
        }

        private void delete_note_click()
        {
            if (notes.Count > 0 && MainWrapPanel.Children.Count > 0)
            {
                Note deletionObject = (Note)MainWrapPanel.Children[0];

                foreach (Note note in notes)
                {
                    if (note == deletionObject)
                    {
                        notes.Remove(note);

                        origin_element.Tab_history.history_entry(
                            $"note '{note.getNoteData().dateTime}'" +
                            $" '{note.getNoteData().title}' deleted"
                            );

                        if (notes.Count <= active_note)
                        {
                            active_note = notes.Count;
                        }

                        update_output();
                        break;
                    }
                }
            }
        }

        internal void loadNotes()
        {
            ObservableCollection<NoteData> noteDatas = new XML_Handler(origin_element).MyNote_load_notes();

            if (noteDatas.Count > 0)
            {
                int i = 1;

                foreach (NoteData data in noteDatas)
                {
                    data.id = i;
                    i++;

                    Note note = new Note(origin_element, data);

                    notes.Add(note);
                }

                add_usc_to_panel(notes[0]);
            }
            else
            {
                add_note(new_note());
            }

            update_output();
        }

        internal Note new_note()
        {
            Note note = new Note(origin_element);

            note.getNoteData().id = notes.Count + 1;

            return note;
        }

        private void note_selection(int active_note = 0)
        {
            MainWrapPanel.Children.Clear();

            foreach (Note note in notes)
            {
                if (note.getNoteData().id == active_note)
                {
                    MainWrapPanel.Children.Add(note);
                    break;
                }
            }
        }


        private void deselect_all()
        {
            foreach (Button selector in NoteSelectionWrapPanel.Children)
            {
                selector.Background = new SolidColorBrush(Colors.MistyRose);

            }
        }

        private void note_selection_click_handler(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            string name = button.Name;
            active_note = Int32.Parse(name.Replace("button", ""));

            note_selection(active_note);

            //SharedLogic.GetMainWindow().Tab_history.history_entry(
            //    $"{name} '{button.Content}' clicked"
            //    );

            update_output();
        }

        internal void update_output()
        {
            if (MainWrapPanel != null)
            {
                NoteSelectionWrapPanel.Children.Clear();

                int i = 1;

                foreach (Note note in notes)
                {
                    Button button = new Button();

                    note.getNoteData().id = i;
                    string title = note.getNoteData().title;

                    button.Name = $"button{i}";

                    button.Background = new SolidColorBrush(Colors.FloralWhite);
                    button.Foreground = new SolidColorBrush(Colors.SlateGray);

                    if (i == active_note)
                    {
                        button.Background = new SolidColorBrush(Colors.NavajoWhite);
                        note_selection(i);
                    }

                    button.Content = $"{title}";
                    button.Click += Button_Click;
                    button.PreviewMouseLeftButtonDown += Button_PreviewMouseLeftButtonDown;
                    button.PreviewMouseLeftButtonUp += Button_PreviewMouseLeftButtonUp;

                    button.ToolTip = $"{button.Content}";

                    button.Margin = new Thickness(5, 0, 5, 0);
                    button.Padding = new Thickness(2);

                    NoteSelectionWrapPanel.Children.Add(button);

                    i++;
                }

                TB_NoteCount.Text = $"Notes x {notes.Count}";
                TB_User.Text = $"User : {Environment.UserName}";
            }
        }

        private void Button_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            note_selection_click_handler(sender, e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            note_selection_click_handler(sender, e);
        }

        private void BTN_NewNote_Click(object sender, RoutedEventArgs e)
        {
            add_note_click();
        }

        private void BTN_DeleteNote_Click(object sender, RoutedEventArgs e)
        {
            delete_note_click();
        }

    }
}
/*  END OF FILE
 * 
 * 
 */