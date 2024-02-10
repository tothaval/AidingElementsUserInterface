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
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Ribbon;

namespace AidingElementsUserInterface.Core.AEUI_Data
{
    internal class LevelData
    {
        private int level;
        private string name;
        private string description;

        private bool visibility_flag; 
        private bool login_flag;
        private bool security_flag;
        private bool hasBackground;

        private ColorData background;

        internal int LEVEL => level;
        internal string NAME => name;
        internal string DESCRIPTION => description;
        internal bool VISIBILITY_FLAG => visibility_flag;

        internal bool LOGIN_FLAG => login_flag;
        internal bool SECURITY_FLAG => security_flag;
        internal bool HASBACKGROUND => hasBackground;
        internal ColorData Background => background;


        public LevelData(int level, string name, bool lOGIN_FLAG, bool sECURITY_FLAG)
        {
            this.visibility_flag = true;


            this.level = level;
            this.name = name;
            this.description = "SYSTEM PANEL";

            login_flag = lOGIN_FLAG;
            security_flag = sECURITY_FLAG;


            this.hasBackground = false;

            if (this.background == null)
            {
                this.background = new ColorData();
            }
        }


        public LevelData(
            int level,
            string name,
            string description,
            bool lOGIN_FLAG,
            bool sECURITY_FLAG,
            bool visibility_flag,
            bool hasBackground = false,
            ColorData colorData = null)
        {
            this.visibility_flag = true;

            this.level = level;
            this.name = name;
            this.description = description;

            login_flag = lOGIN_FLAG;
            security_flag = sECURITY_FLAG;
            this.visibility_flag = visibility_flag;

            this.hasBackground = hasBackground;

            if (colorData == null)
            {
                this.background = new ColorData();
            }
            else
            {
                this.background = colorData;
            }
        }

        internal void SetBackground(bool hasBackground, ColorData? colorData )
        {
            this.hasBackground = hasBackground;

            if (colorData == null)
            {
               background = new ColorData();
            }
            else
            {
                background = colorData;
            }            
        }

        internal void SetName(string name) { this.name  = name; }
        internal void SetDescription(string description) { this.description = description;}        

    }
}
/*  END OF FILE
 * 
 * 
 */