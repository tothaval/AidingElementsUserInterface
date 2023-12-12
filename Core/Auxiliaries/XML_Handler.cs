/* Aiding Elements User Interface
 *      XML_Handler class
 * 
 * xml save and load functionality
 * 
 * init:        2023|11|30
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 * 
 * origin:      MyNote_2023_11_01
 */
using AidingElementsUserInterface.Core.FlatShareCC_Data;
using AidingElementsUserInterface.Core.MyNote_Data;
using AidingElementsUserInterface.Elements.FlatShareCC;
using AidingElementsUserInterface.Elements.MyNote;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml;

namespace AidingElementsUserInterface.Core.Auxiliaries
{
    internal class XML_Handler : IO_Handler
    {
        internal CoreData coreData_data { get; set; }
        internal FlatData flatShareCC_data { get; set; }
        internal MyNote myNote_element { get; set; }

        internal XML_Handler(CoreData coreData) : base(coreData)
        {

        }

        internal XML_Handler(FlatData flatShareCC) : base(flatShareCC)
        {
            flatShareCC_data = flatShareCC;
        }

        internal XML_Handler(MyNote myNote) : base(myNote)
        {
            myNote_element = myNote;
        }

        //private void checkExistence(string path)
        //{
        //    if (!File.Exists(path))
        //    {
        //        Directory.CreateDirectory(path);
        //    }
        //}

        // Core loading and saving via XML
        #region Core
        #region CoreData loading
        internal CoreData? CoreData_load()
        {
            SharedLogic logic = new SharedLogic();

            CoreData coreData = new CoreData(true);
            XmlDocument xmlDocument = new XmlDocument();

            
            if (File.Exists(CoreData_file))
            {
                xmlDocument.Load(CoreData_file);
                XmlNode node = xmlDocument.SelectSingleNode("Core");
                XmlNode node_CoreData = node.SelectSingleNode("CoreData");

                if (node != null && node_CoreData != null)
                {
                    coreData.brushtype = node_CoreData.SelectSingleNode("brushtype").InnerText;

                    coreData.background = logic.ParseColor(node_CoreData.SelectSingleNode("background").InnerText);
                    coreData.borderbrush = logic.ParseColor(node_CoreData.SelectSingleNode("borderbrush").InnerText);
                    coreData.foreground = logic.ParseColor(node_CoreData.SelectSingleNode("foreground").InnerText);
                    coreData.highlight = logic.ParseColor(node_CoreData.SelectSingleNode("highlight").InnerText);

                    coreData.cornerRadius = logic.ParseCornerRadius(node_CoreData.SelectSingleNode("cornerRadius").InnerText);

                    coreData.thickness = logic.ParseThickness(node_CoreData.SelectSingleNode("thickness").InnerText);

                    coreData.fontSize = Int32.Parse(node_CoreData.SelectSingleNode("fontSize").InnerText);
                    coreData.fontFamily = new FontFamily(node_CoreData.SelectSingleNode("fontFamily").InnerText);

                    // path values
                    //public string imageFilePath { get; set; }
                    coreData.buttonImageFilePath = node_CoreData.SelectSingleNode("buttonImageFilePath").InnerText;
                    coreData.containerImageFilePath = node_CoreData.SelectSingleNode("containerImageFilePath").InnerText;

                    // main window size values
                    coreData.mainWindowHeight = Int32.Parse(node_CoreData.SelectSingleNode("mainWindowHeight").InnerText);
                    coreData.mainWindowWidth = Int32.Parse(node_CoreData.SelectSingleNode("mainWindowWidth").InnerText);

                    return coreData;
                }

                return null;

            }

            return null;
        }

        #endregion CoreData loading

        #region CoreData saving
        internal void CoreData_save()
        {
            coreData_data = new SharedLogic().GetDataHandler().GetCoreData();

            XmlDocument xmlDocument = new XmlDocument();

            XmlNode node = xmlDocument.CreateElement("Core");
            xmlDocument.AppendChild(node);

            XmlNode node_CoreData = xmlDocument.CreateElement("CoreData");

            XmlNode brushtype = xmlDocument.CreateElement("brushtype");
            brushtype.InnerText = coreData_data.brushtype;
            node_CoreData.AppendChild(brushtype);

            XmlNode background = xmlDocument.CreateElement("background");
            background.InnerText = coreData_data.background.ToString();
            node_CoreData.AppendChild(background);

            XmlNode borderbrush = xmlDocument.CreateElement("borderbrush");
            borderbrush.InnerText = coreData_data.borderbrush.ToString();
            node_CoreData.AppendChild(borderbrush);
            
            XmlNode foreground = xmlDocument.CreateElement("foreground");
            foreground.InnerText = coreData_data.foreground.ToString();
            node_CoreData.AppendChild(foreground);

            XmlNode highlight = xmlDocument.CreateElement("highlight");
            highlight.InnerText = coreData_data.highlight.ToString();
            node_CoreData.AppendChild(highlight);

            XmlNode cornerRadius = xmlDocument.CreateElement("cornerRadius");
            cornerRadius.InnerText = coreData_data.cornerRadius.ToString();
            node_CoreData.AppendChild(cornerRadius);

            XmlNode thickness = xmlDocument.CreateElement("thickness");
            thickness.InnerText = coreData_data.thickness.ToString();
            node_CoreData.AppendChild(thickness);
            
            XmlNode fontSize = xmlDocument.CreateElement("fontSize");
            fontSize.InnerText = coreData_data.fontSize.ToString();
            node_CoreData.AppendChild(fontSize);

            XmlNode fontFamily = xmlDocument.CreateElement("fontFamily");
            fontFamily.InnerText = coreData_data.fontFamily.ToString();
            node_CoreData.AppendChild(fontFamily);

            XmlNode buttonImageFilePath = xmlDocument.CreateElement("buttonImageFilePath");
            buttonImageFilePath.InnerText = coreData_data.buttonImageFilePath;
            node_CoreData.AppendChild(buttonImageFilePath);

            XmlNode containerImageFilePath = xmlDocument.CreateElement("containerImageFilePath");
            containerImageFilePath.InnerText = coreData_data.containerImageFilePath;
            node_CoreData.AppendChild(containerImageFilePath);

            XmlNode mainWindowHeight = xmlDocument.CreateElement("mainWindowHeight");
            mainWindowHeight.InnerText = coreData_data.mainWindowHeight.ToString();
            node_CoreData.AppendChild(mainWindowHeight);

            XmlNode mainWindowWidth = xmlDocument.CreateElement("mainWindowWidth");
            mainWindowWidth.InnerText = coreData_data.mainWindowWidth.ToString();
            node_CoreData.AppendChild(mainWindowWidth);
             
            node.AppendChild(node_CoreData);

            try
            {
                xmlDocument.Save(CoreData_file);
            }
            catch (Exception)
            {

            }

        }
        #endregion CoreData saving
        #endregion Core



        // FlatShareCostCalculator loading and saving via XML
        #region FlatShareCC
        #region FlatShareCC loading
        internal ObservableCollection<CostUpdateData>? FlatShareCC_load_cost_update()
        {
            ObservableCollection<CostUpdateData> data_list = new ObservableCollection<CostUpdateData>();
            XmlDocument xmlDocument = new XmlDocument();

            foreach (string filename in scan_directory(FlatShareCC_xml_folder))
            {

                if (filename != FlatShareCC_data_file)
                {
                    CostUpdateData costUpdateData = new CostUpdateData();

                    xmlDocument.Load(filename);

                    XmlNode SharedApartmentCalculator = xmlDocument.SelectSingleNode("FlatShareCostCalculator");
                    XmlNode cost_update_node = SharedApartmentCalculator.SelectSingleNode("cost_update_data");

                    if (SharedApartmentCalculator != null && cost_update_node != null)
                    {
                        // string values
                        XmlNode cause = cost_update_node.SelectSingleNode("cause");
                        costUpdateData.cause = cause.InnerText;

                        XmlNode based_on_AB = cost_update_node.SelectSingleNode("based_on_AB");
                        costUpdateData.based_on_AB = based_on_AB.InnerText;

                        // bool values
                        XmlNode is_annual_billing = cost_update_node.SelectSingleNode("is_annual_billing");
                        costUpdateData.isAnnualBilling = Boolean.Parse(is_annual_billing.InnerText);

                        XmlNode is_rent_change = cost_update_node.SelectSingleNode("is_rent_change");
                        costUpdateData.isRentChange = Boolean.Parse(is_rent_change.InnerText);

                        // date values
                        XmlNode cu_received_node = cost_update_node.SelectSingleNode("cost_update_received");
                        costUpdateData.cost_update_received = DateTime.Parse(cu_received_node.InnerText);

                        XmlNode period_start_node = cost_update_node.SelectSingleNode("period_start");
                        costUpdateData.period_start = DateTime.Parse(period_start_node.InnerText);

                        XmlNode period_end_node = cost_update_node.SelectSingleNode("period_end");
                        costUpdateData.period_end = DateTime.Parse(period_end_node.InnerText);

                        // double values
                        XmlNode initial_cold_rent = cost_update_node.SelectSingleNode("initial_cold_rent");
                        costUpdateData.initial_cold_rent = Double.Parse(initial_cold_rent.InnerText);

                        XmlNode initial_extra_costs_advance = cost_update_node.SelectSingleNode("initial_extra_costs_advance");
                        costUpdateData.initial_extra_costs_advance = Double.Parse(initial_extra_costs_advance.InnerText);

                        XmlNode cold_rent = cost_update_node.SelectSingleNode("cold_rent");
                        costUpdateData.cold_rent = Double.Parse(cold_rent.InnerText);

                        XmlNode extra_costs_advance = cost_update_node.SelectSingleNode("extra_costs_advance");
                        costUpdateData.extra_costs_advance = Double.Parse(extra_costs_advance.InnerText);

                        XmlNode annual_costs = cost_update_node.SelectSingleNode("annual_costs");
                        costUpdateData.annual_costs = Double.Parse(annual_costs.InnerText);

                        XmlNode extra_costs_shared = cost_update_node.SelectSingleNode("extra_costs_shared");
                        costUpdateData.extra_costs_shared = Double.Parse(extra_costs_shared.InnerText);

                        XmlNode extra_costs_heating = cost_update_node.SelectSingleNode("extra_costs_heating");
                        costUpdateData.extra_costs_heating = Double.Parse(extra_costs_heating.InnerText);


                        XmlNode heating_units_usage = cost_update_node.SelectSingleNode("heating_units_usage");
                        costUpdateData.heating_units_usage = Double.Parse(heating_units_usage.InnerText);

                        XmlNode heating_units_shared = cost_update_node.SelectSingleNode("heating_units_shared");
                        costUpdateData.heating_units_shared = Double.Parse(heating_units_shared.InnerText);


                        XmlNode rooms = cost_update_node.SelectSingleNode("rooms");
                        XmlNodeList room_nodes = rooms.SelectNodes("room");

                        int counter = 0;

                        foreach (XmlNode room_node in room_nodes)
                        {
                            BillingPeriodData room = costUpdateData.roomConsumptionValues[counter];

                            XmlNode room_heating_units_usage = room_node.SelectSingleNode("room_heating_units_usage");
                            room.heating_units_usage = Double.Parse(room_heating_units_usage.InnerText);

                            XmlNode room_payments = room_node.SelectSingleNode("room_payments");

                            if (room_payments != null)
                            {
                                XmlNodeList payed = room_payments.SelectNodes("payment");

                                foreach (XmlNode payment_node in payed)
                                {
                                    double value = Double.Parse(payment_node.InnerText);
                                    room.monthly_payments.Add(value);
                                }
                            }

                            costUpdateData.roomConsumptionValues[counter] = room;

                            counter++;
                        }

                        data_list.Add(costUpdateData);
                    }
                }
            }

            return data_list;
        }


        internal ObservableCollection<object>? FlatShareCC_load_data()
        {
            ObservableCollection<object> data_list = new ObservableCollection<object>();
            XmlDocument xmlDocument = new XmlDocument();

            FlatData flatData = new FlatData();
            FlatCosts flatCosts = new FlatCosts();

            data_list.Add(flatData);
            data_list.Add(flatCosts);

            //MessageBox.Show(File.Exists(FlatShareCC_data_file).ToString());

            if (File.Exists(FlatShareCC_data_file))
            {
                xmlDocument.Load(FlatShareCC_data_file);

                XmlNode SharedApartmentCalculator = xmlDocument.SelectSingleNode("FlatShareCostCalculator");
                XmlNode initial_contract = SharedApartmentCalculator.SelectSingleNode("data");

                if (SharedApartmentCalculator != null && initial_contract != null)
                {
                    data_list.Clear();

                    XmlNode living_space = initial_contract.SelectSingleNode("flat_space");
                    flatData.flat_space = Double.Parse(living_space.InnerText);

                    XmlNode cold_rent = initial_contract.SelectSingleNode("cold_rent");
                    flatCosts.cold_rent = Double.Parse(cold_rent.InnerText);

                    XmlNode extra_costs = initial_contract.SelectSingleNode("extra_costs_advance");
                    flatCosts.extra_costs_advance = Double.Parse(extra_costs.InnerText);

                    XmlNode rooms = initial_contract.SelectSingleNode("rooms");
                    XmlNodeList room_nodes = rooms.SelectNodes("room");

                    foreach (XmlNode room_node in room_nodes)
                    {
                        RoomData room = new RoomData();

                        XmlNode room_id = room_node.SelectSingleNode("id");
                        room.id = Int32.Parse(room_id.InnerText);

                        XmlNode room_name = room_node.SelectSingleNode("name");
                        room.name = room_name.InnerText;

                        XmlNode room_area = room_node.SelectSingleNode("area");
                        room.area = Double.Parse(room_area.InnerText);

                        flatData.rooms.Add(room);
                    }

                    data_list.Add(flatData);
                    data_list.Add(flatCosts);

                    return data_list;
                }

                return data_list;
            }

            return null;
        }
        #endregion FlatShareCC loading

        #region FlatShareCC saving
        internal void FlatShareCC_save_cost_update(CostUpdateData costUpdateData)
        {
            XmlDocument xmlDocument = new XmlDocument();

            XmlNode node = xmlDocument.CreateElement("FlatShareCostCalculator");
            xmlDocument.AppendChild(node);

            XmlNode data = xmlDocument.CreateElement("cost_update_data");

            // string values
            XmlNode cause = xmlDocument.CreateElement("cause");
            cause.InnerText = costUpdateData.cause.ToString();
            data.AppendChild(cause);

            XmlNode based_on_AB = xmlDocument.CreateElement("based_on_AB");
            based_on_AB.InnerText = costUpdateData.based_on_AB.ToString();
            data.AppendChild(based_on_AB);

            // bool values
            XmlNode is_annual_billing = xmlDocument.CreateElement("is_annual_billing");
            is_annual_billing.InnerText = costUpdateData.isAnnualBilling.ToString();
            data.AppendChild(is_annual_billing);

            XmlNode is_rent_change = xmlDocument.CreateElement("is_rent_change");
            is_rent_change.InnerText = costUpdateData.isRentChange.ToString();
            data.AppendChild(is_rent_change);

            // date values
            XmlNode cost_update_received = xmlDocument.CreateElement("cost_update_received");
            cost_update_received.InnerText = costUpdateData.cost_update_received.ToString("d");
            data.AppendChild(cost_update_received);

            XmlNode period_start = xmlDocument.CreateElement("period_start");
            period_start.InnerText = costUpdateData.period_start.ToString("d");
            data.AppendChild(period_start);

            XmlNode period_end = xmlDocument.CreateElement("period_end");
            period_end.InnerText = costUpdateData.period_end.ToString("d");
            data.AppendChild(period_end);

            // double values   
            XmlNode initial_cold_rent = xmlDocument.CreateElement("initial_cold_rent");
            initial_cold_rent.InnerText = costUpdateData.initial_cold_rent.ToString();
            data.AppendChild(initial_cold_rent);

            XmlNode initial_extra_costs_advance = xmlDocument.CreateElement("initial_extra_costs_advance");
            initial_extra_costs_advance.InnerText = costUpdateData.initial_extra_costs_advance.ToString();
            data.AppendChild(initial_extra_costs_advance);

            XmlNode cold_rent = xmlDocument.CreateElement("cold_rent");
            cold_rent.InnerText = costUpdateData.cold_rent.ToString();
            data.AppendChild(cold_rent);

            XmlNode extra_costs_advance = xmlDocument.CreateElement("extra_costs_advance");
            extra_costs_advance.InnerText = costUpdateData.extra_costs_advance.ToString();
            data.AppendChild(extra_costs_advance);

            XmlNode annual_costs = xmlDocument.CreateElement("annual_costs");
            annual_costs.InnerText = costUpdateData.annual_costs.ToString();
            data.AppendChild(annual_costs);

            XmlNode extra_costs_shared = xmlDocument.CreateElement("extra_costs_shared");
            extra_costs_shared.InnerText = costUpdateData.extra_costs_shared.ToString();
            data.AppendChild(extra_costs_shared);

            XmlNode extra_costs_heating = xmlDocument.CreateElement("extra_costs_heating");
            extra_costs_heating.InnerText = costUpdateData.extra_costs_heating.ToString();
            data.AppendChild(extra_costs_heating);

            XmlNode heating_units_usage = xmlDocument.CreateElement("heating_units_usage");
            heating_units_usage.InnerText = costUpdateData.heating_units_usage.ToString();
            data.AppendChild(heating_units_usage);

            XmlNode heating_units_shared = xmlDocument.CreateElement("heating_units_shared");
            heating_units_shared.InnerText = costUpdateData.heating_units_shared.ToString();
            data.AppendChild(heating_units_shared);

            XmlNode rooms = xmlDocument.CreateElement("rooms");

            foreach (BillingPeriodData room in costUpdateData.roomConsumptionValues)
            {
                XmlNode room_node = xmlDocument.CreateElement("room");

                XmlNode room_id = xmlDocument.CreateElement("id");
                room_id.InnerText = room.room.id.ToString();
                room_node.AppendChild(room_id);

                XmlNode room_name = xmlDocument.CreateElement("name");
                room_name.InnerText = room.room.name.ToString();
                room_node.AppendChild(room_name);

                XmlNode room_area = xmlDocument.CreateElement("area");
                room_area.InnerText = room.room.area.ToString();
                room_node.AppendChild(room_area);

                XmlNode room_heating_units_usage = xmlDocument.CreateElement("room_heating_units_usage");
                room_heating_units_usage.InnerText = room.heating_units_usage.ToString();
                room_node.AppendChild(room_heating_units_usage);

                XmlNode room_payments = xmlDocument.CreateElement("room_payments");

                foreach (double payment in room.monthly_payments)
                {
                    XmlNode payed = xmlDocument.CreateElement("payment");
                    payed.InnerText = payment.ToString();
                    room_payments.AppendChild(payed);
                }
                room_node.AppendChild(room_payments);


                rooms.AppendChild(room_node);
            }
            data.AppendChild(rooms);

            node.AppendChild(data);

            xmlDocument.Save(
                $@".\data\{costUpdateData.cost_update_received.Year}_" +
                $@"{costUpdateData.cost_update_received.Month}_" +
                $@"{costUpdateData.cost_update_received.Day}_" +
                $@"{costUpdateData.cause}.xml"
                );
        }


        internal void FlatShareCC_save_data(FlatData flatData, FlatCosts flatCosts)
        {
            XmlDocument xmlDocument = new XmlDocument();

            XmlNode node = xmlDocument.CreateElement("FlatShareCostCalculator");
            xmlDocument.AppendChild(node);

            XmlNode data = xmlDocument.CreateElement("data");

            XmlNode living_space = xmlDocument.CreateElement("flat_space");
            living_space.InnerText = flatData.flat_space.ToString();
            data.AppendChild(living_space);

            XmlNode cold_costs = xmlDocument.CreateElement("cold_rent");
            cold_costs.InnerText = flatCosts.cold_rent.ToString();
            data.AppendChild(cold_costs);

            XmlNode extra_costs = xmlDocument.CreateElement("extra_costs_advance");
            extra_costs.InnerText = flatCosts.extra_costs_advance.ToString();
            data.AppendChild(extra_costs);

            XmlNode rooms = xmlDocument.CreateElement("rooms");

            foreach (RoomData room in flatData.rooms)
            {
                XmlNode room_node = xmlDocument.CreateElement("room");

                XmlNode room_id = xmlDocument.CreateElement("id");
                room_id.InnerText = room.id.ToString();
                room_node.AppendChild(room_id);

                XmlNode room_name = xmlDocument.CreateElement("name");
                room_name.InnerText = room.name.ToString();
                room_node.AppendChild(room_name);

                XmlNode room_area = xmlDocument.CreateElement("area");
                room_area.InnerText = room.area.ToString();
                room_node.AppendChild(room_area);

                rooms.AppendChild(room_node);
            }
            data.AppendChild(rooms);

            node.AppendChild(data);

            xmlDocument.Save(FlatShareCC_data_file);
        }
        #endregion FlatShareCC saving
        #endregion FlatShareCC

        // MyNote loading and saving via XML
        #region MyNote
        #region MyNote loading
        internal StringBuilder MyNote_load_history()
        {
            StringBuilder history = new StringBuilder();
            XmlDocument xmlDocument = new XmlDocument();

            if (File.Exists(MyNote_history_file))
            {
                xmlDocument.Load(MyNote_history_file);
                XmlNode node = xmlDocument.SelectSingleNode("MyNote_History");
                history.Append(node["history"].InnerText);
            }

            return history;
        }

        internal NoteData MyNote_load_log()
        {
            NoteData data = new NoteData();
            XmlDocument xmlDocument = new XmlDocument();

            if (File.Exists(MyNote_log_file))
            {
                xmlDocument.Load(MyNote_log_file);

                XmlNode node = xmlDocument.SelectSingleNode("MyNote_Protocol");
                data.dateTime = DateTime.Parse(node["time"].InnerText);

                node = xmlDocument.SelectSingleNode("MyNote_Protocol");
                data.title = node["title"].InnerText;

                node = xmlDocument.SelectSingleNode("MyNote_Protocol");
                data.content = new StringBuilder(node["content"].InnerText);
            }
            else
            {
                data.content = new StringBuilder($"{DateTime.Now}\n{data.title}");
            }

            return data;
        }

        internal ObservableCollection<NoteData> MyNote_load_notes()
        {
            ObservableCollection<NoteData> notes = new ObservableCollection<NoteData>();

            foreach (string file in scan_directory(MyNote_notes_folder))
            {
                NoteData data = new NoteData();
                XmlDocument xmlDocument = new XmlDocument();

                xmlDocument.Load(file);

                XmlNode node = xmlDocument.SelectSingleNode("MyNote_Notes");
                data.id = Int32.Parse(node["id"].InnerText);
                data.dateTime = DateTime.Parse(node["time"].InnerText);
                data.title = node["title"].InnerText;
                data.content = new StringBuilder(node["content"].InnerText);

                notes.Add(data);
            }

            return notes;
        }

        internal ObservableCollection<MatrixElement> MyNote_load_matrix_elements()
        {
            ObservableCollection<MatrixElement> elements = new ObservableCollection<MatrixElement>();

            foreach (string file in scan_directory(MyNote_notes_matrix_folder))
            {
                MatrixElement mE = new MatrixElement(myNote_element);

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(file);

                XmlNode node = xmlDocument.SelectSingleNode("MyNote_Notes_Matrix_Element");

                XmlNode me_Id = node.SelectSingleNode("id");
                mE.id = Int32.Parse(me_Id.InnerText);

                XmlNodeList notes = node.SelectNodes("note");

                foreach (XmlNode note_node in notes)
                {
                    NoteData data = new NoteData();
                    Note note = new Note(myNote_element);

                    data.id = Int32.Parse(note_node["id"].InnerText);
                    data.dateTime = DateTime.Parse(note_node["time"].InnerText);
                    data.title = note_node["title"].InnerText;
                    data.content = new StringBuilder(note_node["content"].InnerText);

                    note.loadNoteData(data);

                    mE.insert(note);
                }

                elements.Add(mE);
            }

            return elements;
        }
        #endregion MyNote loading

        #region MyNote saving
        internal void MyNote_save_history(StringBuilder history)
        {
            XmlDocument xmlDocument = new XmlDocument();

            XmlNode node = xmlDocument.CreateElement("MyNote_History");
            xmlDocument.AppendChild(node);

            XmlNode time = xmlDocument.CreateElement("history");
            time.InnerText = history.ToString();
            node.AppendChild(time);

            try
            {
                xmlDocument.Save(MyNote_history_file);
            }
            catch (Exception)
            {

            }

        }

        internal void MyNote_save_log(NoteData data)
        {
            XmlDocument xmlDocument = new XmlDocument();

            XmlNode protocol = xmlDocument.CreateElement("MyNote_Protocol");
            xmlDocument.AppendChild(protocol);

            XmlNode time = xmlDocument.CreateElement("time");
            time.InnerText = data.dateTime.ToString();
            protocol.AppendChild(time);

            XmlNode title = xmlDocument.CreateElement("title");
            title.InnerText = data.title.ToString();
            protocol.AppendChild(title);

            XmlNode content = xmlDocument.CreateElement("content");
            content.InnerText = data.content.ToString();
            protocol.AppendChild(content);

            try
            {
                xmlDocument.Save(MyNote_log_file);
            }
            catch (Exception)
            {

            }


        }

        internal void MyNote_save_notes(ObservableCollection<Note> notes)
        {
            delete_files(MyNote_notes_folder);

            foreach (Note note in notes)
            {
                XmlDocument xmlDocument = new XmlDocument();

                XmlNode protocol = xmlDocument.CreateElement("MyNote_Notes");
                xmlDocument.AppendChild(protocol);

                XmlNode id = xmlDocument.CreateElement("id");
                id.InnerText = note.getNoteData().id.ToString();
                protocol.AppendChild(id);

                XmlNode time = xmlDocument.CreateElement("time");
                time.InnerText = note.getNoteData().dateTime.ToString();
                protocol.AppendChild(time);

                XmlNode title = xmlDocument.CreateElement("title");
                title.InnerText = note.getNoteData().title.ToString();
                protocol.AppendChild(title);

                XmlNode content = xmlDocument.CreateElement("content");
                content.InnerText = note.getNoteData().content.ToString();
                protocol.AppendChild(content);

                try
                {
                    xmlDocument.Save(@$"{MyNote_notes_folder}\{note.getNoteData().id}.xml");
                }
                catch (Exception)
                {

                }



            }
        }

        internal void MyNote_save_MatrixElement(MatrixElement mE)
        {
            int i = 0;

            XmlDocument XD_matrix_element = new XmlDocument();

            XmlNode root = XD_matrix_element.CreateElement("MyNote_Notes_Matrix_Element");
            XD_matrix_element.AppendChild(root);

            XmlNode id = XD_matrix_element.CreateElement("id");
            id.InnerText = mE.id.ToString();
            root.AppendChild(id);

            foreach (Note note in mE.extract())
            {
                XmlNode notes_node = XD_matrix_element.CreateElement("note");

                XmlNode note_id = XD_matrix_element.CreateElement("id");
                note_id.InnerText = i.ToString();
                notes_node.AppendChild(note_id);

                XmlNode time = XD_matrix_element.CreateElement("time");
                time.InnerText = note.getNoteData().dateTime.ToString();
                notes_node.AppendChild(time);

                XmlNode title = XD_matrix_element.CreateElement("title");
                title.InnerText = note.getNoteData().title.ToString();
                notes_node.AppendChild(title);

                XmlNode content = XD_matrix_element.CreateElement("content");
                content.InnerText = note.getNoteData().content.ToString();
                notes_node.AppendChild(content);

                i++;

                root.AppendChild(notes_node);
            }


            try
            {
                XD_matrix_element.Save(@$"{MyNote_notes_matrix_folder}\{mE.id}.xml");
            }
            catch (Exception)
            {

            }


        }
        #endregion MyNote saving
        #endregion MyNote

        private List<string> scan_directory(string path)
        {
            List<string> scan_list = new List<string>();

            scan_list = Directory.GetFiles(path).ToList<string>();

            StringBuilder stringBuilder = new StringBuilder();

            foreach (string item in scan_list)
            {
                stringBuilder.AppendLine(item);
            }


            //MessageBox.Show(stringBuilder.ToString());

            return scan_list;
        }
    }
}
/*  END OF FILE
 * 
 * 
 */