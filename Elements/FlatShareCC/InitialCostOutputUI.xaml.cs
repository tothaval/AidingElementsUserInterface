/* Aiding Elements User Interface
 *      FlatShareCC element 
 *          InitialCostOutputUI user control
 * 
 * init:        2023|12|03
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 * 
 * origin:      FlatShareCostCalculator_2023_11_18(MIT-license) https://github.com/tothaval/Flat-Share-CC
 */
using AidingElementsUserInterface.Core.FlatShareCC_Data;

using System.Windows.Controls;

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