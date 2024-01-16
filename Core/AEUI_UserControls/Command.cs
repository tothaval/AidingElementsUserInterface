/* Aiding Elements User Interface
 *      Command element 
 * 
 * basic input element for core commands.
 * 
 * init:        2024|01|10
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.Auxiliaries;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

        private void execute()
        {
            if (input_command.Substring(0, 1).Equals(">"))
            {
                string target = input_command.Substring(1);

                Type? type = Type.GetType($"AidingElementsUserInterface.Elements.{target}, AidingElementsUserInterface");
                UserControl userControl;

                if (type == null)
                {
                    if (target.Equals("°"))
                    {
                        // typelist abarbeiten
                    }
                }

                

                if (type != null)
                {
                    userControl = (UserControl)Activator.CreateInstance(type);

                    new SharedLogic().GetMainWindow().coreCanvas.add_element_to_canvas(userControl);
                }

                type = Type.GetType($"AidingElementsUserInterface.Elements.{target}.{target}, AidingElementsUserInterface");
                if (type != null)
                {
                    userControl = (UserControl)Activator.CreateInstance(type);

                    new SharedLogic().GetMainWindow().coreCanvas.add_element_to_canvas(userControl);
                }


                type = Type.GetType($"AidingElementsUserInterface.Core.AEUI_UserControls.{target}, AidingElementsUserInterface");
                if (type != null)
                {
                    userControl = (UserControl)Activator.CreateInstance(type);

                    new SharedLogic().GetMainWindow().coreCanvas.add_element_to_canvas(userControl);
                }                
            }
            else if (input_command.Substring(0, 1).Equals("<"))
            {
                string target = input_command.Substring(1);

                Type? type = Type.GetType($"AidingElementsUserInterface.Elements.{target}, AidingElementsUserInterface");
                UserControl userControl;

                if (type == null)
                {
                    if (target.Equals("°"))
                    {
                        new SharedLogic().GetMainWindow().coreCanvas.selectAll();
                    }
                    else
                    {
                        int number;
                        try
                        {
                            number = Int32.Parse(target);

                            new SharedLogic().GetMainWindow().coreCanvas.selectContainer(number);
                        }
                        catch (Exception)
                        {                        
                        }                        
                    }                    
                }


                if (type != null)
                {
                    new SharedLogic().GetMainWindow().coreCanvas.selectType(type);
                }

                type = Type.GetType($"AidingElementsUserInterface.Elements.{target}.{target}, AidingElementsUserInterface");
                if (type != null)
                {
                    new SharedLogic().GetMainWindow().coreCanvas.selectType(type);
                }


                type = Type.GetType($"AidingElementsUserInterface.Core.AEUI_UserControls.{target}, AidingElementsUserInterface");
                if (type != null)
                {
                    new SharedLogic().GetMainWindow().coreCanvas.selectType(type);
                }
            }
        }

        private void coreTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                input_command = coreTextBox.getText();

                execute();
            }
        }
    }
}
/*  END OF FILE
 * 
 * 
 */