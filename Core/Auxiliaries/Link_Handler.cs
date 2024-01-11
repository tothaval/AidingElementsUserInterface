/* Aiding Elements User Interface
 *      Link_Handler class
 * 
 * basic LinkData management class
 * 
 * init:        2023|11|30
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 * 
 * origin:      MyNote_2023_11_01
 */
using AidingElementsUserInterface.Core.AEUI_Data;

using System.Collections.ObjectModel;

namespace AidingElementsUserInterface.Core.Auxiliaries
{
    internal class Link_Handler
    {
        private ObservableCollection<LinkData> links = new ObservableCollection<LinkData>();

        public Link_Handler()
        {

        }

        internal ObservableCollection<LinkData> GetLinks()
        {
            return links;
        }

        internal void AddLink(LinkData linkData)
        {
            links.Add(linkData);
        }

        internal void RemoveLink(LinkData linkData)
        {
            if (links.Contains(linkData))
            {
                links.Remove(linkData);
            }
        }
    }
}
/*  END OF FILE
 * 
 * 
 */