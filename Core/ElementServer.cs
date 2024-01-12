/* Aiding Elements User Interface
 *      ElementHandler class
 * 
 * this class manages element creation and max amount of instances
 * 
 * init:        2023|12|07
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.AEUI_UserControls;
using AidingElementsUserInterface.Core.Auxiliaries;
using AidingElementsUserInterface.Elements.FlatShareCC;

using System.Windows.Controls;
//using System.Windows.Forms;

namespace AidingElementsUserInterface.Core
{
    internal class ElementServer
    {
        FlatShareCC flatShareCC;

        // element instance creation
        #region element instantiation
        internal CoreContainer GetCoreContainer(UserControl content, CoreCanvas target)
        {
            return new CoreContainer(content, target);
        }

        internal CoreContainer? instantiate(UserControl content, CoreCanvas target)
        {
            if (content != null)
            {
                if (content.GetType() == typeof(FlatShareCC))
                {
                    flatShareCC = (FlatShareCC)content;
                }

                CoreContainer coreContainer = GetCoreContainer(content, target);

                coreContainer.GetContainerData().containerLocation = target.Name;


                new SharedLogic().GetElementHandler().addElement(coreContainer, target);

                return coreContainer;
            }

            return null;
        }


        internal FlatShareCC returnFlatShareCC()
        {
                return flatShareCC;      
        }
        #endregion element instantiation
    }
}
/*  END OF FILE
 * 
 * 
 */