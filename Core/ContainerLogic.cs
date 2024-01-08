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
using AidingElementsUserInterface.Core.AEUI_UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AidingElementsUserInterface.Core
{
    internal static class ContainerLogic
    {
        // Container Movement
        public static void DragStart(
            ref bool elementDrag,
            CoreContainer CORE_ContainerElement,
            ref int z_position,
            ref int dragLevel,
            ref Point dragPoint,
            ref CoreCanvas canvas
            )
        {
            if (elementDrag == false)
            {
                z_position = Panel.GetZIndex(CORE_ContainerElement);
            }

            // start dragging
            elementDrag = true;
            Panel.SetZIndex(CORE_ContainerElement, dragLevel);

            // save start point of dragging
            dragPoint = Mouse.GetPosition(canvas);
        }

        public static void DragMove(
            ref bool elementDrag,
            CoreContainer CORE_ContainerElement,
            ref Point dragPoint,
            ref CoreCanvas canvas
            )
        {
            if (elementDrag)
            {
                Point newPoint = Mouse.GetPosition(canvas);
                double left = Canvas.GetLeft(CORE_ContainerElement);
                double top = Canvas.GetTop(CORE_ContainerElement);
                Canvas.SetLeft(CORE_ContainerElement, left + (newPoint.X - dragPoint.X));
                Canvas.SetTop(CORE_ContainerElement, top + (newPoint.Y - dragPoint.Y));

                canvas.MoveSelection(CORE_ContainerElement, dragPoint, newPoint);

                dragPoint = newPoint;

            }
        }

        public static void DragStop(
            ref bool elementDrag,
            CoreContainer CORE_ContainerElement,
            ref int z_position
            )
        {
            elementDrag = false;

            Panel.SetZIndex(CORE_ContainerElement, z_position);
        }

        public static void PositionUserControl(
            UserControl userControl,
            double x,
            double y
            )
        {
            Canvas.SetLeft(userControl, x);
            Canvas.SetTop(userControl, y);
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


        // Container Coloring
        public static void ApplyColorOnBorder(
            Border border,
            string brushtype,
            int brushOrientation,
            string color1_string,
            string color2_string,
            string color3_string,
            string color4_string)
        {
            //if (brushType == 0)
            //{
            //    border.Background = new SolidColorBrush(YS_CLASS_ConfigData.ColorConverter(color1_string).Color);
            //    border.BorderBrush = new SolidColorBrush(YS_CLASS_ConfigData.ColorConverter(color1_string).Color);
            //}

            //else if (brushType == 1)
            //{
            //    RadialGradientBrush radialGradientBrush = new RadialGradientBrush();

            //    // Set the GradientOrigin to the center of the area being painted.
            //    radialGradientBrush.GradientOrigin = new Point(0.5, 0.5);

            //    // Set the gradient center to the center of the area being painted.
            //    radialGradientBrush.Center = new Point(0.5, 0.5);

            //    // Set the radius of the gradient circle so that it extends to
            //    // the edges of the area being painted.
            //    radialGradientBrush.RadiusX = 0.5;
            //    radialGradientBrush.RadiusY = 0.5;

            //    // Create four gradient stops.
            //    radialGradientBrush.GradientStops.Add(new GradientStop(YS_CLASS_ConfigData.ColorConverter(color1_string).Color, 0.0));
            //    radialGradientBrush.GradientStops.Add(new GradientStop(YS_CLASS_ConfigData.ColorConverter(color2_string).Color, 0.25));
            //    radialGradientBrush.GradientStops.Add(new GradientStop(YS_CLASS_ConfigData.ColorConverter(color3_string).Color, 0.75));
            //    radialGradientBrush.GradientStops.Add(new GradientStop(YS_CLASS_ConfigData.ColorConverter(color4_string).Color, 1.0));

            //    border.Background = radialGradientBrush;
            //    border.BorderBrush = radialGradientBrush;
            //}

            //else if (brushType == 2)
            //{
            //    if (brushOrientation == 0)
            //    {
            //        LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
            //        linearGradientBrush.StartPoint = new Point(0.5, 1);
            //        linearGradientBrush.EndPoint = new Point(0.5, 0);
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color1_string).Color, 0.2));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color2_string).Color, 0.4));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color3_string).Color, 0.6));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color4_string).Color, 0.8));

            //        border.Background = linearGradientBrush;
            //        border.BorderBrush = linearGradientBrush;
            //    }
            //    else if (brushOrientation == 1)
            //    {
            //        LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
            //        linearGradientBrush.StartPoint = new Point(0.5, 0);
            //        linearGradientBrush.EndPoint = new Point(0.5, 1);
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color1_string).Color, 0.2));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color2_string).Color, 0.4));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color3_string).Color, 0.6));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color4_string).Color, 0.8));

            //        border.Background = linearGradientBrush;
            //        border.BorderBrush = linearGradientBrush;
            //    }
            //    else if (brushOrientation == 2)
            //    {
            //        LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
            //        linearGradientBrush.StartPoint = new Point(1, 0.5);
            //        linearGradientBrush.EndPoint = new Point(0, 0.5);
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color1_string).Color, 0.2));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color2_string).Color, 0.4));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color3_string).Color, 0.6));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color4_string).Color, 0.8));

            //        border.Background = linearGradientBrush;
            //        border.BorderBrush = linearGradientBrush;
            //    }
            //    else if (brushOrientation == 3)
            //    {
            //        LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
            //        linearGradientBrush.StartPoint = new Point(0, 0.5);
            //        linearGradientBrush.EndPoint = new Point(1, 0.5);
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color1_string).Color, 0.2));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color2_string).Color, 0.4));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color3_string).Color, 0.6));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color4_string).Color, 0.8));

            //        border.Background = linearGradientBrush;
            //        border.BorderBrush = linearGradientBrush;
            //    }
            //}
        }

        public static void ApplyColorOnCanvasElement(
            Border border,
            Canvas userControl,
            int brushType,
            int brushOrientation,
            string color1_string,
            string color2_string,
            string color3_string,
            string color4_string)
        {
            //if (brushType == 0)
            //{
            //    border.Background = new SolidColorBrush(YS_CLASS_ConfigData.ColorConverter(color1_string).Color);
            //    border.BorderBrush = new SolidColorBrush(YS_CLASS_ConfigData.ColorConverter(color1_string).Color);

            //    userControl.Background = new SolidColorBrush(YS_CLASS_ConfigData.ColorConverter(color1_string).Color);
            //}

            //else if (brushType == 1)
            //{
            //    RadialGradientBrush radialGradientBrush = new RadialGradientBrush();

            //    // Set the GradientOrigin to the center of the area being painted.
            //    radialGradientBrush.GradientOrigin = new Point(0.5, 0.5);

            //    // Set the gradient center to the center of the area being painted.
            //    radialGradientBrush.Center = new Point(0.5, 0.5);

            //    // Set the radius of the gradient circle so that it extends to
            //    // the edges of the area being painted.
            //    radialGradientBrush.RadiusX = 0.5;
            //    radialGradientBrush.RadiusY = 0.5;

            //    // Create four gradient stops.
            //    radialGradientBrush.GradientStops.Add(new GradientStop(YS_CLASS_ConfigData.ColorConverter(color1_string).Color, 0.0));
            //    radialGradientBrush.GradientStops.Add(new GradientStop(YS_CLASS_ConfigData.ColorConverter(color2_string).Color, 0.25));
            //    radialGradientBrush.GradientStops.Add(new GradientStop(YS_CLASS_ConfigData.ColorConverter(color3_string).Color, 0.75));
            //    radialGradientBrush.GradientStops.Add(new GradientStop(YS_CLASS_ConfigData.ColorConverter(color4_string).Color, 1.0));

            //    border.Background = radialGradientBrush;
            //    border.BorderBrush = radialGradientBrush;

            //    userControl.Background = radialGradientBrush;
            //}

            //else if (brushType == 2)
            //{
            //    if (brushOrientation == 0)
            //    {
            //        LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
            //        linearGradientBrush.StartPoint = new Point(0.5, 1);
            //        linearGradientBrush.EndPoint = new Point(0.5, 0);
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color1_string).Color, 0.2));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color2_string).Color, 0.4));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color3_string).Color, 0.6));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color4_string).Color, 0.8));

            //        border.Background = linearGradientBrush;
            //        border.BorderBrush = linearGradientBrush;

            //        userControl.Background = linearGradientBrush;
            //    }
            //    else if (brushOrientation == 1)
            //    {
            //        LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
            //        linearGradientBrush.StartPoint = new Point(0.5, 0);
            //        linearGradientBrush.EndPoint = new Point(0.5, 1);
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color1_string).Color, 0.2));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color2_string).Color, 0.4));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color3_string).Color, 0.6));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color4_string).Color, 0.8));

            //        border.Background = linearGradientBrush;
            //        border.BorderBrush = linearGradientBrush;

            //        userControl.Background = linearGradientBrush;
            //    }
            //    else if (brushOrientation == 2)
            //    {
            //        LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
            //        linearGradientBrush.StartPoint = new Point(1, 0.5);
            //        linearGradientBrush.EndPoint = new Point(0, 0.5);
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color1_string).Color, 0.2));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color2_string).Color, 0.4));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color3_string).Color, 0.6));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color4_string).Color, 0.8));

            //        border.Background = linearGradientBrush;
            //        border.BorderBrush = linearGradientBrush;

            //        userControl.Background = linearGradientBrush;
            //    }
            //    else if (brushOrientation == 3)
            //    {
            //        LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
            //        linearGradientBrush.StartPoint = new Point(0, 0.5);
            //        linearGradientBrush.EndPoint = new Point(1, 0.5);
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color1_string).Color, 0.2));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color2_string).Color, 0.4));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color3_string).Color, 0.6));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color4_string).Color, 0.8));

            //        border.Background = linearGradientBrush;
            //        border.BorderBrush = linearGradientBrush;

            //        userControl.Background = linearGradientBrush;
            //    }
            //}
        }

        public static void ApplyColorOnShape(
            Shape shape,
            int brushType,
            int brushOrientation,
            string color1_string,
            string color2_string,
            string color3_string,
            string color4_string,
            bool polyline)
        {
            //if (brushType == 0)
            //{
            //    if (!polyline)
            //    {
            //        shape.Fill = new SolidColorBrush(YS_CLASS_ConfigData.ColorConverter(color1_string).Color);
            //    }

            //    //shape.Fill = new SolidColorBrush(config.colorConverter(color1_string).Color);
            //    shape.Stroke = new SolidColorBrush(YS_CLASS_ConfigData.ColorConverter(color1_string).Color);
            //}

            //else if (brushType == 1)
            //{
            //    RadialGradientBrush radialGradientBrush = new RadialGradientBrush();

            //    // Set the GradientOrigin to the center of the area being painted.
            //    radialGradientBrush.GradientOrigin = new Point(0.5, 0.5);

            //    // Set the gradient center to the center of the area being painted.
            //    radialGradientBrush.Center = new Point(0.5, 0.5);

            //    // Set the radius of the gradient circle so that it extends to
            //    // the edges of the area being painted.
            //    radialGradientBrush.RadiusX = 0.5;
            //    radialGradientBrush.RadiusY = 0.5;

            //    // Create four gradient stops.
            //    radialGradientBrush.GradientStops.Add(new GradientStop(YS_CLASS_ConfigData.ColorConverter(color1_string).Color, 0.0));
            //    radialGradientBrush.GradientStops.Add(new GradientStop(YS_CLASS_ConfigData.ColorConverter(color2_string).Color, 0.25));
            //    radialGradientBrush.GradientStops.Add(new GradientStop(YS_CLASS_ConfigData.ColorConverter(color3_string).Color, 0.75));
            //    radialGradientBrush.GradientStops.Add(new GradientStop(YS_CLASS_ConfigData.ColorConverter(color4_string).Color, 1.0));

            //    if (!polyline)
            //    {
            //        shape.Fill = radialGradientBrush;
            //    }
            //    shape.Stroke = radialGradientBrush;
            //    //shape.Fill = radialGradientBrush;
            //}

            //else if (brushType == 2)
            //{
            //    if (brushOrientation == 0)
            //    {
            //        LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
            //        linearGradientBrush.StartPoint = new Point(0.5, 1);
            //        linearGradientBrush.EndPoint = new Point(0.5, 0);
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color1_string).Color, 0.2));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color2_string).Color, 0.4));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color3_string).Color, 0.6));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color4_string).Color, 0.8));

            //        if (!polyline)
            //        {
            //            shape.Fill = linearGradientBrush;
            //        }
            //        shape.Stroke = linearGradientBrush;
            //        //shape.Fill = linearGradientBrush;
            //    }
            //    else if (brushOrientation == 1)
            //    {
            //        LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
            //        linearGradientBrush.StartPoint = new Point(0.5, 0);
            //        linearGradientBrush.EndPoint = new Point(0.5, 1);
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color1_string).Color, 0.2));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color2_string).Color, 0.4));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color3_string).Color, 0.6));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color4_string).Color, 0.8));

            //        if (!polyline)
            //        {
            //            shape.Fill = linearGradientBrush;
            //        }
            //        shape.Stroke = linearGradientBrush;
            //        //shape.Fill = linearGradientBrush;
            //    }
            //    else if (brushOrientation == 2)
            //    {
            //        LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
            //        linearGradientBrush.StartPoint = new Point(0, 0.5);
            //        linearGradientBrush.EndPoint = new Point(1, 0.5);
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color1_string).Color, 0.2));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color2_string).Color, 0.4));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color3_string).Color, 0.6));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color4_string).Color, 0.8));

            //        if (!polyline)
            //        {
            //            shape.Fill = linearGradientBrush;
            //        }
            //        shape.Stroke = linearGradientBrush;
            //        //shape.Fill = linearGradientBrush;
            //    }
            //    else if (brushOrientation == 3)
            //    {
            //        LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
            //        linearGradientBrush.StartPoint = new Point(1, 0.5);
            //        linearGradientBrush.EndPoint = new Point(0, 0.5);
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color1_string).Color, 0.2));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color2_string).Color, 0.4));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color3_string).Color, 0.6));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color4_string).Color, 0.8));

            //        if (!polyline)
            //        {
            //            shape.Fill = linearGradientBrush;
            //        }
            //        shape.Stroke = linearGradientBrush;
            //        //shape.Fill = linearGradientBrush;
            //    }
            //}


        }


        public static void ApplyColorOnText(
            Label label,
            int brushType,
            int brushOrientation,
            string color1_string,
            string color2_string,
            string color3_string,
            ref string color4_string)
        {
            //if (brushType == 0)
            //{
            //    label.Foreground = new SolidColorBrush(YS_CLASS_ConfigData.ColorConverter(color1_string).Color);
            //}

            //else if (brushType == 1)
            //{
            //    RadialGradientBrush radialGradientBrush = new RadialGradientBrush();

            //    // Set the GradientOrigin to the center of the area being painted.
            //    radialGradientBrush.GradientOrigin = new Point(0.5, 0.5);

            //    // Set the gradient center to the center of the area being painted.
            //    radialGradientBrush.Center = new Point(0.5, 0.5);

            //    // Set the radius of the gradient circle so that it extends to
            //    // the edges of the area being painted.
            //    radialGradientBrush.RadiusX = 0.5;
            //    radialGradientBrush.RadiusY = 0.5;

            //    // Create four gradient stops.
            //    radialGradientBrush.GradientStops.Add(new GradientStop(YS_CLASS_ConfigData.ColorConverter(color1_string).Color, 0.0));
            //    radialGradientBrush.GradientStops.Add(new GradientStop(YS_CLASS_ConfigData.ColorConverter(color2_string).Color, 0.25));
            //    radialGradientBrush.GradientStops.Add(new GradientStop(YS_CLASS_ConfigData.ColorConverter(color3_string).Color, 0.75));
            //    radialGradientBrush.GradientStops.Add(new GradientStop(YS_CLASS_ConfigData.ColorConverter(color4_string).Color, 1.0));

            //    label.Foreground = radialGradientBrush;
            //}

            //else if (brushType == 2)
            //{
            //    if (brushOrientation == 0)
            //    {
            //        LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
            //        linearGradientBrush.StartPoint = new Point(0.5, 1);
            //        linearGradientBrush.EndPoint = new Point(0.5, 0);
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color1_string).Color, 0.2));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color2_string).Color, 0.4));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color3_string).Color, 0.6));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color4_string).Color, 0.8));

            //        label.Foreground = linearGradientBrush;
            //    }
            //    else if (brushOrientation == 1)
            //    {
            //        LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
            //        linearGradientBrush.StartPoint = new Point(0.5, 0);
            //        linearGradientBrush.EndPoint = new Point(0.5, 1);
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color1_string).Color, 0.2));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color2_string).Color, 0.4));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color3_string).Color, 0.6));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color4_string).Color, 0.8));

            //        label.Foreground = linearGradientBrush;
            //    }
            //    else if (brushOrientation == 2)
            //    {
            //        LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
            //        linearGradientBrush.StartPoint = new Point(1, 0.5);
            //        linearGradientBrush.EndPoint = new Point(0, 0.5);
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color1_string).Color, 0.2));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color2_string).Color, 0.4));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color3_string).Color, 0.6));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color4_string).Color, 0.8));

            //        label.Foreground = linearGradientBrush;
            //    }
            //    else if (brushOrientation == 3)
            //    {
            //        LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
            //        linearGradientBrush.StartPoint = new Point(0, 0.5);
            //        linearGradientBrush.EndPoint = new Point(1, 0.5);
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color1_string).Color, 0.2));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color2_string).Color, 0.4));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color3_string).Color, 0.6));
            //        linearGradientBrush.GradientStops.Add(
            //            new GradientStop(YS_CLASS_ConfigData.ColorConverter(color4_string).Color, 0.8));

            //        label.Foreground = linearGradientBrush;
            //    }
            //}
        }

        public static void ApplyElementColorDataFromList(
            List<object> list,
            int brushType,
            int brushOrientation,
            string color1_string,
            string color2_string,
            string color3_string,
            string color4_string,
            string imageFilepath,
            bool imageIsBackground)
        {
            brushType = (int)list[0];
            brushOrientation = (int)list[1];
            color1_string = (string)list[2];
            color2_string = (string)list[3];
            color3_string = (string)list[4];
            color4_string = (string)list[5];
            imageFilepath = (string)list[6];
            imageIsBackground = (bool)list[7];
        }

        public static List<object> ReturnElementColorDataAsList(
            int brushType,
            int brushOrientation,
            string color1_string,
            string color2_string,
            string color3_string,
            string color4_string,
            string imageFilepath,
            bool imageIsBackground)
        {
            List<object> list = new List<object>();

            list.Add(brushType);
            list.Add(brushOrientation);
            list.Add(color1_string);
            list.Add(color2_string);
            list.Add(color3_string);
            list.Add(color4_string);
            list.Add(imageFilepath);
            list.Add(imageIsBackground);

            return list;
        }


    }
}
/*  END OF FILE
 * 
 * 
 */