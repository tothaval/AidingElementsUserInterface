/* Aiding Elements User Interface
 *      Shape element 
 * 
 * basic element to display a shape drawn via shapes element
 * 
 * 
 * init:        2024|02|24
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AidingElementsUserInterface.Elements
{
    /// <summary>
    /// Interaktionslogik für Shape.xaml
    /// </summary>
    public partial class Shape : UserControl
    {
        private System.Windows.Shapes.Shape shape;
        private double width, height;

        public Shape()
        {
                InitializeComponent();
        }

        public Shape(System.Windows.Shapes.Shape shape)
        {
            InitializeComponent();

            this.shape = shape;

            border.Child = shape;
        }

        internal void changeShapeSize(double width, double height)
        {
            this.width = width;
            this.height = height;

            shape.Width = width;
            shape.Height = height;
        }

        internal void setShape(System.Windows.Shapes.Shape shape)
        {
            border.Child = null;

            this.shape = shape;

            border.Child = shape;
        }
    }
}
/*  END OF FILE
 * 
 * 
 */