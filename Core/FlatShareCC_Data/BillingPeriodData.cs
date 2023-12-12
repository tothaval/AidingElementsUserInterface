/* Aiding Elements User Interface
 *      BillingPeriodData class
 * 
 * inherits from:
 * 
 * FlatShareCC data class
 * 
 * init:        2023|12|03
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 * 
 * origin:      FlatShareCostCalculator_2023_11_18(MIT-license) https://github.com/tothaval/Flat-Share-CC
 */
using System.Collections.Generic;

namespace AidingElementsUserInterface.Core.FlatShareCC_Data
{
    internal class BillingPeriodData
    {
        internal RoomData room { get; set; }

        internal double heating_units_usage { get; set; }

        internal List<double> monthly_payments { get; set; }

        internal BillingPeriodData(RoomData _room)
        {
            room = _room;

            heating_units_usage = 0;
            monthly_payments = new List<double>();
        }
    }
}
/*  END OF FILE
 * 
 * 
 */