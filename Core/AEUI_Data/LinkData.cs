using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AidingElementsUserInterface.Core.AEUI_Data
{
    internal class LinkData
    {
        private string _link; // url, directory path or filename
        private string _linkText;

        internal string GetLink => _link;
        internal string GetLinkText => _linkText;

        public LinkData()
        {

        }

        public LinkData(string link, string linktext)
        {
            _link = link;
            _linkText = linktext;
        }
       
        internal void SetLink(string link) 
        {
            _link = link;
        }

        internal void SetLinkText(string linkText)
        {
            _linkText = linkText;
        }
    }
}
