﻿using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.AEUI_UserControls;
using AidingElementsUserInterface.Core.Auxiliaries;
using AidingElementsUserInterface.Core.MyNote_Data;
using AidingElementsUserInterface.Elements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;

namespace AidingElementsUserInterface.Core.AEUI_Logic
{
    internal class UserSpace_xml : IO_Handler
    {
        public UserSpace_xml()
        {

        }

        #region CanvasData loading
        internal CanvasData? CanvasData_load(string path)
        {
            if (File.Exists(@$"{path}"))
            {
                XmlDocument xmlDocument = new XmlDocument();
                CanvasData canvasData = new CanvasData(true);

                xmlDocument.Load(path);

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

                        canvasData.canvasID = Int32.Parse(node_CanvasData.SelectSingleNode("canvasID").InnerText);

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
                    }
                }

                return canvasData;
            }

            return null;
        }

        #endregion CanvasData loading

        #region CanvasData saving
        internal void CanvasData_save(CoreCanvas userScreen, int id)
        {
            CanvasData canvasData = userScreen.getCanvasData();
            LevelSystem levelSystem = userScreen.GetLevelSystem();

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

                XmlNode canvasID = xmlDocument.CreateElement("canvasID");
                canvasID.InnerText = canvasData.canvasID.ToString();
                node_CanvasData.AppendChild(canvasID);

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

                //MessageBox.Show(canvasData.imageFilePath);

                check_path($@"{UserSpace_folder}screen_{id}");

                xmlDocument.Save($@"{UserSpace_folder}screen_{id}\{CanvasData_file}");

                try
                {
                }
                catch (Exception)
                {

                }
            }
        }
        #endregion CanvasData saving

        // ColorData
        #region ColorData
        // ColorData loading
        #region ColorData loading
        private ColorData? loadColorData(XmlNode node, string ColorDataNode)
        {
            SharedLogic logic = new SharedLogic();

            ColorData colorData = new ColorData(true);

            if (node != null)
            {
                XmlNode? node_ColorDataNode = node.SelectSingleNode(ColorDataNode);

                if (node_ColorDataNode != null)
                {
                    XmlNode? brushtype = node_ColorDataNode.SelectSingleNode("brushtype");
                    if (brushtype != null)
                    {
                        colorData.brushtype = brushtype.InnerText;
                    }

                    XmlNode? brushpath = node_ColorDataNode.SelectSingleNode("brushpath");
                    if (brushpath != null)
                    {
                        colorData.brushpath = brushpath.InnerText;
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

                // his name and mind are quite stupid.
                //MessageBox.Show(node_ColorDataNode.InnerXml);
                //MessageBox.Show(node_ColorDataNode.InnerText);
                //MessageBox.Show(node_ColorDataNode.OuterXml);
                //MessageBox.Show(node_ColorDataNode.Name);
                //MessageBox.Show(node_ColorDataNode.LocalName);
                //MessageBox.Show(node_ColorDataNode.Value);
                //MessageBox.Show(node_ColorDataNode.ParentNode.Name);

                //MessageBox.Show(node_ColorDataNode.OuterXml + "\n" + node.OuterXml);

            }

            return colorData;
        }
        #endregion ColorData loading

        // ColorData saving
        #region ColorData saving
        internal XmlNode? saveColorData(XmlDocument xmlDocument, ColorData data, string ColorDataNode)
        {
            if (data != null)
            {
                XmlNode _ColorDataNode = xmlDocument.CreateElement(ColorDataNode);

                XmlNode brushtype = xmlDocument.CreateElement("brushtype");
                brushtype.InnerText = data.brushtype.ToString();
                _ColorDataNode.AppendChild(brushtype);

                XmlNode brushpath = xmlDocument.CreateElement("brushpath");
                brushpath.InnerText = data.brushpath.ToString();
                _ColorDataNode.AppendChild(brushpath);

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
        #endregion ColorData saving
        #endregion ColorData

        // Container
        #region Container
        #region Container loading
        internal ObservableCollection<CoreContainer> Container_load(int i)
        {
            ObservableCollection<CoreContainer> container_list = new ObservableCollection<CoreContainer>();

            if (i < 11 && i > 0)
            {
                foreach (string filename in scan_directory($"{UserSpace_folder}screen_{i}\\Container\\"))
                {
                    if (File.Exists(filename))
                    {
                        XmlDocument xmlDocument = new XmlDocument();

                        xmlDocument.Load(filename);

                        XmlNode? node_Container = xmlDocument.SelectSingleNode("ContainerData");

                        if (node_Container != null)
                        {
                            ContainerData auxData = loadContainerData(node_Container, i);

                            ContainerData containerData = auxData;

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

                                containerData.SetElement(userControl);

                                CoreContainer coreContainer = new CoreContainer(containerData, userControl);
                                coreContainer.setPosition(container_position);

                                container_list.Add(coreContainer);
                            }
                        }
                    }
                }
            }

            return container_list;
        }
        #endregion Container loading

        #region Container saving
        internal void Container_save(CoreContainer coreContainer, int counter, int canvasID)
        {
            XmlDocument xmlDocument = new XmlDocument();
            CoreContainer container = coreContainer;

            if (coreContainer != null)
            {
                XmlNode node_ContainerData = ContainerData_save(xmlDocument, container.GetContainerData());

                xmlDocument.AppendChild(node_ContainerData);

                XmlNode node = node_ContainerData;

                if (node != null)
                {
                    XmlNode node_CanvasName = xmlDocument.CreateElement("CanvasID");
                    node_CanvasName.InnerText = container.GetContainerData().CanvasID.ToString();
                    node.AppendChild(node_CanvasName);

                    XmlNode node_ContainerDataFilename = xmlDocument.CreateElement("ContainerDataFilename");
                    node_ContainerDataFilename.InnerText = $"{canvasID}_{counter}.xml";
                    node.AppendChild(node_ContainerDataFilename);

                    XmlNode node_z_position = xmlDocument.CreateElement("level");
                    node_z_position.InnerText = container.GetContainerData().level.ToString();
                    node.AppendChild(node_z_position);

                    XmlNode node_Content = Content_save(xmlDocument, container);

                    if (node_Content != null)
                    {
                        node.AppendChild(node_Content);
                    }
                }

                XmlNode node_Position = xmlDocument.CreateElement("Position");

                XmlNode node_position_x = xmlDocument.CreateElement("x");

                int x = (int)container.get_dragPoint().X;
                int y = (int)container.get_dragPoint().Y;

                if (x < 0)
                {
                    x *= -1;
                }
                if (y < 0)
                {
                    y *= -1;
                }

                node_position_x.InnerText = x.ToString();
                node_Position.AppendChild(node_position_x);

                XmlNode node_position_y = xmlDocument.CreateElement("y");
                node_position_y.InnerText = y.ToString();
                node_Position.AppendChild(node_position_y);

                node.AppendChild(node_Position);

                try
                {
                    xmlDocument.Save(
                        $@"{UserSpace_folder}screen_{canvasID}\Container\{canvasID}_{counter}.xml");
                }
                catch (Exception)
                {
                    //MessageBox.Show($@"{UserSpace_folder}screen_{canvasID}\Container\{canvasID}_{counter}.xml");
                }
            }
        }
        internal XmlNode? ContainerData_save(XmlDocument xmlDocument, ContainerData containerData)
        {
            if (containerData != null)
            {
                XmlNode node_container = xmlDocument.CreateElement("Container");

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

        #endregion Container saving
        #endregion Container

        // ContainerData
        #region ContainerData
        #region ContainerData loading
        internal ContainerData? loadContainerData(XmlNode node, int canvasID)
        {
            ContainerData containerData = new ContainerData();
            XmlNode? node_ContainerData = node;

            if (node_ContainerData != null)
            {
                XmlNode? node_CoreData = NodeCheck(node_ContainerData, "CoreData");

                CoreData? aux_data = loadCoreData(node_CoreData);
                if (aux_data != null)
                {
                    containerData.UpdateCoreData(aux_data);
                    containerData.SetCanvasID(canvasID);
                }

                XmlNode? containerLocation = NodeCheck(node_ContainerData, "CanvasID");
                if (containerLocation != null)
                {
                    containerData.CanvasID = Int32.Parse(containerLocation.InnerText);
                }

                XmlNode? ContainerDataFilename = NodeCheck(node_ContainerData, "ContainerDataFilename");
                if (ContainerDataFilename != null)
                {
                    containerData.ContainerDataFilename = ContainerDataFilename.InnerText;
                }

                XmlNode? z_position = NodeCheck(node_ContainerData, "level");
                if (z_position != null)
                {
                    containerData.level = Int32.Parse(z_position.InnerText);
                }
            }

            return containerData;
        }
        #endregion ContainerData loading
        #endregion ContainerData

        #region Content loading
        internal UserControl? ContentData_load(Type type, XmlNode node_ContentData)
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
        #endregion Content loading

        #region Content saving
        internal XmlNode? Content_save(XmlDocument xmlDocument, CoreContainer container)
        {
            if (container.GetContainerData().GetElement() != null)
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

            return null;
        }
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
        #endregion Content saving

        // CoreData loading
        #region CoreData loading
        internal CoreData? loadCoreData(XmlNode node)
        {
            SharedLogic logic = new SharedLogic();
            CoreData coreData = new CoreData(true);

            if (node != null)
            {
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

                //MessageBox.Show(
                //    coreData.background.color1_string + "\n" +
                //    coreData.foreground.color1_string + "\n" +
                //    coreData.borderbrush.color1_string + "\n" +
                //    coreData.highlight.color1_string + "\n" +
                //    coreData.width + "\n" +
                //    coreData.height + "\n" +
                //    coreData.cornerRadius + "\n" +
                //    coreData.thickness + "\n" +
                //    coreData.fontFamily + "\n" +
                //    coreData.fontSize + "\n" +
                //    coreData.background.brushpath + "\n" +
                //    coreData.background.brushtype);

                return coreData;
            }

            return null;
        }
        #endregion CoreData loading

        // CoreData saving
        #region CoreData saving
        internal XmlNode? saveCoreData(XmlDocument xmlDocument, XmlNode node, CoreData data)
        {
            if (data != null)
            {
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

                return node;
            }

            return null;
        }
        #endregion CoreData saving

        internal XmlNode? NodeCheck(XmlNode node, string SubNodeName)
        {
            XmlNode xmlNode = node.SelectSingleNode(SubNodeName);

            return xmlNode;
        }

        internal LevelSystem UserSpace_Level_load(string path, int canvasID)
        {
            if (File.Exists(@$"{path}"))
            {
                XmlDocument xmlDocument = new XmlDocument();

                xmlDocument.Load(path);

                XmlNode node = xmlDocument.SelectSingleNode("LevelSystem");

                // LevelSystem load
                if (node != null)
                {
                    XmlNodeList? nodeList_LevelData = node.SelectNodes("LevelData");
                    ObservableCollection<LevelData> levels = new ObservableCollection<LevelData>();

                    for (int i = 0; i < nodeList_LevelData.Count; i++)
                    {
                        int level;
                        string name;
                        string description;
                        bool loginFlag;
                        bool securityFlag;
                        bool visibilityFlag;
                        bool hasBackground;
                        ColorData colorData;

                        if (nodeList_LevelData[i] != null)
                        {
                            level = Int32.Parse(nodeList_LevelData[i].SelectSingleNode("LEVEL").InnerText);

                            name = nodeList_LevelData[i].SelectSingleNode("NAME").InnerText;

                            description = nodeList_LevelData[i].SelectSingleNode("DESCRIPTION").InnerText;

                            loginFlag = bool.Parse(nodeList_LevelData[i].SelectSingleNode("SECURITY_FLAG").InnerText);

                            securityFlag = bool.Parse(nodeList_LevelData[i].SelectSingleNode("LOGIN_FLAG").InnerText);

                            visibilityFlag = bool.Parse(nodeList_LevelData[i].SelectSingleNode("VISIBILITY_FLAG").InnerText);

                            hasBackground = bool.Parse(nodeList_LevelData[i].SelectSingleNode("HASBACKGROUND").InnerText);

                            if (hasBackground)
                            {            
                                colorData = loadColorData(nodeList_LevelData[i], "background");

                                LevelData levelData = new LevelData(level, name, description, loginFlag, securityFlag, visibilityFlag, hasBackground, colorData);

                                levels.Add(levelData);
                            }
                            else
                            {
                                LevelData levelData = new LevelData(level, name, description, loginFlag, securityFlag, visibilityFlag, hasBackground, null);

                                levels.Add(levelData);
                            }

                        }
                    }

                    LevelSystem levelSystem = new LevelSystem(levels, canvasID);

                    return levelSystem;
                }
            }

            return null;
        }

        internal XmlNode? UserSpace_Level_save(XmlDocument xmlDocument, XmlNode node_LevelData, LevelData levelData)
        {
            if (levelData != null)
            {
                XmlNode LEVEL = xmlDocument.CreateElement("LEVEL");
                LEVEL.InnerText = levelData.LEVEL.ToString();
                node_LevelData.AppendChild(LEVEL);

                XmlNode NAME = xmlDocument.CreateElement("NAME");
                NAME.InnerText = levelData.NAME.ToString();
                node_LevelData.AppendChild(NAME);

                XmlNode DESCRIPTION = xmlDocument.CreateElement("DESCRIPTION");
                DESCRIPTION.InnerText = levelData.DESCRIPTION.ToString();
                node_LevelData.AppendChild(DESCRIPTION);

                XmlNode SECURITY_FLAG = xmlDocument.CreateElement("SECURITY_FLAG");
                SECURITY_FLAG.InnerText = levelData.SECURITY_FLAG.ToString();
                node_LevelData.AppendChild(SECURITY_FLAG);

                XmlNode LOGIN_FLAG = xmlDocument.CreateElement("LOGIN_FLAG");
                LOGIN_FLAG.InnerText = levelData.LOGIN_FLAG.ToString();
                node_LevelData.AppendChild(LOGIN_FLAG);

                XmlNode VISIBILITY_FLAG = xmlDocument.CreateElement("VISIBILITY_FLAG");
                VISIBILITY_FLAG.InnerText = levelData.VISIBILITY_FLAG.ToString();
                node_LevelData.AppendChild(VISIBILITY_FLAG);

                XmlNode HASBACKGROUND = xmlDocument.CreateElement("HASBACKGROUND");
                HASBACKGROUND.InnerText = levelData.HASBACKGROUND.ToString();
                node_LevelData.AppendChild(HASBACKGROUND);

                XmlNode? aux_background = saveColorData(xmlDocument, levelData.Background, "background");
                if (aux_background != null)
                {
                    node_LevelData.AppendChild(aux_background);
                }

                return node_LevelData;
            }

            return null;
        }

        internal void saveLevels(LevelSystem levelSystem)
        {
            if (levelSystem != null)
            {
                XmlDocument xmlDocument = new XmlDocument();

                XmlNode node = xmlDocument.CreateElement("LevelSystem");
                xmlDocument.AppendChild(node);

                foreach (LevelData level in levelSystem.getLevels())
                {
                    XmlNode node_LevelData = xmlDocument.CreateElement("LevelData");
                    XmlNode newNode = UserSpace_Level_save(xmlDocument, node_LevelData, level);

                    if (newNode != null)
                    {
                        node.AppendChild(newNode);
                    }
                }

                //MessageBox.Show(canvasData.imageFilePath);

                //if (levelSystem.CanvasID == 0)
                //{
                //    check_path($@"{SYSTEM_folder}");

                //    xmlDocument.Save($@"{SYSTEM_folder}\{LevelData_file}");
                //}
                //else
                //{
                check_path($@"{UserSpace_folder}screen_{levelSystem.CanvasID}");

                xmlDocument.Save($@"{UserSpace_folder}screen_{levelSystem.CanvasID}\{LevelData_file}");
                //}

            }
        }
    }
}
