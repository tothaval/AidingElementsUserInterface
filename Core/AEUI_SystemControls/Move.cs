using AidingElementsUserInterface.Core.AEUI_UserControls;
using AidingElementsUserInterface.Core.Auxiliaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AidingElementsUserInterface.Core.AEUI_SystemControls
{
    internal class Move : CoreContainer
    {
        CoreButton CB_MoveSelection = new CoreButton("move selection");

        // target canvas combobox oder so noch einbauen

        public Move()
        {
            build();
            registerEvents();
        }


        private void build()
        {
            hideContainerNesting(this);

            content_border.Child = CB_MoveSelection;

        }

        private void registerEvents()
        {
            CB_MoveSelection.button.Click += CB_MoveSelection_Click;
        }

        private void CB_MoveSelection_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new SharedLogic().GetMainWindow().CORE_CANVAS_SWITCH.move();
        }
    }
}
