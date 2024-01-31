/* Aiding Elements User Interface
 *      Command element 
 * 
 * basic input element for core commands.
 * 
 * init:        2024|01|10
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.AEUI_Logic;
using AidingElementsUserInterface.Core.Auxiliaries;

using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using MessageBox = System.Windows.MessageBox;

namespace AidingElementsUserInterface.Core.AEUI_UserControls
{
    internal class Command : CoreContainer
    {
        private CoreTextBox coreTextBox;
        private StringBuilder input_command = new StringBuilder();

        public Command()
        {
            InitializeComponent();

            build();
        }

        private void build()
        {
            hideContainerNesting(this);

            coreTextBox = new CoreTextBox();
            coreTextBox.MinWidth = 100;

            this.content_border.Child = coreTextBox;

            coreTextBox.textbox.KeyUp += coreTextBox_KeyUp;
        }

        internal CoreTextBox GetCoreTextBox()
        {
            return coreTextBox;
        }

        internal StringBuilder GetInputCommand()
        {
            return input_command;
        }
       
        private void coreTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                input_command.Append(coreTextBox.getText());

                new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS.GetCentral().ExecuteCommandRequest(coreTextBox.getText());
            }
        }
    }
}
/*  END OF FILE
 * 
 * 
 */