/* Aiding Elements User Interface
 *      ElementHandler class
 * 
 * this class manages element creation and max amount of instances
 * 
 * init:        2023|12|07
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Elements;
using AidingElementsUserInterface.Elements.FlatShareCC;
using AidingElementsUserInterface.Elements.MyNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AidingElementsUserInterface.Core
{
    internal class ElementServer
    {
        private MyNote myNote_instance;

        private FlatShareCC flatShareCC_instance;


        // element instance creation
        #region element instantiation
        internal CoreContainer coreContainer(UserControl content, CoreCanvas target)
        {
            return new CoreContainer(content, target);
        }


        internal CoreContainer instantiate_FlatShareCC(CoreCanvas target)
        {
            if (flatShareCC_instance == null)
            {
                FlatShareCC fscc = new FlatShareCC();

                flatShareCC_instance = fscc;

                return coreContainer(fscc, target);
            }
            
            return coreContainer(flatShareCC_instance, target);
        }

        internal CoreContainer instantiate_Manual(CoreCanvas target)
        {
            Manual manual = new Manual();

            return coreContainer(manual, target);

        }

        internal CoreContainer instantiate_MyNote(CoreCanvas target)
        {
            if (myNote_instance == null)
            {
                MyNote note = new MyNote();

                myNote_instance = note;

                return coreContainer(note, target);
            }

            return coreContainer(myNote_instance, target);
        }
        #endregion element instantiation

        internal UserControl returnElement<T>(T element_content)
        {
            if (element_content is MyNote mynote)
            {
                if (myNote_instance != null)
                {
                    return myNote_instance;
                }
            }

            if (element_content is FlatShareCC flatShareCC)
            {
                if (flatShareCC_instance != null)
                {
                    return flatShareCC_instance;
                }

            }

            return null;
        }

        internal void update_instance<T>(T element_content)
        {
            if (element_content is MyNote mynote)
            {
                if (mynote != null)
                {
                    myNote_instance = mynote;
                }
            }

            if (element_content is FlatShareCC flatShareCC)
            {
                if (flatShareCC != null)
                {
                    flatShareCC_instance = flatShareCC;
                }

            }
        }
    }
}
