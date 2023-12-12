/* Aiding Elements User Interface
 *      FlatShareCC element 
 * 
 * element for calculating flat costs per room, initially
 * based on area, later, when consumption values are known,
 * cost calculation is based on area and heating units usage
 * 
 * init:        2023|12|03
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 * 
 * origin:      FlatShareCostCalculator_2023_11_18(MIT-license) https://github.com/tothaval/Flat-Share-CC
 */
using AidingElementsUserInterface.Core.Auxiliaries;
using AidingElementsUserInterface.Core.FlatShareCC_Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Windows.Threading;
/*BKA2022 gezahlt: 2491,92 €
BKA2022 Kosten:   2668,13 €
BKA2022 Differenz: 176,21 €

Nachzahlung nach Mietkontoausgleich: 104,22 €

Aufteilung
Tim 79,19 €
Stephan 25,03 €

Vinz müsste 164,41 € von der Hausverwaltung erhalten,
dann müsste die Hausverwaltung 340,62 € von Stephan und Tim
erhalten, dann wäre alles korrekt bezahlt. Geht aber vermutlich
nicht, weil es der Hausverwaltung zuviel Aufwand bedeutet.
Die Hausverwaltung erhält auch nicht die Differenz aus 2022,
sondern hat das Mietkontovermögen aus dem gesamten Mietzeitraum
aufgerechnet. Übrig blieben 104,22€.
768,99 / 1599,42 = 0,48
501,16 / 1599,42 = 0,31
329,27 / 1599,42 = 0,21

Aufteilung Zahlungsüberhang bei Vinz
V-diff(Name) = Vorauszahlungsdifferenz
Z-über(Name) = Zahlungsüberhang

-315,59 € V-diff(Tim)
 -25,03 € V-diff(Stephan)
-340,62 € V-diff(gesamt)

+164,41 € Z-über Vinz

Tim Vorauszahlungsdifferenzanteil = V-diff(Tim)/V-diff(gesamt)
= 0,9265

Stephan Vorauszahlungsdifferenzanteil = V-diff(Stephan)/V-diff(gesamt)
= 0,0735

Zahlungsüberhangverteilung = V-diff(Name) * Z-über[1n]
Tim 152,33 €
Stephan 12,08 €
gesamt  164,41 €

Lösung:
Hausverwaltung nach Schlüssel oder über 1 Konto und persönlichen
Barausgleich bezahlen. Anschließend Vinz insgesamt 164,41 zukommen
lassen. Vinz zahlt es aufs Konto ein und meldet dem Jobcenter das
Guthaben, vermutlich erhält er irgendwann eine Aufforderung, dieses
ans Jobcenter zurückzuzahlen, da das Jobcenter ja letztlich die
Heizkosten übernimmt.

Idee:

Tim zahlt an Vinz und an die Hausverwaltung.
An die Hausverwaltung die geforderten 104,22 €,
an Vinz den Überhang in Höhe von 164,41 €.

Tim erhält von mir 25,03 € + 12,08 € = 37,11 €.

D.h. ich könnte ihm die Nebenkosten für 1 Monat erlassen
und müsste 0,11€ an Tim zahlen.*/

namespace AidingElementsUserInterface.Elements.FlatShareCC
{
    /// <summary>
    /// Interaktionslogik für FlatShareCC.xaml
    /// </summary>
    public partial class FlatShareCC : UserControl
    {
        private FlatData flatData = new FlatData();
        private FlatCosts flatCosts = new FlatCosts();

        private ObservableCollection<CostUpdateData> costUpdates = new ObservableCollection<CostUpdateData>();

        private DispatcherTimer _timer = new DispatcherTimer();

        private XML_Handler xml_handler;

        public FlatShareCC()
        {
            InitializeComponent();

            xml_handler = new XML_Handler(flatData);

            _timer.Tick += __FlatShareCC_loading_intervall;
            _timer.Interval = TimeSpan.FromSeconds(0.25);
            _timer.Start();
        }

        private async void __FlatShareCC_loading_intervall(object sender, EventArgs e)
        {
            FlatDataUI_object.focus();

            if (load_data())
            {
                FlatDataUI_object.load_data(flatData);
                InitialCostsUI_object.load_data(flatCosts);
            }
            
            load_costUpdates();
        }

        internal void addCostUpdate(CostUpdateData costUpdate)
        {
            costUpdates.Add(costUpdate);
        }

        internal ObservableCollection<CostUpdateData> getCostUpdates()
        {
            return costUpdates;
        }


        internal FlatCosts getFlatCosts()
        {
            return flatCosts;
        }

        internal FlatData getFlatData()
        {
            return flatData;
        }

        private bool load_costUpdates()
        {
            bool loadable_data_detected = false;

            costUpdates = xml_handler.FlatShareCC_load_cost_update();

            if (costUpdates == null)
            {
                return loadable_data_detected = false;
            }

            foreach (CostUpdateData item in costUpdates)
            {
                UpdateCostsUI_object.add_UpdateCosts(item);

                item.add_origin_element(this);
            }

            loadable_data_detected = true;

            return loadable_data_detected;
        }


        private bool load_data()
        {
            bool loadable_data_detected = false;
            ObservableCollection<object> data = xml_handler.FlatShareCC_load_data();

            if (data == null)
            {
                return loadable_data_detected = false;
            }

            foreach (object item in data)
            {
                if (item.GetType() == typeof(FlatData))
                {
                    flatData = (FlatData)item;
                }
                else if (item.GetType() == typeof(FlatCosts))
                {
                    flatCosts = (FlatCosts)item;
                }
            }

            loadable_data_detected = true;

            return loadable_data_detected;
        }

        internal void removeCostUpdate(CostUpdateData costUpdateData)
        {
            costUpdates.Remove(costUpdateData);
        }

        internal void save_data(FlatData data)
        {
            this.flatData = data;

            xml_handler.FlatShareCC_save_data(flatData, flatCosts);

            InitialCostsUI_object.focus();
        }

        internal void save_data(FlatCosts costs)
        {
            this.flatCosts = costs;

            xml_handler.FlatShareCC_save_data(flatData, flatCosts);
        }

        internal void save_costUpdates()
        {
            TabControl tabControl = UpdateCostsUI_object.MainTabControl;
            IO_Handler iO_Handler = new IO_Handler(new FlatData());

            // alle dateien löschen
            iO_Handler.delete_files(iO_Handler.FlatShareCC_xml_folder, iO_Handler.FlatShareCC_data_file);


            foreach (TabItem tab_item in tabControl.Items)
            {
                UpdateCosts cost_update_item = (UpdateCosts)tab_item.Content;

                cost_update_item.save_costUpdate();
            }

        }

        internal void end_this()
        {
            save_data(flatCosts);
            save_data(flatData);
            save_costUpdates();
        }

    }
}
/*  END OF FILE
 * 
 * 
 */