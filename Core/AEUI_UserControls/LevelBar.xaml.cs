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

namespace AidingElementsUserInterface.Core.AEUI_UserControls
{
    /// <summary>
    /// Interaktionslogik für LevelBar.xaml
    /// </summary>
    public partial class LevelBar : UserControl
    {
        public LevelBar()
        {
            InitializeComponent();

            build();
        }

        private void build()
        {   
            LevelID.setText("0");
            LevelName.setText("level name");
            LevelPurpose.setText("level purpose");
            LevelVisibility.setText("level visibility");
            LevelElementCount.setText("level element count");
        }

        internal void update(string ID, string Name, string Purpose, string Visibility, string ElementCount)
        {
            LevelID.setText(ID);
            LevelName.setText(Name);
            LevelPurpose.setText(Purpose);
            LevelVisibility.setText(Visibility);
            LevelElementCount.setText(ElementCount);
        }
    }
}
