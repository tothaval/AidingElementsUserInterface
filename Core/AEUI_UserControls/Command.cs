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
        private string input_command;

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
       
        private void coreTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                //MessageBox.Show(GetCanvas().Name);

                new SharedLogic().GetMainWindow().coreCanvas.GetCentral().ExecuteCommandRequest(coreTextBox.getText());

                // returns null because canvas is null somehow
                //GetCanvas().GetCentral().ExecuteCommandRequest(coreTextBox.getText());
            }
        }
    }
}
/*  END OF FILE
 * 
 * 
 */