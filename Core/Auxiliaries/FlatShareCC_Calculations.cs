/* Aiding Elements User Interface
 *      FlatShareCC_Calculations class
 * 
 * inherits from:
 * 
 * FlatShareCC linkData and logic class
 * 
 * init:        2023|12|03
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 * 
 * origin:      FlatShareCostCalculator_2023_11_18(MIT-license) https://github.com/tothaval/Flat-Share-CC
 */
using AidingElementsUserInterface.Core.FlatShareCC_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AidingElementsUserInterface.Core.Auxiliaries
{
    internal class FlatShareCC_Calculations
    {
        FlatData flatData;
        FlatCosts flatCosts;

        CostUpdateData costUpdateData;

        internal FlatShareCC_Calculations(CostUpdateData costUpdate)
        {
            this.costUpdateData = costUpdate;

            this.flatData = this.costUpdateData.get_FlatData();
            this.flatCosts = this.costUpdateData.get_FlatCosts();
        }


        internal FlatShareCC_Calculations(FlatData _flatData, FlatCosts _flatCosts)
        {
            this.flatData = _flatData;
            this.flatCosts = _flatCosts;

            costUpdateData = new CostUpdateData(flatData, flatCosts);
        }


        internal double shared_space()
        {
            double shared_space = flatData.flat_space;

            foreach (RoomData room in flatData.rooms)
            {
                shared_space -= room.area;
            }

            return shared_space;
        }


    }
}
/*  END OF FILE
 * 
 * 
 */