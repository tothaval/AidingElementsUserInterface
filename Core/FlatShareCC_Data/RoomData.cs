/* Aiding Elements User Interface
 *      RoomData class
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
namespace AidingElementsUserInterface.Core.FlatShareCC_Data
{
    internal class RoomData
    {
        internal int id { get; set; }
        internal string name { get; set; }

        internal double area { get; set; }

        internal RoomData()
        {
            id = 0;
            name = string.Empty;
            area = 0;
        }
    }
}
/*  END OF FILE
 * 
 * 
 */