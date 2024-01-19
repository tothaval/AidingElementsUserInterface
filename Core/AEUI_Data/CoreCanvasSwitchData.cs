using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AidingElementsUserInterface.Core.AEUI_Data
{
    internal static class CoreCanvasSwitchData
    {
        private const int CORECANVAS_CAP = 11;
        private const int CORECANVAS_LEVEL_CAP = 201;
        private const int CORECANVAS_DIMENSION_CAP = 10000;

        
        private const int CORECANVAS_DRAG_LEVEL = 30000;
        private const int CORECANVAS_HOVER_LEVEL = 3000;


        internal static int Get_CORECANVAS_CAP => CORECANVAS_CAP;

        internal static int Get_CORECANVAS_DIMENSION_CAP => CORECANVAS_DIMENSION_CAP;

        internal static int Get_CORECANVAS_DRAG_LEVEL => CORECANVAS_DRAG_LEVEL;

        internal static int Get_CORECANVAS_HOVER_LEVEL => CORECANVAS_HOVER_LEVEL;

        internal static int Get_CORECANVAS_LEVEL_CAP => CORECANVAS_LEVEL_CAP;        

    }
}
