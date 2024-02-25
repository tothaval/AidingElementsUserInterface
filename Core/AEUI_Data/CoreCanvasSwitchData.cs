/* Aiding Elements User Interface
 *      CoreCanvasSwitchData class
 * 
 * this class provides drag and hover level data
 * and stores max caps as technical boundaries
 * for the aeui system
 * 
 * init:        2024|01|19
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AidingElementsUserInterface.Core.AEUI_Data
{
    internal static class CoreCanvasSwitchData
    {
        private const int CORECANVAS_CAP = 11;              // tested 21 screens and 101 levels
        private const int CORECANVAS_LEVEL_CAP = 201;       // worked, level system needs a fix
        private const int CORECANVAS_DIMENSION_CAP = 10000; // to correctly generate levels in level 0 overview based on CAP
                                                            // despite that, no immediate problem came to the eye.

        
        private const int CORECANVAS_DRAG_LEVEL = 30000;
        private const int CORECANVAS_HOVER_LEVEL = 3000;


        internal static int Get_CORECANVAS_CAP => CORECANVAS_CAP;

        internal static int Get_CORECANVAS_DIMENSION_CAP => CORECANVAS_DIMENSION_CAP;

        internal static int Get_CORECANVAS_DRAG_LEVEL => CORECANVAS_DRAG_LEVEL;

        internal static int Get_CORECANVAS_HOVER_LEVEL => CORECANVAS_HOVER_LEVEL;

        internal static int Get_CORECANVAS_LEVEL_CAP => CORECANVAS_LEVEL_CAP;        

    }
}
/*  END OF FILE
 * 
 * 
 */