/* Aiding Elements User Interface
 *      Paste element 
 *  
 * paste selected elements onto active screen
 * 
 * init:        2024|01|28
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.AEUI_UserControls;
using AidingElementsUserInterface.Core.Auxiliaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AidingElementsUserInterface.Core.AEUI_SystemControls
{
    internal class Paste : CoreContainer
    {
        CoreButton CB_PasteSelection = new CoreButton("paste selection");

        public Paste()
        {
            build();
            registerEvents();
        }


        private void build()
        {
            hideContainerNesting(this);

            content_border.Child = CB_PasteSelection;

        }

        private void registerEvents()
        {
            CB_PasteSelection.button.Click += CB_MoveSelection_Click;
        }

        private void CB_MoveSelection_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new SharedLogic().GetMainWindow().CORE_CANVAS_SWITCH.paste();
        }
    }
}
/*  END OF FILE
 * 
 * 
 */