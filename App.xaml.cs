﻿/* Aiding Elements User Interface
 *      App 
 * 
 * autogenerated project file 
 */
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.Auxiliaries;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;

namespace AidingElementsUserInterface
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        XML_Handler handler = new XML_Handler();

        public App()
        {
            AddResources();
        }

        private void AddResources()
        {
            CoreDataResources();
        }

        private void CoreDataResources()
        {
            CoreData coreData = handler.CoreData_load();

            if (coreData == null)
            {
                coreData = new CoreData();
            }

            Application.Current.Resources["CoreData_background"] = coreData.background.GetBrush();
            Application.Current.Resources["CoreData_borderbrush"] = coreData.borderbrush.GetBrush();
            Application.Current.Resources["CoreData_foreground"] = coreData.foreground.GetBrush();
            Application.Current.Resources["CoreData_highlight"] = coreData.highlight.GetBrush();

            Application.Current.Resources["CoreData_cornerRadius"] = coreData.cornerRadius;
            Application.Current.Resources["CoreData_thickness"] = coreData.thickness;

            Application.Current.Resources["CoreData_fontSize"] = (double)coreData.fontSize;
            Application.Current.Resources["CoreData_fontFamily"] = coreData.fontFamily;

            Application.Current.Resources["CoreData_width"] = coreData.width;
            Application.Current.Resources["CoreData_height"] = coreData.height;

            if (File.Exists(coreData.background.brushtype))
            {
                Application.Current.Resources["CoreData_image"] = coreData.background.GetBrush();
                Application.Current.Resources["CoreData_background"] = coreData.background.GetBrush();
            }
        }
    }
}
