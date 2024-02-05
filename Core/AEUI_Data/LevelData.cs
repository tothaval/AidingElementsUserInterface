/* Aiding Elements User Interface
 *      LevelData class
 * 
 * class provides level data  
 * 
 * init:        2024|01|28
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
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

        internal int LEVEL => level;
        internal string NAME => name;               
        internal string DESCRIPTION => description;
        internal bool ZERO_FLAG => zero_flag;
        internal bool VISIBILITY_FLAG => visibility_flag;

        internal bool LOGIN_FLAG => login_flag;
        internal bool SECURITY_FLAG => security_flag;


        public LevelData(int level, string name, bool lOGIN_FLAG, bool sECURITY_FLAG)
        {
            this.visibility_flag = true;


            this.level = level;
            this.name = name;
            this.description = "SYSTEM PANEL";

            this.zero_flag = true;
            login_flag = lOGIN_FLAG;
            security_flag = sECURITY_FLAG;
        }


        public LevelData(int level, string name, string description, bool zero_flag, bool lOGIN_FLAG, bool sECURITY_FLAG)
        {
            this.visibility_flag = true;

            this.level = level;
            this.name = name;
            this.description = description;

            this.zero_flag = zero_flag;
            login_flag = lOGIN_FLAG;
            security_flag = sECURITY_FLAG;
        }
    }
}
/*  END OF FILE
 * 
 * 
 */