using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.Auxiliaries;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
    /// Interaktionslogik für Adjust.xaml
    /// </summary>
    public partial class Adjust : UserControl
    {
        public Adjust()
        {
            InitializeComponent();

            build();
            registerEvents();
        }

        private void build()
        {
            CB_AdjustSelection.setContent("apply to selection");

            CVC_X.setIdentifier("x: "); CVC_X.setText("0");
            CVC_Y.setIdentifier("y: "); CVC_Y.setText("0");

            CVC_Level.setIdentifier("level: "); CVC_Level.setText("0");// aus levelData soweit existent

            CVC_a.setIdentifier("width: "); CVC_a.setText("150");
            CVC_b.setIdentifier("height: "); CVC_b.setText("120");

            CVC_r.setIdentifier("rotation: "); CVC_r.setText("0");

            CVC_VisibilityList.setIdentifier("level visibility list: ");CVC_VisibilityList.setText("[-100, 97], 98, 99, 100");
        }

        internal struct ADJUST_DATA_STRUCTURE
        {
            public int x;
            public int y;
            public int level;

            public double width;
            public double height;
            public double rotation;

            public string level_visibility_list;

            public ADJUST_DATA_STRUCTURE(
                int x, int y, int level,
                double width, double height, double rotation,
                string level_visibility_list)
            {
                this.x = x;
                this.y = y;
                this.level = level;
                this.width = width;
                this.height = height;
                this.rotation = rotation;
                this.level_visibility_list = level_visibility_list;             
            }

            public override string ToString() => $"{x} {y} {level}\n{width} {height} {rotation}\n{level_visibility_list}";
        }

        private ADJUST_DATA_STRUCTURE ParseCVCs()
        {
            int x, y, level;
            double width, height, rotation;
            string level_visibility_list;

            x = Int32.Parse(CVC_X.value);
            y = Int32.Parse(CVC_Y.value);
            level = Int32.Parse(CVC_Level.value);


            width = double.Parse(CVC_a.value);
            height = double.Parse(CVC_b.value);
            rotation = double.Parse(CVC_r.value);

            level_visibility_list = CVC_VisibilityList.value;

            return new ADJUST_DATA_STRUCTURE(x, y, level, width, height, rotation, level_visibility_list);
        }
        private void registerEvents()
        {
            CB_AdjustSelection.button.Click += CB_AdjustSelection_Click;
        }

        private void CB_AdjustSelection_Click(object sender, RoutedEventArgs e)
        {
            new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS.ADJUST_GATE(ParseCVCs());
        }
    }
}
