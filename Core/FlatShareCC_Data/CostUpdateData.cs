/* Aiding Elements User Interface
 *      RoomData class
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
using AidingElementsUserInterface.Core.Auxiliaries;
using AidingElementsUserInterface.Elements.FlatShareCC;

using System;
using System.Collections.ObjectModel;

namespace AidingElementsUserInterface.Core.FlatShareCC_Data
{
    internal class CostUpdateData
    {
        internal bool isAnnualBilling { get; set; }
        internal bool isRentChange { get; set; }
        
        internal string cause { get; set; }
        internal string based_on_AB { get; set; }
        
        internal double initial_cold_rent { get; set; }
        internal double initial_extra_costs_advance { get; set; }
        
        internal double cold_rent { get; set; }
        internal double extra_costs_advance { get; set; }
        
        //
        internal double annual_costs { get; set; }
        internal double extra_costs_shared { get; set; }
        internal double extra_costs_heating { get; set; }
        
        internal double heating_units_usage { get; set; }
        internal double heating_units_shared { get; set; }

        internal FlatShareCC origin_element;


        // room heating units usage and monthly payments
        internal ObservableCollection<BillingPeriodData> roomConsumptionValues { get; set; }

        internal DateTime period_start { get; set; }
        internal DateTime period_end { get; set; }
        internal DateTime cost_update_received { get; set; }

        private readonly FlatCosts flatCosts;
        private readonly FlatData flatData;


        internal CostUpdateData(FlatData data, FlatCosts costs)
        {
            if (data != null && costs != null)
            {
                flatData = data;
                flatCosts = costs;

                cold_rent = flatCosts.cold_rent;
                extra_costs_advance = flatCosts.extra_costs_advance;

                roomConsumptionValues = new ObservableCollection<BillingPeriodData>();

                foreach (RoomData room in data.rooms)
                {
                    roomConsumptionValues.Add(new BillingPeriodData(room));
                }
            }

            isAnnualBilling = false;
            isRentChange = false;

            based_on_AB = "";
            cause = "";

            period_start = new DateTime();
            period_end = new DateTime();
            cost_update_received = new DateTime();
        }

        internal CostUpdateData()
        {
            XML_Handler aUX_XML_ = new XML_Handler(new FlatData());
            ObservableCollection<object> data = aUX_XML_.FlatShareCC_load_data();

            if (data != null)
            {
                flatData = (FlatData)data[0];
                flatCosts = (FlatCosts)data[1];

                cold_rent = flatCosts.cold_rent;
                extra_costs_advance = flatCosts.extra_costs_advance;

                roomConsumptionValues = new ObservableCollection<BillingPeriodData>();

                foreach (RoomData room in flatData.rooms)
                {
                    roomConsumptionValues.Add(new BillingPeriodData(room));
                }
            }
            
            isAnnualBilling = false;
            isRentChange = false;

            based_on_AB = "";
            cause = "";

            period_start = new DateTime();
            period_end = new DateTime();
            cost_update_received = new DateTime();
        }

        internal void add_origin_element(FlatShareCC flatShareCC)
        {
            origin_element = flatShareCC;
        }

        internal FlatCosts get_FlatCosts()
        {
            return flatCosts;
        }

        internal FlatData get_FlatData()
        {
            return flatData;
        }
    }
}
/*  END OF FILE
 * 
 * 
 */