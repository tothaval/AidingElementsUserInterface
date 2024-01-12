﻿/* Aiding Elements User Interface
 *      OptionsElement_ButtonData element 
 * 
 * basic options element to configure ButtonData properties.
 * 
 * init:        2024|01|11
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.Auxiliaries;

using System.Windows;
using System.Windows.Controls;

namespace AidingElementsUserInterface.Core.AEUI_UserControls
{
    internal class OptionsElement_ButtonData : CoreContainer
    {
        private ButtonData buttonData;
        private Data_Handler handler;

        private WrapPanel wrapPanel = new WrapPanel() { Orientation = Orientation.Vertical };


        private CoreValueChange CVC_cornerRadius = new CoreValueChange("corner radius");
        private CoreValueChange CVC_thickness = new CoreValueChange("thickness");
        private CoreValueChange CVC_fontSize = new CoreValueChange("fontSize");
        private CoreValueChange CVC_fontFamily = new CoreValueChange("fontFamily");
        private CoreValueChange CVC_imageFilePath = new CoreValueChange("image", true);
        private CoreValueChange CVC_height = new CoreValueChange("height");
        private CoreValueChange CVC_width = new CoreValueChange("width");

        private CoreButton CB_saveChanges = new CoreButton("save changes");


        public OptionsElement_ButtonData()
        {
            InitializeComponent();

            build();
        }

        public OptionsElement_ButtonData(ButtonData buttonData)
        {
            this.buttonData = buttonData;

            InitializeComponent();

            build();
        }

        private void build()
        {
            CB_saveChanges.button.Click += CB_saveChanges_Click;
            handler = new SharedLogic().GetDataHandler();

            if (buttonData == null)
            {
                if (handler != null)
                {
                    buttonData = handler.GetButtonData();
                }
            }

            CVC_cornerRadius.setText(buttonData.cornerRadius.ToString());
            CVC_thickness.setText(buttonData.thickness.ToString());
            CVC_fontSize.setText(buttonData.fontSize.ToString());
            CVC_fontFamily.setText(buttonData.fontFamily.ToString());
            CVC_imageFilePath.setText(buttonData.imageFilePath.ToString());
            CVC_height.setText(buttonData.height.ToString());
            CVC_width.setText(buttonData.width.ToString());

            wrapPanel.Children.Add(CVC_cornerRadius);
            wrapPanel.Children.Add(CVC_thickness);
            wrapPanel.Children.Add(CVC_fontSize);
            wrapPanel.Children.Add(CVC_fontFamily);
            wrapPanel.Children.Add(CVC_imageFilePath);
            wrapPanel.Children.Add(CVC_height);
            wrapPanel.Children.Add(CVC_width);

            wrapPanel.Children.Add(CB_saveChanges);

            content_border.Child = wrapPanel;
        }

        private void CB_saveChanges_Click(object sender, RoutedEventArgs e)
        {
            //ideas:
            //make new function, build in type check, either here, ore in CoreValueChange by passing in
            // an int type, check string using this function, and using get..()
            string splitter = CVC_cornerRadius.value;
            string[] split = splitter.Split(',');

            double a, b, c, d;

            a = double.Parse(split[0]);
            b = double.Parse(split[1]);
            c = double.Parse(split[2]);
            d = double.Parse(split[3]);

            buttonData.cornerRadius = new CornerRadius(a, b, c, d);

            splitter = CVC_thickness.value;
            split = splitter.Split(',');

            a = double.Parse(split[0]);
            b = double.Parse(split[1]);
            c = double.Parse(split[2]);
            d = double.Parse(split[3]);

            buttonData.thickness = new Thickness(a, b, c, d);
            buttonData.fontSize = int.Parse(CVC_fontSize.Value);
            buttonData.fontFamily = new System.Windows.Media.FontFamily(CVC_fontFamily.Value);
            buttonData.imageFilePath = CVC_imageFilePath.Value;
            buttonData.height = double.Parse(CVC_height.Value);
            buttonData.width = double.Parse(CVC_width.Value);

            handler.SetData(buttonData);
        }
    }
}
/*  END OF FILE
 * 
 * 
 */