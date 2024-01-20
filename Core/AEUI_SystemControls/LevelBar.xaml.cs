using AidingElementsUserInterface.Core.AEUI_Data;
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
        }

        internal void update(LevelData levelData)
        {
            CL_LevelId.setText(levelData.LEVEL.ToString());

            CL_LevelName.setText(levelData.NAME.ToString());
            CL_LevelDescription.setText(levelData.DESCRIPTION.ToString());

            CL_LevelLogin.setText(levelData.LOGIN_FLAG.ToString());

            CL_LevelSecurityPanel.setText(levelData.SECURITY_FLAG.ToString());

            CL_LevelElementCount.setText(levelData.ELEMENT_COUNT.ToString());

        }
    }
}
