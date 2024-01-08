using AidingElementsUserInterface.Core.Auxiliaries;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AidingElementsUserInterface.Core.AEUI_UserControls
{
    internal class CoreCommand : CoreContainer
    {
        private CoreTextBox coreTextBox;
        private string input_command;

        public CoreCommand()
        {
            InitializeComponent();

            build();
        }

        private void build()
        {
            coreTextBox= new CoreTextBox();
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
