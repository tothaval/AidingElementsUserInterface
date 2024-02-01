/* Aiding Elements User Interface
 *      Copy element 
 *  
 * 
 * 
 * init:        2024|01|28
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.AEUI_UserControls;
using AidingElementsUserInterface.Core.Auxiliaries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AidingElementsUserInterface.Core.AEUI_SystemControls
{
    internal class Copy : CoreContainer
    {
        CoreButton CB_CopySelection = new CoreButton("copy selection");

        public Copy()
        {
            build();
            registerEvents();
        }


        private void build()
        {
            hideContainerNesting(this);

            content_border.Child = CB_CopySelection;
        }

  

        private void registerEvents()
        {
            CB_CopySelection.button.Click += CB_CopySelection_Click;
        }

        private void CB_CopySelection_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (new SharedLogic().GetMainWindow().CORE_CANVAS_SWITCH != null)
            {
                new SharedLogic().GetMainWindow().CORE_CANVAS_SWITCH.copy();
            }            
         
            e.Handled = true;
        }
    }
}
/*  END OF FILE
 * 
 * 
 */