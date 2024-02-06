/* Aiding Elements User Interface
 *      ContainerLogic class
 * 
 * handles container related tasks like movement calculation
 * over the canvas where the element is placed/instantiated
 * 
 * 
 * init:        2023|11|29
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.AEUI_UserControls;

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AidingElementsUserInterface.Core.AEUI_Logic
{
    internal static class ContainerLogic
    {
        // Container Movement
        public static void DragStart(
            ref bool elementDrag,
            ref CoreContainer CORE_ContainerElement,
            ref Point dragPoint,
            ref CoreCanvas canvas
            )
        {

            // start dragging
            elementDrag = true;

            // save start point of dragging
            dragPoint = Mouse.GetPosition(canvas);

            canvas.MapSelection(CORE_ContainerElement, dragPoint);
        }

        public static void DragMove(
            ref bool elementDrag,
            ref CoreContainer CORE_ContainerElement,
            ref Point dragPoint,
            ref CoreCanvas canvas
            )
        {
            if (elementDrag)
            {
                Point newPoint = Mouse.GetPosition(canvas);

                double left = Canvas.GetLeft(CORE_ContainerElement);
                double top = Canvas.GetTop(CORE_ContainerElement);

                double x, y;
                x = newPoint.X - dragPoint.X + left;
                y = newPoint.Y - dragPoint.Y + top;

                Canvas.SetLeft(CORE_ContainerElement, x);
                Canvas.SetTop(CORE_ContainerElement, y);

                canvas.MoveSelection(CORE_ContainerElement, dragPoint, newPoint);

                dragPoint = newPoint;
            }
        }

        public static void DragStop(
            ref bool elementDrag
            )
        {
            elementDrag = false;
        }

        public static void RotateUserControl(
            UserControl userControl,
            double angle
            )
        {
            RotateTransform rt = new RotateTransform()
            {
                CenterX = userControl.ActualWidth / 2, //elementWidth; // * 0.5;
                CenterY = userControl.ActualHeight / 2, //elementHeight; //  * 0.5;

                Angle = angle
            };

            userControl.RenderTransform = rt;
        }
    }
}
/*  END OF FILE
 * 
 * 
 */