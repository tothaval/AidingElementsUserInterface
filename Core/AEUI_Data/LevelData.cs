using AidingElementsUserInterface.Core.AEUI_UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AidingElementsUserInterface.Core.AEUI_Data
{
    internal class LevelData
    {
        private int level;
        private string name;
        private string description;

        private bool zero_flag; // lower = false, upper = true
        private bool visibility_flag; 
        private bool login_flag;
        private bool security_flag;

        private ObservableCollection<CoreContainer> level_items;

        internal int LEVEL => level;
        internal string NAME => name;
               
        internal string DESCRIPTION => description;


        internal bool ZERO_FLAG => zero_flag;
        internal bool VISIBILITY_FLAG => visibility_flag;

        internal bool LOGIN_FLAG => login_flag;
        internal bool SECURITY_FLAG => security_flag;

        internal int ELEMENT_COUNT => level_items.Count;

        public LevelData(int level, string name, bool lOGIN_FLAG, bool sECURITY_FLAG, ObservableCollection<CoreContainer> level_items = null)
        {
            this.visibility_flag = true;


            this.level = level;
            this.name = name;
            this.description = "SYSTEM PANEL";

            this.zero_flag = true;
            login_flag = lOGIN_FLAG;
            security_flag = sECURITY_FLAG;

            this.level_items = level_items;

            if (level_items == null)
            {
                this.level_items = new ObservableCollection<CoreContainer>();
            }
        }


        public LevelData(int level, string name, string description, bool zero_flag, bool lOGIN_FLAG, bool sECURITY_FLAG, ObservableCollection<CoreContainer> level_items = null)
        {
            this.visibility_flag = true;

            this.level = level;
            this.name = name;
            this.description = description;

            this.zero_flag = zero_flag;
            login_flag = lOGIN_FLAG;
            security_flag = sECURITY_FLAG;
            this.level_items = level_items;
        }

        internal void ADD_level_item(CoreContainer item) 
        {
            if (item != null)
            {
                level_items.Add(item);
            }            
        }

        internal bool CLEAR()
        {
            level_items.Clear();

            if (level_items.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        internal void REMOVE_level_item(CoreContainer item)
        {
            level_items.Remove(item);
        }


        internal void REMOVE_level_item(int container_id)
        {
            level_items.RemoveAt(container_id);
        }

    }
}
