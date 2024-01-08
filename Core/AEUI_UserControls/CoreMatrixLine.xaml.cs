using AidingElementsUserInterface.Elements.MyNote;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
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