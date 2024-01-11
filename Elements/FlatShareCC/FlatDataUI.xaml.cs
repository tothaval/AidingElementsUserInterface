﻿/* Aiding Elements User Interface
 *      FlatShareCC element 
 *          FlatDataUI user control
 * 
 * init:        2023|12|03
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 * 
 * origin:      FlatShareCostCalculator_2023_11_18(MIT-license) https://github.com/tothaval/Flat-Share-CC
 */
using AidingElementsUserInterface.Core;
using AidingElementsUserInterface.Core.Auxiliaries;
using AidingElementsUserInterface.Core.FlatShareCC_Data;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AidingElementsUserInterface.Elements.FlatShareCC
{
    /// <summary>
    /// Interaktionslogik für FlatDataUI.xaml
    /// </summary>
    public partial class FlatDataUI : UserControl
    {
        private FlatData flatData = new FlatData();
        private int counter = 1;

        public FlatDataUI()
        {
            InitializeComponent();

            setup_ui();
        }

        public void focus()
        {
            DIE_area.focus_and_select();
        }

        private void add_room_die()
        {
            RoomData room = new RoomData();
            room.id = counter;
            room.name = $"room {counter}";

            flatData.rooms.Add(room);

            SP_rooms.Children.Add(setup_room_die());
            counter++;
        }

        private void fresh_state()
        {
            BTN_add_room.IsEnabled = true;
            BTN_delete_room.IsEnabled = true;

            BTN_reset.IsEnabled = false;
            BTN_reset.Visibility = Visibility.Collapsed;
        }

        private void load_room_die(CoreDataInputElement room, RoomData data)
        {
            room.configurate($"{data.name}", false, $"{data.area}", "m²");
            room.insert_value(data.area, 2);

            SP_rooms.Children.Add(room);
        }

        internal void load_data(FlatData data)
        {
            this.flatData = data;

            DIE_area.insert_value(flatData.flat_space, 2);
            shared_area_update();

            foreach (RoomData room in flatData.rooms)
            {
                if (SP_rooms.Children.Count < flatData.rooms.Count)
                {
                    load_room_die(setup_room_die(room.area), room);
                }

            }

            save_state();
        }

        private void save_state()
        {
            BTN_add_room.IsEnabled = false;
            BTN_delete_room.IsEnabled = false;

            BTN_reset.IsEnabled = true;
            BTN_reset.Visibility = Visibility.Visible;
        }

        private CoreDataInputElement setup_room_die(double value = 0)
        {
            CoreDataInputElement room = new CoreDataInputElement();
            string text = "0,0";

            if (value != 0)
            {
                text = value.ToString();
            }

            room.configurate($"room {counter}", false, $"{text}", "m²");
            room.set_input_value_type(0.0);

            room.KeyDown += Room_KeyDown;

            return room;
        }

        private void setup_ui(double value = 0)
        {
            string text = "0,0";

            if (value != 0)
            {
                text = value.ToString();
            }

            DIE_area.configurate("apartment size", false, $"{text}", "m²");
            DIE_area.set_input_value_type(0.0);

            DIE_area.KeyDown += DIE_area_KeyDown;

            DIE_shared_area.configurate("shared area", true, "0", Units.area);
        }

        private void shared_area_update()
        {
            double share = flatData.flat_space;

            foreach (RoomData room in flatData.rooms)
            {
                share -= room.area;
            }

            DIE_shared_area.insert_value(share, 2);
        }

        private void BTN_add_room_Click(object sender, RoutedEventArgs e)
        {
            add_room_die();
        }

        private void BTN_delete_room_Click(object sender, RoutedEventArgs e)
        {
            if (SP_rooms.Children.Count > 0)
            {
                SP_rooms.Children.RemoveAt(SP_rooms.Children.Count - 1);
                flatData.rooms.RemoveAt(flatData.rooms.Count - 1);
                counter--;
            }

            if (SP_rooms.Children.Count == 0)
            {
                counter = 0;
            }
        }

        private void BTN_save_data_Click(object sender, RoutedEventArgs e)
        {
            save_state();

            FlatShareCC origin = new SharedLogic().GetFlatShareCC();

            origin.save_data(flatData);
        }

        private void BTN_reset_Click(object sender, RoutedEventArgs e)
        {
            fresh_state();
        }

        private void DIE_area_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                flatData.flat_space = (double)((CoreDataInputElement)sender).value;

                DIE_area.ToolTip = flatData.flat_space.ToString();

                BTN_add_room.Focus();
            }
        }

        private void Room_KeyDown(object sender, KeyEventArgs e)
        {
            CoreDataInputElement room = (CoreDataInputElement)sender;

            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                int index = SP_rooms.Children.IndexOf(room);
                int next = index + 1;

                flatData.rooms[index].area = (double)room.value;

                room.ToolTip = $"{flatData.rooms[index].name}\n{flatData.rooms[index].area}";

                if (next < SP_rooms.Children.Count)
                {
                    ((CoreDataInputElement)(SP_rooms.Children[next])).focus_and_select();
                }
                else
                {
                    BTN_save_data.Focus();
                }

                shared_area_update();
            }
        }
    }
}
/*  END OF FILE
 * 
 * 
 */