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
using System.Windows;

namespace AidingElementsUserInterface.Core.AEUI_Logic
{
    internal class LevelSystem
    {
        SharedLogic logic = new SharedLogic();

        private ObservableCollection<LevelData> _levels = new ObservableCollection<LevelData>();
        private int canvasID = 0;
                
        internal int current_level {get;set;}
        
        internal int max_range { get;set;}
        internal int min_range { get;set;}
        
        private string visibility_MODE = "all"; // all | range | level

        private LevelData ZERO_LEVEL = new LevelData(0, "Zero Level", true, true); // 'level system level'

        internal int CanvasID => canvasID;

        public LevelSystem(int canvasID)
        {
            current_level = 1;

            max_range = CoreCanvasSwitchData.Get_CORECANVAS_LEVEL_CAP - 1;
            min_range = 1;

            this.canvasID = canvasID;

            initiate();
        }

        public LevelSystem(ObservableCollection<LevelData> levels, int canvasID, int level_id)
        {
            _levels = levels;                
            this.canvasID = canvasID;

            current_level = level_id;

            max_range = CoreCanvasSwitchData.Get_CORECANVAS_LEVEL_CAP - 1;
            min_range = 1;

            LevelChange();
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

        internal async void LevelChange()
        {
            await Task.Delay(5);

            if (logic.GetMainWindow().Get_SYSTEM_ACTIVE_FLAG)
            {
                CoreCanvas current_screen = logic.GetMainWindow().Get_SYTEM_CANVAS.Get_SYSTEM_CANVAS;
                current_screen.NoLevelBackground();

                if (current_screen != null)
                {
                    current_screen.LSD.setCanvas(ref current_screen);


                    if (current_level <= 0)
                    {
                        current_level = 0;
                        current_screen.LSD.Visibility = Visibility.Visible;
                        current_screen.LSD.update();
                    }
                    else
                    {
                        current_screen.LSD.Visibility = Visibility.Collapsed;
                        current_screen.LSD.clear();
                    }

                    if (current_level > CoreCanvasSwitchData.Get_CORECANVAS_LEVEL_CAP - 1)
                    {
                        current_level = CoreCanvasSwitchData.Get_CORECANVAS_LEVEL_CAP - 1;
                    }

                    if (Get_CURRENT_LEVEL() != null)
                    {
                        if (Get_CURRENT_LEVEL().HASBACKGROUND)
                        {
                            if (Get_CURRENT_LEVEL().Background != null)
                            {
                                current_screen.SetBackground(Get_CURRENT_LEVEL().Background);
                            }                            
                        }
                    }

                    current_screen.SetVisibility(current_level, visibility_MODE);

  
                }
            }
            else
            {
                if (logic.GetMainWindow().Get_ACTIVE_CANVAS != null)
                {
                    CoreCanvas current_screen = logic.GetMainWindow().Get_ACTIVE_CANVAS;
                    current_screen.NoLevelBackground();

                    if (current_screen != null)
                    {
                        current_screen.LSD.setCanvas(ref current_screen);

                        if (current_level <= 0)
                        {
                            current_level = 0;
                            current_screen.LSD.Visibility = Visibility.Visible;
                            current_screen.LSD.update();
                        }
                        else
                        {
                            current_screen.LSD.Visibility = Visibility.Collapsed;
                            current_screen.LSD.clear();
                        }

                        if (current_level > CoreCanvasSwitchData.Get_CORECANVAS_LEVEL_CAP - 1)
                        {
                            current_level = CoreCanvasSwitchData.Get_CORECANVAS_LEVEL_CAP - 1;
                        }

                        if (Get_CURRENT_LEVEL() != null)
                        {
                            if (Get_CURRENT_LEVEL().HASBACKGROUND)
                            {
                                current_screen.SetBackground(Get_CURRENT_LEVEL().Background);
                            }
                        }

                        current_screen.SetVisibility(current_level, visibility_MODE);

                        //current_screen.getCanvasData().setLevel_id(current_level);
                    }
                }
            }
        }

        internal LevelData? Get_CURRENT_LEVEL()
        {            
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

        internal void SetRange(int min, int max)
        {
            min_range = min;
            max_range = max;
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