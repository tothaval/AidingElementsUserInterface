/* Aiding Elements User Interface
 *      TXT_0 class
 * 
 * intermediate solution to serve textual contents to recipient elements
 * 
 * init:        2023|11|27
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AidingElementsUserInterface.Texts
{
    internal class TXT_0
    {
        public string language { get; set; }

        public TXT_0(string language)
        {
            this.language = language;
        }

        #region strings
        public string containerLoadingError()
        {
            if (language.Equals("english"))
            {
                return "Container Loading Error!";
            }
            else
            {
                return "language selection error";
            }
        }


        public string quitQuestion()
        {
            if (language.Equals("english"))
            {
                return "Do you want to quit?";
            }
            else
            {
                return "language selection error";
            }
        }

        public string quitTitle()
        {
            if (language.Equals("english"))
            {
                return "Aiding Elements UI shutdown";
            }
            else
            {
                return "language selection error";
            }
        }

        public string shutdownQuestion()
        {
            if (language.Equals("english"))
            {
                return "Are you sure you want to shutdown your computer?";
            }
            else
            {
                return "language selection error";
            }
        }

        public string shutdownTitle()
        {
            if (language.Equals("english"))
            {
                return "PC Shutdown";
            }
            else
            {
                return "language selection error";
            }
        }
        #endregion strings

    }
}
/*  END OF FILE
 * 
 * 
 */