/* Aiding Elements User Interface
 *      XML_Handler class
 * 
 * xml save and load functionality
 * 
 * init:        2023|11|30
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 * 
 * origin:      MyNote_2023_11_01
 */
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.AEUI_UserControls;
using AidingElementsUserInterface.Core.FlatShareCC_Data;
using AidingElementsUserInterface.Core.MyNote_Data;
using AidingElementsUserInterface.Elements;
using AidingElementsUserInterface.Elements.MyNote;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;
using static System.Windows.Forms.LinkLabel;
using Link = AidingElementsUserInterface.Elements.Link;

namespace AidingElementsUserInterface.Core.Auxiliaries
{
    internal class XML_Handler : IO_Handler
    {
        internal CoreData coreData_data { get; set; }
        internal FlatData flatShareCC_data { get; set; }
        internal MyNote myNote_element { get; set; }

        public XML_Handler()
        {

        }

        internal XML_Handler(FlatData flatShareCC)
        {
            flatShareCC_data = flatShareCC;
        }

        internal XML_Handler(MyNote myNote)
        {
            myNote_element = myNote;
        }

        private XmlNode? NodeCheck(XmlNode node, string SubNodeName)
        {
            XmlNode xmlNode = node.SelectSingleNode(SubNodeName);

            return xmlNode;
        }


        private XmlNode Content_save(XmlDocument xmlDocument, CoreContainer container)
        {
            XmlNode node_Content = xmlDocument.CreateElement("Content");

            XmlNode type = xmlDocument.CreateElement("Type");
            Type t = container.GetContainerData().GetElement().GetType();
            type.InnerText = t.Name;
            node_Content.AppendChild(type);

            XmlNode node_ContentData = xmlDocument.CreateElement("Data");
            XmlNode? aux_ContentData = null;

            ContentData? contentData = null;

            if (t == typeof(Elements.FileLink))
            {
                Elements.FileLink fileLink = (Elements.FileLink)container.GetContainerData().GetElement();
                contentData = fileLink.GetContentData();
            }

            if (t == typeof(Elements.Image))
            {
                Elements.Image image = (Elements.Image)container.GetContainerData().GetElement();
                contentData = image.GetContentData();
            }

            if (t == typeof(Elements.Link))
            {
                Elements.Link link = (Elements.Link)container.GetContainerData().GetElement();
                contentData = link.GetContentData();
            }



            aux_ContentData = saveContent(xmlDocument, node_ContentData, contentData);

            if (aux_ContentData != null)
            {
                node_Content.AppendChild(aux_ContentData);
            }

            return node_Content;
        }

        // Core loading and saving via XML
        #region Core
        internal XmlNode? saveContent(XmlDocument xmlDocument, XmlNode node, ContentData data)
        {
            if (data != null)
            {
                foreach (string key in data.GetValuesDictionary().Keys)
                {
                    XmlNode newNode = xmlDocument.CreateElement(key);
                    newNode.InnerText = data.GetValuesDictionary()[key].ToString();

                    node.AppendChild(newNode);
                }

                //XmlNode link = xmlDocument.CreateElement("Link");
                //link.InnerText = contentData.GetLink;
                //node_Container.AppendChild(link);

                //XmlNode linkText = xmlDocument.CreateElement("LinkText");
                //linkText.InnerText = contentData.GetLinkText;
                //node_Container.AppendChild(linkText);

                return node;
            }

            return null;
        }
        // ButtonData
        // + color linkData? OR replace color values in core linkData OR if colordata == null, use coredata
        #region ButtonData
        #region ButtonData loading
        internal CoreData? ButtonData_load()
        {
            SharedLogic logic = new SharedLogic();

            CoreData buttonData = new CoreData(true);
            XmlDocument xmlDocument = new XmlDocument();

            if (File.Exists(ButtonData_file))
            {
                xmlDocument.Load(ButtonData_file);
                XmlNode node = xmlDocument.SelectSingleNode("Core");

                if (node != null)
                {
                    XmlNode node_ButtonData = node.SelectSingleNode("ButtonData");

                    if (node_ButtonData != null)
                    {
                        XmlNode node_CoreData = node_ButtonData.SelectSingleNode("CoreData");

                        CoreData aux_data = loadCoreData(node_CoreData);

                        if (aux_data != null)
                        {
                            buttonData = aux_data;
                        }

                        return buttonData;
                    }
                }

                return null;
            }

            return null;
        }
        #endregion ButtonData loading

        #region ButtonData saving
        internal void save_ButtonData(CoreData buttonData)
        {
            if (buttonData != null)
            {
                XmlDocument xmlDocument = new XmlDocument();

                XmlNode node = xmlDocument.CreateElement("Core");
                xmlDocument.AppendChild(node);

                XmlNode node_ButtonData = xmlDocument.CreateElement("ButtonData");
                XmlNode node_CoreData = xmlDocument.CreateElement("CoreData");

                XmlNode? aux_node = saveCoreData(xmlDocument, node_CoreData, buttonData);

                if (aux_node != null)
                {
                    node_ButtonData.AppendChild(aux_node);
                }

                node.AppendChild(node_ButtonData);

                try
                {
                    xmlDocument.Save(ButtonData_file);
                }
                catch (Exception)
                {

                }
            }
        }


        internal void ButtonData_save()
        {
            CoreData buttonData = new SharedLogic().GetDataHandler().GetButtonData();

            save_ButtonData(buttonData);
        }
        #endregion ButtonData saving
        #endregion ButtonData


        // CanvasData
        // + color linkData? OR replace color values in core linkData OR if colordata == null, use coredata
        #region CanvasData
        #region CanvasData loading
        internal CanvasData? CanvasData_load()
        {
            CanvasData canvasData = new CanvasData(true);
            XmlDocument xmlDocument = new XmlDocument();

            if (File.Exists(CanvasData_file))
            {
                xmlDocument.Load(CanvasData_file);
                XmlNode node = xmlDocument.SelectSingleNode("Core");

                if (node != null)
                {
                    XmlNode node_CanvasData = node.SelectSingleNode("CanvasData");

                    if (node_CanvasData != null)
                    {
                        XmlNode node_CoreData = node_CanvasData.SelectSingleNode("CoreData");

                        CoreData aux_data = loadCoreData(node_CoreData);

                        if (aux_data != null)
                        {
                            canvasData.apply_CoreData(aux_data);
                        }

                        canvasData.canvasName = node_CanvasData.SelectSingleNode("canvasName").InnerText;

                        canvasData.grouping_displacement = Int32.Parse(node_CanvasData.SelectSingleNode("grouping_displacement").InnerText);


                        XmlNode? element_spacing = NodeCheck(node_CanvasData, "element_spacing");
                        if (element_spacing != null)
                        {
                            if (element_spacing.InnerText.Equals("-1"))
                            {
                                canvasData.element_spacing = GridLength.Auto;
                            }
                            else
                            {
                                canvasData.element_spacing = new GridLength(Double.Parse(element_spacing.InnerText));
                            }
                        }

                        return canvasData;
                    }
                }

                return null;
            }

            return null;
        }
        #endregion CanvasData loading

        #region CanvasData saving
        internal void CanvasData_save()
        {
           ObservableCollection<CoreCanvas> screens = new SharedLogic().GetMainWindow().CORE_CANVAS_SWITCH.Get_coreCanvasScreens;


            for (int i = 0; i < screens.Count; i++)
            {
                CanvasData canvasData = screens[i].getCanvasData();

                if (canvasData != null)
                {
                    XmlDocument xmlDocument = new XmlDocument();

                    XmlNode node = xmlDocument.CreateElement("Core");
                    xmlDocument.AppendChild(node);

                    XmlNode node_CanvasData = xmlDocument.CreateElement("CanvasData");
                    XmlNode node_CoreData = xmlDocument.CreateElement("CoreData");

                    XmlNode? aux_node = saveCoreData(xmlDocument, node_CoreData, canvasData);

                    if (aux_node != null)
                    {
                        node_CanvasData.AppendChild(aux_node);
                    }

                    XmlNode canvasName = xmlDocument.CreateElement("canvasName");
                    canvasName.InnerText = canvasData.canvasName;
                    node_CanvasData.AppendChild(canvasName);

                    XmlNode grouping_displacement = xmlDocument.CreateElement("grouping_displacement");
                    grouping_displacement.InnerText = canvasData.grouping_displacement.ToString();
                    node_CanvasData.AppendChild(grouping_displacement);

                    XmlNode node_element_spacing = xmlDocument.CreateElement("element_spacing");
                    node_element_spacing.InnerText = canvasData.element_spacing.ToString();
                    if (node_element_spacing.InnerText.Equals("Auto") || node_element_spacing.InnerText.Equals("auto"))
                    {
                        node_element_spacing.InnerText = "-1";
                    }
                    node_CanvasData.AppendChild(node_element_spacing);

                    node.AppendChild(node_CanvasData);

                    try
                    {
                        xmlDocument.Save(CanvasData_file);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
        #endregion CanvasData saving
        #endregion CanvasData

        // ContainerData
        #region ContainerData
        #region ContainerData loading
        private ContainerData? loadContainerData(XmlNode node)
        {
            ContainerData containerData = new ContainerData();
            SharedLogic logic = new SharedLogic();

            XmlNode? node_ContainerData = NodeCheck(node, "ContainerData");

            if (node_ContainerData != null)
            {
                XmlNode? node_CoreData = NodeCheck(node_ContainerData, "CoreData");

                CoreData? aux_data = loadCoreData(node_CoreData);
                if (aux_data != null)
                {
                    containerData = new ContainerData(aux_data);
                }

                XmlNode? containerLocation = NodeCheck(node_ContainerData, "CanvasName");
                if (containerLocation != null)
                {
                    containerData.CanvasName = containerLocation.InnerText;
                }

                XmlNode? ContainerDataFilename = NodeCheck(node_ContainerData, "ContainerDataFilename");
                if (ContainerDataFilename != null)
                {
                    containerData.ContainerDataFilename = ContainerDataFilename.InnerText;
                }

                XmlNode? z_position = NodeCheck(node_ContainerData, "z_position");
                if (z_position != null)
                {
                    containerData.z_position = Int32.Parse(z_position.InnerText);
                }
            }

            return containerData;
        }

        internal ObservableCollection<CoreContainer> Container_load()
        {
            ObservableCollection<CoreContainer> container_list = new ObservableCollection<CoreContainer>();

            foreach (string filename in scan_directory(ContainerData_xml_folder))
            {
                if (File.Exists(filename))
                {
                    XmlDocument xmlDocument = new XmlDocument();

                    xmlDocument.Load(filename);

                    XmlNode? node_Container = xmlDocument.SelectSingleNode("Container");


                    if (node_Container != null)
                    {
                        ContainerData containerData = loadContainerData(node_Container);

                        XmlNode node_Content = node_Container.SelectSingleNode("Content");

                        if (node_Content != null)
                        {
                            UserControl userControl = new UserControl();

                            XmlNode node_Type = node_Content.SelectSingleNode("Type");

                            if (node_Type != null)
                            {
                                Type? type = Type.GetType($"AidingElementsUserInterface.Elements.{node_Type.InnerText}, AidingElementsUserInterface");

                                if (type != null)
                                {
                                    XmlNode? node_Data = node_Content.SelectSingleNode("Data");

                                    if (node_Data != null)
                                    {
                                        //containerData = loadContentData(node_Data);
                                        userControl = ContentData_load(type, node_Data);
                                    }
                                    else
                                    {
                                        userControl = (UserControl)Activator.CreateInstance(type);
                                    }
                                }
                                else
                                {
                                    Type? type_in_folder = Type.GetType($"AidingElementsUserInterface.Elements.{node_Type.InnerText}.{node_Type.InnerText}, AidingElementsUserInterface");

                                    if (type_in_folder != null)
                                    {

                                        userControl = (UserControl)Activator.CreateInstance(type_in_folder);
                                    }
                                    else
                                    {
                                        Type? core_type = Type.GetType($"AidingElementsUserInterface.Core.AEUI_UserControls.{node_Type.InnerText}, AidingElementsUserInterface");

                                        if (core_type != null)
                                        {
                                            userControl = (UserControl)Activator.CreateInstance(core_type);
                                        }
                                    }
                                }
                            }

                            Point container_position;
                            XmlNode node_Position = node_Container.SelectSingleNode("Position");

                            if (node_Position != null)
                            {
                                XmlNode node_x = node_Position.SelectSingleNode("x");
                                XmlNode node_y = node_Position.SelectSingleNode("y");

                                if (node_x != null && node_y != null)
                                {
                                    container_position = new Point(
                                        Double.Parse(node_x.InnerText),
                                        Double.Parse(node_y.InnerText)
                                        );
                                }
                            }

                            if (containerData == null)
                            {
                                containerData = new ContainerData();
                            }

                            containerData.SetElement(userControl);

                            CoreContainer coreContainer = new CoreContainer(containerData);
                            coreContainer.setPosition(container_position);

                            container_list.Add(coreContainer);
                        }
                    }
                }
            }

            return container_list;
        }

        #endregion Container loading

        #region Container saving
        internal void Container_save(CoreContainer coreContainer, int counter = 0)
        {
            XmlDocument xmlDocument = new XmlDocument();
            CoreContainer container = coreContainer;

            if (coreContainer != null)
            {
                XmlNode node_ContainerData = ContainerData_save(xmlDocument, container.GetContainerData());

                XmlNode node = node_ContainerData;

                if (node != null)
                {
                    XmlNode node_CanvasName = xmlDocument.CreateElement("CanvasName");
                    node_CanvasName.InnerText = container.GetContainerData().CanvasName;
                    node.AppendChild(node_CanvasName);

                    XmlNode node_ContainerDataFilename = xmlDocument.CreateElement("ContainerDataFilename");
                    node_ContainerDataFilename.InnerText = $"{counter}.xml";
                    node.AppendChild(node_ContainerDataFilename);

                    XmlNode node_z_position = xmlDocument.CreateElement("z_position");
                    node_z_position.InnerText = container.GetContainerData().z_position.ToString();
                    node.AppendChild(node_z_position);

                    XmlNode node_Content = Content_save(xmlDocument, container);

                    if (node_Content != null)
                    {
                        node.AppendChild(node_Content);
                    }
                }

                XmlNode node_Position = xmlDocument.CreateElement("Position");

                XmlNode node_position_x = xmlDocument.CreateElement("x");
                node_position_x.InnerText = ((int)container.get_dragPoint().X).ToString();
                node_Position.AppendChild(node_position_x);

                XmlNode node_position_y = xmlDocument.CreateElement("y");
                node_position_y.InnerText = ((int)container.get_dragPoint().Y).ToString();
                node_Position.AppendChild(node_position_y);

                node.AppendChild(node_Position);

                try
                {
                    xmlDocument.Save($@".\{ContainerData_xml_folder}{counter}.xml");
                }
                catch (Exception)
                {

                }
            }
        }

        internal XmlNode? ContainerData_save(XmlDocument xmlDocument, ContainerData containerData)
        {
            if (containerData != null)
            {
                XmlNode node = xmlDocument.CreateElement("Container");
                xmlDocument.AppendChild(node);

                XmlNode node_ContainerData = xmlDocument.CreateElement("ContainerData");

                XmlNode node_CoreData = xmlDocument.CreateElement("CoreData");

                XmlNode? aux_node = saveCoreData(xmlDocument, node_CoreData, containerData.settings);

                if (aux_node != null)
                {
                    node_ContainerData.AppendChild(aux_node);
                }

                return node_ContainerData;
            }

            return null;
        }

        internal void ContainerDataTemplate_save()
        {
            XmlDocument xmlDocument = new XmlDocument();

            ContainerData containerData = new SharedLogic().GetDataHandler().GetContainerData();

            XmlNode node_ContainerData = ContainerData_save(xmlDocument, containerData);

            XmlNode node = node_ContainerData;

            if (node != null)
            {
                XmlNode node_CanvasName = xmlDocument.CreateElement("CONTAINER_DATA_TEMPLATE");
                node_CanvasName.InnerText = containerData.CanvasName;
                node.AppendChild(node_CanvasName);

                XmlNode node_ContainerDataFilename = xmlDocument.CreateElement("ContainerDataFilename");
                node_ContainerDataFilename.InnerText = ContainerData_file;
                node.AppendChild(node_ContainerDataFilename);

                XmlNode node_z_position = xmlDocument.CreateElement("z_position");
                node_z_position.InnerText = "0";
                node.AppendChild(node_z_position);

                node.AppendChild(node_ContainerData);

                XmlNode node_Position = xmlDocument.CreateElement("Position");

                XmlNode node_position_x = xmlDocument.CreateElement("x");
                node_position_x.InnerText = "120";
                node_Position.AppendChild(node_position_x);

                XmlNode node_position_y = xmlDocument.CreateElement("y");
                node_position_y.InnerText = "100";
                node_Position.AppendChild(node_position_y);

                node.AppendChild(node_Position);

                try
                {
                    xmlDocument.Save(ContainerData_file);
                }
                catch (Exception)
                {

                }
            }
        }
        #endregion ContainerData saving
        #endregion ContainerData

        // CoreData
        #region CoreData
        #region CoreData loading
        internal CoreData? CoreData_load()
        {
            SharedLogic logic = new SharedLogic();

            CoreData coreData = new CoreData(true);
            XmlDocument xmlDocument = new XmlDocument();

            if (File.Exists(CoreData_file))
            {
                xmlDocument.Load(CoreData_file);
                XmlNode node = xmlDocument.SelectSingleNode("Core");

                if (node != null)
                {
                    XmlNode node_CoreData = node.SelectSingleNode("CoreData");

                    if (node_CoreData != null)
                    {
                        CoreData aux_data = loadCoreData(node_CoreData);

                        if (aux_data != null)
                        {
                            coreData = aux_data;

                            return coreData;
                        }
                    }
                }

                return null;
            }

            return null;
        }


        private ColorData? loadColorData(XmlNode node, string ColorDataNode)
        {
            SharedLogic logic = new SharedLogic();

            ColorData colorData = new ColorData(true);


            if (node != null)
            {
                XmlNode? node_ColorDataNode = node.SelectSingleNode(ColorDataNode);


                XmlNode? brushtype = node_ColorDataNode.SelectSingleNode("brushtype");
                if (brushtype != null)
                {
                    colorData.brushtype = brushtype.InnerText;
                }

                XmlNode? gradiantEndPoint = node_ColorDataNode.SelectSingleNode("gradiantEndPoint");
                if (gradiantEndPoint != null)
                {
                    colorData.gradiantEndPoint = logic.ParsePoint(gradiantEndPoint.InnerText);
                }

                XmlNode? gradiantOrigin = node_ColorDataNode.SelectSingleNode("gradiantOrigin");
                if (gradiantOrigin != null)
                {
                    colorData.gradiantOrigin = logic.ParsePoint(gradiantOrigin.InnerText);
                }

                XmlNode? gradiantStartPoint = node_ColorDataNode.SelectSingleNode("gradiantStartPoint");
                if (gradiantStartPoint != null)
                {
                    colorData.gradiantStartPoint = logic.ParsePoint(gradiantStartPoint.InnerText);
                }

                XmlNode? color1_string = node_ColorDataNode.SelectSingleNode("color1_string");
                if (color1_string != null)
                {
                    colorData.color1_string = color1_string.InnerText;
                }

                XmlNode? color2_string = node_ColorDataNode.SelectSingleNode("color2_string");
                if (color2_string != null)
                {
                    colorData.color2_string = color2_string.InnerText;
                }

                XmlNode? color3_string = node_ColorDataNode.SelectSingleNode("color3_string");
                if (color3_string != null)
                {
                    colorData.color3_string = color3_string.InnerText;
                }

                XmlNode? color4_string = node_ColorDataNode.SelectSingleNode("color4_string");
                if (color4_string != null)
                {
                    colorData.color4_string = color4_string.InnerText;
                }
            }

            return colorData;
        }



        private CoreData? loadCoreData(XmlNode node)
        {
            SharedLogic logic = new SharedLogic();
            CoreData coreData = new CoreData(true);

            if (node != null)
            {
                coreData.imageIsBackground = bool.Parse(node.SelectSingleNode("imageIsBackground").InnerText);

                ColorData aux_background = loadColorData(node, "background");
                if (aux_background != null) { coreData.background = aux_background; }

                ColorData aux_borderbrush = loadColorData(node, "borderbrush");
                if (aux_borderbrush != null) { coreData.borderbrush = aux_borderbrush; }

                ColorData aux_foreground = loadColorData(node, "foreground");
                if (aux_foreground != null) { coreData.foreground = aux_foreground; }

                ColorData aux_highlight = loadColorData(node, "highlight");
                if (aux_highlight != null) { coreData.highlight = aux_highlight; }

                coreData.cornerRadius = logic.ParseCornerRadius(node.SelectSingleNode("cornerRadius").InnerText);

                coreData.thickness = logic.ParseThickness(node.SelectSingleNode("thickness").InnerText);

                coreData.fontSize = Int32.Parse(node.SelectSingleNode("fontSize").InnerText);
                coreData.fontFamily = new FontFamily(node.SelectSingleNode("fontFamily").InnerText);

                coreData.height = Double.Parse(node.SelectSingleNode("height").InnerText);
                coreData.width = Double.Parse(node.SelectSingleNode("width").InnerText);

                coreData.imageFilePath = node.SelectSingleNode("imageFilePath").InnerText;

                return coreData;
            }

            return null;
        }
        #endregion CoreData loading

        #region CoreData saving
        internal void CoreData_save()
        {
            coreData_data = new SharedLogic().GetDataHandler().GetCoreData();

            if (coreData_data != null)
            {
                XmlDocument xmlDocument = new XmlDocument();

                XmlNode node = xmlDocument.CreateElement("Core");
                xmlDocument.AppendChild(node);

                XmlNode node_CoreData = xmlDocument.CreateElement("CoreData");

                XmlNode? aux_node = saveCoreData(xmlDocument, node_CoreData, coreData_data);

                if (aux_node != null)
                {
                    node.AppendChild(aux_node);
                }

                try
                {
                    xmlDocument.Save(CoreData_file);
                }
                catch (Exception)
                {

                }
            }
        }

        internal XmlNode? saveColorData(XmlDocument xmlDocument, ColorData data, string ColorDataNode)
        {
            if (data != null)
            {
                XmlNode _ColorDataNode = xmlDocument.CreateElement(ColorDataNode);

                XmlNode brushtype = xmlDocument.CreateElement("brushtype");
                brushtype.InnerText = data.brushtype.ToString();
                _ColorDataNode.AppendChild(brushtype);

                XmlNode gradiantEndPoint = xmlDocument.CreateElement("gradiantEndPoint");
                gradiantEndPoint.InnerText = data.gradiantEndPoint.ToString();
                _ColorDataNode.AppendChild(gradiantEndPoint);

                XmlNode gradiantOrigin = xmlDocument.CreateElement("gradiantOrigin");
                gradiantOrigin.InnerText = data.gradiantOrigin.ToString();
                _ColorDataNode.AppendChild(gradiantOrigin);

                XmlNode gradiantStartPoint = xmlDocument.CreateElement("gradiantStartPoint");
                gradiantStartPoint.InnerText = data.gradiantStartPoint.ToString();
                _ColorDataNode.AppendChild(gradiantStartPoint);

                XmlNode color1_string = xmlDocument.CreateElement("color1_string");
                color1_string.InnerText = data.color1_string;
                _ColorDataNode.AppendChild(color1_string);

                XmlNode color2_string = xmlDocument.CreateElement("color2_string");
                color2_string.InnerText = data.color2_string.ToString();
                _ColorDataNode.AppendChild(color2_string);

                XmlNode color3_string = xmlDocument.CreateElement("color3_string");
                color3_string.InnerText = data.color3_string;
                _ColorDataNode.AppendChild(color3_string);

                XmlNode color4_string = xmlDocument.CreateElement("color4_string");
                color4_string.InnerText = data.color4_string;
                _ColorDataNode.AppendChild(color4_string);

                return _ColorDataNode;
            }

            return null;
        }


        internal XmlNode? saveCoreData(XmlDocument xmlDocument, XmlNode node, CoreData data)
        {
            if (data != null)
            {
                XmlNode imageIsBackground = xmlDocument.CreateElement("imageIsBackground");
                imageIsBackground.InnerText = data.imageIsBackground.ToString();
                node.AppendChild(imageIsBackground);

                XmlNode? aux_background = saveColorData(xmlDocument, data.background, "background");
                if (aux_background != null)
                {
                    node.AppendChild(aux_background);
                }

                XmlNode? aux_borderbrush = saveColorData(xmlDocument, data.borderbrush, "borderbrush");
                if (aux_borderbrush != null)
                {
                    node.AppendChild(aux_borderbrush);
                }

                XmlNode? aux_foreground = saveColorData(xmlDocument, data.foreground, "foreground");
                if (aux_foreground != null)
                {
                    node.AppendChild(aux_foreground);
                }

                XmlNode? aux_highlight = saveColorData(xmlDocument, data.highlight, "highlight");
                if (aux_highlight != null)
                {
                    node.AppendChild(aux_highlight);
                }

                XmlNode cornerRadius = xmlDocument.CreateElement("cornerRadius");
                cornerRadius.InnerText = data.cornerRadius.ToString();
                node.AppendChild(cornerRadius);

                XmlNode thickness = xmlDocument.CreateElement("thickness");
                thickness.InnerText = data.thickness.ToString();
                node.AppendChild(thickness);

                XmlNode fontSize = xmlDocument.CreateElement("fontSize");
                fontSize.InnerText = data.fontSize.ToString();
                node.AppendChild(fontSize);

                XmlNode fontFamily = xmlDocument.CreateElement("fontFamily");
                fontFamily.InnerText = data.fontFamily.ToString();
                node.AppendChild(fontFamily);

                XmlNode height = xmlDocument.CreateElement("height");
                height.InnerText = data.height.ToString();
                node.AppendChild(height);

                XmlNode width = xmlDocument.CreateElement("width");
                width.InnerText = data.width.ToString();
                node.AppendChild(width);

                XmlNode imageFilePath = xmlDocument.CreateElement("imageFilePath");
                imageFilePath.InnerText = data.imageFilePath;
                node.AppendChild(imageFilePath);


                return node;
            }

            return null;
        }
        #endregion CoreData saving
        #endregion CoreData

        #region LabelData
        #region LabelData loading
        internal CoreData? LabelData_load()
        {
            SharedLogic logic = new SharedLogic();

            CoreData buttonData = new CoreData(true);
            XmlDocument xmlDocument = new XmlDocument();

            if (File.Exists(ButtonData_file))
            {
                xmlDocument.Load(ButtonData_file);
                XmlNode node = xmlDocument.SelectSingleNode("Core");

                if (node != null)
                {
                    XmlNode node_ButtonData = node.SelectSingleNode("LabelData");

                    if (node_ButtonData != null)
                    {
                        XmlNode node_CoreData = node_ButtonData.SelectSingleNode("CoreData");

                        CoreData aux_data = loadCoreData(node_CoreData);

                        if (aux_data != null)
                        {
                            buttonData = aux_data;
                        }

                        return buttonData;
                    }
                }

                return null;
            }

            return null;
        }
        #endregion LabelData loading

        #region LabelData saving
        internal void save_LabelData(CoreData labelData)
        {
            if (labelData != null)
            {
                XmlDocument xmlDocument = new XmlDocument();

                XmlNode node = xmlDocument.CreateElement("Core");
                xmlDocument.AppendChild(node);

                XmlNode node_ButtonData = xmlDocument.CreateElement("LabelData");
                XmlNode node_CoreData = xmlDocument.CreateElement("CoreData");

                XmlNode? aux_node = saveCoreData(xmlDocument, node_CoreData, labelData);

                if (aux_node != null)
                {
                    node_ButtonData.AppendChild(aux_node);
                }

                node.AppendChild(node_ButtonData);

                try
                {
                    xmlDocument.Save(ButtonData_file);
                }
                catch (Exception)
                {

                }
            }
        }


        internal void LabelData_save()
        {
            CoreData labelData = new SharedLogic().GetDataHandler().GetLabelData();

            save_LabelData(labelData);
        }
        #endregion LabelData saving
        #endregion LabelData

        // LinkData
        #region LinkData
        #region LinkData loading
        private UserControl? ContentData_load(Type type, XmlNode node_ContentData)
        {
            ContentData contentData = loadContentData(node_ContentData);
            UserControl userControl = null;

            if (contentData != null)
            {
                if (type == typeof(FileLink))
                {
                    userControl = new FileLink(contentData);

                }

                if (type == typeof(Elements.Image))
                {
                    userControl = new Elements.Image(contentData);
                }

                if (type == typeof(Link))
                {
                    userControl = new Link(contentData);
                }
            }

            return userControl;
        }
        private ContentData? loadContentData(XmlNode node)
        {
            ContentData contentData = new ContentData();

            if (node != null && contentData != null)
            {
                foreach (XmlNode item in node.ChildNodes)
                {
                    contentData.AddValue(item.Name, item.InnerText);
                }

                return contentData;
            }

            return null;
        }
        private LinkData? loadLinkData(XmlNode node)
        {
            LinkData linkData = new LinkData();

            if (node != null)
            {
                XmlNode? link_node = node.SelectSingleNode("Link");
                XmlNode? linkText_node = node.SelectSingleNode("LinkText");

                if (link_node != null && linkText_node != null)
                {
                    string link = link_node.InnerText;
                    string linkText = linkText_node.InnerText;

                    if (link != null && linkText != null)
                    {
                        linkData.SetLink(link);
                        linkData.SetLinkText(linkText);
                    }
                }

                return linkData;
            }

            return null;
        }
        #endregion LinkData loading

        #region LinkData saving
        internal XmlNode? saveLinkData(XmlDocument xmlDocument, XmlNode node, LinkData data)
        {
            if (data != null)
            {
                XmlNode link = xmlDocument.CreateElement("Link");
                link.InnerText = data.GetLink;
                node.AppendChild(link);

                XmlNode linkText = xmlDocument.CreateElement("LinkText");
                linkText.InnerText = data.GetLinkText;
                node.AppendChild(linkText);

                return node;
            }

            return null;
        }
        #endregion LinkData saving
        #endregion LinkData

        // MainWindowData
        #region MainWindowData
        #region MainWindowData loading
        internal MainWindowData? MainWindowData_load()
        {
            SharedLogic logic = new SharedLogic();

            MainWindowData mainWindowData = new MainWindowData(true);
            XmlDocument xmlDocument = new XmlDocument();


            if (File.Exists(MainWindowData_file))
            {
                xmlDocument.Load(MainWindowData_file);
                XmlNode? node = xmlDocument.SelectSingleNode("Core");

                if (node != null)
                {
                    XmlNode? node_MainWindowData = node.SelectSingleNode("MainWindowData");

                    if (node_MainWindowData != null)
                    {
                        XmlNode? node_CoreData = node_MainWindowData.SelectSingleNode("CoreData");

                        if (node_CoreData != null)
                        {
                            CoreData? aux_data = loadCoreData(node_CoreData);

                            if (aux_data != null)
                            {
                                mainWindowData.apply_CoreData(aux_data);
                            }
                        }

                        XmlNode? node_initialPositionX = node_MainWindowData.SelectSingleNode("initialPositionX");
                        XmlNode? node_initialPositionY = node_MainWindowData.SelectSingleNode("initialPositionY");
                        if (node_initialPositionX != null && node_initialPositionY != null)
                        {
                            mainWindowData.initialPosition = new Point(
                                Int32.Parse(node_initialPositionX.InnerText),
                                Int32.Parse(node_initialPositionY.InnerText))
                                ;
                        }

                        XmlNode? node_language = node_MainWindowData.SelectSingleNode("language");
                        if (node_language != null)
                        {
                            mainWindowData.language = node_language.InnerText;
                        }

                        return mainWindowData;
                    }
                }

                return null;
            }

            return null;
        }

        #endregion MainWindowData loading

        #region MainWindowData saving
        internal void MainWindowData_save()
        {
            MainWindowData mainWindowData = new SharedLogic().GetDataHandler().GetMainWindowData();

            if (mainWindowData != null)
            {
                XmlDocument xmlDocument = new XmlDocument();

                XmlNode node = xmlDocument.CreateElement("Core");
                xmlDocument.AppendChild(node);

                XmlNode node_MainWindowData = xmlDocument.CreateElement("MainWindowData");

                XmlNode node_CoreData = xmlDocument.CreateElement("CoreData");

                XmlNode? aux_node = saveCoreData(xmlDocument, node_CoreData, mainWindowData);

                if (aux_node != null)
                {
                    node_MainWindowData.AppendChild(aux_node);
                }

                XmlNode initialPositionX = xmlDocument.CreateElement("initialPositionX");
                initialPositionX.InnerText = ((int)mainWindowData.initialPosition.X).ToString();
                node_MainWindowData.AppendChild(initialPositionX);

                XmlNode initialPositionY = xmlDocument.CreateElement("initialPositionY");
                initialPositionY.InnerText = ((int)mainWindowData.initialPosition.Y).ToString();
                node_MainWindowData.AppendChild(initialPositionY);

                XmlNode language = xmlDocument.CreateElement("language");
                language.InnerText = mainWindowData.language;
                node_MainWindowData.AppendChild(language);

                node.AppendChild(node_MainWindowData);

                try
                {
                    xmlDocument.Save(MainWindowData_file);
                }
                catch (Exception)
                {

                }
            }
        }
        #endregion MainWindowData saving
        #endregion MainWindowData

        // TextBoxData
        #region TextBoxData
        #region TextBoxData loading
        internal CoreData? TextBoxData_load()
        {
            SharedLogic logic = new SharedLogic();

            CoreData textBoxData = new CoreData(true);
            XmlDocument xmlDocument = new XmlDocument();


            if (File.Exists(TextBoxData_file))
            {
                xmlDocument.Load(TextBoxData_file);
                XmlNode node = xmlDocument.SelectSingleNode("Core");

                if (node != null)
                {
                    XmlNode node_TextBoxData = node.SelectSingleNode("TextBoxData");

                    if (node_TextBoxData != null)
                    {
                        XmlNode node_CoreData = node_TextBoxData.SelectSingleNode("CoreData");

                        if (node_CoreData != null)
                        {
                            CoreData aux_data = loadCoreData(node_CoreData);

                            if (aux_data != null)
                            {
                                textBoxData.apply_CoreData(aux_data);
                            }
                        }

                        return textBoxData;
                    }
                }

                return null;
            }

            return null;
        }

        #endregion TextBoxData loading

        #region TextBoxData saving
        internal void TextBoxData_save()
        {
            CoreData textBoxData = new SharedLogic().GetDataHandler().GetTextBoxData();

            if (textBoxData != null)
            {
                XmlDocument xmlDocument = new XmlDocument();

                XmlNode node = xmlDocument.CreateElement("Core");
                xmlDocument.AppendChild(node);

                XmlNode node_TextBoxData = xmlDocument.CreateElement("TextBoxData");
                XmlNode node_CoreData = xmlDocument.CreateElement("CoreData");

                XmlNode? aux_node = saveCoreData(xmlDocument, node_CoreData, textBoxData);

                if (aux_node != null)
                {
                    node_TextBoxData.AppendChild(aux_node);
                }

                node.AppendChild(node_TextBoxData);

                try
                {
                    xmlDocument.Save(TextBoxData_file);
                }
                catch (Exception)
                {

                }
            }

        }
        #endregion TextBoxData saving
        #endregion TextBoxData

#endregion Core


        // FlatShareCostCalculator loading and saving via XML
        #region FlatShareCC
        #region FlatShareCC loading
        internal ObservableCollection<CostUpdateData>? FlatShareCC_load_cost_update()
        {
            ObservableCollection<CostUpdateData> data_list = new ObservableCollection<CostUpdateData>();
            XmlDocument xmlDocument = new XmlDocument();

            foreach (string filename in scan_directory(FlatShareCC_xml_folder))
            {

                if (filename != FlatShareCC_data_file)
                {
                    CostUpdateData costUpdateData = new CostUpdateData();

                    xmlDocument.Load(filename);

                    XmlNode SharedApartmentCalculator = xmlDocument.SelectSingleNode("FlatShareCostCalculator");
                    XmlNode cost_update_node = SharedApartmentCalculator.SelectSingleNode("cost_update_data");

                    if (SharedApartmentCalculator != null && cost_update_node != null)
                    {
                        // string values
                        XmlNode cause = cost_update_node.SelectSingleNode("cause");
                        costUpdateData.cause = cause.InnerText;

                        XmlNode based_on_AB = cost_update_node.SelectSingleNode("based_on_AB");
                        costUpdateData.based_on_AB = based_on_AB.InnerText;

                        // bool values
                        XmlNode is_annual_billing = cost_update_node.SelectSingleNode("is_annual_billing");
                        costUpdateData.isAnnualBilling = Boolean.Parse(is_annual_billing.InnerText);

                        XmlNode is_rent_change = cost_update_node.SelectSingleNode("is_rent_change");
                        costUpdateData.isRentChange = Boolean.Parse(is_rent_change.InnerText);

                        // date values
                        XmlNode cu_received_node = cost_update_node.SelectSingleNode("cost_update_received");
                        costUpdateData.cost_update_received = DateTime.Parse(cu_received_node.InnerText);

                        XmlNode period_start_node = cost_update_node.SelectSingleNode("period_start");
                        costUpdateData.period_start = DateTime.Parse(period_start_node.InnerText);

                        XmlNode period_end_node = cost_update_node.SelectSingleNode("period_end");
                        costUpdateData.period_end = DateTime.Parse(period_end_node.InnerText);

                        // double values
                        XmlNode initial_cold_rent = cost_update_node.SelectSingleNode("initial_cold_rent");
                        costUpdateData.initial_cold_rent = Double.Parse(initial_cold_rent.InnerText);

                        XmlNode initial_extra_costs_advance = cost_update_node.SelectSingleNode("initial_extra_costs_advance");
                        costUpdateData.initial_extra_costs_advance = Double.Parse(initial_extra_costs_advance.InnerText);

                        XmlNode cold_rent = cost_update_node.SelectSingleNode("cold_rent");
                        costUpdateData.cold_rent = Double.Parse(cold_rent.InnerText);

                        XmlNode extra_costs_advance = cost_update_node.SelectSingleNode("extra_costs_advance");
                        costUpdateData.extra_costs_advance = Double.Parse(extra_costs_advance.InnerText);

                        XmlNode annual_costs = cost_update_node.SelectSingleNode("annual_costs");
                        costUpdateData.annual_costs = Double.Parse(annual_costs.InnerText);

                        XmlNode extra_costs_shared = cost_update_node.SelectSingleNode("extra_costs_shared");
                        costUpdateData.extra_costs_shared = Double.Parse(extra_costs_shared.InnerText);

                        XmlNode extra_costs_heating = cost_update_node.SelectSingleNode("extra_costs_heating");
                        costUpdateData.extra_costs_heating = Double.Parse(extra_costs_heating.InnerText);


                        XmlNode heating_units_usage = cost_update_node.SelectSingleNode("heating_units_usage");
                        costUpdateData.heating_units_usage = Double.Parse(heating_units_usage.InnerText);

                        XmlNode heating_units_shared = cost_update_node.SelectSingleNode("heating_units_shared");
                        costUpdateData.heating_units_shared = Double.Parse(heating_units_shared.InnerText);


                        XmlNode rooms = cost_update_node.SelectSingleNode("rooms");
                        XmlNodeList room_nodes = rooms.SelectNodes("room");

                        int counter = 0;

                        foreach (XmlNode room_node in room_nodes)
                        {
                            BillingPeriodData room = costUpdateData.roomConsumptionValues[counter];

                            XmlNode room_heating_units_usage = room_node.SelectSingleNode("room_heating_units_usage");
                            room.heating_units_usage = Double.Parse(room_heating_units_usage.InnerText);

                            XmlNode room_payments = room_node.SelectSingleNode("room_payments");

                            if (room_payments != null)
                            {
                                XmlNodeList payed = room_payments.SelectNodes("payment");

                                foreach (XmlNode payment_node in payed)
                                {
                                    double value = Double.Parse(payment_node.InnerText);
                                    room.monthly_payments.Add(value);
                                }
                            }

                            costUpdateData.roomConsumptionValues[counter] = room;

                            counter++;
                        }

                        data_list.Add(costUpdateData);
                    }
                }
            }

            return data_list;
        }


        internal ObservableCollection<object>? FlatShareCC_load_data()
        {
            ObservableCollection<object> data_list = new ObservableCollection<object>();
            XmlDocument xmlDocument = new XmlDocument();

            FlatData flatData = new FlatData();
            FlatCosts flatCosts = new FlatCosts();

            data_list.Add(flatData);
            data_list.Add(flatCosts);

            //MessageBox.Show(File.Exists(FlatShareCC_data_file).ToString());

            if (File.Exists(FlatShareCC_data_file))
            {
                xmlDocument.Load(FlatShareCC_data_file);

                XmlNode SharedApartmentCalculator = xmlDocument.SelectSingleNode("FlatShareCostCalculator");
                XmlNode initial_contract = SharedApartmentCalculator.SelectSingleNode("linkData");

                if (SharedApartmentCalculator != null && initial_contract != null)
                {
                    data_list.Clear();

                    XmlNode living_space = initial_contract.SelectSingleNode("flat_space");
                    flatData.flat_space = Double.Parse(living_space.InnerText);

                    XmlNode cold_rent = initial_contract.SelectSingleNode("cold_rent");
                    flatCosts.cold_rent = Double.Parse(cold_rent.InnerText);

                    XmlNode extra_costs = initial_contract.SelectSingleNode("extra_costs_advance");
                    flatCosts.extra_costs_advance = Double.Parse(extra_costs.InnerText);

                    XmlNode rooms = initial_contract.SelectSingleNode("rooms");
                    XmlNodeList room_nodes = rooms.SelectNodes("room");

                    foreach (XmlNode room_node in room_nodes)
                    {
                        RoomData room = new RoomData();

                        XmlNode room_id = room_node.SelectSingleNode("id");
                        room.id = Int32.Parse(room_id.InnerText);

                        XmlNode room_name = room_node.SelectSingleNode("name");
                        room.name = room_name.InnerText;

                        XmlNode room_area = room_node.SelectSingleNode("area");
                        room.area = Double.Parse(room_area.InnerText);

                        flatData.rooms.Add(room);
                    }

                    data_list.Add(flatData);
                    data_list.Add(flatCosts);

                    return data_list;
                }

                return data_list;
            }

            return null;
        }
        #endregion FlatShareCC loading

        #region FlatShareCC saving
        internal void FlatShareCC_save_cost_update(CostUpdateData costUpdateData)
        {
            XmlDocument xmlDocument = new XmlDocument();

            XmlNode node = xmlDocument.CreateElement("FlatShareCostCalculator");
            xmlDocument.AppendChild(node);

            XmlNode data = xmlDocument.CreateElement("cost_update_data");

            // string values
            XmlNode cause = xmlDocument.CreateElement("cause");
            cause.InnerText = costUpdateData.cause.ToString();
            data.AppendChild(cause);

            XmlNode based_on_AB = xmlDocument.CreateElement("based_on_AB");
            based_on_AB.InnerText = costUpdateData.based_on_AB.ToString();
            data.AppendChild(based_on_AB);

            // bool values
            XmlNode is_annual_billing = xmlDocument.CreateElement("is_annual_billing");
            is_annual_billing.InnerText = costUpdateData.isAnnualBilling.ToString();
            data.AppendChild(is_annual_billing);

            XmlNode is_rent_change = xmlDocument.CreateElement("is_rent_change");
            is_rent_change.InnerText = costUpdateData.isRentChange.ToString();
            data.AppendChild(is_rent_change);

            // date values
            XmlNode cost_update_received = xmlDocument.CreateElement("cost_update_received");
            cost_update_received.InnerText = costUpdateData.cost_update_received.ToString("d");
            data.AppendChild(cost_update_received);

            XmlNode period_start = xmlDocument.CreateElement("period_start");
            period_start.InnerText = costUpdateData.period_start.ToString("d");
            data.AppendChild(period_start);

            XmlNode period_end = xmlDocument.CreateElement("period_end");
            period_end.InnerText = costUpdateData.period_end.ToString("d");
            data.AppendChild(period_end);

            // double values   
            XmlNode initial_cold_rent = xmlDocument.CreateElement("initial_cold_rent");
            initial_cold_rent.InnerText = costUpdateData.initial_cold_rent.ToString();
            data.AppendChild(initial_cold_rent);

            XmlNode initial_extra_costs_advance = xmlDocument.CreateElement("initial_extra_costs_advance");
            initial_extra_costs_advance.InnerText = costUpdateData.initial_extra_costs_advance.ToString();
            data.AppendChild(initial_extra_costs_advance);

            XmlNode cold_rent = xmlDocument.CreateElement("cold_rent");
            cold_rent.InnerText = costUpdateData.cold_rent.ToString();
            data.AppendChild(cold_rent);

            XmlNode extra_costs_advance = xmlDocument.CreateElement("extra_costs_advance");
            extra_costs_advance.InnerText = costUpdateData.extra_costs_advance.ToString();
            data.AppendChild(extra_costs_advance);

            XmlNode annual_costs = xmlDocument.CreateElement("annual_costs");
            annual_costs.InnerText = costUpdateData.annual_costs.ToString();
            data.AppendChild(annual_costs);

            XmlNode extra_costs_shared = xmlDocument.CreateElement("extra_costs_shared");
            extra_costs_shared.InnerText = costUpdateData.extra_costs_shared.ToString();
            data.AppendChild(extra_costs_shared);

            XmlNode extra_costs_heating = xmlDocument.CreateElement("extra_costs_heating");
            extra_costs_heating.InnerText = costUpdateData.extra_costs_heating.ToString();
            data.AppendChild(extra_costs_heating);

            XmlNode heating_units_usage = xmlDocument.CreateElement("heating_units_usage");
            heating_units_usage.InnerText = costUpdateData.heating_units_usage.ToString();
            data.AppendChild(heating_units_usage);

            XmlNode heating_units_shared = xmlDocument.CreateElement("heating_units_shared");
            heating_units_shared.InnerText = costUpdateData.heating_units_shared.ToString();
            data.AppendChild(heating_units_shared);

            XmlNode rooms = xmlDocument.CreateElement("rooms");

            foreach (BillingPeriodData room in costUpdateData.roomConsumptionValues)
            {
                XmlNode room_node = xmlDocument.CreateElement("room");

                XmlNode room_id = xmlDocument.CreateElement("id");
                room_id.InnerText = room.room.id.ToString();
                room_node.AppendChild(room_id);

                XmlNode room_name = xmlDocument.CreateElement("name");
                room_name.InnerText = room.room.name.ToString();
                room_node.AppendChild(room_name);

                XmlNode room_area = xmlDocument.CreateElement("area");
                room_area.InnerText = room.room.area.ToString();
                room_node.AppendChild(room_area);

                XmlNode room_heating_units_usage = xmlDocument.CreateElement("room_heating_units_usage");
                room_heating_units_usage.InnerText = room.heating_units_usage.ToString();
                room_node.AppendChild(room_heating_units_usage);

                XmlNode room_payments = xmlDocument.CreateElement("room_payments");

                foreach (double payment in room.monthly_payments)
                {
                    XmlNode payed = xmlDocument.CreateElement("payment");
                    payed.InnerText = payment.ToString();
                    room_payments.AppendChild(payed);
                }
                room_node.AppendChild(room_payments);


                rooms.AppendChild(room_node);
            }
            data.AppendChild(rooms);

            node.AppendChild(data);

            xmlDocument.Save(
                $@".\linkData\{costUpdateData.cost_update_received.Year}_" +
                $@"{costUpdateData.cost_update_received.Month}_" +
                $@"{costUpdateData.cost_update_received.Day}_" +
                $@"{costUpdateData.cause}.xml"
                );
        }


        internal void FlatShareCC_save_data(FlatData flatData, FlatCosts flatCosts)
        {
            XmlDocument xmlDocument = new XmlDocument();

            XmlNode node = xmlDocument.CreateElement("FlatShareCostCalculator");
            xmlDocument.AppendChild(node);

            XmlNode data = xmlDocument.CreateElement("linkData");

            XmlNode living_space = xmlDocument.CreateElement("flat_space");
            living_space.InnerText = flatData.flat_space.ToString();
            data.AppendChild(living_space);

            XmlNode cold_costs = xmlDocument.CreateElement("cold_rent");
            cold_costs.InnerText = flatCosts.cold_rent.ToString();
            data.AppendChild(cold_costs);

            XmlNode extra_costs = xmlDocument.CreateElement("extra_costs_advance");
            extra_costs.InnerText = flatCosts.extra_costs_advance.ToString();
            data.AppendChild(extra_costs);

            XmlNode rooms = xmlDocument.CreateElement("rooms");

            foreach (RoomData room in flatData.rooms)
            {
                XmlNode room_node = xmlDocument.CreateElement("room");

                XmlNode room_id = xmlDocument.CreateElement("id");
                room_id.InnerText = room.id.ToString();
                room_node.AppendChild(room_id);

                XmlNode room_name = xmlDocument.CreateElement("name");
                room_name.InnerText = room.name.ToString();
                room_node.AppendChild(room_name);

                XmlNode room_area = xmlDocument.CreateElement("area");
                room_area.InnerText = room.area.ToString();
                room_node.AppendChild(room_area);

                rooms.AppendChild(room_node);
            }
            data.AppendChild(rooms);

            node.AppendChild(data);

            xmlDocument.Save(FlatShareCC_data_file);
        }
        #endregion FlatShareCC saving
        #endregion FlatShareCC

        // MyNote loading and saving via XML
        #region MyNote
        #region MyNote loading
        internal StringBuilder MyNote_load_history()
        {
            StringBuilder history = new StringBuilder();
            XmlDocument xmlDocument = new XmlDocument();

            if (File.Exists(MyNote_history_file))
            {
                xmlDocument.Load(MyNote_history_file);
                XmlNode node = xmlDocument.SelectSingleNode("MyNote_History");
                history.Append(node["history"].InnerText);
            }

            return history;
        }

        internal NoteData MyNote_load_log()
        {
            NoteData data = new NoteData();
            XmlDocument xmlDocument = new XmlDocument();

            if (File.Exists(MyNote_log_file))
            {
                xmlDocument.Load(MyNote_log_file);

                XmlNode node = xmlDocument.SelectSingleNode("MyNote_Protocol");
                data.dateTime = DateTime.Parse(node["time"].InnerText);

                node = xmlDocument.SelectSingleNode("MyNote_Protocol");
                data.title = node["title"].InnerText;

                node = xmlDocument.SelectSingleNode("MyNote_Protocol");
                data.content = new StringBuilder(node["content"].InnerText);
            }
            else
            {
                data.content = new StringBuilder($"{DateTime.Now}\n{data.title}");
            }

            return data;
        }

        internal ObservableCollection<NoteData> MyNote_load_notes()
        {
            ObservableCollection<NoteData> notes = new ObservableCollection<NoteData>();

            foreach (string file in scan_directory(MyNote_notes_folder))
            {
                NoteData data = new NoteData();
                XmlDocument xmlDocument = new XmlDocument();

                xmlDocument.Load(file);

                XmlNode node = xmlDocument.SelectSingleNode("MyNote_Notes");
                data.id = Int32.Parse(node["id"].InnerText);
                data.dateTime = DateTime.Parse(node["time"].InnerText);
                data.title = node["title"].InnerText;
                data.content = new StringBuilder(node["content"].InnerText);

                notes.Add(data);
            }

            return notes;
        }

        internal ObservableCollection<MatrixElement> MyNote_load_matrix_elements()
        {
            ObservableCollection<MatrixElement> elements = new ObservableCollection<MatrixElement>();

            foreach (string file in scan_directory(MyNote_notes_matrix_folder))
            {
                MatrixElement mE = new MatrixElement(myNote_element);

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(file);

                XmlNode node = xmlDocument.SelectSingleNode("MyNote_Notes_Matrix_Element");

                XmlNode me_Id = node.SelectSingleNode("id");
                mE.id = Int32.Parse(me_Id.InnerText);

                XmlNodeList notes = node.SelectNodes("note");

                foreach (XmlNode note_node in notes)
                {
                    NoteData data = new NoteData();
                    Note note = new Note(myNote_element);

                    data.id = Int32.Parse(note_node["id"].InnerText);
                    data.dateTime = DateTime.Parse(note_node["time"].InnerText);
                    data.title = note_node["title"].InnerText;
                    data.content = new StringBuilder(note_node["content"].InnerText);

                    note.loadNoteData(data);

                    mE.insert(note);
                }

                elements.Add(mE);
            }

            return elements;
        }
        #endregion MyNote loading

        #region MyNote saving
        internal void MyNote_save_history(StringBuilder history)
        {
            XmlDocument xmlDocument = new XmlDocument();

            XmlNode node = xmlDocument.CreateElement("MyNote_History");
            xmlDocument.AppendChild(node);

            XmlNode time = xmlDocument.CreateElement("history");
            time.InnerText = history.ToString();
            node.AppendChild(time);

            try
            {
                xmlDocument.Save(MyNote_history_file);
            }
            catch (Exception)
            {

            }

        }

        internal void MyNote_save_log(NoteData data)
        {
            XmlDocument xmlDocument = new XmlDocument();

            XmlNode protocol = xmlDocument.CreateElement("MyNote_Protocol");
            xmlDocument.AppendChild(protocol);

            XmlNode time = xmlDocument.CreateElement("time");
            time.InnerText = data.dateTime.ToString();
            protocol.AppendChild(time);

            XmlNode title = xmlDocument.CreateElement("title");
            title.InnerText = data.title.ToString();
            protocol.AppendChild(title);

            XmlNode content = xmlDocument.CreateElement("content");
            content.InnerText = data.content.ToString();
            protocol.AppendChild(content);

            try
            {
                xmlDocument.Save(MyNote_log_file);
            }
            catch (Exception)
            {

            }


        }

        internal void MyNote_save_notes(ObservableCollection<Note> notes)
        {
            delete_files(MyNote_notes_folder);

            foreach (Note note in notes)
            {
                XmlDocument xmlDocument = new XmlDocument();

                XmlNode protocol = xmlDocument.CreateElement("MyNote_Notes");
                xmlDocument.AppendChild(protocol);

                XmlNode id = xmlDocument.CreateElement("id");
                id.InnerText = note.getNoteData().id.ToString();
                protocol.AppendChild(id);

                XmlNode time = xmlDocument.CreateElement("time");
                time.InnerText = note.getNoteData().dateTime.ToString();
                protocol.AppendChild(time);

                XmlNode title = xmlDocument.CreateElement("title");
                title.InnerText = note.getNoteData().title.ToString();
                protocol.AppendChild(title);

                XmlNode content = xmlDocument.CreateElement("content");
                content.InnerText = note.getNoteData().content.ToString();
                protocol.AppendChild(content);

                try
                {
                    xmlDocument.Save(@$"{MyNote_notes_folder}\{note.getNoteData().id}.xml");
                }
                catch (Exception)
                {

                }



            }
        }

        internal void MyNote_save_MatrixElement(MatrixElement mE)
        {
            int i = 0;

            XmlDocument XD_matrix_element = new XmlDocument();

            XmlNode root = XD_matrix_element.CreateElement("MyNote_Notes_Matrix_Element");
            XD_matrix_element.AppendChild(root);

            XmlNode id = XD_matrix_element.CreateElement("id");
            id.InnerText = mE.id.ToString();
            root.AppendChild(id);

            foreach (Note note in mE.extract())
            {
                XmlNode notes_node = XD_matrix_element.CreateElement("note");

                XmlNode note_id = XD_matrix_element.CreateElement("id");
                note_id.InnerText = i.ToString();
                notes_node.AppendChild(note_id);

                XmlNode time = XD_matrix_element.CreateElement("time");
                time.InnerText = note.getNoteData().dateTime.ToString();
                notes_node.AppendChild(time);

                XmlNode title = XD_matrix_element.CreateElement("title");
                title.InnerText = note.getNoteData().title.ToString();
                notes_node.AppendChild(title);

                XmlNode content = XD_matrix_element.CreateElement("content");
                content.InnerText = note.getNoteData().content.ToString();
                notes_node.AppendChild(content);

                i++;

                root.AppendChild(notes_node);
            }


            try
            {
                XD_matrix_element.Save(@$"{MyNote_notes_matrix_folder}\{mE.id}.xml");
            }
            catch (Exception)
            {

            }


        }
        #endregion MyNote saving
        #endregion MyNote
    }
}
/*  END OF FILE
 * 
 * 
 */