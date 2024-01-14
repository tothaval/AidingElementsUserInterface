/* Aiding Elements User Interface
 *      FlatCosts class
 * 
 * inherits from:
 * 
 * FlatShareCC linkData class
 * 
 * init:        2023|12|03
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 * 
 * origin:      FlatShareCostCalculator_2023_11_18(MIT-license) https://github.com/tothaval/Flat-Share-CC
 */

namespace AidingElementsUserInterface.Core.FlatShareCC_Data
{
    internal class FlatCosts
    {
        internal double cold_rent { get; set; }
        internal double extra_costs_advance { get; set; }

        internal FlatCosts()
        {
            cold_rent = 0;
            extra_costs_advance = 0;
        }
    }
}
/*  END OF FILE
 * 
 * 
 */