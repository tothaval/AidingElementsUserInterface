/* Aiding Elements User Interface
 *      FlatData class
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
using AidingElementsUserInterface.Elements.FlatShareCC;
using System.Collections.ObjectModel;

namespace AidingElementsUserInterface.Core.FlatShareCC_Data
{
    internal class FlatData
    {
        public int flat_id { get; set; }
        public string flat_address { get; set; }
        public double flat_space { get; set; }

        public ObservableCollection<RoomData> rooms { get; set; }

        public FlatData()
        {
            rooms = new ObservableCollection<RoomData> { };
        }
    }
}
