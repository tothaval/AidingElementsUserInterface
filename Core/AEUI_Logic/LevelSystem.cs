/* Aiding Elements User Interface
 *      LevelSystem class
 *  
 * this class manages the level mechanic per corecanvas
 * levels are basically a z-index based depth layer
 * the z-index functionality is used to hide and show elements
 * on different z-indices.
 * 
 * init:        2024|01|28
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.AEUI_UserControls;
using AidingElementsUserInterface.Core.Auxiliaries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AidingElementsUserInterface.Core.AEUI_Logic
{
    internal class LevelSystem
    {
        // the question remaining regarding level counting: 1-200 or -100 <-> +100
        private ObservableCollection<LevelData> _levels = new ObservableCollection<LevelData>();
        private int canvasID = 0;

        internal int current_level {get;set;}
        private string visibility_MODE = "all"; // all | range | level

        private LevelData ZERO_LEVEL = new LevelData(0, "Zero Level", true, true); // 'level system level'

        public LevelSystem(int canvasID)
        {
            current_level = 1;
            this.canvasID = canvasID;

            initiate();
        }

        public LevelSystem(ObservableCollection<LevelData> levels, int canvasID)
        {
            _levels.Clear();

            current_level = 1;
            _levels = levels;                
            this.canvasID = canvasID;
        }


        internal ObservableCollection<LevelData> getLevels() { return _levels; }

        private void initiate()
        {
            if (ZERO_LEVEL != null)
            {
                _levels.Add(ZERO_LEVEL);

                for (int i = 1; i < CoreCanvasSwitchData.Get_CORECANVAS_LEVEL_CAP; i++)
                {
                    bool flag = false;
                    bool flag2 = false;

                    _levels.Add(new LevelData(i, $"level {i}", "desc", flag, flag2, true));
                }
            }
        }

        internal void LevelChange()
        {
            CoreCanvas current_screen = new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS;

            if (current_screen != null)
            {
                if (current_level < 0)
                {
                    current_level = 0;
                }
                if(current_level > CoreCanvasSwitchData.Get_CORECANVAS_LEVEL_CAP - 1)
                {
                    current_level = CoreCanvasSwitchData.Get_CORECANVAS_LEVEL_CAP - 1;
                }

                current_screen.SetVisibility(current_level, visibility_MODE);
            }

        }

        internal LevelData? Get_CURRENT_LEVEL()
        {
            LevelChange();
            return _levels[current_level];

        }

        internal int Get_LEVEL()
        {
            return current_level;
        }

        internal LevelData? Get_ZERO_LEVEL()
        {
            LevelChange();
            current_level = 0;

            return ZERO_LEVEL;
        }

        internal LevelData? First()
        {
            LevelChange();

            current_level = 1;

            return _levels[1];
        }

        internal LevelData? Last()
        {
            current_level = CoreCanvasSwitchData.Get_CORECANVAS_LEVEL_CAP - 1;

            LevelChange();

            return _levels[current_level];
        }


        internal LevelData? Next()
        {
            if (current_level < 200)
            {
                current_level++;
            }
            else
            {
                current_level = CoreCanvasSwitchData.Get_CORECANVAS_LEVEL_CAP - 1;
            }

            LevelChange();

            return _levels[current_level];
        }


        internal LevelData? Prev()
        {
            if (current_level > 0)
            {
                current_level--;
            }
            else
            {
                current_level = 0;
            }

            LevelChange();

            return _levels[current_level];
        }

        internal void SetCurrentLevel(int level)
        {
            current_level = level;

            LevelChange();
        }

        internal void SetVisibilityMODE(string MODE)
        {
            if (MODE.Equals("all") | MODE.Equals("level") | MODE.Equals("range"))
            {
                visibility_MODE = MODE;
            }
            else
            {
                visibility_MODE = "all";
            }
        }
    }
}
/*  END OF FILE
 * 
 * 
 */