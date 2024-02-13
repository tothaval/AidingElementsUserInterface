/* Aiding Elements User Interface
 *      LocalDrives element 
 * 
 * basic element to acces hardware drives accessible to the user
 * 
 * init:        2024|01|16
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core;
using AidingElementsUserInterface.Core.AEUI_UserControls;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace AidingElementsUserInterface.Elements
{
    /// <summary>
    /// Interaktionslogik für LocalDrives.xaml
    /// </summary>
    public partial class LocalDrives : UserControl
    {        
        private DriveInfo[] allDrives = DriveInfo.GetDrives();
        private double gigabyte = 1024 * 1024 * 1024;

        public LocalDrives()
        {
            InitializeComponent();

            updateDriveInfo();
        }

        internal void updateDriveInfo()
        {
            stackPanel.Children.Clear();

            allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in allDrives)
            {
                CoreValueChange driveInfoElement = new CoreValueChange(true, true, true, drive.Name);

                if (drive.IsReady == false)
                {
                    driveInfoElement.setText(
                        $"{drive.DriveType}");
                    driveInfoElement.coreButton._disabled();                    
                }
                else
                {
                    double free_space = drive.AvailableFreeSpace / gigabyte;
                    double total_space = drive.TotalSize / gigabyte;

                    double percentage = free_space / total_space * 100;

                    driveInfoElement.setIdentifier($"{drive.Name}\n{drive.DriveType} {drive.DriveFormat}");
                       
                    driveInfoElement.setText($"{free_space:N2}/{total_space:N2} GB\n{percentage:N2}% free space");

                    driveInfoElement.coreButton.setTooltip($"{drive.VolumeLabel} ({drive.Name})");
                }

                driveInfoElement.coreButton.button.Click += Button_Click;
                driveInfoElement.setObject(drive);

                stackPanel.Children.Add(driveInfoElement);
            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            CoreButton coreButton = (CoreButton)button.Parent;

            CoreValueChange driveInfoElement;

            DependencyObject ucParent = coreButton.Parent;

            while (!(ucParent is CoreValueChange))
            {
                ucParent = LogicalTreeHelper.GetParent(ucParent);
            }

            driveInfoElement = (CoreValueChange)ucParent;

            try
            {
                Process.Start("explorer.exe", ((DriveInfo)driveInfoElement._Object).Name);
            }
            catch (Exception exception)
            {
                DriveInfo driveInfo = (DriveInfo)driveInfoElement._Object;

                MessageBox.Show($"could not process drive {driveInfo.Name}\n" +
                    $"{driveInfo.VolumeLabel}\n" +
                    $"{driveInfo.DriveFormat}\n" +
                    $"{exception.Message}\n" +
                    $"{exception.StackTrace}\n" +
                    $"{exception.Source}");
            }
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {

        }
    }
}
/*  END OF FILE
 * 
 * 
 */