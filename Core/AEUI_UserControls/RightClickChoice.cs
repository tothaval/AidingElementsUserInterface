/* Aiding Elements User Interface
 *      RightClickChoice element
 * 
 * inherits from: CoreContainer user control
 * 
 * element offers buttons to quit aeui or shutdown computer
 * 
 * init:        2023|11|27
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core;
using AidingElementsUserInterface.Core.Auxiliaries;

using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace AidingElementsUserInterface.Core.AEUI_UserControls
{
    internal class RightClickChoice : CoreContainer
    {

        public RightClickChoice()
        {
            InitializeComponent();

            build();
        }

        private async void build()
        {
            hideContainerNesting(this);

            await Task.Delay(12);

            WrapPanel wrapPanel = new WrapPanel() { Orientation = Orientation.Vertical };

            CoreButton CB_QuitButton = new CoreButton("quit");

            CoreButton CB_ShutdownButton = new CoreButton("shutdown");

            //CB_GraphicsTest.fileLinkElement.Click += CB_GraphicsTest_Click;
            CB_QuitButton.button.Click += CB_QuitButton_Click;
            CB_ShutdownButton.button.Click += CB_ShutdownButton_Click;

            CB_QuitButton.Margin = new Thickness(1);
            CB_ShutdownButton.Margin = new Thickness(1);

            //wrap_panel.Children.Add(CB_GraphicsTest);
            wrapPanel.Children.Add(CB_QuitButton);
            wrapPanel.Children.Add(CB_ShutdownButton);

            content_border.Child = wrapPanel;
        }


        // element events
        #region element events
        // CORE_RightClickChoiceElement
        #region CORE_RightClickChoiceElement
        private void RCC_Loaded(object sender, RoutedEventArgs e)
        {

        }
        #endregion CORE_RightClickChoiceElement

        // clicks
        #region button clicks
        //private void CB_GraphicsTest_Click(object sender, RoutedEventArgs e)

        //{
        //MainWindow mainWindow = YS_CLASS_ConfigData.Return_MainWindow();

        //Polyline poly = new Polyline();
        //poly.VerticalAlignment = VerticalAlignment.Center;
        //poly.Stroke = System.Windows.SystemColors.WindowTextBrush;
        //poly.StrokeThickness = 2;

        ////Content = poly;

        ////for (int i = 0; i < 2000; i++)
        ////{
        ////    poly.Points.Add(new System.Windows.Point(i, 96 * (1 - Math.Sin(i * Math.PI / 192))));
        ////}


        //for (int i = 0; i < 628; i += 4)
        //{

        //    poly.Points.Add(new System.Windows.Point(0, 250));
        //    poly.Points.Add(new System.Windows.Point(i, 250 + 100 * Math.Sin(i / 100)));
        //}

        //mainWindow.MainWindowCanvas.Children.Add(poly);
        //}


        private void CB_QuitButton_Click(object sender, RoutedEventArgs e)
        {
            new SharedLogic().QuitApplicationCommand();
        }

        private void CB_ShutdownButton_Click(object sender, RoutedEventArgs e)
        {
            new SharedLogic().ShutdownCommand();
        }
        #endregion button clicks
        #endregion element events


    }
}
/*  END OF FILE
 * 
 * 
 */