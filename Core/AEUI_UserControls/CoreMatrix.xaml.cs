/* Aiding Elements User Interface
 *      CoreMatrix element 
 * 
 * basic element for stacking and removing of CoreMatrixLines
 * 
 * init:        2024|01|10
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace AidingElementsUserInterface.Core.AEUI_UserControls
{
    /// <summary>
    /// Interaktionslogik für CoreMatrix.xaml
    /// </summary>
    public partial class CoreMatrix : UserControl
    {
        private Type type;

        private ObservableCollection<CoreMatrixLine> coreMatrixLines = new ObservableCollection<CoreMatrixLine>();

        public CoreMatrix()
        {
            InitializeComponent();

            AddCoreMatrixLine();
        }

        public CoreMatrix(Type type)
        {
            this.type = type;

            InitializeComponent();

            AddCoreMatrixLine();
        }

        private void AddCoreMatrixLine()
        {
            if (type != null)
            {
                CoreMatrixLine coreMatrixLine = new CoreMatrixLine(type);

                coreMatrixLine.fill(1);

                coreMatrixLines.Add(coreMatrixLine);

                WP.Children.Add(coreMatrixLine);
            }
        }

        internal ObservableCollection<CoreMatrixLine> GetCoreMatrixLines()
        {
            return coreMatrixLines;
        }

        internal void loadNotesMatrix()
        {
            //XML_Handler xml = new XML_Handler(origin_element);

            //elements = xml.MyNote_load_matrix_elements();

            //foreach (MatrixElement item in elements)
            //{
            //    item.CKX_Select.Checked += CKX_Select_Checked;
            //    item.CKX_Delete.Checked += CKX_Delete_Checked;

            //    item.CKX_Select.Unchecked += CKX_Select_Unchecked; ;
            //    item.CKX_Delete.Unchecked += CKX_Delete_Unchecked; ;

            //    WP.Children.Add(item);
            //}

            //if (elements.Count == 0 || elements == null)
            //{
            //    AddCoreMatrixLine();
            //}
        }


        internal void save_matrix()
        {
            //XML_Handler handler = new XML_Handler(origin_element);

            //handler.delete_files(handler.MyNote_notes_matrix_folder);

            //foreach (MatrixElement matrixElement in elements)
            //{
            //    handler.MyNote_save_MatrixElement(matrixElement);
            //}
        }

        internal void SetType(Type type)
        {
            this.type = type;
        }

        private void BTN_add_Click(object sender, RoutedEventArgs e)
        {
            AddCoreMatrixLine();
        }

        private void BTN_delete_Click(object sender, RoutedEventArgs e)
        {
            if (WP.Children.Count > 0)
            {
                coreMatrixLines.RemoveAt(coreMatrixLines.Count - 1);
                WP.Children.RemoveAt(WP.Children.Count - 1);
            }
        }
    }
}
/*  END OF FILE
 * 
 * 
 */