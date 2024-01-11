/* Aiding Elements User Interface
 *      CoreMatrixLine element 
 * 
 * basic element for adding or removing a custom number of user controls.
 * 
 * init:        2024|01|11
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace AidingElementsUserInterface.Core.AEUI_UserControls
{
    /// <summary>
    /// Interaktionslogik für CoreMatrixLine.xaml
    /// </summary>
    public partial class CoreMatrixLine : UserControl
    {
        Type type;

        public CoreMatrixLine()
        {
            InitializeComponent();
        }

        public CoreMatrixLine(Type type)
        {
            InitializeComponent();

            this.type = type;
        }

        public ObservableCollection<UserControl> extract()
        {
            ObservableCollection<UserControl> userControls = new ObservableCollection<UserControl>();

            foreach (UserControl item in SP.Children)
            {
                userControls.Add(item);
            }

            return userControls;
        }

        public void fill(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                if (type != null)
                {
                    insert(type);
                }                
            }
        }

        public void insert(Type type)
        {
            UserControl userControl = (UserControl)Activator.CreateInstance(type);

            if (userControl != null)
            {
                if (type == typeof(CoreTextBox))
                {
                    CoreTextBox ctb = (CoreTextBox)userControl;
                    ctb._readonly_caret();
                }

                SP.Children.Add(userControl);
            }            
        }

        public void insert(UserControl userControl)
        {
            SP.Children.Add(userControl);
        }

        public void remove_last()
        {
                int index = SP.Children.Count - 1;

                if (index > -1)
                {
                    SP.Children.RemoveAt(index);
                }
        }


        private void BTN_minus_Click(object sender, RoutedEventArgs e)
        {
            remove_last();
        }

        private void BTN_plus_Click(object sender, RoutedEventArgs e)
        {
            if (type != null)
            {
                insert(type);
            }
            
        }
    }
}
/*  END OF FILE
 * 
 * 
 */