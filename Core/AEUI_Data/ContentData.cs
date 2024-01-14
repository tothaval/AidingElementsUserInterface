/* Aiding Elements User Interface
 *      ContentData class
 *      
 * inherits from: ContainerData class
 * 
 * access element content properties via dictionary
 * 
 * init:        2024|01|14
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using System.Collections.Generic;
using System.Windows;

namespace AidingElementsUserInterface.Core.AEUI_Data
{
    internal class ContentData : ContainerData
    {
        private Dictionary<string, object> values = new Dictionary<string, object>();

        internal ContentData()
        {

        }

        internal void AddValue(string key, object value)
        {
            values.Add(key, value);
        }

        internal void Clear()
        {
            values.Clear();
        }

        internal object? GetValue(string key)
        {
            if (values.ContainsKey(key))
            {
                return values[key];
            }

            return null;
        }

        internal Dictionary<string, object> GetValuesDictionary()
        {
            return values;
        }
    }
}
/*  END OF FILE
 * 
 * 
 */