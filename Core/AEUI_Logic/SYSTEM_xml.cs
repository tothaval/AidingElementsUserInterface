/* Aiding Elements User Interface
 *      SYSTEM_xml class
 * 
 * class provides separated xml save and load functions for SYSTEM components
 * 
 * init:        2024|01|28
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.AEUI_Data;
using AidingElementsUserInterface.Core.AEUI_UserControls;
using AidingElementsUserInterface.Core.Auxiliaries;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace AidingElementsUserInterface.Core.AEUI_Logic
{
    internal class SYSTEM_xml : IO_Handler
    {

        public SYSTEM_xml()
        {
                
        }

        internal CanvasData? SYSTEM_CanvasData_load(string path)
        {
            if (File.Exists(@$"{path}"))
            {
                XmlDocument xmlDocument = new XmlDocument();
                CanvasData canvasData = new CanvasData();

                xmlDocument.Load(path);

                XmlNode node = xmlDocument.SelectSingleNode("Core");

                if (node != null)
                {
                    XmlNode node_CanvasData = node.SelectSingleNode("CanvasData");

                    if (node_CanvasData != null)
                    {
                        XmlNode node_CoreData = node_CanvasData.SelectSingleNode("CoreData");

                        CoreData aux_data = new XML_Handler().loadCoreData(node_CoreData);

                        if (aux_data != null)
                        {
                            canvasData.apply_CoreData(aux_data);
                        }

                        canvasData.canvasName = node_CanvasData.SelectSingleNode("canvasName").InnerText;

                        canvasData.grouping_displacement = Int32.Parse(node_CanvasData.SelectSingleNode("grouping_displacement").InnerText);


                        XmlNode? element_spacing = new XML_Handler().NodeCheck(node_CanvasData, "element_spacing");
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


        internal void SYSTEM_CanvasData_save(CanvasData canvasData, LevelSystem levelSystem)
        {
            if (canvasData != null)
            {
                XmlDocument xmlDocument = new XmlDocument();

                XmlNode node = xmlDocument.CreateElement("Core");
                xmlDocument.AppendChild(node);

                XmlNode node_CanvasData = xmlDocument.CreateElement("CanvasData");
                XmlNode node_CoreData = xmlDocument.CreateElement("CoreData");

                XmlNode? aux_node = new XML_Handler().saveCoreData(xmlDocument, node_CoreData, canvasData);

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

                XmlNode node_LevelSystem = xmlDocument.CreateElement("LevelSystem");

                XmlNode aux_LevelData = saveLevels(xmlDocument, node_LevelSystem, levelSystem);

                if (aux_LevelData != null)
                {
                    node.AppendChild(aux_LevelData);
                }

                try
                {
                    xmlDocument.Save($"{SYSTEM_folder}{CanvasData_file}");
                }
                catch (Exception)
                {

                }
            }
        }

        internal ObservableCollection<CoreContainer> SYSTEM_Container_load()
        {
            ObservableCollection<CoreContainer> container_list = new ObservableCollection<CoreContainer>();

            string path = $@"{SYSTEM_folder}{CanvasData_file}";

            //MessageBox.Show($@".\{Core_Screens_folder}{folder_counter}\Container");
            //ContainerData_xml_folder

            if (File.Exists(path))
            {
                XmlDocument xmlDocument = new XmlDocument();

                xmlDocument.Load(path);

                XmlNode? node_Container = xmlDocument.SelectSingleNode("Container");


                if (node_Container != null)
                {
                    ContainerData containerData = new XML_Handler().loadContainerData(node_Container, 0);

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
                                    userControl = new XML_Handler().ContentData_load(type, node_Data);
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
                            containerData = new ContainerData(0);
                        }

                        containerData.SetElement(userControl);

                        CoreContainer coreContainer = new CoreContainer(containerData);
                        coreContainer.setPosition(container_position);

                        container_list.Add(coreContainer);
                    }
                }
            }


            return container_list;
        }

        internal void SYSTEM_Container_save(CoreContainer coreContainer, int counter = 0)
        {
            XmlDocument xmlDocument = new XmlDocument();
            CoreContainer container = coreContainer;

            if (coreContainer != null)
            {
                MessageBox.Show(container.GetContainerData().element.GetType().Name);

                XmlNode node_ContainerData = new XML_Handler().ContainerData_save(xmlDocument, container.GetContainerData());

                xmlDocument.AppendChild(node_ContainerData);

                XmlNode node = node_ContainerData;


                if (node != null)
                {
                    XmlNode node_CanvasID = xmlDocument.CreateElement("CanvasID");
                    node_CanvasID.InnerText = container.GetContainerData().CanvasID.ToString();
                    node.AppendChild(node_CanvasID);

                    XmlNode node_ContainerDataFilename = xmlDocument.CreateElement("ContainerDataFilename");
                    node_ContainerDataFilename.InnerText = $"{counter}.xml";
                    node.AppendChild(node_ContainerDataFilename);

                    XmlNode node_z_position = xmlDocument.CreateElement("level");
                    node_z_position.InnerText = container.GetContainerData().level.ToString();
                    node.AppendChild(node_z_position);

                    XmlNode node_Content = new XML_Handler().Content_save(xmlDocument, container);

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
                    xmlDocument.Save($"{SYSTEM_Container_folder}\\{counter}.xml");
                }
                catch (Exception)
                {

                }
            }
        }

        internal XmlNode? SYSTEM_Level_save(XmlDocument xmlDocument, XmlNode node_LevelData, LevelData levelData)
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

                XmlNode ZERO_FLAG = xmlDocument.CreateElement("ZERO_FLAG");
                ZERO_FLAG.InnerText = levelData.ZERO_FLAG.ToString();
                node_LevelData.AppendChild(ZERO_FLAG);

                return node_LevelData;
            }

            return null;
        }

        internal XmlNode? saveLevels(XmlDocument xmlDocument, XmlNode node, LevelSystem levelSystem)
        {
            if (levelSystem != null)
            {
                foreach (LevelData level in levelSystem.getLevels())
                {
                    XmlNode node_LevelData = xmlDocument.CreateElement("LevelData");
                    XmlNode newNode = SYSTEM_Level_save(xmlDocument, node_LevelData, level);

                    if (newNode != null)
                    {
                        node.AppendChild(newNode);
                    }                    
                }

                return node;
            }

            return null;
        }

    }
}
/*  END OF FILE
 * 
 * 
 */