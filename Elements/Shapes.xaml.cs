/* Aiding Elements User Interface
 *      Shapes element 
 * 
 * basic element to draw basic shapes on screen
 * amount, size, rotation, level and shape type can be set
 * currently only rectangle and triangle implemented
 *  
 * init:        2024|02|20
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core;
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.AEUI_UserControls;
using AidingElementsUserInterface.Core.Auxiliaries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
    /// Interaktionslogik für Shapes.xaml
    /// </summary>
    public partial class Shapes : UserControl
    {
        CoreData coreData;
        ObservableCollection<System.Windows.Shapes.Shape> shapes;

        // bugs:
        // when changing borderbrush or highlight over coreOptions, background changes too
        // need to look into that later, as often the passing of data is quite tricky.

        // if anyone reads this, do you know why c# or wpf, don't know which one, executes
        // most of the code double time? i can register this behaviour when using a messagebox
        // inside the container saving function, could be also true inside the loading function,
        // can not recall atm. Every object is processed twice.



        public Shapes()
        {

            InitializeComponent();

            build();
            registerEvents();

        }

        private void build()
        {
            // use containerdata for all values?
            coreData = new CoreData();
            shapes = new ObservableCollection<System.Windows.Shapes.Shape>();

            CVC_Amount.setIdentifier("shape amount");
            CVC_Amount.setText("1");

            CVC_Width.setIdentifier("shape width");
            CVC_Width.setText(coreData.width.ToString());

            CVC_Height.setIdentifier("shape height");
            CVC_Height.setText(coreData.height.ToString());

            CVC_Rotation.setIdentifier("shape rotation");
            CVC_Rotation.setText("0.0");

            CVC_Level.setIdentifier("shape level");
            CVC_Level.setText("1");

            CB_AddShape.setContent("draw shape");
        }

        private void registerEvents()
        {
            CB_AddShape.button.Click += CB_AddShape_Click;

        }

        private async void CB_AddShape_Click(object sender, RoutedEventArgs e)
        {
            double rotation;
            int level;
            int multitude_offset = 0;

            try
            {
                rotation = Double.Parse(CVC_Rotation.Value);
                level = Int32.Parse(CVC_Level.Value);
            }
            catch (Exception)
            {
                rotation = 0.0;
                level = 1;
            }

            GetShape();

            if (new SharedLogic().GetMainWindow().Get_SYSTEM_ACTIVE_FLAG)
            {
                CoreCanvas coreCanvas = new SharedLogic().GetMainWindow().Get_SYTEM_CANVAS.Get_SYSTEM_CANVAS;
                              

                foreach (System.Windows.Shapes.Shape shape in shapes)
                {
                    ContainerData containerData = new ContainerData(coreData, coreCanvas.getCanvasData().canvasID, level, rotation);
                    containerData.SetElement(shape);

                    await Task.Delay(4);

                    CoreContainer coreContainer = new CoreContainer(shape, containerData, ref coreCanvas);
                    coreContainer.setRotation(rotation);

                    await Task.Delay(4);

                    coreCanvas.add_element_to_canvas(coreContainer, new Point(100 + multitude_offset, 100 + multitude_offset));

                    multitude_offset += 25;
                }
            }
            else
            {
                if (new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS != null)
                {
                    CoreCanvas coreCanvas = new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS;
                    foreach (System.Windows.Shapes.Shape shape in shapes)
                    {
                        ContainerData containerData = new ContainerData(coreData, coreCanvas.getCanvasData().canvasID, level, rotation);
                        containerData.SetElement(shape);

                        await Task.Delay(4);

                        CoreContainer coreContainer = new CoreContainer(shape, containerData, ref coreCanvas);
                        coreContainer.setRotation(rotation);

                        await Task.Delay(4);

                        coreCanvas.add_element_to_canvas(coreContainer, new Point(100 + multitude_offset, 100 + multitude_offset));

                        multitude_offset += 25;
                    }
                }
            }

            e.Handled = true;
        }


        private void GetShape()
        {
            shapes.Clear();

            int amount;
            double width;
            double height;
            
            try
            {
                amount = Int32.Parse(CVC_Amount.Value);
                width = Double.Parse(CVC_Width.Value);
                height = Double.Parse(CVC_Height.Value);
            }
            catch (Exception)
            {
                amount = 1;
                width = coreData.width;
                height = coreData.height;
            }

            if (CBI_Ellipse.IsSelected)
            {

                for (int i = 0; i < amount; i++)
                {
                    Ellipse ellipse = new Ellipse()
                    {
                        Width = width,
                        Height = height,
                        Fill = coreData.background.GetBrush(),
                        Stroke = coreData.borderbrush.GetBrush()
                    };

                    shapes.Add(ellipse);
                }

                border_ShapeDisplay.Child = new Ellipse()
                {
                    Width = width,
                    Height = height,
                    Fill = coreData.background.GetBrush(),
                    Stroke = coreData.borderbrush.GetBrush()
                };

            }
            else if (CBI_Rectangle.IsSelected)
            {              
                for (int i = 0; i < amount; i++)
                {
                    Rectangle rectangle = new Rectangle()
                    {
                        Width = width,
                        Height = height,
                        Fill = coreData.background.GetBrush(),
                        Stroke = coreData.borderbrush.GetBrush()
                    };

                    shapes.Add(rectangle);
                }

                border_ShapeDisplay.Child = new Rectangle()
                {
                    Width = width,
                    Height = height,
                    Fill = coreData.background.GetBrush(),
                    Stroke = coreData.borderbrush.GetBrush()
                };
            }
        }



        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetShape();

            e.Handled = true;
        }
    }
}
/*  END OF FILE
 * 
 * 
 */