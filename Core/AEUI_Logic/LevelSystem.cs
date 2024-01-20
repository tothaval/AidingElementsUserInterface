using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.AEUI_UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AidingElementsUserInterface.Core.AEUI_Logic
{
    internal class LevelSystem
    {

        private ObservableCollection<CoreContainer> _container = new ObservableCollection<CoreContainer>();
        
        private ObservableCollection<LevelData> _levels = new ObservableCollection<LevelData>();

        private int current_level = 1;

        private LevelData ZERO_LEVEL = new LevelData(0, "Zero Level", true, true); // 'level system level'

        public LevelSystem()
        {

            initiate();
        }

        private void initiate()
        {
            //load, if fails, instantiate first time style

            if (ZERO_LEVEL != null)
            {
                _levels.Add(ZERO_LEVEL);

                bool upper = false;
                int lower = 0;

                for (int i = 1; i < CoreCanvasSwitchData.Get_CORECANVAS_LEVEL_CAP; i++)
                {
                    lower++;

                    bool flag = false;
                    bool flag2 = false;

                    // test areas
                    if (i==2 && i == 4)
                    {
                        flag = true;

                        if (i == 4)
                        {
                            flag2 = true;
                        }

                    }
                    // test area

                    _levels.Add(new LevelData(i, $"level {i}", "desc", upper, flag, flag2, _container));

                    if (lower == 100)
                    {
                        upper = true;
                    }

                }
            }
            else
            {
                throw new Exception();
            }           
        }

        internal LevelData? System()
        {
            int levelCap = CoreCanvasSwitchData.Get_CORECANVAS_LEVEL_CAP - 1;

            int lowerLevelCap = levelCap;

            return _levels[lowerLevelCap];
        }

        internal LevelData? Get_CURRENT_LEVEL()
        {
            return _levels[current_level];
        }

        internal LevelData? Get_ZERO_LEVEL()
        {
            return ZERO_LEVEL;
        }

        internal LevelData? First()
        {            
            int levelCap = CoreCanvasSwitchData.Get_CORECANVAS_LEVEL_CAP - 1;
                        
            return _levels[1];
        }

        internal LevelData? Last()
        {
            int levelCap = CoreCanvasSwitchData.Get_CORECANVAS_LEVEL_CAP - 1;

            int upperLevelCap = levelCap;

            return _levels[upperLevelCap];
        }


        internal LevelData? Next()
        {
            // upper and lower math, if below zero, lower 1 - 100, if above zero, upper 101-200

            //int levelCap = CoreCanvasSwitchData.Get_CORECANVAS_LEVEL_CAP - 1;

            //int upperLevelCap = levelCap;

            //if (current_level <= upperLevelCap)
            //{
            //    current_level++;
            //}
            //else
            //{
            //    current_level = upperLevelCap;
            //}


            return _levels[current_level];
        }


        internal LevelData? Prev()
        {
            // upper and lower math, if below zero, lower 1 - 100, if above zero, upper 101-200

            //int levelCap = CoreCanvasSwitchData.Get_CORECANVAS_LEVEL_CAP - 1;

            //int lowerLevelCap = levelCap;

            //if (current_level <= lowerLevelCap)
            //{
            //    current_level++;
            //}
            //else
            //{
            //    current_level = lowerLevelCap;
            //}


            return _levels[current_level];
        }

    }
}
