using AidingElementsUserInterface.Core.FlatShareCC_Data;
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

namespace AidingElementsUserInterface.Elements.FlatShareCC
{
    /// <summary>
    /// Interaktionslogik für InitialCostOutputUI.xaml
    /// </summary>
    public partial class InitialCostOutputUI : UserControl
    {
        private FlatData fd;
        private FlatCosts fc;

        private CostUpdateData cud;

        private int rnr;

        public InitialCostOutputUI()
        {

        }


        internal InitialCostOutputUI(int room_id, FlatData flatData, FlatCosts flatCosts)
        {
            InitializeComponent();

            fd = flatData;
            fc = flatCosts;

            rnr = room_id;

            border.Child = new RentOutputUI(rnr, fd, fc);
        }


        internal InitialCostOutputUI(int room_id, CostUpdateData costUpdateData)
        {
            InitializeComponent();

            cud = costUpdateData;

            rnr = room_id;

            setup_cost_update_output();
        }

        private void setup_cost_update_output()
        {
            if (cud.isAnnualBilling)
            {
                border.Child = new CostOutputUI(rnr, cud);
            }

            if (cud.isRentChange)
            {
                border.Child = new RentOutputUI(rnr, cud);
            }
        }



    }
}
/*  END OF FILE
 * 
 * 
 */