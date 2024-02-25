/* Aiding Elements User Interface
 *      LinkData class
 *      
 * link element properties
 * 
 * init:        2024|04|01
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */

namespace AidingElementsUserInterface.Core.AEUI_Data
{
    internal class LinkData
    {
        private string _link; // url, directory path or filename
        private string _linkText;

        internal string GetLink => _link;
        internal string GetLinkText => _linkText;

        internal LinkData()
        {

        }

        internal LinkData(string link, string linktext)
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
/*  END OF FILE
 * 
 * 
 */