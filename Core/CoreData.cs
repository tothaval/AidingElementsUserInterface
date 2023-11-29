using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace AidingElementsUserInterface.Core
{
    internal class CoreData
    {
        public Color background { get; set; }
        public Color borderbrush { get; set; }
        public Color foreground { get; set; }
        public Color highlight { get; set; }

        public CornerRadius cornerRadius { get; set; }

        public Thickness thickness { get; set; }


        public CoreData()
        {
            background = Colors.BlanchedAlmond;
            borderbrush = Colors.Black;
            foreground = Colors.DarkSlateGray;
            highlight = Colors.Azure;

            cornerRadius = new CornerRadius(14);

            thickness = new Thickness(2);
        }
    }
}
