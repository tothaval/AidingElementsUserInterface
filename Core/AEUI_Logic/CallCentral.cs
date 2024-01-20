/* Aiding Elements User Interface
 *      MainWindow 
 * 
 * command execution class
 * 
 * init:        2024|01|17
 * DEV:         Stephan Kammel
 * mail:        kammel@posteo.de
 */
using AidingElementsUserInterface.Core.Auxiliaries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AidingElementsUserInterface.Core.AEUI_Logic
{
    internal class CallCentral
    {
        private CoreCanvas executionRequestOrigin;

        private ObservableCollection<RequestReturn> executionRequests = new ObservableCollection<RequestReturn>();
        internal ObservableCollection<RequestReturn> GetExecutionRequests => executionRequests;

        internal CallCentral(ref CoreCanvas executionRequestOrigin)
        {
            this.executionRequestOrigin = executionRequestOrigin;
        }

        internal void ExecuteCommandRequest(object requestItem)
        {
            if (requestItem != null)
            {
                // ... maybe send objects to encapsulate a command within an autorization frame

                executionRequests.Add(CommandRequestReturn(requestItem));
            }
        }

        private RequestReturn CommandRequestReturn(object command)
        {
            ExecutionRequestReturn CommandRequestReturn = new ExecutionRequestReturn(command, ref executionRequestOrigin);

            // do something or do not something

            return CommandRequestReturn.GetRequestReturn();
        }

        internal class RequestReturn
        {
            internal object RequestItem = new object();

            internal string[] CommandShards = new string[] { };

            internal string[] IssuedCommands = new string[] { };

            internal object[] CommunicationProtocolls = new object[] { };

            internal RequestReturn()
            {


            }
        }

        private struct SigilCodexEntry
        {
            internal int CodexIndex { get; }
            internal string SigilIdentifier { get; }
            internal string Sigil { get; }

            public SigilCodexEntry(int codexIndex, string sigilIdentifier, string sigil)
            {
                CodexIndex = codexIndex;
                SigilIdentifier = sigilIdentifier;
                Sigil = sigil;
            }

            public override string ToString() => $"{CodexIndex} {SigilIdentifier} {Sigil}";
        }


        private protected class ExecutionRequestReturn
        {
            private CoreCanvas executionRequestOrigin;
            private RequestReturn requestReturn = new RequestReturn();
            private List<SigilCodexEntry> SIGIL_Codex = new List<SigilCodexEntry>();

            internal protected ExecutionRequestReturn(object requestItem, ref CoreCanvas executionRequestOrigin)
            {
                requestReturn.RequestItem = requestItem;
                this.executionRequestOrigin = executionRequestOrigin;

                GetSIGILs();
                ProcessRequest();
            }

            private enum SIGILs
            {
                SIGIL_SelectAll,
                SIGIL_SelectByIdentifier,
                SIGIL_CreateByIdentifier,
                SIGIL_ForceCreateAll,
                SIGIL_ConfirmCreateAll,
                SIGIL_ForceRemoveAll,
                SIGIL_ConfirmRemoveAll
            }

            private void GetSIGILs()
            {
                SIGIL_Codex = new List<SigilCodexEntry>()
                {
                    new SigilCodexEntry(0, "SIGIL_SelectAll", "<°"),
                    new SigilCodexEntry(1, "SIGIL_SelectByIdentifier", "<"),
                    new SigilCodexEntry(2, "SIGIL_CreateByIdentifier", ">"),
                    new SigilCodexEntry(3, "SIGIL_ForceCreateAll", "!.>°"),
                    new SigilCodexEntry(4, "SIGIL_ConfirmCreateAll", ".>°"),
                    new SigilCodexEntry(5, "SIGIL_ForceRemoveAll", "!.<°"),
                    new SigilCodexEntry(6, "SIGIL_3_ConfirmRemoveAll", ".<°"),
                };

            }


            internal protected RequestReturn GetRequestReturn()
            {
                return requestReturn;
            }



            private protected void ProcessRequest()
            {
                if (requestReturn != null)
                {
                    if (requestReturn.RequestItem.GetType() == typeof(string))
                    {
                        requestReturn.CommandShards = requestReturn.RequestItem.ToString().Split(' ');

                        if (requestReturn.CommandShards != null)
                        {
                            foreach (string shard in requestReturn.CommandShards)
                            {
                                if (shard != null)
                                {
                                    requestReturn.IssuedCommands.Append(ProcessCommandShard(shard));
                                }

                            }
                        }
                    }
                }
            }

            private string? COMMAND_SelectAll(int codex, string shard)
            {
                executionRequestOrigin.selectAll();

                return SIGIL_Codex[codex].SigilIdentifier;
            }

            private string? COMMAND_SelectByIdentifier(int codex, string shard)
            {
                if (shard.Substring(0, 1).Equals("<"))
                {
                    string target = shard.Substring(1);

                    Type? type = Type.GetType($"AidingElementsUserInterface.Elements.{target}, AidingElementsUserInterface");

                    if (type == null)
                    {
                        if (target.Equals("°"))
                        {
                            return COMMAND_SelectAll(0, shard);
                        }
                        else
                        {
                            int number;
                            try
                            {
                                number = Int32.Parse(target);

                                executionRequestOrigin.selectContainer(number);

                                return SIGIL_Codex[codex].SigilIdentifier;
                            }
                            catch (Exception)
                            {
                                return "ERR";
                            }
                        }
                    }

                    if (type != null)
                    {
                        new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS.selectType(type);
                        return SIGIL_Codex[codex].SigilIdentifier;
                    }

                    type = Type.GetType($"AidingElementsUserInterface.Elements.{target}.{target}, AidingElementsUserInterface");
                    if (type != null)
                    {
                        new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS.selectType(type);
                        return SIGIL_Codex[codex].SigilIdentifier;
                    }

                    type = Type.GetType($"AidingElementsUserInterface.Core.AEUI_UserControls.{target}, AidingElementsUserInterface");
                    if (type != null)
                    {
                        new SharedLogic().GetMainWindow().Get_ACTIVE_CANVAS.selectType(type);
                        return SIGIL_Codex[codex].SigilIdentifier;
                    }
                }

                return "ERR";
            }

            private string? COMMAND_CreateByIdentifier(int codex, string shard)
            {
                if (shard.Substring(0, 1).Equals(">"))
                {
                    string target = shard.Substring(1);

                    UserControl userControl;


                    Type? type = Type.GetType($"AidingElementsUserInterface.Core.AEUI_SystemControls.{target}, AidingElementsUserInterface");
                    if (type != null)
                    {
                        userControl = (UserControl)Activator.CreateInstance(type);

                        executionRequestOrigin.add_element_to_canvas(userControl);

                        return SIGIL_Codex[codex].SigilIdentifier;
                    }

                    type = Type.GetType($"AidingElementsUserInterface.Core.AEUI_UserControls.{target}, AidingElementsUserInterface");
                    if (type != null)
                    {
                        userControl = (UserControl)Activator.CreateInstance(type);

                        executionRequestOrigin.add_element_to_canvas(userControl);

                        return SIGIL_Codex[codex].SigilIdentifier;
                    }


                    type = Type.GetType($"AidingElementsUserInterface.Elements.{target}, AidingElementsUserInterface");
                    if (type != null)
                    {
                        userControl = (UserControl)Activator.CreateInstance(type);

                        executionRequestOrigin.add_element_to_canvas(userControl);

                        return SIGIL_Codex[codex].SigilIdentifier;
                    }

                    type = Type.GetType($"AidingElementsUserInterface.Elements.{target}.{target}, AidingElementsUserInterface");
                    if (type != null)
                    {
                        userControl = (UserControl)Activator.CreateInstance(type);

                        executionRequestOrigin.add_element_to_canvas(userControl);

                        return SIGIL_Codex[codex].SigilIdentifier;
                    }

                    return "ERR";
                }

                return "ERR";
            }

            private string? COMMAND_ForceRemoveAll(int codex, string shard)
            {
                if (shard.Equals("!.<°"))
                {
                    executionRequestOrigin.selectAll();
                    executionRequestOrigin.delete_selected_items();

                    return SIGIL_Codex[codex].SigilIdentifier;
                }

                return "ERR";
            }

            private protected string? ProcessCommandShard(string shard)
            {
                string? IssuedCommand = null;
                int UnknownCommandError = 0;

                int codex_entry = IsSigil(shard);

                switch (codex_entry)
                {
                    case -442:
                        UnknownCommandError = -442;
                        break;

                    case (int)SIGILs.SIGIL_SelectAll:
                        IssuedCommand = COMMAND_SelectAll(codex_entry, shard);
                        break;

                    case (int)SIGILs.SIGIL_SelectByIdentifier:
                        IssuedCommand = COMMAND_SelectByIdentifier(codex_entry, shard);
                        break;
                    case (int)SIGILs.SIGIL_CreateByIdentifier:
                        IssuedCommand = COMMAND_CreateByIdentifier(codex_entry, shard);
                        break;
                    //case (int)SIGILs.SIGIL_ForceCreateAll:
                    //    IssuedCommand =
                    //    break;
                    //case (int)SIGILs.SIGIL_ConfirmCreateAll:
                    //    IssuedCommand =
                    //    break;

                    case (int)SIGILs.SIGIL_ForceRemoveAll:
                        IssuedCommand = COMMAND_ForceRemoveAll(codex_entry, shard);
                        break;

                    //case (int)SIGILs.SIGIL_ConfirmRemoveAll:
                    //    IssuedCommand =
                    //    break;


                    default:
                        break;
                }

                if (UnknownCommandError == -442 | IssuedCommand.Equals("ERR"))
                {
                    return IssuedCommand = "ERR unknown command";
                }

                return IssuedCommand;
            }

            private protected int IsSigil(string shard)
            {
                foreach (SigilCodexEntry SCE in SIGIL_Codex)
                {
                    if (shard.StartsWith(SCE.Sigil))
                    {
                        return SCE.CodexIndex;
                    }
                }

                return -442;
            }

        }

    }

}
/*  END OF FILE
 * 
 * 
 */