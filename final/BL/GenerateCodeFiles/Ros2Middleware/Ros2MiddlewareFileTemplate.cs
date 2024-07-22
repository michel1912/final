using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
using MongoDB.Bson;
using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using WebApiCSharp.Services;
using WebApiCSharp.Models;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebApiCSharp.GenerateCodeFiles
{
    public class Ros2MiddlewareFileTemplate
    {
        public static string GetPackageFileTargetProjectDependencies(InitializeProject initProj)
        {
            string result = "";

            foreach (string targetPackage in initProj.RosTarget.RosTargetProjectPackages)
            {
                result += GenerateFilesUtils.GetIndentationStr(0, 0, "<build_depend>" + targetPackage + "</build_depend>");
                result += GenerateFilesUtils.GetIndentationStr(0, 0, "<exec_depend>" + targetPackage + "</exec_depend>");
            }

            return result;
        }

        public static string GetPackageFilefoxy(InitializeProject initProj)
        {
            string file = @"<?xml version=""1.0""?>
<package format=""3"">
  <name>" + GenerateRos2Middleware.ROS2_MIDDLEWARE_PACKAGE_NAME + @"</name>
  <version>0.0.0</version>
  <description>The " + GenerateRos2Middleware.ROS2_MIDDLEWARE_PACKAGE_NAME + @" package</description>
  <maintainer email=""mic@todo.todo"">mic</maintainer>
  <license>TODO</license>
  <buildtool_depend>ament_cmake</buildtool_depend>
" + GetPackageFileTargetProjectDependencies(initProj) + pythonContainer.GetPackageFilefoxy();

            return file;
        }

        public static string GetCMakeListsFilefoxy()
        {
            string file = @"
cmake_minimum_required(VERSION 3.5)
project(" + GenerateRos2Middleware.ROS2_MIDDLEWARE_PACKAGE_NAME + pythonContainer.GetCMakeListsFilyfoxy();

            return file;
        }

        public static string GetSetupFilefoxy(string console_main)
        {
            string file = pythonContainer.GetSetUpFilefoxy() + console_main + @"'
        ],
    },
)
";

            return file;
        }

        public static string GetImportsForMiddlewareNode(PLPsData data, InitializeProject initProj)
        {
            string result = "";
            Dictionary<string, HashSet<string>> unImports = new Dictionary<string, HashSet<string>>();
            List<RosImport> imports = new List<RosImport>();

            foreach (RosGlue glue in data.RosGlues.Values)
            {
                imports.AddRange(glue.RosServiceActivation.Imports);

                foreach (var lVar in glue.GlueLocalVariablesInitializations)
                {
                    imports.AddRange(lVar.Imports);
                }
            }

            foreach (RosImport im in imports)
            {
                im.From = im.From == null ? "" : im.From;

                if (!unImports.ContainsKey(im.From))
                {
                    unImports.Add(im.From, new HashSet<string>());
                }

                foreach (string sIm in im.Imports)
                {
                    unImports[im.From].Add(sIm);
                }
            }

            foreach (KeyValuePair<string, HashSet<string>> keyVal in unImports)
            {
                string baseS = keyVal.Key.Replace(" ", "").Length == 0 ? "" : "from " + keyVal.Key + " ";
                result += GenerateFilesUtils.GetIndentationStr(0, 0, baseS + "import " + String.Join(",", keyVal.Value));
            }

            return result;
        }

        private static string GetLocalVariableTypeClasses(PLPsData data)
        {
            string result = "";

            foreach (LocalVariableTypePLP type in data.LocalVariableTypes)
            {
                result += GenerateFilesUtils.GetIndentationStr(0, 4, "def " + type.TypeName + "ToDict(lt):");
                List<string> saFields = type.SubFields.Select(x => "\"" + x.FieldName + "\": lt." + x.FieldName).ToList();
                result += GenerateFilesUtils.GetIndentationStr(1, 4, "return {" + String.Join(", ", saFields) + "}");
                result += Environment.NewLine;
                result += GenerateFilesUtils.GetIndentationStr(0, 4, "class " + type.TypeName + ":");
                result += GenerateFilesUtils.GetIndentationStr(1, 4, "def __init__(self, " + String.Join(", ", type.SubFields.Select(x => x.FieldName)) + "):");

                foreach (LocalVariableCompoundTypeField field in type.SubFields)
                {
                    result += GenerateFilesUtils.GetIndentationStr(2, 4, "self." + field.FieldName + "=" + field.FieldName);
                }

                result += Environment.NewLine;
                result += GenerateFilesUtils.GetIndentationStr(1, 4, "def __init__(self):");

                foreach (LocalVariableCompoundTypeField field in type.SubFields)
                {
                    result += GenerateFilesUtils.GetIndentationStr(2, 4, "self." + field.FieldName + "=None");
                }
            }

            return result;
        }

        private static string GetListenToMongoDbCommandsInitFunction(PLPsData data) //for ros2
        {
            string result = "";

            result += GenerateFilesUtils.GetIndentationStr(1, 4, "def _init_(self, _topic_listener):");
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.current_action_sequence_id = 1");
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.current_action_for_execution_id = None");
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "self._topic_listener = _topic_listener");
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.ready_to_activate = \"\" ");

            foreach (RosGlue glue in data.RosGlues.Values)
            {
                if (glue.RosActionActivation != null && !string.IsNullOrEmpty(glue.RosActionActivation.ActionName))
                {
                    result += GenerateFilesUtils.GetIndentationStr(2, 4, "self." + glue.Name + "_service_name = \"" + glue.RosActionActivation.ActionPath + "\"");
                }
            }

            result += Environment.NewLine;
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.listen_to_mongodb_commands()");

            return result;
        }

        public static CompoundVarTypePLP GetCompundTypeByName(string compTypeName, PLPsData data)
        {
            List<CompoundVarTypePLP> cl = data.GlobalCompoundTypes.Where(x => x.TypeName.Equals(compTypeName)).ToList();

            return cl.Count == 0 ? null : cl[0];
        }

        public static CompoundVarTypePLP_Variable GetCompundVariableByName(CompoundVarTypePLP oComp, string subFields, PLPsData data)
        {
            string[] bits = subFields.Split(".");

            if (oComp == null || bits.Length == 0) return null;
            List<CompoundVarTypePLP_Variable> lv = oComp.Variables.Where(x => x.Name.Equals(bits[0])).ToList();
            if (lv.Count == 0) return null;
            if (bits.Length == 1) return lv[0];
            bits[0] = "";

            return GetCompundVariableByName(GetCompundTypeByName(lv[0].Type, data), String.Join(".", bits.Where(x => !String.IsNullOrEmpty(x))), data);
        }

        private static LocalVariableTypePLP GetUnderlineLocalVariableTypeByVarName(PLPsData data, PLP plp, string variableName)
        {
            string underlineTypeName = GetUnderlineLocalVariableNameTypeByVarName(data, plp, variableName);
            underlineTypeName = underlineTypeName == null ? "" : underlineTypeName;
            return data.LocalVariableTypes.Where(x => x.TypeName.Equals(underlineTypeName)).FirstOrDefault();
        }

        public static string GetUnderlineLocalVariableNameTypeByVarName(PLPsData data, PLP plp, string variableName) // NO CHANGES NEED 
        {
            string[] bits = variableName.Split(".");
            string baseVarName = bits[0] + "." + (bits.Length > 1 ? bits[1] : "");
            List<GlobalVariableDeclaration> dl = data.GlobalVariableDeclarations.Where(x => ("state." + x.Name).Equals(baseVarName)).ToList();

            if (dl.Count > 0)
            {
                if (GenerateFilesUtils.IsPrimitiveType(dl[0].Type))
                {
                    return null;
                }

                if (PLPsData.ANY_VALUE_TYPE_NAME.Equals(dl[0].Type))
                {
                    return dl[0].UnderlineLocalVariableType;
                }

                CompoundVarTypePLP comp = GetCompundTypeByName(dl[0].Type, data);
                CompoundVarTypePLP_Variable oVar = GetCompundVariableByName(comp, string.Join(".", bits.Skip(1)), data);
                return oVar == null || !oVar.Type.Equals(PLPsData.ANY_VALUE_TYPE_NAME)
                    ? null
                    : oVar.UnderlineLocalVariableType;
            }
            else
            {
                List<GlobalVariableModuleParameter> temp = plp.GlobalVariableModuleParameters.Where(x => x.Name.Equals(bits[0])).ToList();

                if (temp.Count == 0) return null;

                CompoundVarTypePLP comp = GetCompundTypeByName(temp[0].Type, data);
                CompoundVarTypePLP_Variable oVar = GetCompundVariableByName(comp, string.Join(".", bits.Skip(1)), data);

                return oVar == null || !oVar.Type.Equals(PLPsData.ANY_VALUE_TYPE_NAME)
                    ? null
                    : oVar.UnderlineLocalVariableType;
            }
        }

        private static string GetCodeLineWithLocalVarRefference(string codeLine, HashSet<string> localVarNames)
        {
            string result = codeLine;

            foreach (string varName in localVarNames.OrderByDescending(x => x.Length))
            {
                string pattern = @"\b(?<!\.)" + varName + @"\b";
                string replaceTo = "self.localVarNamesAndValues[self.listenTargetModule][\"" + varName + "\"]";
                result = Regex.Replace(result, pattern, replaceTo);
            }

            return result;
        }

        private static string GetAOS_TopicListenerServerClass(PLPsData data)
        {
            string result = "";
            result += GenerateFilesUtils.GetIndentationStr(0, 4, "class AOS_TopicListenerServer:");
            result += GenerateFilesUtils.GetIndentationStr(1, 4, "def __init__(self):");
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.localVarNamesAndValues = {", false);

            List<RosGlue> gluesWithLocalVars = data.RosGlues.Values
                .Where(x => x.LocalVariablesInitializationFromGlobalVariables.Count > 0 ||
                            x.GlueLocalVariablesInitializations.Count > 0)
                .ToList();

            HashSet<string> localVarNames = new HashSet<string>();

            foreach (var glue in gluesWithLocalVars)
            {
                result += GenerateFilesUtils.GetIndentationStr(0, 4, "\"" + glue.Name + "\":{", false);

                foreach (var localVar in glue.GlueLocalVariablesInitializations)
                {
                    localVarNames.Add(localVar.LocalVarName);
                    result += GenerateFilesUtils.GetIndentationStr(0, 4, "\"" + localVar.LocalVarName + "\": " +
                                                                         (string.IsNullOrEmpty(localVar.InitialValue)
                                                                             ? "None"
                                                                             : localVar.InitialValue) +
                                                                         (localVar ==
                                                                             glue.GlueLocalVariablesInitializations
                                                                                 .Last() && glue
                                                                                 .LocalVariablesInitializationFromGlobalVariables
                                                                                 .Count == 0
                                                                                 ? ""
                                                                                 : ", "), false);
                }

                foreach (var localFromGlob in glue.LocalVariablesInitializationFromGlobalVariables)
                {
                    localVarNames.Add(localFromGlob.InputLocalVariable);
                    result += GenerateFilesUtils.GetIndentationStr(0, 4,
                        "\"" + localFromGlob.InputLocalVariable + "\": None" +
                        (localFromGlob == glue.LocalVariablesInitializationFromGlobalVariables.Last() ? "" : ", "),
                        false);
                }

                result += GenerateFilesUtils.GetIndentationStr(0, 4, "}" + (glue != gluesWithLocalVars.Last() ? ", " : ""), false);
            }

            result += GenerateFilesUtils.GetIndentationStr(0, 4, "}");
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.setListenTarget(\"initTopicListener\")");

            Dictionary<string, Dictionary<string, List<GlueLocalVariablesInitialization>>> topicsToListen =
                new Dictionary<string, Dictionary<string, List<GlueLocalVariablesInitialization>>>();

            foreach (var glue in data.RosGlues.Values)
            {
                foreach (var oLVar in glue.GlueLocalVariablesInitializations)
                {
                    if (!string.IsNullOrEmpty(oLVar.RosTopicPath))
                    {
                        string cbFunc = "cb_" + oLVar.RosTopicPath.Replace("/", "_");

                        if (!topicsToListen.ContainsKey(oLVar.RosTopicPath))
                        {
                            topicsToListen[oLVar.RosTopicPath] = new Dictionary<string, List<GlueLocalVariablesInitialization>>();
                        }

                        if (!topicsToListen[oLVar.RosTopicPath].ContainsKey(glue.Name))
                        {
                            topicsToListen[oLVar.RosTopicPath][glue.Name] = new List<GlueLocalVariablesInitialization>();
                        }

                        topicsToListen[oLVar.RosTopicPath][glue.Name].Add(oLVar);
                    }
                }
            }

            foreach (var topic in topicsToListen)
            {
                result += GenerateFilesUtils.GetIndentationStr(2, 4,
                    "rclpy.create_subscription(\"" + topic.Key + "\", " +
                    topic.Value.Values.ToList()[0][0].TopicMessageType + ", self.cb_" + topic.Key.Replace("/", "_") +
                    ", 1000)");
            }

            result += Environment.NewLine;

            foreach (var topic in topicsToListen)
            {
                result += GenerateFilesUtils.GetIndentationStr(1, 4, "def cb_" + topic.Key.Replace("/", "_") + "(self, data):");
                result += GenerateFilesUtils.GetIndentationStr(2, 4, "try:");

                foreach (var glueTopic in topic.Value)
                {
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "if self.listenTargetModule == \"" + glueTopic.Key + "\":");
                    result += GenerateFilesUtils.GetIndentationStr(4, 4, "if DEBUG:");
                    result += GenerateFilesUtils.GetIndentationStr(5, 4, "print(\"handling topic call:" + glueTopic.Key + "\")");
                    result += GenerateFilesUtils.GetIndentationStr(5, 4, "print(data)");

                    foreach (var localVar in glueTopic.Value)
                    {
                        result += GenerateFilesUtils.GetIndentationStr(4, 4,
                            "#-----------------------------------------------------------------------");
                        result += GenerateFilesUtils.GetIndentationStr(4, 4,
                            "value = self." + glueTopic.Key + "_get_value_" + localVar.LocalVarName + "(data)");
                        result += GenerateFilesUtils.GetIndentationStr(4, 4,
                            "self.updateLocalVariableValue(\"" + localVar.LocalVarName + "\", value)");
                    }
                }

                result += GenerateFilesUtils.GetIndentationStr(2, 4, "except Exception as e:");
                result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerError(str(e), traceback.format_exc(e), 'topic " + topic.Key + "')");
                result += Environment.NewLine;
            }

            foreach (var topic in topicsToListen)
            {
                foreach (var glueTopic in topic.Value)
                {
                    foreach (var localVar in glueTopic.Value)
                    {
                        result += GenerateFilesUtils.GetIndentationStr(1, 4,
                            "def " + glueTopic.Key + "_get_value_" + localVar.LocalVarName + "(self, __input):");
                        result += GenerateFilesUtils.GetIndentationStr(2, 4,
                            GetCodeLineWithLocalVarRefference(localVar.AssignmentCode, localVarNames), true, true);
                        result += Environment.NewLine;
                    }
                }
            }

            result += pythonContainer.GetAOS_TopicListenerServerClass();
            Dictionary<string, List<GlueLocalVariablesInitialization>> rosParamVariables = new Dictionary<string, List<GlueLocalVariablesInitialization>>();

            foreach (var glue in data.RosGlues.Values)
            {
                List<GlueLocalVariablesInitialization> glueParamVariables = glue.GlueLocalVariablesInitializations
                    .Where(x => !string.IsNullOrEmpty(x.RosParameterPath)).ToList();

                if (glueParamVariables.Count > 0)
                {
                    rosParamVariables.Add(glue.Name, glueParamVariables);
                }
            }

            foreach (var glueRosParamLocalVars in rosParamVariables)
            {
                result += GenerateFilesUtils.GetIndentationStr(2, 4, "if self.listenTargetModule == \"" + glueRosParamLocalVars.Key + "\":");

                foreach (var localParam in glueRosParamLocalVars.Value)
                {
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "self.checkParameterValue_" + localParam.LocalVarName + "()");
                }
            }

            foreach (var glueRosParamLocalVars in rosParamVariables)
            {
                foreach (var localParam in glueRosParamLocalVars.Value)
                {
                    result += GenerateFilesUtils.GetIndentationStr(1, 4,
                        "def checkParameterValue_" + localParam.LocalVarName +
                        "(self):#TODO:: need to see how to update ROS parameters. using threading disable other topic listeners");
                    result += GenerateFilesUtils.GetIndentationStr(2, 4,
                        "if self.listenTargetModule == \"" + glueRosParamLocalVars.Key + "\":");
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "try:");
                    result += GenerateFilesUtils.GetIndentationStr(4, 4,
                        "#__input = rospy.get_param('" + localParam.RosParameterPath + "')");
                    result += GenerateFilesUtils.GetIndentationStr(4, 4, "#" + localParam.LocalVarName + " = __input");
                    result += GenerateFilesUtils.GetIndentationStr(4, 4,
                        "#self.updateLocalVariableValue(\"" + localParam.LocalVarName + "\", " +
                        localParam.LocalVarName + ")");
                    result += GenerateFilesUtils.GetIndentationStr(4, 4,
                        "self.updateLocalVariableValue(\"" + localParam.LocalVarName + "\", True)");
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "except:");
                    result += GenerateFilesUtils.GetIndentationStr(4, 4, "pass");
                    result += GenerateFilesUtils.GetIndentationStr(3, 4,
                        "#threading.Timer(1, self.checkParameterValue_" + localParam.LocalVarName + ").start()");
                }
            }

            result += pythonContainer.GetAOS_TopicListenerServerClass1();
            return result;
        }

        private static string GetHeavyLocalVariablesList(PLPsData data)
        {
            string result = "";

            foreach (PLP plp in data.PLPs.Values)
            {
                string plpHeavyVars = string.Join<String>(",",
                    data.LocalVariablesListings.Where(x => x.IsHeavyVariable && x.SkillName == plp.Name)
                        .Select(x => "\"" + x.VariableName + "\"").ToArray());
                if (plpHeavyVars.Length > 0)
                {
                    result += (result.Length > 0 ? ", " : "") + "\"" + plp.Name + "\" : [" + plpHeavyVars + "]";
                }
            }

            return "HEAVY_LOCAL_VARS={" + result + "}";
        }

        private static string GetHandleModuleFunctionV2(PLPsData data)
        {
            string result = "";

            foreach (RosGlue glue in data.RosGlues.Values)
            {
                PLP plp = data.PLPs[glue.Name];
                result += GenerateFilesUtils.GetIndentationStr(1, 4, "def handle_" + glue.Name + "(self, params):");
                result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.responseNotByLocalVariables = None");

                // Initialize local variables
                foreach (LocalVariablesInitializationFromGlobalVariable oGlVar in glue.LocalVariablesInitializationFromGlobalVariables)
                {
                    result += GenerateFilesUtils.GetIndentationStr(2, 4, oGlVar.InputLocalVariable + " = \"\"");
                }

                // Try block for local variable assignment
                if (glue.LocalVariablesInitializationFromGlobalVariables.Count > 0)
                {
                    result += GenerateFilesUtils.GetIndentationStr(2, 4, "try:");

                    foreach (LocalVariablesInitializationFromGlobalVariable oGlVar in glue.LocalVariablesInitializationFromGlobalVariables)
                    {
                        LocalVariableTypePLP underlineType = GetUnderlineLocalVariableTypeByVarName(data, plp, oGlVar.FromGlobalVariable);

                        if (underlineType != null)
                        {
                            if (oGlVar.FromGlobalVariable.StartsWith(PLPsData.GLOBAL_VARIABLE_STATE_REF))
                            {
                                result += GenerateFilesUtils.GetIndentationStr(3, 4, "globVarName = \"" + oGlVar.FromGlobalVariable + "\"");
                            }
                            else
                            {
                                string baseGlobalParameter = plp.GlobalVariableModuleParameters
                                    .Where(x => oGlVar.FromGlobalVariable.StartsWith(x.Name + ".") ||
                                                oGlVar.FromGlobalVariable.Equals(x.Name))
                                    .Select(x => x.Name).FirstOrDefault();
                                result += GenerateFilesUtils.GetIndentationStr(3, 4,
                                    "globVarName = \"" + oGlVar.FromGlobalVariable + "\".replace(\"" +
                                    baseGlobalParameter + "\", params[\"ParameterLinks\"][\"" + baseGlobalParameter +
                                    "\"], 1)");
                            }

                            result += GenerateFilesUtils.GetIndentationStr(3, 4,
                                "dbVar = aos_GlobalVariablesAssignments_collection.find_one({\"GlobalVariableName\": globVarName})");
                            result += GenerateFilesUtils.GetIndentationStr(3, 4,
                                oGlVar.InputLocalVariable + " = " + underlineType.TypeName + "()");
                            
                            foreach (LocalVariableCompoundTypeField field in underlineType.SubFields)
                            {
                                result += GenerateFilesUtils.GetIndentationStr(3, 4,
                                    oGlVar.InputLocalVariable + "." + field.FieldName +
                                    " = dbVar[\"LowLevelValue\"][\"" + field.FieldName + "\"]");
                            }

                            result += GenerateFilesUtils.GetIndentationStr(3, 4,
                                "self._topicListener.localVarNamesAndValues[\"" + glue.Name + "\"][\"" +
                                oGlVar.InputLocalVariable + "\"] = " + underlineType.TypeName + "ToDict(" +
                                oGlVar.InputLocalVariable + ")");
                        }
                        else
                        {
                            string[] bits = oGlVar.FromGlobalVariable.Split(".");
                            string varDesc = "[\"" + String.Join("\"][\"", bits) + "\"]";
                            result += GenerateFilesUtils.GetIndentationStr(3, 4,
                                oGlVar.InputLocalVariable + " = params[\"ParameterValues\"]" + varDesc);
                            result += GenerateFilesUtils.GetIndentationStr(3, 4,
                                "self._topicListener.updateLocalVariableValue(\"" + oGlVar.InputLocalVariable +
                                "\", \"" + oGlVar.Consistency + "\", " + oGlVar.InputLocalVariable + ")"); //added
                        }
                    }

                    result += GenerateFilesUtils.GetIndentationStr(2, 4, "except Exception as e:");
                    result += GenerateFilesUtils.GetIndentationStr(3, 4,
                        "registerError(str(e), traceback.format_exc(), 'Action: " + glue.Name + ", illegalActionObs')");
                    result += GenerateFilesUtils.GetIndentationStr(3, 4,
                        "self.responseNotByLocalVariables = \"illegalActionObs\"");
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "return self.responseNotByLocalVariables");
                }

                if (glue.RosServiceActivation != null && !string.IsNullOrEmpty(glue.RosServiceActivation.ServiceName))
                {
                    result += GenerateFilesUtils.GetIndentationStr(2, 4, "try:");
                    result += GenerateFilesUtils.GetIndentationStr(3, 4,
                        "registerLog(\"wait for service: moduleName=" + glue.Name + ", serviceName=" +
                        glue.RosServiceActivation.ServiceName + "\")");
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "req = NavigateToCoordinates.Request()");
                    List<string> parameterLines = new List<string>();
                    string indentedAssignment = "";
                    foreach (var assignment in glue.RosServiceActivation.ParametersAssignments)
                    {
                        indentedAssignment = GenerateFilesUtils.GetIndentationStr(3, 4, assignment.AssignServiceFieldCode);
                    }

                    string[] parts = indentedAssignment.Split(',').Select(p => p.Trim()).ToArray();

                    for (int i = 0; i < parts.Length; i++)
                    {
                        parts[i] = GenerateFilesUtils.GetIndentationStr(3, 4, parts[i]);
                    }

                    string serviceCallParam = string.Join(Environment.NewLine, parts);

                    result += serviceCallParam;
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "self.get_logger().info(\"Sending request to service, moduleName=" + glue.Name + "\")");
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "future = self.cli.call_async(req)");
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerLog(\"Service call made, waiting for response\")");
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "future.add_done_callback(self.navigate_callback)");
                    result += GenerateFilesUtils.GetIndentationStr(2, 4, "except Exception as e:");
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerError(str(e), traceback.format_exc(), 'Action: " + glue.Name + "')");
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "print(\"Service call failed\")");
                }

                result += GenerateFilesUtils.GetIndentationStr(2, 4, "return self.responseNotByLocalVariables");

                result += "\n";
                result += GenerateFilesUtils.GetIndentationStr(1, 4, "def navigate_callback(self, future):");
                result += GenerateFilesUtils.GetIndentationStr(2, 4, "try:");
                result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerLog(\"navigate_callback invoked\")");
                result += GenerateFilesUtils.GetIndentationStr(3, 4, "result = future.result()");
                result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerLog(\"Future result obtained\")");
                result += GenerateFilesUtils.GetIndentationStr(3, 4, "if result is not None:");
                GlueLocalVariablesInitialization localVarFromServiceReponse = glue.GlueLocalVariablesInitializations
                    .Where(x => x.FromROSServiceResponse.HasValue && x.FromROSServiceResponse.Value).FirstOrDefault();
                
                if (localVarFromServiceReponse != null)
                {
                    //there is a bug here 
                    result += GenerateFilesUtils.GetIndentationStr(4, 4, localVarFromServiceReponse.AssignmentCode,
                        true, true);
                    result += GenerateFilesUtils.GetIndentationStr(4, 4,
                        "self._topicListener.updateLocalVariableValue(\"" + localVarFromServiceReponse.LocalVarName +
                        "\", \"" + localVarFromServiceReponse.Consistency + "\", " +
                        localVarFromServiceReponse.LocalVarName + ")"); 
                }

                result += GenerateFilesUtils.GetIndentationStr(4, 4, "registerLog(f\"Service response success: {skillSuccess}\")");
                result += GenerateFilesUtils.GetIndentationStr(4, 4, "if DEBUG:");
                result += GenerateFilesUtils.GetIndentationStr(5, 4, "print(\"navigate service terminated\")");
                result += GenerateFilesUtils.GetIndentationStr(3, 4, "else:");
                result += GenerateFilesUtils.GetIndentationStr(4, 4, "self.get_logger().error(\"Service call failed, result is None\")");
                result += GenerateFilesUtils.GetIndentationStr(4, 4, "registerLog(\"Service call failed, result is None\")");
                result += GenerateFilesUtils.GetIndentationStr(2, 4, "except Exception as e:");
                result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerError(str(e), traceback.format_exc(), 'Action: navigate')");
                result += GenerateFilesUtils.GetIndentationStr(3, 4, "print(\"Service call failed\")");
            }

            return result;
        }

        private static string GetListenToMongoDbCommandsInitFunctionV2(PLPsData data)
        {
            string result = "";

            result += GenerateFilesUtils.GetIndentationStr(1, 0, "def __init__(self, topic_listener):");
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "super().__init__('listen_to_mongodb_commands')");
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.current_action_sequence_id = 1");
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.current_action_for_execution_id = None");
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.responseNotByLocalVariables = None");

            foreach (RosGlue glue in data.RosGlues.Values)
            {
                if (glue.RosServiceActivation != null && !string.IsNullOrEmpty(glue.RosServiceActivation.ServiceName))
                {
                    result += GenerateFilesUtils.GetIndentationStr(2, 4, "self." + glue.Name + "_service_name = \"" + glue.RosServiceActivation.ServicePath + "\"");
                }
            }

            result += GenerateFilesUtils.GetIndentationStr(2, 4, "self._topicListener = topic_listener");
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.cli = self.create_client(NavigateToCoordinates, self.navigate_service_name)");
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "while not self.cli.wait_for_service(timeout_sec=1.0):");
            result += GenerateFilesUtils.GetIndentationStr(3, 4, "self.get_logger().info('Service not available, waiting again...')");
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.timer = self.create_timer(1.0, self.listen_to_mongodb_commands)");

            return result;
        }


        public static string GetAosRos2MiddlewareNodeFile(PLPsData data, InitializeProject initProj)
        {
            string pythonVersion = "python3"; // ROS2 supports only Python 3
            string file = @"#!/usr/bin/" + pythonVersion + pythonContainer.getImports() +
                          GetImportsForMiddlewareNode(data, initProj) + @"
DEBUG = " + (initProj.MiddlewareConfiguration.DebugOn ? "True" : "False") + Environment.NewLine +
                          GetHeavyLocalVariablesList(data) + pythonContainer.getFromMiddleWare() +
                          GetLocalVariableTypeClasses(data) + @"
class ListenToMongoDbCommands(Node):
    " + GetListenToMongoDbCommandsInitFunctionV2(data) + @"
" + GetHandleModuleFunctionV2(data) + pythonContainer.getFromNodeFile() + GetModuleResponseFunctionPartV2(data) +
                          pythonContainer.getRegister() + GetListenToMongoCommandsFunctionPartV2(data) +
                          pythonContainer.timer_function() + check_LocalVars(data) +
                          pythonContainer.Register_ModuleResponse() + reset_navigation_vars(data) +
                          pythonContainer.getTopicListener() + GetAOS_TopicListenerServerClassV2(data) + @"
" + shareClassMashehoKazy(data) + pythonContainer.getMain();
            
            return file;
        }

        private static string GetModuleResponseFunctionPartV2(PLPsData data)
        {
            string result = "";

            foreach (RosGlue glue in data.RosGlues.Values)
            {
                PLP plp = data.PLPs[glue.Name];
                result += GenerateFilesUtils.GetIndentationStr(2, 4, "if moduleName == \"" + glue.Name + "\":");
                result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerLog(\"in the function registerModuleResponse for " + glue.Name + ":::::::: \")");

                HashSet<string> localVarNames = new HashSet<string>();
                
                foreach (var oVar in glue.GlueLocalVariablesInitializations)
                {
                    localVarNames.Add(oVar.LocalVarName);
                    result += GenerateFilesUtils.GetIndentationStr(3, 4,
                        oVar.LocalVarName + " = self._topicListener.localVarNamesAndValues[\"" + glue.Name + "\"][\"" +
                        oVar.LocalVarName + "\"]");
                    result += GenerateFilesUtils.GetIndentationStr(3, 4,
                        "registerLog(\"" + oVar.LocalVarName + ":  \"  +  str(" + oVar.LocalVarName + "))");
                }

                foreach (var oVar in glue.LocalVariablesInitializationFromGlobalVariables)
                {
                    localVarNames.Add(oVar.InputLocalVariable);
                    result += GenerateFilesUtils.GetIndentationStr(3, 4,
                        oVar.InputLocalVariable + " = self._topicListener.localVarNamesAndValues[\"" + glue.Name +
                        "\"][\"" + oVar.InputLocalVariable + "\"]");
                }

                if (glue.ResponseRules.Count == 0)
                {
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "if moduleResponse == \"\":");
                    result += GenerateFilesUtils.GetIndentationStr(4, 4, "moduleResponse = \"DefaultObservation\"");
                }

                foreach (var responseRule in glue.ResponseRules)
                {
                    result += GenerateFilesUtils.GetIndentationStr(3, 4,
                        "if moduleResponse == \"\" and " +
                        (string.IsNullOrEmpty(responseRule.Condition) ? "True" : responseRule.Condition) + ":");
                    result += GenerateFilesUtils.GetIndentationStr(4, 4,
                        "moduleResponse = \"" + glue.Name + "_" + responseRule.Response + "\"");
                }

                foreach (var responseRule in glue.ResponseRules)
                {
                    foreach (var assign in responseRule.ResponseAssignmentsToGlobalVar)
                    {
                        result += GenerateFilesUtils.GetIndentationStr(3, 4, "if moduleResponse == \"" + glue.Name + "_" + responseRule.Response + "\":");
                        result += GenerateFilesUtils.GetIndentationStr(4, 4, "assignGlobalVar[\"" + assign.GlobalVarName + "\"] = " + assign.Value);
                    }
                }

                if (!String.IsNullOrEmpty(glue.ResponseFromStringLocalVariable))
                {
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "moduleResponse = str(" + glue.ResponseFromStringLocalVariable + ")");
                }

                result += Environment.NewLine;
            }

            return result;
        }


        private static string reset_navigation_vars(PLPsData data)
        {
            string result = "";

            foreach (RosGlue glue in data.RosGlues.Values)
            {
                PLP plp = data.PLPs[glue.Name];
                HashSet<string> localVarNames = new HashSet<string>();
                foreach (var oVar in glue.GlueLocalVariablesInitializations)
                {
                    localVarNames.Add(oVar.LocalVarName);
                    result += GenerateFilesUtils.GetIndentationStr(3, 4,
                        "self._topicListener.localVarNamesAndValues[\"" + glue.Name + "\"][\"" + oVar.LocalVarName +
                        "\"] = False ");
                }

                foreach (var oVar in glue.LocalVariablesInitializationFromGlobalVariables)
                {
                    localVarNames.Add(oVar.InputLocalVariable);
                    result += GenerateFilesUtils.GetIndentationStr(3, 4,
                        "self._topicListener.localVarNamesAndValues[\"" + glue.Name + "\"][\"" +
                        oVar.InputLocalVariable + "\"]= None");
                }
            }

            return result;
        }

        private static string check_LocalVars(PLPsData data)
        {
            string result = "";

            Dictionary<string, Dictionary<string, List<GlueLocalVariablesInitialization>>> topicsToListen = new Dictionary<string, Dictionary<string, List<GlueLocalVariablesInitialization>>>();
            
            foreach (RosGlue glue in data.RosGlues.Values)
            {
                foreach (var oLVar in glue.GlueLocalVariablesInitializations)
                {
                    if (!string.IsNullOrEmpty(oLVar.RosTopicPath))
                    {
                        if (!topicsToListen.ContainsKey(oLVar.RosTopicPath))
                        {
                            topicsToListen[oLVar.RosTopicPath] = new Dictionary<string, List<GlueLocalVariablesInitialization>>();
                        }

                        if (!topicsToListen[oLVar.RosTopicPath].ContainsKey(glue.Name))
                        {
                            topicsToListen[oLVar.RosTopicPath][glue.Name] = new List<GlueLocalVariablesInitialization>();
                        }

                        topicsToListen[oLVar.RosTopicPath][glue.Name].Add(oLVar);
                    }
                }
            }


            foreach (var topic in topicsToListen)
            {
                foreach (var glueTopic in topic.Value)
                {
                    foreach (var localVar in glueTopic.Value)
                    {
                        result += GenerateFilesUtils.GetIndentationStr(2, 4,
                            "if self._topicListener.localVarNamesAndValues[\"" + glueTopic.Key + "\"][\"" +
                            localVar.LocalVarName + "\"]:");
                    }
                }
            }

            return result;
        }

        private static string GetListenToMongoCommandsFunctionPartV2(PLPsData data)
        {
            string result = "";
            
            foreach (PLP plp in data.PLPs.Values)
            {
                result += GenerateFilesUtils.GetIndentationStr(4, 3, "if moduleName == \"" + plp.Name + "\":");
                result += GenerateFilesUtils.GetIndentationStr(5, 4, "print(\"handle " + plp.Name + "\")");
                result += GenerateFilesUtils.GetIndentationStr(5, 4, "self.responseNotByLocalVariables = self.handle_" + plp.Name + "(actionParameters)");
                result += GenerateFilesUtils.GetIndentationStr(5, 4, "registerLog(\"" + plp.Name + " finished:\")");
            }

            return result;
        }

        private static string GetAOS_TopicListenerServerClassV2(PLPsData data)
        {
            string result = "";

            result += GenerateFilesUtils.GetIndentationStr(0, 4, "class AOS_TopicListenerServer(Node):");
            result += GenerateFilesUtils.GetIndentationStr(1, 4, "def __init__(self):");
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "super().__init__('aos_topic_listener_server')");
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.localVarNamesAndValues = {", false);

            List<RosGlue> gluesWithLocalVars = data.RosGlues.Values
                .Where(x => x.LocalVariablesInitializationFromGlobalVariables.Count > 0 ||
                            x.GlueLocalVariablesInitializations.Count > 0).ToList();
            
            HashSet<string> localVarNames = new HashSet<string>();

            for (int j = 0; j < gluesWithLocalVars.Count; j++)
            {
                RosGlue glue = gluesWithLocalVars[j];
                result += GenerateFilesUtils.GetIndentationStr(0, 4, "\"" + glue.Name + "\":{", false);

                for (int i = 0; i < glue.GlueLocalVariablesInitializations.Count; i++)
                {
                    var localVar = glue.GlueLocalVariablesInitializations[i];
                    localVarNames.Add(localVar.LocalVarName);
                    result += GenerateFilesUtils.GetIndentationStr(0, 4, "\"" + localVar.LocalVarName + "\": " +
                                                                         (string.IsNullOrEmpty(localVar.InitialValue)
                                                                             ? "None"
                                                                             : localVar.InitialValue) +
                                                                         (i == glue.GlueLocalVariablesInitializations
                                                                              .Count - 1 &&
                                                                          glue
                                                                              .LocalVariablesInitializationFromGlobalVariables
                                                                              .Count == 0
                                                                             ? ""
                                                                             : ", "), false);
                }

                for (int i = 0; i < glue.LocalVariablesInitializationFromGlobalVariables.Count; i++)
                {
                    var localFromGlob = glue.LocalVariablesInitializationFromGlobalVariables[i];
                    localVarNames.Add(localFromGlob.InputLocalVariable);

                    result += GenerateFilesUtils.GetIndentationStr(0, 4,
                        "\"" + localFromGlob.InputLocalVariable + "\": None" +
                        (i == glue.LocalVariablesInitializationFromGlobalVariables.Count - 1 ? "" : ", "), false);
                }

                result += GenerateFilesUtils.GetIndentationStr(0, 4, "}" + (j < gluesWithLocalVars.Count - 1 ? ", " : ""), false);
            }

            result += GenerateFilesUtils.GetIndentationStr(0, 4, "}");


            foreach (var glue in gluesWithLocalVars)
            {
                foreach (var localVar in glue.GlueLocalVariablesInitializations)
                {
                    if (localVar.Consistency == "ROS")
                    {
                        result += GenerateFilesUtils.GetIndentationStr(2, 4, $"self.declare_parameter('{localVar.LocalVarName}')");
                    }
                }
            }
            
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.setListenTarget(\"initTopicListener\")");
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.subscription = self.create_subscription(Log, '/rosout', self.cb__rosout, 10)");
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.lock = threading.Lock()");
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.get_logger().info(\"AOS_TopicListenerServer initialized and subscribed to /rosout\")");
            result += Environment.NewLine;

            Dictionary<string, Dictionary<string, List<GlueLocalVariablesInitialization>>> topicsToListen = new Dictionary<string, Dictionary<string, List<GlueLocalVariablesInitialization>>>();
           
            foreach (RosGlue glue in data.RosGlues.Values)
            {
                foreach (var oLVar in glue.GlueLocalVariablesInitializations)
                {
                    if (!string.IsNullOrEmpty(oLVar.RosTopicPath))
                    {
                        if (!topicsToListen.ContainsKey(oLVar.RosTopicPath))
                        {
                            topicsToListen[oLVar.RosTopicPath] = new Dictionary<string, List<GlueLocalVariablesInitialization>>();
                        }

                        if (!topicsToListen[oLVar.RosTopicPath].ContainsKey(glue.Name))
                        {
                            topicsToListen[oLVar.RosTopicPath][glue.Name] = new List<GlueLocalVariablesInitialization>();
                        }

                        topicsToListen[oLVar.RosTopicPath][glue.Name].Add(oLVar);
                    }
                }
            }

            foreach (var topic in topicsToListen)
            {
                result += GenerateFilesUtils.GetIndentationStr(1, 4, "def cb_" + topic.Key.Replace("/", "_") + "(self, msg):");
                result += GenerateFilesUtils.GetIndentationStr(2, 4, "try:");
                result += GenerateFilesUtils.GetIndentationStr(3, 4, "print(self.listenTargetModule)");
                foreach (var glueTopic in topic.Value)
                {
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "if self.listenTargetModule == \"" + glueTopic.Key + "\":");
                    result += GenerateFilesUtils.GetIndentationStr(4, 4, "if DEBUG:");
                    result += GenerateFilesUtils.GetIndentationStr(5, 4, "print(\"handling topic call:" + glueTopic.Key + "\")");
                    result += GenerateFilesUtils.GetIndentationStr(5, 4, "print(msg)");
                    foreach (var localVar in glueTopic.Value)
                    {
                        result += GenerateFilesUtils.GetIndentationStr(4, 4, "#-----------------------------------------------------------------------");
                        result += GenerateFilesUtils.GetIndentationStr(4, 4, "value = self." + glueTopic.Key + "_get_value_" + localVar.LocalVarName + "(msg)");
                        result += GenerateFilesUtils.GetIndentationStr(4, 4,
                            "self.updateLocalVariableValue(\"" + localVar.LocalVarName + "\", \"" +
                            localVar.Consistency +
                            "\", value)");
                    }
                }

                result += GenerateFilesUtils.GetIndentationStr(2, 4, "except Exception as e:");
                result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerError(str(e), traceback.format_exc(), 'topic " + topic.Key + "')");
                result += Environment.NewLine;
            }

            foreach (var topic in topicsToListen)
            {
                foreach (var glueTopic in topic.Value)
                {
                    foreach (var localVar in glueTopic.Value)
                    {
                        result += GenerateFilesUtils.GetIndentationStr(1, 4, "def " + glueTopic.Key + "_get_value_" + localVar.LocalVarName + "(self, __input):");
                        result += GenerateFilesUtils.GetIndentationStr(2, 4, GetCodeLineWithLocalVarRefference(localVar.AssignmentCode, localVarNames), true, true);
                        result += Environment.NewLine;
                    }
                }
            }

            result += pythonContainer.initialVars();
            result += Environment.NewLine;
            result += pythonContainer.setListenTarget();
            result += Environment.NewLine;
            result += pythonContainer.getListenTarget();

            return result;
        }

        private static string shareClassMashehoKazy(PLPsData data)
        {
            string result = "";
            result += GenerateFilesUtils.GetIndentationStr(1, 0, "class SharedState1:");
            result += GenerateFilesUtils.GetIndentationStr(2, 1, "def __init__(self):");
            result += GenerateFilesUtils.GetIndentationStr(2, 2, "self.goal_reached = False");
            result += GenerateFilesUtils.GetIndentationStr(2, 2, "self.skill_success=False");

            return result;
        }

        private static Dictionary<string, string> GetLocalConstantAssignments(PLPsData data, HashSet<string> constants)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            List<string> tempCodeLines = data.GlobalVariableDeclarations.Select(x => x.DefaultCode)
                .Where(x => !string.IsNullOrEmpty(x)).ToList();
            List<string> codeLines = new List<string>();

            foreach (string codeLine in tempCodeLines)
            {
                codeLines.AddRange(codeLine.Replace("if", "").Replace("else", "").Replace("{", "").Replace("}", "")
                    .Replace(" ", "").Split(";")
                    .Where(x => !string.IsNullOrEmpty(x) && constants.Any(sConst => x.Contains(sConst))).ToList());
            }

            foreach (string line in codeLines)
            {
                string[] bits = line.Split("=");
                if (bits.Length != 2) throw new Exception("unexpected code ('" + line + "')");
                result[bits[0]] = bits[1];
            }

            return result;
        }

        private static string GetAOS_InitEnvironmentFile(PLPsData data)
        {
            string result = "";
            result += GenerateFilesUtils.GetIndentationStr(0, 4, "class AOS_InitEnvironmentFile:");
            result += GenerateFilesUtils.GetIndentationStr(1, 4, "def __init__(self):");
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "pass");

            Dictionary<string, LocalVariableConstant> constants = new Dictionary<string, LocalVariableConstant>();

            foreach (var lConst in data.LocalVariableConstants)
            {
                constants[lConst.Name] = lConst;

                if (!GenerateFilesUtils.IsPrimitiveType(lConst.Type))
                {
                    result += GenerateFilesUtils.GetIndentationStr(2, 4, lConst.Name + " = " + lConst.Type + "()");
                }

                result += GenerateFilesUtils.GetIndentationStr(2, 4, lConst.InitCode, true, true);
                result += GenerateFilesUtils.GetIndentationStr(1, 4, "");
                result += Environment.NewLine;
            }

            Dictionary<string, string> assignments = GetLocalConstantAssignments(data, constants.Select(x => x.Key).ToHashSet<string>());

            foreach (var assignment in assignments)
            {
                string value = "";
                value = GenerateFilesUtils.IsPrimitiveType(constants[assignment.Value].Type)
                    ? assignment.Value
                    : constants[assignment.Value].Type + "ToDict(" + constants[assignment.Value].Name + ")";
                result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.updateGlobalVarLowLevelValue(\"" + assignment.Key + "\"," + value + ")");
            }

            foreach (var anyValueVar in data.AnyValueStateVariableNames)
            {
                bool added = false;

                foreach (var assignment in assignments)
                {
                    added |= assignment.Key.Equals(anyValueVar);
                    if (added) break;
                }

                if (!added)
                {
                    result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.updateGlobalVarLowLevelValue(\"" + anyValueVar + "\", None)");
                }
            }

            result += pythonContainer.updateLocalVars();

            return result;
        }
    }
}
