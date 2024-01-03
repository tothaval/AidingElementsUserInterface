using AidingElementsUserInterface.Core.AEUI_Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
