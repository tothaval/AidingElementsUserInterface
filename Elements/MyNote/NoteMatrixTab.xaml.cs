﻿/* Aiding Elements User Interface
 *      NoteMatrixTab element 
 * 
 * creates a freely configurable matrix of
 * Note elements within MyNote element
 * 
 * init:        2023|11|30
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 * 
 * origin:      MyNote_2023_11_01
 */
using AidingElementsUserInterface.Core.Auxiliaries;

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AidingElementsUserInterface.Elements.MyNote
{
    /// <summary>
    /// Interaktionslogik für NoteMatrixTab.xaml
    /// </summary>
    public partial class NoteMatrixTab : UserControl
    {
        int hori = 0;

        ObservableCollection<MatrixElement> elements = new ObservableCollection<MatrixElement>();

        private MyNote origin_element;


        public NoteMatrixTab(MyNote myNote)
        {
            origin_element = myNote;

            InitializeComponent();

            loadNotesMatrix();

            LL_hori.Content = hori.ToString();
        }

        private void add_MatrixElement()
        {
            MatrixElement matrixElement = new MatrixElement(origin_element);
            matrixElement.id = elements.Count;

            matrixElement.fill(hori);

            matrixElement.CKX_Select.Checked += CKX_Select_Checked;
            matrixElement.CKX_Delete.Checked += CKX_Delete_Checked;

            matrixElement.CKX_Select.Unchecked += CKX_Select_Unchecked; ;
            matrixElement.CKX_Delete.Unchecked += CKX_Delete_Unchecked; ;

            elements.Add(matrixElement);

            WP.Children.Add(matrixElement);
        }

        internal void loadNotesMatrix()
        {
            XML_Handler xml = new XML_Handler(origin_element);

            elements = xml.MyNote_load_matrix_elements();

            foreach (MatrixElement item in elements)
            {
                item.CKX_Select.Checked += CKX_Select_Checked;
                item.CKX_Delete.Checked += CKX_Delete_Checked;

                item.CKX_Select.Unchecked += CKX_Select_Unchecked; ;
                item.CKX_Delete.Unchecked += CKX_Delete_Unchecked; ;

                WP.Children.Add(item);
            }

            if (elements.Count == 0 || elements == null)
            {
                add_MatrixElement();
            }
        }


        internal void save_matrix()
        {
            XML_Handler handler = new XML_Handler(origin_element);

            handler.delete_files(handler.MyNote_notes_matrix_folder);

            foreach (MatrixElement matrixElement in elements)
            {
                handler.MyNote_save_MatrixElement(matrixElement);
            }
        }


        private void BTN_hori_minus_Click(object sender, RoutedEventArgs e)
        {
            hori--;

            if (hori < 0)
            {
                hori = 0;
            }

            LL_hori.Content = hori.ToString();

            e.Handled = true;
        }

        private void BTN_hori_plus_Click(object sender, RoutedEventArgs e)
        {
            hori++;

            LL_hori.Content = hori.ToString();

            e.Handled = true;
        }

        private void CKX_Delete_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void CKX_Select_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void CKX_Delete_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void CKX_Select_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;

            foreach (MatrixElement item in WP.Children)
            {
                if (item.CKX_Select == checkBox)
                {
                    LL_hori.Content = item.SP.Children.Count;
                }
                else
                {
                    item.CKX_Select.IsChecked = false;
                }
            }
        }



        private void BTN_add_Click(object sender, RoutedEventArgs e)
        {
            add_MatrixElement();
        }

        private void BTN_delete_Click(object sender, RoutedEventArgs e)
        {
            foreach (MatrixElement matrixElement in WP.Children)
            {
                if (matrixElement.CKX_Delete.IsChecked == true)
                {
                    elements.Remove(matrixElement);
                }
            }

            for (int i = 0; i < WP.Children.Count; i++)
            {
                if (!elements.Contains(WP.Children[i]))
                {
                    WP.Children.RemoveAt(i);
                }

            }
        }
    }
}
/*  END OF FILE
 * 
 * 
 */