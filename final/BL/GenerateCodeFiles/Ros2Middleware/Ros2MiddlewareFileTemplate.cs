//
//
//
//
//
// using System.Reflection.Metadata.Ecma335;
// using System.Security.Principal;
// using MongoDB.Bson;
// using System;
// using MongoDB.Bson.Serialization.Attributes;
// using MongoDB.Bson.Serialization.IdGenerators;
// using WebApiCSharp.Services;
// using WebApiCSharp.Models;
// using System.IO;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text.RegularExpressions;
// namespace WebApiCSharp.GenerateCodeFiles
// {
//     public class Ros2MiddlewareFileTemplate
//     {
//         public static string GetPackageFileTargetProjectDependencies(InitializeProject initProj)
//         {
//             string result = "";
//             foreach (string targetPackage in initProj.RosTarget.RosTargetProjectPackages)
//             {
//                 result += GenerateFilesUtils.GetIndentationStr(0, 0,
//                     "<build_depend>" + targetPackage + "</build_depend>");
//                 result += GenerateFilesUtils.GetIndentationStr(0, 0,
//                     "<exec_depend>" + targetPackage + "</exec_depend>");
//             }
//
//             return result;
//         }
//
//   
//         public static string GetPackageFilefoxy(InitializeProject initProj)// CHANGE DEPEND -> <build_depend>roscpp</build_depend> <build_depend>rospy</build_depend> <build_depend>message_generation</build_depend> <build_export_depend>roscpp</build_export_depend> <build_export_depend>rospy</build_export_depend>
//   
//         {
//                 string file = @"<?xml version=""1.0""?>
// <package format=""3"">
//   <name>" + GenerateRos2Middleware.ROS2_MIDDLEWARE_PACKAGE_NAME + @"</name>
//   <version>0.0.0</version>
//   <description>The " + GenerateRos2Middleware.ROS2_MIDDLEWARE_PACKAGE_NAME + @" package</description>
//
//   <maintainer email=""mic@todo.todo"">mic</maintainer>
//
//   <license>TODO</license>
//
//   <buildtool_depend>ament_cmake</buildtool_depend>
// " + GetPackageFileTargetProjectDependencies(initProj) + @"
//
//    <depend>rclpy</depend>
//     <depend>geometry_msgs</depend>
//   <depend>nav2_msgs</depend>
//   <depend>action_msgs</depend>
//     <depend>example_interfaces</depend>
//         <depend>lifecycle_msgs</depend>
//                 <depend>std_msgs</depend>
//                 <depend>datetime</depend>
//                 <depend>traceback</depend>
//                                 <depend>pymongo</depend>
//
//   <export>
//     <build_type>ament_python</build_type>
//   </export>
// </package>";
//                 return file;
//             }
//         
//         public static string GetCMakeListsFilefoxy()
//         {
//             string file = @"
// cmake_minimum_required(VERSION 3.5)
// project(" + GenerateRos2Middleware.ROS2_MIDDLEWARE_PACKAGE_NAME + @")
//
// # Find ROS 2 packages
// find_package(ament_cmake REQUIRED)
// find_package(rclcpp)
// find_package(rclpy REQUIRED)
// find_package(std_msgs REQUIRED)
// find_package(nav2_msgs REQUIRED)
//
// # Export dependencies
// ament_export_dependencies(
//     rclcpp
//     rclpy
//     std_msgs
//     nav2_msgs
// )
//
//
// # Include directories
// include_directories(
//     # include
//     ${ament_INCLUDE_DIRS}
// )
//
//
//
// ament_package()
// ";
//             return file;
//         }
//
//
//         public static string GetSetupFilefoxy(string console_main)
//         {
//             string file = @"
//
// from setuptools import setup
//
// package_name = 'aos_ros2_middleware_auto'
//
// setup(
//     name=package_name,
//     version='0.0.0',
//     packages=[package_name],
//     data_files=[
//         ('share/ament_index/resource_index/packages',
//             ['resource/' + package_name]),
//         ('share/' + package_name, ['package.xml']),
//     ],
//     install_requires=['setuptools'],
//     zip_safe=True,
//     maintainer='michel',
//     maintainer_email='michel1912@github.com',
//     description='TODO: Package description',
//     license='TODO: License declaration',
//     tests_require=['pytest'],
//     entry_points={
//         'console_scripts': [
//             '" + console_main + @"'
//         ],
//     },
// )
// ";
//
//             return file;
//         }
//
//
//         // private static string GetImportsForMiddlewareNode(PLPsData data, InitializeProject initProj)// NO CHANGES NEED
//         // {
//         //     string result = "";
//         //     Dictionary<string, HashSet<string>> unImports = new Dictionary<string, HashSet<string>>();
//         //     List<RosImport> imports = new List<RosImport>();
//         //     Console.WriteLine("22222222222222");     
//         //     foreach (RosGlue glue in data.RosGlues.Values)
//         //     {
//         //         Console.WriteLine("112111111111111");
//         //         Console.WriteLine(glue.RosActionActivation.Imports[0].From);
//         //
//         //         foreach (string item in glue.RosActionActivation.Imports[0].Imports)
//         //         {
//         //             Console.WriteLine(item);   
//         //         }
//         //         foreach (string item in glue.RosActionActivation.Imports[1].Imports)
//         //         {
//         //             Console.WriteLine(item);   
//         //         }
//         //         Console.WriteLine(glue.RosActionActivation.Imports[1].From);
//         //         imports.AddRange(glue.RosActionActivation.Imports);
//         //         Console.WriteLine("113");
//         //
//         //
//         //         foreach (var lVar in glue.GlueLocalVariablesInitializations)
//         //         {
//         //             imports.AddRange(lVar.Imports);
//         //         }
//         //     }
//         //
//         //     foreach (RosImport im in imports)
//         //     {
//         //         im.From = im.From == null ? "" : im.From;
//         //         if (!unImports.ContainsKey(im.From))
//         //         {
//         //             unImports.Add(im.From, new HashSet<string>());
//         //         }
//         //         foreach (string sIm in im.Imports)
//         //         {
//         //             unImports[im.From].Add(sIm);
//         //         }
//         //     }
//         //
//         //     foreach (KeyValuePair<string, HashSet<string>> keyVal in unImports)// NO CHANGES NEED
//         //     {
//         //         string baseS = keyVal.Key.Replace(" ", "").Length == 0 ? "" : "from " + keyVal.Key + " ";
//         //         result += GenerateFilesUtils.GetIndentationStr(0, 0, baseS + "import " + String.Join(",", keyVal.Value));
//         //     }
//         //     return result;
//         // }
//         public static string GetImportsForMiddlewareNode(PLPsData data, InitializeProject initProj)
//         {
//             string result = "";
//             Dictionary<string, HashSet<string>> unImports = new Dictionary<string, HashSet<string>>();
//             List<RosImport> imports = new List<RosImport>();
//             foreach (RosGlue glue in data.RosGlues.Values)
//             {
//                 imports.AddRange(glue.RosServiceActivation.Imports);
//
//                 foreach (var lVar in glue.GlueLocalVariablesInitializations)
//                 {
//                     imports.AddRange(lVar.Imports);
//                 }
//             }
//
//             foreach (RosImport im in imports)
//             {
//                 im.From = im.From == null ? "" : im.From;
//                 if (!unImports.ContainsKey(im.From))
//                 {
//                     unImports.Add(im.From, new HashSet<string>());
//                 }
//
//                 foreach (string sIm in im.Imports)
//                 {
//                     unImports[im.From].Add(sIm);
//                 }
//             }
//
//             foreach (KeyValuePair<string, HashSet<string>> keyVal in unImports)
//             {
//                 string baseS = keyVal.Key.Replace(" ", "").Length == 0 ? "" : "from " + keyVal.Key + " ";
//                 result += GenerateFilesUtils.GetIndentationStr(0, 0,
//                     baseS + "import " + String.Join(",", keyVal.Value));
//             }
//
//             return result;
//         }
//
//         private static string GetLocalVariableTypeClasses(PLPsData data)
//         {
//             string result = "";
//             foreach (LocalVariableTypePLP type in data.LocalVariableTypes)
//             {
//                 result += GenerateFilesUtils.GetIndentationStr(0, 4, "def " + type.TypeName + "ToDict(lt):");
//
//                 List<string> saFields = type.SubFields.Select(x => "\"" + x.FieldName + "\": lt." + x.FieldName).ToList();
//
//                 result += GenerateFilesUtils.GetIndentationStr(1, 4, "return {" + String.Join(", ", saFields) + "}");
//
//                 result += Environment.NewLine;
//
//                 result += GenerateFilesUtils.GetIndentationStr(0, 4, "class " + type.TypeName + ":");
//                 result += GenerateFilesUtils.GetIndentationStr(1, 4, "def __init__(self, " + String.Join(", ", type.SubFields.Select(x => x.FieldName)) + "):");
//                 foreach (LocalVariableCompoundTypeField field in type.SubFields)
//                 {
//                     result += GenerateFilesUtils.GetIndentationStr(2, 4, "self." + field.FieldName + "=" + field.FieldName);
//                 }
//
//                 result += Environment.NewLine;
//
//                 result += GenerateFilesUtils.GetIndentationStr(1, 4, "def __init__(self):");
//                 foreach (LocalVariableCompoundTypeField field in type.SubFields)
//                 {
//                     result += GenerateFilesUtils.GetIndentationStr(2, 4, "self." + field.FieldName + "=None");
//                 }
//             }
//             return result;
//         }
//
//
//         // ros1 
//         // private static string GetListenToMongoDbCommandsInitFunction(PLPsData data)//NEED TO CHECK THIS IN ROS2
//         // {
//         //     string result = "";
//
//         //     result += GenerateFilesUtils.GetIndentationStr(1, 4, "def __init__(self, _topicListener):");
//         //     result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.currentActionSequenceID = 1");
//         //     result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.currentActionFotExecutionId = None");
//         //     result += GenerateFilesUtils.GetIndentationStr(2, 4, "self._topicListener = _topicListener");
//         //     result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.readyToActivate = \"\" ");
//
//         //     foreach (RosGlue glue in data.RosGlues.Values)
//         //     {
//         //         if (glue.RosServiceActivation != null && !string.IsNullOrEmpty(glue.RosServiceActivation.ServiceName))
//         //         {
//         //             result += GenerateFilesUtils.GetIndentationStr(2, 4, "self." + glue.Name + "ServiceName = \"" + glue.RosServiceActivation.ServicePath + "\"");
//         //         }
//         //     }
//
//         //     result += Environment.NewLine;
//
//         //     result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.listen_to_mongodb_commands()");
//         //     return result;
//         // }
//         public static string GetListenToMongoDbCommandsInitFunction(PLPsData data)
//         {
//             if (data == null || data.RosGlues == null || data.RosGlues.Count == 0)
//             {
//                 return @"
// def _init_(self, _topic_listener):
//     self.listen_to_mongodb_commands()";
//             }
//             
//             string result = "";
//
//             result += GenerateFilesUtils.GetIndentationStr(1, 4, "def _init_(self, _topic_listener):");
//             result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.current_action_sequence_id = 1");
//             result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.current_action_for_execution_id = None");
//             result += GenerateFilesUtils.GetIndentationStr(2, 4, "self._topic_listener = _topic_listener");
//             result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.ready_to_activate = \"\" ");
//
//             foreach (RosGlue glue in data.RosGlues.Values)
//             {
//                 if (glue.RosActionActivation != null && !string.IsNullOrEmpty(glue.RosActionActivation.ActionName))
//                 {
//                     result += GenerateFilesUtils.GetIndentationStr(2, 4,
//                         "self." + glue.Name + "_service_name = \"" + glue.RosActionActivation.ActionPath + "\"");
//                 }
//             }
//
//             result += Environment.NewLine;
//             result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.listen_to_mongodb_commands()");
//             return result;
//         }
//
//
//         public static CompoundVarTypePLP GetCompundTypeByName(string compTypeName, PLPsData data)
//         {
//             if (data?.GlobalCompoundTypes == null) return null; //added null checks
//             List<CompoundVarTypePLP> cl = data.GlobalCompoundTypes.Where(x => x.TypeName.Equals(compTypeName)).ToList();
//             return cl.Count == 0 ? null : cl[0];
//         }
//         public static CompoundVarTypePLP_Variable GetCompundVariableByName(CompoundVarTypePLP oComp, string subFields, PLPsData data)
//         {
//             if (oComp == null || data?.GlobalCompoundTypes == null) return null; //added null checks
//
//             string[] bits = subFields.Split(".");
//             if (bits.Length == 0) return null;
//
//             List<CompoundVarTypePLP_Variable> lv = oComp.Variables?.Where(x => x.Name.Equals(bits[0])).ToList();
//             if (lv == null || lv.Count == 0) return null;
//
//             if (bits.Length == 1) return lv[0];
//             bits[0] = "";
//
//             return GetCompundVariableByName(GetCompundTypeByName(lv[0].Type, data),
//                 String.Join(".", bits.Where(x => !String.IsNullOrEmpty(x))), data);
//         }
//
//         private static LocalVariableTypePLP GetUnderlineLocalVariableTypeByVarName(PLPsData data, PLP plp, string variableName)
//         {
//             string underlineTypeName = GetUnderlineLocalVariableNameTypeByVarName(data, plp, variableName);
//             underlineTypeName = underlineTypeName == null ? "" : underlineTypeName;
//             return data.LocalVariableTypes.Where(x => x.TypeName.Equals(underlineTypeName)).FirstOrDefault();
//
//         }
//
//
//         public static string GetUnderlineLocalVariableNameTypeByVarName(PLPsData data, PLP plp, string variableName)
//         {
//             if (data == null)
//             {
//                 throw new ArgumentNullException(nameof(data), "Data cannot be null.");
//             }
//             
//             string[] bits = variableName.Split(".");
//
//             string baseVarName = bits[0] + "." + (bits.Length > 1 ? bits[1] : "");
//             List<GlobalVariableDeclaration> dl = data.GlobalVariableDeclarations
//                 .Where(x => ("state." + x.Name).Equals(baseVarName)).ToList();
//
//             if (dl.Count > 0)
//             {
//                 if (GenerateFilesUtils.IsPrimitiveType(dl[0].Type))
//                 {
//                     return null;
//                 }
//
//                 if (PLPsData.ANY_VALUE_TYPE_NAME.Equals(dl[0].Type))
//                 {
//                     return dl[0].UnderlineLocalVariableType;
//                 }
//
//                 CompoundVarTypePLP comp = GetCompundTypeByName(dl[0].Type, data);
//                 CompoundVarTypePLP_Variable oVar = GetCompundVariableByName(comp, string.Join(".", bits.Skip(1)), data);
//                 return oVar == null || !oVar.Type.Equals(PLPsData.ANY_VALUE_TYPE_NAME)
//                     ? null
//                     : oVar.UnderlineLocalVariableType;
//             }
//             else
//             {
//                 List<GlobalVariableModuleParameter> temp = plp.GlobalVariableModuleParameters
//                     .Where(x => x.Name.Equals(bits[0])).ToList();
//                 if (temp.Count == 0) return null;
//                 CompoundVarTypePLP comp = GetCompundTypeByName(temp[0].Type, data);
//                 CompoundVarTypePLP_Variable oVar = GetCompundVariableByName(comp, string.Join(".", bits.Skip(1)), data);
//                 return oVar == null || !oVar.Type.Equals(PLPsData.ANY_VALUE_TYPE_NAME)
//                     ? null
//                     : oVar.UnderlineLocalVariableType;
//             }
//         }
//         private static string GetHandleModuleFunction(PLPsData data)
//         {
//             string result = "";
//
//             foreach (RosGlue glue in data.RosGlues.Values)
//             {
//                 PLP plp = data.PLPs[glue.Name];
//                 result += GenerateFilesUtils.GetIndentationStr(1, 4, "def handle_" + glue.Name + "(self, params):");
//                 result += GenerateFilesUtils.GetIndentationStr(2, 4, "responseNotByLocalVariables = None");
//                 Dictionary<string, LocalVariablesInitializationFromGlobalVariable> localVarsFromGlobal = new Dictionary<string, LocalVariablesInitializationFromGlobalVariable>();
//
//                 bool hasVar = false;
//
//                 foreach (LocalVariablesInitializationFromGlobalVariable oGlVar in glue.LocalVariablesInitializationFromGlobalVariables)
//                 {
//                     hasVar = true;
//                     result += GenerateFilesUtils.GetIndentationStr(2, 4, oGlVar.InputLocalVariable + " = \"\"");
//                 }
//                 if (hasVar)
//                 {
//                     result += GenerateFilesUtils.GetIndentationStr(2, 4, "try:");
//
//                     foreach (LocalVariablesInitializationFromGlobalVariable oGlVar in glue.LocalVariablesInitializationFromGlobalVariables)
//                     {
//                         LocalVariableTypePLP underlineType = GetUnderlineLocalVariableTypeByVarName(data, plp, oGlVar.FromGlobalVariable);
//
//                         if (underlineType != null)
//                         {
//                             if (oGlVar.FromGlobalVariable.StartsWith(PLPsData.GLOBAL_VARIABLE_STATE_REF))
//                             {
//                                 result += GenerateFilesUtils.GetIndentationStr(3, 4, "globVarName = \"" + oGlVar.FromGlobalVariable + "\"");
//                             }
//                             else
//                             {
//                                 //globVarName = "oDesiredLocation.actual_location".replace("oDesiredLocation", params["ParameterLinks"]["oDesiredLocation"])
//                                 string baseGlobalParameter = plp.GlobalVariableModuleParameters
//                                     .Where(x => oGlVar.FromGlobalVariable.StartsWith(x.Name + ".") || oGlVar.FromGlobalVariable.Equals(x.Name))
//                                     .Select(x => x.Name).FirstOrDefault();
//                                 result += GenerateFilesUtils.GetIndentationStr(3, 4, "globVarName = \"" + oGlVar.FromGlobalVariable + "\".replace(\"" + baseGlobalParameter + "\", params[\"ParameterLinks\"][\"" + baseGlobalParameter + "\"], 1)");
//                             }
//
//                             result += GenerateFilesUtils.GetIndentationStr(3, 4, "dbVar = aos_GlobalVariablesAssignments_collection.find_one({\"GlobalVariableName\": globVarName})");
//                             result += GenerateFilesUtils.GetIndentationStr(3, 4, oGlVar.InputLocalVariable + " = " + underlineType.TypeName + "()");
//                             foreach (LocalVariableCompoundTypeField field in underlineType.SubFields)
//                             {
//                                 //obj_location.z=cupAccurateLocation[""LowLevelValue""][""z""]
//                                 result += GenerateFilesUtils.GetIndentationStr(3, 4, oGlVar.InputLocalVariable + "." + field.FieldName + " = dbVar[\"LowLevelValue\"][\"" + field.FieldName + "\"]");
//                             }
//                             result += GenerateFilesUtils.GetIndentationStr(3, 4, "self._topicListener.localVarNamesAndValues[\"" + glue.Name + "\"][\"" + oGlVar.InputLocalVariable + "\"] = " + underlineType.TypeName + "ToDict(" + oGlVar.InputLocalVariable + ")");
//                         }
//                         else
//                         {
//                             string[] bits = oGlVar.FromGlobalVariable.Split(".");
//                             string varDesc = "[\"" + String.Join("\"][\"", bits) + "\"]";
//                             result += GenerateFilesUtils.GetIndentationStr(3, 4, oGlVar.InputLocalVariable + " = params[\"ParameterValues\"]" + varDesc);
//
//                             result += GenerateFilesUtils.GetIndentationStr(3, 4, "self._topicListener.updateLocalVariableValue(\"" + oGlVar.InputLocalVariable + "\", \"" + oGlVar.Consistency + "\", " + oGlVar.InputLocalVariable + ")");
//                  
//                         }
//                       
//                     }
//                     result += GenerateFilesUtils.GetIndentationStr(2, 4, "except Exception as e:");
//                     result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerError(str(e), traceback.format_exc(e), 'Action: "+glue.Name+", illegalActionObs')");
//                     result += GenerateFilesUtils.GetIndentationStr(3, 4, "responseNotByLocalVariables = \"illegalActionObs\"");
//                 }
//
//                 if (glue.RosServiceActivation != null && !string.IsNullOrEmpty(glue.RosServiceActivation.ServiceName))
//                 {
//                     result += GenerateFilesUtils.GetIndentationStr(2, 4, "try:");
//                     result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerLog(\"wait for service: moduleName:"+glue.Name+", serviceName:"+glue.RosServiceActivation.ServiceName+"\")");
//                     result += GenerateFilesUtils.GetIndentationStr(3, 4, "rospy.wait_for_service(self." + glue.Name + "ServiceName)");
//                     result += GenerateFilesUtils.GetIndentationStr(3, 4, glue.Name + "_proxy = rospy.ServiceProxy(self." + glue.Name + "ServiceName, " + glue.RosServiceActivation.ServiceName + ")");
//
//                     string serviceCallParam = string.Join(", ", glue.RosServiceActivation.ParametersAssignments.Select(x => x.MsgFieldName + "=(" + x.AssignServiceFieldCode + ")"));
//
//                     result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerLog(\"Sending request to service, moduleName:"+ glue.Name+"\")");
//                     result += GenerateFilesUtils.GetIndentationStr(3, 4, "__input = " + glue.Name + "_proxy(" + serviceCallParam + ")");
//                     GlueLocalVariablesInitialization localVarFromServiceReponse = glue.GlueLocalVariablesInitializations.Where(x => x.FromROSServiceResponse.HasValue && x.FromROSServiceResponse.Value).FirstOrDefault();
//                     result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerLog(\"Service response received, moduleName:"+ glue.Name+"\")");
//                     if (localVarFromServiceReponse != null)
//                     {
//                         result += GenerateFilesUtils.GetIndentationStr(3, 4, localVarFromServiceReponse.AssignmentCode, true, true);
//                         result += GenerateFilesUtils.GetIndentationStr(3, 4, "self._topicListener.updateLocalVariableValue(\"" + localVarFromServiceReponse.LocalVarName + "\"," + localVarFromServiceReponse.LocalVarName + ")");
//                     }
//
//
//                     result += GenerateFilesUtils.GetIndentationStr(3, 4, "if DEBUG:");
//                     result += GenerateFilesUtils.GetIndentationStr(4, 4, "print(\"" + glue.Name + " service terminated\")");
//                     result += GenerateFilesUtils.GetIndentationStr(2, 4, "except Exception as e:");
//                     result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerError(str(e),traceback.format_exc(e),'Action: "+glue.Name+"')");
//                     result += GenerateFilesUtils.GetIndentationStr(3, 4, "print(\"Service call failed\")");
//                     result += GenerateFilesUtils.GetIndentationStr(2, 4, "");
//                 }
//
//
//                 result += GenerateFilesUtils.GetIndentationStr(2, 4, "return responseNotByLocalVariables");
//
//             }
//             return result;
//         }
//
//         private static string GetListenToMongoCommandsFunctionPart(PLPsData data)// SELF._TOPICLISTENER MAYBE NEED TO BE CHANGED FROM ROS1 TO ROS2 DIFFRENET COMMAND
//         {
//             string result = "";
//             foreach (PLP plp in data.PLPs.Values)
//             {
//                 result += GenerateFilesUtils.GetIndentationStr(4, 4, "if moduleName == \"" + plp.Name + "\":");
//                 result += GenerateFilesUtils.GetIndentationStr(5, 4, "print(\"handle " + plp.Name + "\")");
//                 result += GenerateFilesUtils.GetIndentationStr(5, 4, "responseNotByLocalVariables = self.handle_" + plp.Name + "(actionParameters)");
//             }
//             return result;
//         }
//         private static string GetModuleResponseFunctionPart(PLPsData data)
//         {
//             string result = "";
//             foreach (RosGlue glue in data.RosGlues.Values)
//             {
//                 PLP plp = data.PLPs[glue.Name];
//                 result += GenerateFilesUtils.GetIndentationStr(2, 4, "if moduleName == \"" + glue.Name + "\":");
//                 HashSet<string> localVarNames = new HashSet<string>();
//                 foreach (var oVar in glue.GlueLocalVariablesInitializations)
//                 {
//                     localVarNames.Add(oVar.LocalVarName);
//                     result += GenerateFilesUtils.GetIndentationStr(3, 4, oVar.LocalVarName+ " = self._topicListener.localVarNamesAndValues[\"" + glue.Name + "\"][\"" + oVar.LocalVarName + "\"]");
//                 }
//                 foreach (var oVar in glue.LocalVariablesInitializationFromGlobalVariables)
//                 {
//                     localVarNames.Add(oVar.InputLocalVariable);
//                     result += GenerateFilesUtils.GetIndentationStr(3, 4, oVar.InputLocalVariable + " = self._topicListener.localVarNamesAndValues[\"" + glue.Name + "\"][\"" + oVar.InputLocalVariable + "\"]");
//                 }
//
//                 result += GenerateFilesUtils.GetIndentationStr(3, 4, "if DEBUG:");
//                 result += GenerateFilesUtils.GetIndentationStr(4, 4, "print(\"" + glue.Name + " action local variables:\")");
//                 foreach (var oVar in glue.GlueLocalVariablesInitializations)
//                 {
//                     if(!oVar.IsHeavyVariable)
//                     {
//                         result += GenerateFilesUtils.GetIndentationStr(4, 4, "print(\"" + oVar.LocalVarName + ":\")");
//                         result += GenerateFilesUtils.GetIndentationStr(4, 4, "print(" + oVar.LocalVarName + ")");
//                     }
//                 }
//
//                 if(glue.ResponseRules.Count == 0)
//                 {
//                     result += GenerateFilesUtils.GetIndentationStr(3, 4, "if moduleResponse == \"\":");
//                     result += GenerateFilesUtils.GetIndentationStr(4, 4, "moduleResponse = \"DefaultObservation\"");
//                 }
//                 foreach (var responseRule in glue.ResponseRules)
//                 {
//                     result += GenerateFilesUtils.GetIndentationStr(3, 4, "if moduleResponse == \"\" and " + (string.IsNullOrEmpty(responseRule.Condition) ? "True" : responseRule.Condition) + ":");
//                     result += GenerateFilesUtils.GetIndentationStr(4, 4, "moduleResponse = \"" + glue.Name + "_" + responseRule.Response + "\"");
//                 }
//
//                 foreach (var responseRule in glue.ResponseRules)
//                 {
//                     foreach (var assign in responseRule.ResponseAssignmentsToGlobalVar)
//                     {
//                         result += GenerateFilesUtils.GetIndentationStr(3, 4, "if moduleResponse == \"" + glue.Name + "_" + responseRule.Response + "\":");
//                         result += GenerateFilesUtils.GetIndentationStr(4, 4, "assignGlobalVar[\"" + assign.GlobalVarName + "\"] = " + assign.Value);
//                     }
//                 }
//               Console.WriteLine("ssssssssssssssssssssssss");
//                 if(!String.IsNullOrEmpty(glue.ResponseFromStringLocalVariable))
//                 {
//                     result += GenerateFilesUtils.GetIndentationStr(3, 4, "moduleResponse = str(" + glue.ResponseFromStringLocalVariable + ")");
//                 }
//                 result += Environment.NewLine;
//             }
//             return result;
//         }
//
//         private static string GetCodeLineWithLocalVarRefference(string codeLine, HashSet<string> localVarNames)
//         {
//             string result = codeLine;
//             foreach (string varName in localVarNames.OrderByDescending(x => x.Length))
//             {
//                 string pattern = @"\b(?<!\.)" + varName + @"\b";
//                 string replaceTo = "self.localVarNamesAndValues[self.listenTargetModule][\"" + varName + "\"]";
//                 result = Regex.Replace(result, pattern, replaceTo);
//             }
//             return result;
//         }
//         private static string GetAOS_TopicListenerServerClass(PLPsData data)
// {
//     string result = "";
//     result += GenerateFilesUtils.GetIndentationStr(0, 4, "class AOS_TopicListenerServer:");
//     result += GenerateFilesUtils.GetIndentationStr(1, 4, "def __init__(self):");
//     result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.localVarNamesAndValues = {", false);
//
//     List<RosGlue> gluesWithLocalVars = data.RosGlues.Values
//         .Where(x => x.LocalVariablesInitializationFromGlobalVariables.Count > 0 || x.GlueLocalVariablesInitializations.Count > 0)
//         .ToList();
//
//     HashSet<string> localVarNames = new HashSet<string>();
//     foreach (var glue in gluesWithLocalVars)
//     {
//         result += GenerateFilesUtils.GetIndentationStr(0, 4, "\"" + glue.Name + "\":{", false);
//
//         foreach (var localVar in glue.GlueLocalVariablesInitializations)
//         {
//             localVarNames.Add(localVar.LocalVarName);
//             result += GenerateFilesUtils.GetIndentationStr(0, 4, "\"" + localVar.LocalVarName + "\": " +
//                 (string.IsNullOrEmpty(localVar.InitialValue) ? "None" : localVar.InitialValue) +
//                 (localVar == glue.GlueLocalVariablesInitializations.Last() && glue.LocalVariablesInitializationFromGlobalVariables.Count == 0 ? "" : ", "), false);
//         }
//
//         foreach (var localFromGlob in glue.LocalVariablesInitializationFromGlobalVariables)
//         {
//             localVarNames.Add(localFromGlob.InputLocalVariable);
//             result += GenerateFilesUtils.GetIndentationStr(0, 4, "\"" + localFromGlob.InputLocalVariable + "\": None" +
//                 (localFromGlob == glue.LocalVariablesInitializationFromGlobalVariables.Last() ? "" : ", "), false);
//         }
//
//         result += GenerateFilesUtils.GetIndentationStr(0, 4, "}" + (glue != gluesWithLocalVars.Last() ? ", " : ""), false);
//     }
//
//     result += GenerateFilesUtils.GetIndentationStr(0, 4, "}");
//     result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.setListenTarget(\"initTopicListener\")");
//
//     // Replace ROS 1 with ROS 2 dependencies and functions
//
//     Dictionary<string, Dictionary<string, List<GlueLocalVariablesInitialization>>> topicsToListen = new Dictionary<string, Dictionary<string, List<GlueLocalVariablesInitialization>>>();
//     foreach (var glue in data.RosGlues.Values)
//     {
//         foreach (var oLVar in glue.GlueLocalVariablesInitializations)
//         {
//             if (!string.IsNullOrEmpty(oLVar.RosTopicPath))
//             {
//                 string cbFunc = "cb_" + oLVar.RosTopicPath.Replace("/", "_");
//                 if (!topicsToListen.ContainsKey(oLVar.RosTopicPath))
//                 {
//                     topicsToListen[oLVar.RosTopicPath] = new Dictionary<string, List<GlueLocalVariablesInitialization>>();
//                 }
//                 if (!topicsToListen[oLVar.RosTopicPath].ContainsKey(glue.Name))
//                 {
//                     topicsToListen[oLVar.RosTopicPath][glue.Name] = new List<GlueLocalVariablesInitialization>();
//                 }
//                 topicsToListen[oLVar.RosTopicPath][glue.Name].Add(oLVar);
//             }
//         }
//     }
//
//     foreach (var topic in topicsToListen)
//     {
//         result += GenerateFilesUtils.GetIndentationStr(2, 4, "rclpy.create_subscription(\"" + topic.Key + "\", " + topic.Value.Values.ToList()[0][0].TopicMessageType + ", self.cb_" + topic.Key.Replace("/", "_") + ", 1000)");
//
//     }
//     result += Environment.NewLine;
//
//     foreach (var topic in topicsToListen)
//     {
//         result += GenerateFilesUtils.GetIndentationStr(1, 4, "def cb_" + topic.Key.Replace("/", "_") + "(self, data):");
//         result += GenerateFilesUtils.GetIndentationStr(2, 4, "try:");
//         foreach (var glueTopic in topic.Value)
//         {
//             result += GenerateFilesUtils.GetIndentationStr(3, 4, "if self.listenTargetModule == \"" + glueTopic.Key + "\":");
//             result += GenerateFilesUtils.GetIndentationStr(4, 4, "if DEBUG:");
//             result += GenerateFilesUtils.GetIndentationStr(5, 4, "print(\"handling topic call:" + glueTopic.Key + "\")");
//             result += GenerateFilesUtils.GetIndentationStr(5, 4, "print(data)");
//             foreach (var localVar in glueTopic.Value)
//             {
//                 result += GenerateFilesUtils.GetIndentationStr(4, 4, "#-----------------------------------------------------------------------");
//                 result += GenerateFilesUtils.GetIndentationStr(4, 4, "value = self." + glueTopic.Key + "_get_value_" + localVar.LocalVarName + "(data)");
//                 result += GenerateFilesUtils.GetIndentationStr(4, 4, "self.updateLocalVariableValue(\"" + localVar.LocalVarName + "\", value)");
//             }
//         }
//         result += GenerateFilesUtils.GetIndentationStr(2, 4, "except Exception as e:");
//         result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerError(str(e), traceback.format_exc(e), 'topic "+topic.Key+"')");
//
//
//         result += Environment.NewLine;
//     }
//
//
//
//     foreach (var topic in topicsToListen)
//     {
//         foreach (var glueTopic in topic.Value)
//         {
//             foreach (var localVar in glueTopic.Value)
//             {
//                 result += GenerateFilesUtils.GetIndentationStr(1, 4, "def " + glueTopic.Key + "_get_value_" + localVar.LocalVarName + "(self, __input):");
//                 result += GenerateFilesUtils.GetIndentationStr(2, 4, GetCodeLineWithLocalVarRefference(localVar.AssignmentCode, localVarNames), true, true);
//
//                 result += Environment.NewLine;
//             }
//         }
//     }
//
//     result += @"
// def initLocalVars(self, moduleNameToInit):
//     if DEBUG:
//         print(""initLocalVars:"")
//         print(moduleNameToInit)
//     for moduleName, localVarNamesAndValuesPerModule in self.localVarNamesAndValues.items():
//         for localVarName, value in localVarNamesAndValuesPerModule.items():
//             if moduleName == moduleNameToInit:
//                 if DEBUG:
//                     print (""init var:"")
//                     print(localVarName)
//                 aos_local_var_collection.replace_one({""Module"": moduleName, ""VarName"": localVarName},
//                                                      {""Module"": moduleName, ""VarName"": localVarName, ""Value"": value},
//                                                      upsert=True)
//                 aosStats_local_var_collection.insert_one(
//                     {""Module"": moduleName, ""VarName"": localVarName, ""value"": value, ""Time"": datetime.datetime.utcnow()})
//
//
// def setListenTarget(self, _listenTargetModule):
//     self.initLocalVars(_listenTargetModule)
//     if DEBUG:
//         print('setListenTopicTargetModule:')
//         print(_listenTargetModule)
//     self.listenTargetModule = _listenTargetModule
// ";
//     Dictionary<string, List<GlueLocalVariablesInitialization>> rosParamVariables = new Dictionary<string, List<GlueLocalVariablesInitialization>>();
//     foreach (var glue in data.RosGlues.Values)
//     {
//         List<GlueLocalVariablesInitialization> glueParamVariables = glue.GlueLocalVariablesInitializations
//             .Where(x => !string.IsNullOrEmpty(x.RosParameterPath)).ToList();
//         if (glueParamVariables.Count > 0)
//         {
//             rosParamVariables.Add(glue.Name, glueParamVariables);
//         }
//     }
//
//     foreach (var glueRosParamLocalVars in rosParamVariables)
//     {
//         result += GenerateFilesUtils.GetIndentationStr(2, 4, "if self.listenTargetModule == \"" + glueRosParamLocalVars.Key + "\":");
//         foreach (var localParam in glueRosParamLocalVars.Value)
//         {
//             result += GenerateFilesUtils.GetIndentationStr(3, 4, "self.checkParameterValue_" + localParam.LocalVarName + "()");
//         }
//     }
//
//
//
//     foreach (var glueRosParamLocalVars in rosParamVariables)
//     {
//         foreach (var localParam in glueRosParamLocalVars.Value)
//         {
//             result += GenerateFilesUtils.GetIndentationStr(1, 4, "def checkParameterValue_" + localParam.LocalVarName + "(self):#TODO:: need to see how to update ROS parameters. using threading disable other topic listeners");
//             result += GenerateFilesUtils.GetIndentationStr(2, 4, "if self.listenTargetModule == \"" + glueRosParamLocalVars.Key + "\":");
//             result += GenerateFilesUtils.GetIndentationStr(3, 4, "try:");
//             result += GenerateFilesUtils.GetIndentationStr(4, 4, "#__input = rospy.get_param('" + localParam.RosParameterPath + "')");
//             result += GenerateFilesUtils.GetIndentationStr(4, 4, "#" + localParam.LocalVarName + " = __input");
//             result += GenerateFilesUtils.GetIndentationStr(4, 4, "#self.updateLocalVariableValue(\"" + localParam.LocalVarName + "\", " + localParam.LocalVarName + ")");
//             result += GenerateFilesUtils.GetIndentationStr(4, 4, "self.updateLocalVariableValue(\"" + localParam.LocalVarName + "\", True)");
//             result += GenerateFilesUtils.GetIndentationStr(3, 4, "except:");
//             result += GenerateFilesUtils.GetIndentationStr(4, 4, "pass");
//             result += GenerateFilesUtils.GetIndentationStr(3, 4, "#threading.Timer(1, self.checkParameterValue_" + localParam.LocalVarName + ").start()");
//         }
//     }
//
//     result += @"
//
//     def updateLocalVariableValue(self, varName, value):
//         if DEBUG and varName not in getHeavyLocalVarList(self.listenTargetModule):
//             print(""update local var:"")
//             print(varName)
//             print(value)
//         if self.listenTargetModule not in self.localVarNamesAndValues:
//             return
//         if self.localVarNamesAndValues[self.listenTargetModule][varName] != value:
//             if DEBUG:
//                 print(""ACTUAL UPDATE --------------------------------------------------------------------------"")
//             self.localVarNamesAndValues[self.listenTargetModule][varName]=value
//             if varName not in getHeavyLocalVarList(self.listenTargetModule):
//                 aos_local_var_collection.replace_one({""Module"": self.listenTargetModule, ""VarName"":varName}, {""Module"": self.listenTargetModule, ""VarName"":varName, ""Value"":value}, upsert=True)
//                 aosStats_local_var_collection.insert_one(
//                     {""Module"": self.listenTargetModule, ""VarName"": varName, ""value"": value, ""Time"": datetime.datetime.utcnow()})
//                 if DEBUG:
//                     print(""WAS UPDATED --------------------------------------------------------------------------"")
//
// ";
//     return result;
// }
//
//
// //         private static string GetAOS_TopicListenerServerClass(PLPsData data)// NEED TO CHANGE TO RCLPY AND CHECK IF WE NEED ANOTHER ROS2 CHANGES
// //         {
// //             string result = "";
// //             result += GenerateFilesUtils.GetIndentationStr(0, 4, "class AOS_TopicListenerServer:");
// //             result += GenerateFilesUtils.GetIndentationStr(1, 4, "def __init__(self):");
// //             result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.localVarNamesAndValues = {", false);
//
// //             List<RosGlue> gluesWithLocalVars = data.RosGlues.Values.Where(x => x.LocalVariablesInitializationFromGlobalVariables.Count > 0 || x.GlueLocalVariablesInitializations.Count > 0).ToList();
//
// //             HashSet<string> localVarNames = new HashSet<string>();
// //             for (int j = 0; gluesWithLocalVars.Count > j; j++)
// //             {
// //                 RosGlue glue = gluesWithLocalVars[j];
//
// //                 result += GenerateFilesUtils.GetIndentationStr(0, 4, "\"" + glue.Name + "\":{", false);
//
// //                 for (int i = 0; glue.GlueLocalVariablesInitializations.Count > i; i++)
// //                 {
// //                     var localVar = glue.GlueLocalVariablesInitializations[i];
// //                     localVarNames.Add(localVar.LocalVarName);
// //                     result += GenerateFilesUtils.GetIndentationStr(0, 4, "\"" + localVar.LocalVarName + "\": " +
// //                             (string.IsNullOrEmpty(localVar.InitialValue) ? "None" : localVar.InitialValue) +
// //                             (i == glue.GlueLocalVariablesInitializations.Count - 1 && glue.LocalVariablesInitializationFromGlobalVariables.Count == 0 ? "" : ", "), false);
// //                 }
// //                 for (int i = 0; i < glue.LocalVariablesInitializationFromGlobalVariables.Count; i++)
// //                 {
// //                     var localFromGlob = glue.LocalVariablesInitializationFromGlobalVariables[i];
// //                     localVarNames.Add(localFromGlob.InputLocalVariable);
// //                     result += GenerateFilesUtils.GetIndentationStr(0, 4, "\"" + localFromGlob.InputLocalVariable + "\": None" +
// //                             (i == glue.LocalVariablesInitializationFromGlobalVariables.Count - 1 ? "" : ", "), false);
// //                 }
// //                 result += GenerateFilesUtils.GetIndentationStr(0, 4, "}" + (j < gluesWithLocalVars.Count - 1 ? ", " : ""), false);
// //             }
// //             result += GenerateFilesUtils.GetIndentationStr(0, 4, "}");
// //             result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.setListenTarget(\"initTopicListener\")");
//
//
// //             Dictionary<string, Dictionary<string, List<GlueLocalVariablesInitialization>>> topicsToListen = new Dictionary<string, Dictionary<string, List<GlueLocalVariablesInitialization>>>();
// //             foreach (RosGlue glue in data.RosGlues.Values)
// //             {
// //                 foreach (var oLVar in glue.GlueLocalVariablesInitializations)
// //                 {
// //                     if (!string.IsNullOrEmpty(oLVar.RosTopicPath))
// //                     {
// //                         string cbFunc = "cb_" + oLVar.RosTopicPath.Replace("/", "_");
// //                         if (!topicsToListen.ContainsKey(oLVar.RosTopicPath))
// //                         {
// //                             topicsToListen[oLVar.RosTopicPath] = new Dictionary<string, List<GlueLocalVariablesInitialization>>();
// //                         }
// //                         if (!topicsToListen[oLVar.RosTopicPath].ContainsKey(glue.Name))
// //                         {
// //                             topicsToListen[oLVar.RosTopicPath][glue.Name] = new List<GlueLocalVariablesInitialization>();
// //                         }
// //                         topicsToListen[oLVar.RosTopicPath][glue.Name].Add(oLVar);
// //                     }
// //                 }
// //             }
//
// //             foreach (var topic in topicsToListen)
// //             {
// //                 result += GenerateFilesUtils.GetIndentationStr(2, 4, "rospy.Subscriber(\"" + topic.Key + "\", " + topic.Value.Values.ToList()[0][0].TopicMessageType + ", self.cb_" + topic.Key.Replace("/", "_") + ", queue_size=1000)");
//
// //             }
// //             result += Environment.NewLine;
//
// //             foreach (var topic in topicsToListen)
// //             {
// //                 result += GenerateFilesUtils.GetIndentationStr(1, 4, "def cb_" + topic.Key.Replace("/", "_") + "(self, data):");
// //                 result += GenerateFilesUtils.GetIndentationStr(2, 4, "try:");
// //                 foreach (var glueTopic in topic.Value)
// //                 {
// //                     result += GenerateFilesUtils.GetIndentationStr(3, 4, "if self.listenTargetModule == \"" + glueTopic.Key + "\":");
// //                     result += GenerateFilesUtils.GetIndentationStr(4, 4, "if DEBUG:");
// //                     result += GenerateFilesUtils.GetIndentationStr(5, 4, "print(\"handling topic call:" + glueTopic.Key + "\")");
// //                     result += GenerateFilesUtils.GetIndentationStr(5, 4, "print(data)");
// //                     foreach (var localVar in glueTopic.Value)
// //                     {
// //                         result += GenerateFilesUtils.GetIndentationStr(4, 4, "#-----------------------------------------------------------------------");
// //                         result += GenerateFilesUtils.GetIndentationStr(4, 4, "value = self." + glueTopic.Key + "_get_value_" + localVar.LocalVarName + "(data)");
// //                         result += GenerateFilesUtils.GetIndentationStr(4, 4, "self.updateLocalVariableValue(\"" + localVar.LocalVarName + "\", value)");
// //                     }
// //                 }
// //                 result += GenerateFilesUtils.GetIndentationStr(2, 4, "except Exception as e:");
// //                 result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerError(str(e), traceback.format_exc(e), 'topic "+topic.Key+"')");
//
//
// //                 result += Environment.NewLine;
// //             }
//
//
//
// //             foreach (var topic in topicsToListen)
// //             {
// //                 foreach (var glueTopic in topic.Value)
// //                 {
// //                     foreach (var localVar in glueTopic.Value)
// //                     {
// //                         result += GenerateFilesUtils.GetIndentationStr(1, 4, "def " + glueTopic.Key + "_get_value_" + localVar.LocalVarName + "(self, __input):");
// //                         result += GenerateFilesUtils.GetIndentationStr(2, 4, GetCodeLineWithLocalVarRefference(localVar.AssignmentCode, localVarNames), true, true);
//
// //                         result += Environment.NewLine;
// //                     }
// //                 }
// //             }
//
// //             result += @"
// //     def initLocalVars(self, moduleNameToInit):
// //         if DEBUG:
// //             print(""initLocalVars:"")
// //             print(moduleNameToInit)
// //         for moduleName, localVarNamesAndValuesPerModule in self.localVarNamesAndValues.items():
// //             for localVarName, value in localVarNamesAndValuesPerModule.items():
// //                 if moduleName == moduleNameToInit:
// //                     if DEBUG:
// //                         print (""init var:"")
// //                         print(localVarName)
// //                     aos_local_var_collection.replace_one({""Module"": moduleName, ""VarName"": localVarName},
// //                                                          {""Module"": moduleName, ""VarName"": localVarName, ""Value"": value},
// //                                                          upsert=True)
// //                     aosStats_local_var_collection.insert_one(
// //                         {""Module"": moduleName, ""VarName"": localVarName, ""value"": value, ""Time"": datetime.datetime.utcnow()})
//
//
// //     def setListenTarget(self, _listenTargetModule):
// //         self.initLocalVars(_listenTargetModule)
// //         if DEBUG:
// //             print('setListenTopicTargetModule:')
// //             print(_listenTargetModule)
// //         self.listenTargetModule = _listenTargetModule
// // ;
// //             Dictionary<string, List<GlueLocalVariablesInitialization>> rosParamVariables = new Dictionary<string, List<GlueLocalVariablesInitialization>>();
// //             foreach (RosGlue glue in data.RosGlues.Values)
// //             {
// //                 List<GlueLocalVariablesInitialization> glueParamVariables = glue.GlueLocalVariablesInitializations.Where(x => !string.IsNullOrEmpty(x.RosParameterPath)).ToList();
// //                 if (glueParamVariables.Count > 0)
// //                 {
// //                     rosParamVariables.Add(glue.Name, glueParamVariables);
// //                 }
// //             }
//
// //             foreach (var glueRosParamLocalVars in rosParamVariables)
// //             {
// //                 result += GenerateFilesUtils.GetIndentationStr(2, 4, "if self.listenTargetModule == \"" + glueRosParamLocalVars.Key + "\":");
// //                 foreach (var localParam in glueRosParamLocalVars.Value)
// //                 {
// //                     result += GenerateFilesUtils.GetIndentationStr(3, 4, "self.checkParameterValue_" + localParam.LocalVarName + "()");
// //                 }
// //             }
//
//
//
// //             foreach (var glueRosParamLocalVars in rosParamVariables)
// //             {
// //                 foreach (var localParam in glueRosParamLocalVars.Value)
// //                 {
// //                     result += GenerateFilesUtils.GetIndentationStr(1, 4, "def checkParameterValue_" + localParam.LocalVarName + "(self):#TODO:: need to see how to update ROS parameters. using threading disable other topic listeners");
// //                     result += GenerateFilesUtils.GetIndentationStr(2, 4, "if self.listenTargetModule == \"" + glueRosParamLocalVars.Key + "\":");
// //                     result += GenerateFilesUtils.GetIndentationStr(3, 4, "try:");
// //                     result += GenerateFilesUtils.GetIndentationStr(4, 4, "#__input = rospy.get_param('" + localParam.RosParameterPath + "')");
// //                     result += GenerateFilesUtils.GetIndentationStr(4, 4, "#" + localParam.LocalVarName + " = __input");
// //                     result += GenerateFilesUtils.GetIndentationStr(4, 4, "#self.updateLocalVariableValue(\"" + localParam.LocalVarName + "\", " + localParam.LocalVarName + ")");
// //                     result += GenerateFilesUtils.GetIndentationStr(4, 4, "self.updateLocalVariableValue(\"" + localParam.LocalVarName + "\", True)");
// //                     result += GenerateFilesUtils.GetIndentationStr(3, 4, "except:");
// //                     result += GenerateFilesUtils.GetIndentationStr(4, 4, "pass");
// //                     result += GenerateFilesUtils.GetIndentationStr(3, 4, "#threading.Timer(1, self.checkParameterValue_" + localParam.LocalVarName + ").start()");
// //                 }
// //             }
//
//
//
//
// //             result += @"
//
// //     def updateLocalVariableValue(self, varName, value):
// //         if DEBUG and varName not in getHeavyLocalVarList(self.listenTargetModule):
// //             print(""update local var:"")
// //             print(varName)
// //             print(value)
// //         if self.listenTargetModule not in self.localVarNamesAndValues:
// //             return
// //         if self.localVarNamesAndValues[self.listenTargetModule][varName] != value:
// //             if DEBUG:
// //                 print(""ACTUAL UPDATE --------------------------------------------------------------------------"")
// //             self.localVarNamesAndValues[self.listenTargetModule][varName]=value
// //             if varName not in getHeavyLocalVarList(self.listenTargetModule):
// //                 aos_local_var_collection.replace_one({""Module"": self.listenTargetModule, ""VarName"":varName}, {""Module"": self.listenTargetModule, ""VarName"":varName, ""Value"":value}, upsert=True)
// //                 aosStats_local_var_collection.insert_one(
// //                     {""Module"": self.listenTargetModule, ""VarName"": varName, ""value"": value, ""Time"": datetime.datetime.utcnow()})
// //                 if DEBUG:
// //                     print(""WAS UPDATED --------------------------------------------------------------------------"")
//
// // ";
// //             return result;
// //         }
//
//
//     private static string GetHeavyLocalVariablesList(PLPsData data)
//     {
//         string result = "";;
//         foreach(PLP plp in data.PLPs.Values)
//         {
//             string plpHeavyVars = string.Join<String>(",", data.LocalVariablesListings.Where(x=>x.IsHeavyVariable && x.SkillName == plp.Name).Select(x=> "\"" + x.VariableName + "\"" ).ToArray());
//             if (plpHeavyVars.Length > 0)
//             {
//                 result += (result.Length > 0 ? ", " : "") + "\"" + plp.Name + "\" : [" + plpHeavyVars + "]";
//             }
//         } 
//         return "HEAVY_LOCAL_VARS={" + result + "}";
//     }
//     
//     // from rclpy.node import Node
//     // from geometry_msgs.msg import Point
//     // from nav2_msgs.action import NavigateToPose
//     // from rcl_interfaces.msg import Log
//     // from interfaces_robot.srv import NavigateToCoordinates
//         private static string GetHandleModuleFunctionV2(PLPsData data)
// {
//     string result = "";
//
//     foreach (RosGlue glue in data.RosGlues.Values)
//     {
//         PLP plp = data.PLPs[glue.Name];
//         result += GenerateFilesUtils.GetIndentationStr(1, 4, "def handle_" + glue.Name + "(self, params):");
//         result += GenerateFilesUtils.GetIndentationStr(2, 4, "responseNotByLocalVariables = None");
//
//         // Initialize local variables
//         foreach (LocalVariablesInitializationFromGlobalVariable oGlVar in glue.LocalVariablesInitializationFromGlobalVariables)
//         {
//             result += GenerateFilesUtils.GetIndentationStr(2, 4, oGlVar.InputLocalVariable + " = \"\"");
//         }
//
//         // Try block for local variable assignment
//         if (glue.LocalVariablesInitializationFromGlobalVariables.Count > 0)
//         {
//             result += GenerateFilesUtils.GetIndentationStr(2, 4, "try:");
//
//             foreach (LocalVariablesInitializationFromGlobalVariable oGlVar in glue.LocalVariablesInitializationFromGlobalVariables)
//             {
//                 LocalVariableTypePLP underlineType = GetUnderlineLocalVariableTypeByVarName(data, plp, oGlVar.FromGlobalVariable);
//
//                 if (underlineType != null)
//                 {
//                     if (oGlVar.FromGlobalVariable.StartsWith(PLPsData.GLOBAL_VARIABLE_STATE_REF))
//                     {
//                         result += GenerateFilesUtils.GetIndentationStr(3, 4, "globVarName = \"" + oGlVar.FromGlobalVariable + "\"");
//                     }
//                     else
//                     {
//                         string baseGlobalParameter = plp.GlobalVariableModuleParameters
//                             .Where(x => oGlVar.FromGlobalVariable.StartsWith(x.Name + ".") || oGlVar.FromGlobalVariable.Equals(x.Name))
//                             .Select(x => x.Name).FirstOrDefault();
//                         result += GenerateFilesUtils.GetIndentationStr(3, 4, "globVarName = \"" + oGlVar.FromGlobalVariable + "\".replace(\"" + baseGlobalParameter + "\", params[\"ParameterLinks\"][\"" + baseGlobalParameter + "\"], 1)");
//                     }
//
//                     result += GenerateFilesUtils.GetIndentationStr(3, 4, "dbVar = aos_GlobalVariablesAssignments_collection.find_one({\"GlobalVariableName\": globVarName})");
//                     result += GenerateFilesUtils.GetIndentationStr(3, 4, oGlVar.InputLocalVariable + " = " + underlineType.TypeName + "()");
//                     foreach (LocalVariableCompoundTypeField field in underlineType.SubFields)
//                     {
//                         result += GenerateFilesUtils.GetIndentationStr(3, 4, oGlVar.InputLocalVariable + "." + field.FieldName + " = dbVar[\"LowLevelValue\"][\"" + field.FieldName + "\"]");
//                     }
//                     result += GenerateFilesUtils.GetIndentationStr(3, 4, "self._topicListener.localVarNamesAndValues[\"" + glue.Name + "\"][\"" + oGlVar.InputLocalVariable + "\"] = " + underlineType.TypeName + "ToDict(" + oGlVar.InputLocalVariable + ")");
//                 }
//                 else
//                 {
//                     string[] bits = oGlVar.FromGlobalVariable.Split(".");
//                     string varDesc = "[\"" + String.Join("\"][\"", bits) + "\"]";
//                     result += GenerateFilesUtils.GetIndentationStr(3, 4, oGlVar.InputLocalVariable + " = params[\"ParameterValues\"]" + varDesc);
//                     result += GenerateFilesUtils.GetIndentationStr(3, 4, "self._topicListener.updateLocalVariableValue(\"" + oGlVar.InputLocalVariable + "\", \"" + oGlVar.Consistency + "\", " + oGlVar.InputLocalVariable + ")");
//                 }
//             }
//             result += GenerateFilesUtils.GetIndentationStr(2, 4, "except Exception as e:");
//             result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerError(str(e), traceback.format_exc(), 'Action: " + glue.Name + ", illegalActionObs')");
//             result += GenerateFilesUtils.GetIndentationStr(3, 4, "responseNotByLocalVariables = \"illegalActionObs\"");
//             result += GenerateFilesUtils.GetIndentationStr(3, 4, "return responseNotByLocalVariables");
//         }
//
//         // Try block for service activation
//         if (glue.RosServiceActivation != null && !string.IsNullOrEmpty(glue.RosServiceActivation.ServiceName))
//         {
//             result += GenerateFilesUtils.GetIndentationStr(2, 4, "try:");
//             result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerLog(\"wait for service: moduleName=" + glue.Name + ", serviceName=" + glue.RosServiceActivation.ServiceName + "\")");
//             result += GenerateFilesUtils.GetIndentationStr(3, 4, "req = NavigateToCoordinates.Request()");
//             result += GenerateFilesUtils.GetIndentationStr(3, 4, "req.x = float(nav_to_x)");
//             result += GenerateFilesUtils.GetIndentationStr(3, 4, "req.y = float(nav_to_y)");
//             result += GenerateFilesUtils.GetIndentationStr(3, 4, "req.z = float(nav_to_z)");
//             result += GenerateFilesUtils.GetIndentationStr(3, 4, "self.get_logger().info(\"Sending request to service, moduleName=" + glue.Name + "\")");
//             result += GenerateFilesUtils.GetIndentationStr(3, 4, "future = self.cli.call_async(req)");
//             result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerLog(\"Service call made, waiting for response\")");
//             result += GenerateFilesUtils.GetIndentationStr(3, 4, "future.add_done_callback(self.navigate_callback)");
//
//             result += GenerateFilesUtils.GetIndentationStr(3, 4, "while not self._topicListener.localVarNamesAndValues[\"" + glue.Name + "\"][\"goal_reached\"]:");
//             result += GenerateFilesUtils.GetIndentationStr(4, 4, "rclpy.spin_once(self._topicListener, timeout_sec=0.1)");
//             result += GenerateFilesUtils.GetIndentationStr(3, 4, "time.sleep(0.1)");
//
//             result += GenerateFilesUtils.GetIndentationStr(2, 4, "except Exception as e:");
//             result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerError(str(e), traceback.format_exc(), 'Action: " + glue.Name + "')");
//             result += GenerateFilesUtils.GetIndentationStr(3, 4, "print(\"Service call failed\")");
//         }
//
//         result += GenerateFilesUtils.GetIndentationStr(2, 4, "return responseNotByLocalVariables");
//
//         // Adding the callback function
//         result += "\n";
//         result += GenerateFilesUtils.GetIndentationStr(1, 4, "def navigate_callback(self, future):");
//         result += GenerateFilesUtils.GetIndentationStr(2, 4, "try:");
//         result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerLog(\"navigate_callback invoked\")");
//         result += GenerateFilesUtils.GetIndentationStr(3, 4, "result = future.result()");
//         result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerLog(\"Future result obtained\")");
//         result += GenerateFilesUtils.GetIndentationStr(3, 4, "if result is not None:");
//         result += GenerateFilesUtils.GetIndentationStr(4, 4, "self.get_logger().info(\"Service response received, moduleName=navigate\")");
//         result += GenerateFilesUtils.GetIndentationStr(4, 4, "skillSuccess = result.success");
//         result += GenerateFilesUtils.GetIndentationStr(4, 4, "registerLog(f\"Service response success: {skillSuccess}\")");
//         result += GenerateFilesUtils.GetIndentationStr(4, 4, "self._topicListener.updateLocalVariableValue(\"skillSuccess\", \"DB\", skillSuccess)");
//         result += GenerateFilesUtils.GetIndentationStr(4, 4, "if DEBUG:");
//         result += GenerateFilesUtils.GetIndentationStr(5, 4, "print(\"navigate service terminated\")");
//         result += GenerateFilesUtils.GetIndentationStr(3, 4, "else:");
//         result += GenerateFilesUtils.GetIndentationStr(4, 4, "self.get_logger().error(\"Service call failed, result is None\")");
//         result += GenerateFilesUtils.GetIndentationStr(4, 4, "registerLog(\"Service call failed, result is None\")");
//         result += GenerateFilesUtils.GetIndentationStr(2, 4, "except Exception as e:");
//         result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerError(str(e), traceback.format_exc(), 'Action: navigate')");
//         result += GenerateFilesUtils.GetIndentationStr(3, 4, "print(\"Service call failed\")");
//     }
//
//     return result;
// }
//         private static string GetListenToMongoDbCommandsInitFunctionV2(PLPsData data)
//         {
//             string result = "";
//
//             result += GenerateFilesUtils.GetIndentationStr(1, 0, "def __init__(self, topic_listener, shared_state):");
//             result += GenerateFilesUtils.GetIndentationStr(2, 4, "super().__init__('listen_to_mongodb_commands')");
//             result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.current_action_sequence_id = 1");
//             result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.current_action_for_execution_id = None");
//             result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.shared_state = shared_state");
//             result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.navigate_service_name = \"/navigate_to_coordinates\"");
//             result += GenerateFilesUtils.GetIndentationStr(2, 4, "self._topicListener = topic_listener");
//             result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.cli = self.create_client(NavigateToCoordinates, self.navigate_service_name)");
//
//             foreach (RosGlue glue in data.RosGlues.Values)
//             {
//                 if (glue.RosServiceActivation != null && !string.IsNullOrEmpty(glue.RosServiceActivation.ServiceName))
//                 {
//                     result += GenerateFilesUtils.GetIndentationStr(2, 4, "self." + glue.Name + "_service_name = \"" + glue.RosServiceActivation.ServicePath + "\"");
//                 }
//             }
//
//             result += GenerateFilesUtils.GetIndentationStr(2, 4, "while not self.cli.wait_for_service(timeout_sec=1.0):");
//             result += GenerateFilesUtils.GetIndentationStr(3, 4, "self.get_logger().info('Service not available, waiting again...')");
//
//             result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.timer = self.create_timer(1.0, self.listen_to_mongodb_commands)");
//
//             return result;
//         }
//
//         // print(""1111111111111111111111111111111"")
//         // aos_local_var_collection1.replace_one({""Module"": ""MIC"", ""VarName"": ""STAM""},
//         // {""Module"": ""MIC"", ""VarName"":  ""STAM"", ""Value"": ""2""}, upsert=True)
//         // aosStats_local_var_collection1.insert_one(
//         // {""Module"": ""MIC"", ""VarName"": ""STAM"", ""value"": ""2"", ""Time"": datetime.datetime.utcnow()})
//         // print(""22222222222222222222222222222"")
// public static string GetAosRos2MiddlewareNodeFile(PLPsData data, InitializeProject initProj)
// {
//     string pythonVersion = "python3"; // ROS2 supports only Python 3
//     string file = @"#!/usr/bin/" + pythonVersion + @"
// import datetime
// import rclpy
// import pymongo
// import traceback
// import operator
// import time
//
// " + GetImportsForMiddlewareNode(data, initProj) + @"
//
//
// DEBUG = " + (initProj.MiddlewareConfiguration.DebugOn ? "True" : "False") + Environment.NewLine +
// GetHeavyLocalVariablesList(data) + @"
//
// aosDbConnection = pymongo.MongoClient(""mongodb://localhost:27017/"")
// aosDB = aosDbConnection[""AOS""]
// aos_statisticsDB = aosDbConnection[""AOS_Statistics""]
// aos_local_var_collection = aosDB[""LocalVariables""]
// aos_local_var_collection1 = aosDB[""LocalVariablesDB""]
// aosStats_local_var_collection1 = aos_statisticsDB[""LocalVariablesDB""]
//
// aosStats_local_var_collection = aos_statisticsDB[""LocalVariables""]
// aos_GlobalVariablesAssignments_collection = aosDB[""GlobalVariablesAssignments""]
// aos_ModuleResponses_collection = aosDB[""ModuleResponses""]
// collActionForExecution = aosDB[""ActionsForExecution""]
// collLogs = aosDB[""Logs""]
// collActions = aosDB[""Actions""]
//
//
//
// def registerError(errorStr, trace, comments=None):
//     error = {
//         ""Component"": ""RosMiddleware"", ""Event"": errorStr, ""Advanced"": trace, ""LogLevel"": 2, ""LogLevelDesc"": ""Error"",
//         ""Time"": datetime.datetime.utcnow()
//     }
//     if comments is not None:
//         error = {
//             ""Component"": ""RosMiddleware"", ""Error"": errorStr, ""Advanced"": str(comments) + "". "" + str(trace),
//             ""Time"": datetime.datetime.utcnow()
//         }
//     collLogs.insert_one(error)
//
// def registerLog(logStr):
//     log = {
//         ""Component"": ""RosMiddleware"", ""Event"": logStr, ""LogLevel"": 5, ""LogLevelDesc"": ""Debug"", ""Advanced"": """",
//         ""Time"": datetime.datetime.utcnow()
//     }
//     collLogs.insert_one(log)
//
// def getHeavyLocalVarList(moduleName):
//     return HEAVY_LOCAL_VARS.get(moduleName, [])
//
//
//
// " + GetLocalVariableTypeClasses(data) + @"
//
// class ListenToMongoDbCommands(Node):
//     " + GetListenToMongoDbCommandsInitFunctionV2(data) + @"
//
// " + GetHandleModuleFunctionV2(data) + @"
//     
//
//     def saveHeavyLocalVariableToDB(self, moduleName):
//         for varName in getHeavyLocalVarList(moduleName):
//             value = self._topicListener.localVarNamesAndValues[moduleName][varName]
//             aos_local_var_collection.replace_one({""Module"": moduleName, ""VarName"": varName},
//                                                  {""Module"": moduleName, ""VarName"": varName,
//                                                   ""Value"": value}, upsert=True)
//             aosStats_local_var_collection.insert_one(
//                 {""Module"": moduleName, ""VarName"": varName, ""value"": value,
//                  ""Time"": datetime.datetime.utcnow()})
//
//
//
//
//     def registerModuleResponse(self, moduleName, startTime, actionSequenceID, responseNotByLocalVariables):
//         registerLog(""in the function registerModuleResponse:::::::: "")
//         self._topicListener.initLocalVars(moduleName)  # Ensure initialization
//         registerLog(""in the function registerModuleResponse222222222222:::::::: "")
//
//         self.saveHeavyLocalVariableToDB(moduleName)
//         registerLog(""in the function registerModuleResponse333333333333333:::::::: "")
//
//         filter1 = {""ActionSequenceId"": actionSequenceID}
//         # if DEBUG:
//             # print(""registerModuleResponse()"")
//
//         if responseNotByLocalVariables is not None:
//             moduleResponseItem = {""Module"": moduleName, ""ActionSequenceId"": actionSequenceID,
//                                   ""ModuleResponseText"": responseNotByLocalVariables, ""StartTime"": startTime,
//                                   ""EndTime"": datetime.datetime.utcnow(),
//                                   ""ActionForExecutionId"": self.current_action_for_execution_id}
//             aos_ModuleResponses_collection.replace_one(filter1, moduleResponseItem, upsert=True)
//             return
//         registerLog(""in the function registerModuleResponse444444444:::::::: "")
//         # time.sleep(2)
//         moduleResponse = """"
//         assignGlobalVar = {}
//
//
// " + GetModuleResponseFunctionPartV2(data) + @"
//
//
//
//
//         registerLog(""this is the response of the navigation : ""+moduleResponse)
//         moduleLocalVars = self._topicListener.localVarNamesAndValues.get(moduleName, {})
//         moduleResponseItem = {""Module"": moduleName, ""ActionSequenceId"": actionSequenceID,
//                               ""ModuleResponseText"": moduleResponse, ""StartTime"": startTime, ""EndTime"": datetime.datetime.utcnow(),
//                               ""ActionForExecutionId"": self.current_action_for_execution_id,
//                               ""LocalVariables"": moduleLocalVars}
//
//         aos_ModuleResponses_collection.replace_one(filter1, moduleResponseItem, upsert=True)
//         for varName, value in assignGlobalVar.items():
//             isInit = value is not None
//             aos_GlobalVariablesAssignments_collection.replace_one({""GlobalVariableName"": varName},
//                                                                   {""GlobalVariableName"": varName, ""LowLevelValue"": value,
//                                                                    ""IsInitialized"": isInit, ""UpdatingActionSequenceId"": actionSequenceID,
//                                                                    ""ModuleResponseId"": moduleResponseItem[""_id""]}, upsert=True)
//
//     def listen_to_mongodb_commands(self):
//         filter1 = {""ActionSequenceId"": self.current_action_sequence_id}
//         actionForExecution = collActionForExecution.find_one(filter1)
//         if actionForExecution:
//             if DEBUG:
//                 print(""~~"")
//                 print(""actionID:"", actionForExecution[""ActionID""])
//             moduleName = actionForExecution[""ActionName""]
//             actionParameters = actionForExecution[""Parameters""]
//             self.current_action_for_execution_id = actionForExecution[""_id""]
//             registerLog(""navigate start with id :::"" + str(self.current_action_sequence_id))
//             self._topicListener.setListenTarget(moduleName)
//             time.sleep(0.3)
//             moduleActivationStart = datetime.datetime.utcnow()
//             responseNotByLocalVariables = None
//             print(""module name:"", moduleName)
//             registerLog(""Request to call to module: "" + moduleName)
//             registerLog(""navigate start:"")
//
//
// " + GetListenToMongoCommandsFunctionPartV2(data) + @"
//             time.sleep(0.3)
//             self._topicListener.setListenTarget(""after action"")
//             # while not self._topicListener.localVarNamesAndValues[""navigate""][""goal_reached""]:
//             #               rclpy.spin_once(self._topicListener, timeout_sec=0.1)
//  
//             registerLog(""after while loop of goal reached""+ str(self._topicListener.localVarNamesAndValues[""navigate""][""goal_reached""]))    
//             self.registerModuleResponse(moduleName, moduleActivationStart, self.current_action_sequence_id,
//                                         responseNotByLocalVariables)
//             if DEBUG:
//                 print(""navigate finished"")
//             self.current_action_sequence_id += 1
//             self.currentActionFotExecutionId = None
//     time.sleep(0.1)
//
//         
// " + GetAOS_TopicListenerServerClassV2(data) + @"
//
// " + shareClassMashehoKazy(data) + @"
//
//
//
// def main(args=None):
//     rclpy.init(args=args)
//     shared_state = SharedState1()
//     topic_listener = AOS_TopicListenerServer(shared_state)
//     command_listener = ListenToMongoDbCommands(topic_listener,shared_state)
//
//     topic_listener.get_logger().info(""Nodes initialized and command_listener is about to spin"")
//     executor = MultiThreadedExecutor()
//     executor.add_node(topic_listener)
//     executor.add_node(command_listener)
//     try:
//         rclpy.spin(command_listener)  # Only spin the command_listener node
//     except KeyboardInterrupt:
//         pass
//     finally:
//         executor.shutdown()
//         command_listener.destroy_node()
//         topic_listener.destroy_node()
//         rclpy.shutdown()
//
//
//
//
// ";
//     return file;
// }
//
// public static List<LocalVariablesInitializationFromGlobalVariable> checkConsistencyInDB(PLPsData data)
// {
//     List<LocalVariablesInitializationFromGlobalVariable> resultList = new List<LocalVariablesInitializationFromGlobalVariable>();
//
//     foreach (RosGlue glue in data.RosGlues.Values)
//     {
//         foreach (var oVar in glue.GlueLocalVariablesInitializations)
//         {
//             if (oVar.Consistency == "DB")
//             {
//                 string moduleName = glue.Name;
//                 string varName = oVar.LocalVarName;
//
//                 // Create an instance of LocalVariablesInitializationFromGlobalVariable
//                 LocalVariablesInitializationFromGlobalVariable localVar = new LocalVariablesInitializationFromGlobalVariable
//                 {
//                     VariableName = varName,
//                     Consistency = oVar.Consistency,
//                 };
//
//                 // Add the variable info to the result list
//                 resultList.Add(localVar);
//             }
//         }
//     }
//
//     return resultList;
// }
//
//
//
// private static string GetModuleResponseFunctionPartV2(PLPsData data)
// {
//     string result = "";
//
//     foreach (RosGlue glue in data.RosGlues.Values)
//     {
//         PLP plp = data.PLPs[glue.Name];
//         result += GenerateFilesUtils.GetIndentationStr(2, 4, "if moduleName == \"" + glue.Name + "\":");
//         result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerLog(\"in the function registerModuleResponse for " + glue.Name + ":::::::: \")");
//
//         HashSet<string> localVarNames = new HashSet<string>();
//         foreach (var oVar in glue.GlueLocalVariablesInitializations)
//         {
//             localVarNames.Add(oVar.LocalVarName);
//             result += GenerateFilesUtils.GetIndentationStr(3, 4, oVar.LocalVarName + " = self._topicListener.localVarNamesAndValues[\"" + glue.Name + "\"][\"" + oVar.LocalVarName + "\"]");
//             result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerLog(\"" + oVar.LocalVarName + ":  \"  +  str(" + oVar.LocalVarName + "))");
//         }
//         foreach (var oVar in glue.LocalVariablesInitializationFromGlobalVariables)
//         {
//             localVarNames.Add(oVar.InputLocalVariable);
//             result += GenerateFilesUtils.GetIndentationStr(3, 4, oVar.InputLocalVariable + " = self._topicListener.localVarNamesAndValues[\"" + glue.Name + "\"][\"" + oVar.InputLocalVariable + "\"]");
//         }
//
//         result += GenerateFilesUtils.GetIndentationStr(3, 4, "nav_to_x = self._topicListener.localVarNamesAndValues[\"" + glue.Name + "\"][\"nav_to_x\"]");
//         result += GenerateFilesUtils.GetIndentationStr(3, 4, "nav_to_y = self._topicListener.localVarNamesAndValues[\"" + glue.Name + "\"][\"nav_to_y\"]");
//         result += GenerateFilesUtils.GetIndentationStr(3, 4, "nav_to_z = self._topicListener.localVarNamesAndValues[\"" + glue.Name + "\"][\"nav_to_z\"]");
//
//         result += GenerateFilesUtils.GetIndentationStr(3, 4, "if " + string.Join(" and ", localVarNames.Select(varName => varName)) + ":");
//         result += GenerateFilesUtils.GetIndentationStr(4, 4, "moduleResponse = \"" + glue.Name + "_eSuccess\"");
//         result += GenerateFilesUtils.GetIndentationStr(3, 4, "else:");
//         result += GenerateFilesUtils.GetIndentationStr(4, 4, "moduleResponse = \"" + glue.Name + "_eFailed\"");
//         
//         result += Environment.NewLine;
//     }
//
//     return result;
// }
// private static string GetListenToMongoCommandsFunctionPartV2(PLPsData data)
// {
//     string result = "";
//     foreach (PLP plp in data.PLPs.Values)
//     {
//         result += GenerateFilesUtils.GetIndentationStr(4, 3, "if moduleName == \"" + plp.Name + "\":");
//         result += GenerateFilesUtils.GetIndentationStr(5, 4, "print(\"handle " + plp.Name + "\")");
//         result += GenerateFilesUtils.GetIndentationStr(5, 4, "responseNotByLocalVariables = self.handle_" + plp.Name + "(actionParameters)");
//         result += GenerateFilesUtils.GetIndentationStr(5, 4, "registerLog(\"" + plp.Name + " finished:\")");
//     }
//     return result;
// }
//
//      private static string GetAOS_TopicListenerServerClassV2(PLPsData data)
// {
//     string result = "";
//
//      result += GenerateFilesUtils.GetIndentationStr(0, 4, "class AOS_TopicListenerServer(Node):");
//     result += GenerateFilesUtils.GetIndentationStr(1, 4, "def __init__(self, shared_state):");
//     result += GenerateFilesUtils.GetIndentationStr(2, 4, "super().__init__('aos_topic_listener_server')");
//     result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.shared_state = shared_state");
//     result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.localVarNamesAndValues = {", false);
//
//     // Initialize local variables
//     List<RosGlue> gluesWithLocalVars = data.RosGlues.Values
//         .Where(x => x.LocalVariablesInitializationFromGlobalVariables.Count > 0 || x.GlueLocalVariablesInitializations.Count > 0)
//         .ToList();
//
//     for (int j = 0; j < gluesWithLocalVars.Count; j++)
//     {
//         RosGlue glue = gluesWithLocalVars[j];
//         result += GenerateFilesUtils.GetIndentationStr(0, 4, "\"" + glue.Name + "\":{", false);
//
//         for (int i = 0; i < glue.GlueLocalVariablesInitializations.Count; i++)
//         {
//             var localVar = glue.GlueLocalVariablesInitializations[i];
//             result += GenerateFilesUtils.GetIndentationStr(0, 4, "\"" + localVar.LocalVarName + "\": " +
//                     (string.IsNullOrEmpty(localVar.InitialValue) ? "None" : localVar.InitialValue) +
//                     (i == glue.GlueLocalVariablesInitializations.Count - 1 && glue.LocalVariablesInitializationFromGlobalVariables.Count == 0 ? "" : ", "), false);
//         }
//         for (int i = 0; i < glue.LocalVariablesInitializationFromGlobalVariables.Count; i++)
//         {
//             var localFromGlob = glue.LocalVariablesInitializationFromGlobalVariables[i];
//             result += GenerateFilesUtils.GetIndentationStr(0, 4, "\"" + localFromGlob.InputLocalVariable + "\": None" +
//                     (i == glue.LocalVariablesInitializationFromGlobalVariables.Count - 1 ? "" : ", "), false);
//         }
//         result += GenerateFilesUtils.GetIndentationStr(0, 4, "}" + (j < gluesWithLocalVars.Count - 1 ? ", " : ""), false);
//     }
//     result += GenerateFilesUtils.GetIndentationStr(0, 4, "}");
//     result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.setListenTarget(\"initTopicListener\")");
//     result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.subscription = self.create_subscription(Log, '/rosout', self.cb__rosout, 10)");
//     result += GenerateFilesUtils.GetIndentationStr(2, 4, "print(\"LOLO\")");
//     result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.get_logger().info(\"AOS_TopicListenerServer initialized and subscribed to /rosout\")");
//     result += Environment.NewLine;
//
//     // Topic callbacks
//     Dictionary<string, Dictionary<string, List<GlueLocalVariablesInitialization>>> topicsToListen = new Dictionary<string, Dictionary<string, List<GlueLocalVariablesInitialization>>>();
//     foreach (RosGlue glue in data.RosGlues.Values)
//     {
//         foreach (var oLVar in glue.GlueLocalVariablesInitializations)
//         {
//             if (!string.IsNullOrEmpty(oLVar.RosTopicPath))
//             {
//                 if (!topicsToListen.ContainsKey(oLVar.RosTopicPath))
//                 {
//                     topicsToListen[oLVar.RosTopicPath] = new Dictionary<string, List<GlueLocalVariablesInitialization>>();
//                 }
//                 if (!topicsToListen[oLVar.RosTopicPath].ContainsKey(glue.Name))
//                 {
//                     topicsToListen[oLVar.RosTopicPath][glue.Name] = new List<GlueLocalVariablesInitialization>();
//                 }
//                 topicsToListen[oLVar.RosTopicPath][glue.Name].Add(oLVar);
//             }
//         }
//     }
//
//     foreach (var topic in topicsToListen)
//     {
//         result += GenerateFilesUtils.GetIndentationStr(1, 4, "def cb_" + topic.Key.Replace("/", "_") + "(self, msg):");
//         result += GenerateFilesUtils.GetIndentationStr(2, 4, "try:");
//         result += GenerateFilesUtils.GetIndentationStr(3, 4, "print(self.listenTargetModule)");
//         foreach (var glueTopic in topic.Value)
//         {
//             result += GenerateFilesUtils.GetIndentationStr(3, 4, "if self.listenTargetModule == \"" + glueTopic.Key + "\":");
//             result += GenerateFilesUtils.GetIndentationStr(4, 4, "if DEBUG:");
//             result += GenerateFilesUtils.GetIndentationStr(5, 4, "print(\"handling topic call:" + glueTopic.Key + "\")");
//             result += GenerateFilesUtils.GetIndentationStr(5, 4, "print(msg)");
//             foreach (var localVar in glueTopic.Value)
//             {
//                 result += GenerateFilesUtils.GetIndentationStr(4, 4, "#-----------------------------------------------------------------------");
//                 result += GenerateFilesUtils.GetIndentationStr(4, 4, "value = self." + glueTopic.Key + "_get_value_" + localVar.LocalVarName + "(msg)");
//                 result += GenerateFilesUtils.GetIndentationStr(4, 4, "self.updateLocalVariableValue(\"" + localVar.LocalVarName + "\", \""+ localVar.Consistency+"\", value)");
//                 Console.WriteLine(localVar.Consistency+"  coooooooooooooooooooooooooooooooooooooooooooooooooooooooo");
//                 result += GenerateFilesUtils.GetIndentationStr(4, 4, "self.updateLocalVariableValue(\"skillSuccess\", DB, value)"); // Should be changed if needed
//             }
//         }
//         result += GenerateFilesUtils.GetIndentationStr(2, 4, "except Exception as e:");
//         result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerError(str(e), traceback.format_exc(), 'topic " + topic.Key + "')");
//         result += Environment.NewLine;
//     }
//
//     // Helper methods for local variables
//     foreach (var topic in topicsToListen)
//     {
//         foreach (var glueTopic in topic.Value)
//         {
//             foreach (var localVar in glueTopic.Value)
//             {
//                 result += GenerateFilesUtils.GetIndentationStr(1, 4, "def " + glueTopic.Key + "_get_value_" + localVar.LocalVarName + "(self, __input):");
//                 result += GenerateFilesUtils.GetIndentationStr(2, 4, "if 'Goal has been reached.' in __input.msg:");
//                 result += GenerateFilesUtils.GetIndentationStr(3, 4, "self.shared_state.goal_reached = True");
//                 result += GenerateFilesUtils.GetIndentationStr(3, 4, "self.skill_success = True"); // Should be changed if needed
//                 result += GenerateFilesUtils.GetIndentationStr(3, 4, "return True");
//                 result += GenerateFilesUtils.GetIndentationStr(2, 4, "return False");
//                 result += Environment.NewLine;
//             }
//         }
//     }
//
//     // initLocalVars method
//     result += @"
//     def initLocalVars(self, moduleNameToInit):
//         for moduleName, localVarNamesAndValuesPerModule in self.localVarNamesAndValues.items():
//             for localVarName, value in localVarNamesAndValuesPerModule.items():
//                 if moduleName == moduleNameToInit:
//                     aos_local_var_collection.replace_one({""Module"": moduleName, ""VarName"": localVarName},
//                                                          {""Module"": moduleName, ""VarName"": localVarName, ""Value"": value},
//                                                          upsert=True)
//                     aosStats_local_var_collection.insert_one(
//                         {""Module"": moduleName, ""VarName"": localVarName, ""value"": value, ""Time"": datetime.datetime.utcnow()})
// ";
//     result += Environment.NewLine;
//
//     // setListenTarget method
//     result += @"
//     def setListenTarget(self, _listenTargetModule):
//         self.initLocalVars(_listenTargetModule)
//         if DEBUG:
//             print('setListenTopicTargetModule:')
//             print(_listenTargetModule)
//         self.listenTargetModule = _listenTargetModule
// ";
//     result += Environment.NewLine;
//
//     // updateLocalVariableValue method
//     result += @"
//     def updateLocalVariableValue(self, varName, Consistency, value):
//      if DEBUG and varName not in getHeavyLocalVarList(self.listenTargetModule):
//         print(""update local var:"")
//         print(varName)
//         print(value)
//
//      if self.listenTargetModule not in self.localVarNamesAndValues:
//         return
//
//      current_value = self.localVarNamesAndValues[self.listenTargetModule].get(varName)
//
//      if current_value != value:
//         if DEBUG:
//             print(""ACTUAL UPDATE --------------------------------------------------------------------------"")
//
//         self.localVarNamesAndValues[self.listenTargetModule][varName] = value
//
//         if varName not in getHeavyLocalVarList(self.listenTargetModule):
//             if Consistency == ""DB"":
//                 # Save to statistics database
//                 aos_local_var_collection1.replace_one(
//                     {""Module"": self.listenTargetModule, ""VarName"": varName},
//                     {""Module"": self.listenTargetModule, ""VarName"": varName, ""value"": value, ""Time"": datetime.datetime.utcnow()},
//                     upsert=True
//                 )
//                 aosStats_local_var_collection1.insert_one(
//                         {""Module"": self.listenTargetModule, ""VarName"": varName, ""value"": value, ""Time"": datetime.datetime.utcnow()})
//                 if DEBUG:
//                     print(""WAS UPDATED IN DB -------------------------------------------------------------------"")
//
//             # Always save to main database
//             aos_local_var_collection.replace_one(
//                 {""Module"": self.listenTargetModule, ""VarName"": varName},
//                 {""Module"": self.listenTargetModule, ""VarName"": varName, ""Value"": value},
//                 upsert=True
//             )
//             if DEBUG:
//                 print(""WAS UPDATED --------------------------------------------------------------------------"")
//
// ";
//     return result;
// }
//
//    
// private static string shareClassMashehoKazy(PLPsData data)
// {
//     string result = "";
//
//     result += GenerateFilesUtils.GetIndentationStr(1, 0, "class SharedState1:");
//     result += GenerateFilesUtils.GetIndentationStr(2, 1, "def __init__(self):");
//     result += GenerateFilesUtils.GetIndentationStr(2, 2, "self.goal_reached = False");
//     result += GenerateFilesUtils.GetIndentationStr(2, 2, "self.skill_success=False");
//     return result;
// }
//
//
//         private static Dictionary<string, string> GetLocalConstantAssignments(PLPsData data, HashSet<string> constants)
//         {
//             Dictionary<string, string> result = new Dictionary<string, string>();
//             List<string> tempCodeLines = data.GlobalVariableDeclarations.Select(x => x.DefaultCode).Where(x => !string.IsNullOrEmpty(x)).ToList();
//             List<string> codeLines = new List<string>();
//             foreach (string codeLine in tempCodeLines)
//             {
//                 codeLines.AddRange(codeLine.Replace("if", "").Replace("else", "").Replace("{", "").Replace("}", "").Replace(" ", "").Split(";")
//                     .Where(x => !string.IsNullOrEmpty(x) && constants.Any(sConst => x.Contains(sConst))).ToList());
//             }
//
//             foreach (string line in codeLines)
//             {
//                 string[] bits = line.Split("=");
//                 if (bits.Length != 2) throw new Exception("unexpected code ('" + line + "')");
//                 result[bits[0]] = bits[1];
//             }
//
//
//
//
//             return result;
//         }
//
//         private static string GetAOS_InitEnvironmentFile(PLPsData data)
//         {
//             string result = "";
//             result += GenerateFilesUtils.GetIndentationStr(0, 4, "class AOS_InitEnvironmentFile:");
//             result += GenerateFilesUtils.GetIndentationStr(1, 4, "def __init__(self):");
//             result += GenerateFilesUtils.GetIndentationStr(2, 4, "pass");
//
//             Dictionary<string, LocalVariableConstant> constants = new Dictionary<string, LocalVariableConstant>();
//             foreach (var lConst in data.LocalVariableConstants)
//             {
//                 constants[lConst.Name] = lConst;
//                 if (!GenerateFilesUtils.IsPrimitiveType(lConst.Type))
//                 {
//                     result += GenerateFilesUtils.GetIndentationStr(2, 4, lConst.Name + " = " + lConst.Type + "()");
//                 }
//                 result += GenerateFilesUtils.GetIndentationStr(2, 4, lConst.InitCode, true, true);
//                 result += GenerateFilesUtils.GetIndentationStr(1, 4, "");
//                 result += Environment.NewLine;
//             }
//
//             Dictionary<string, string> assignments = GetLocalConstantAssignments(data, constants.Select(x => x.Key).ToHashSet<string>());
//             foreach (var assignment in assignments)
//             {
//                 string value = "";
//                 value = GenerateFilesUtils.IsPrimitiveType(constants[assignment.Value].Type) ? assignment.Value : constants[assignment.Value].Type + "ToDict(" + constants[assignment.Value].Name + ")";
//                 result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.updateGlobalVarLowLevelValue(\"" + assignment.Key + "\"," + value + ")");
//             }
//
//             foreach (var anyValueVar in data.AnyValueStateVariableNames)
//             {
//                 bool added = false;
//                 foreach (var assignment in assignments)
//                 {
//                     added |= assignment.Key.Equals(anyValueVar);
//                     if (added) break;
//                 }
//                 if (!added)
//                 {
//                     result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.updateGlobalVarLowLevelValue(\"" + anyValueVar + "\", None)");
//                 }
//             }
//
//
//             result += @"
//     def updateGlobalVarLowLevelValue(self, varName, value):
//         isInit = value is not None
//         aos_GlobalVariablesAssignments_collection.replace_one({""GlobalVariableName"": varName},{""GlobalVariableName"": varName, ""LowLevelValue"": value,
//                                                                                                ""IsInitialized"": isInit, ""UpdatingActionSequenceId"": ""initialization"",
//                                                                                                ""ModuleResponseId"": ""initialization""},upsert=True)
//
//
// ";
//             return result;
//         }
//     }
// }
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
        private static string GetPackageFileTargetProjectDependencies(InitializeProject initProj)
        {
            string result = "";
            foreach (string targetPackage in initProj.RosTarget.RosTargetProjectPackages)
            {
                result += GenerateFilesUtils.GetIndentationStr(0, 0, "<build_depend>" + targetPackage + "</build_depend>");
                result += GenerateFilesUtils.GetIndentationStr(0, 0, "<exec_depend>" + targetPackage + "</exec_depend>");
            }
            return result;
        }
        public static string GetPackageFilefoxy(InitializeProject initProj)// CHANGE DEPEND -> <build_depend>roscpp</build_depend> <build_depend>rospy</build_depend> <build_depend>message_generation</build_depend> <build_export_depend>roscpp</build_export_depend> <build_export_depend>rospy</build_export_depend>
  
        {
                string file = @"<?xml version=""1.0""?>
<package format=""3"">
  <name>" + GenerateRos2Middleware.ROS2_MIDDLEWARE_PACKAGE_NAME + @"</name>
  <version>0.0.0</version>
  <description>The " + GenerateRos2Middleware.ROS2_MIDDLEWARE_PACKAGE_NAME + @" package</description>

  <maintainer email=""mic@todo.todo"">mic</maintainer>

  <license>TODO</license>

  <buildtool_depend>ament_cmake</buildtool_depend>
" + GetPackageFileTargetProjectDependencies(initProj) + @"

   <depend>rclpy</depend>
    <depend>geometry_msgs</depend>
  <depend>nav2_msgs</depend>
  <depend>action_msgs</depend>
    <depend>example_interfaces</depend>
        <depend>lifecycle_msgs</depend>
                <depend>std_msgs</depend>
                <depend>datetime</depend>
                <depend>traceback</depend>
                                <depend>pymongo</depend>

  <export>
    <build_type>ament_python</build_type>
  </export>
</package>";
                return file;
            }
        
        public static string GetCMakeListsFilefoxy()
        {
            string file = @"
cmake_minimum_required(VERSION 3.5)
project(" + GenerateRos2Middleware.ROS2_MIDDLEWARE_PACKAGE_NAME + @")

# Find ROS 2 packages
find_package(ament_cmake REQUIRED)
find_package(rclcpp)
find_package(rclpy REQUIRED)
find_package(std_msgs REQUIRED)
find_package(nav2_msgs REQUIRED)

# Export dependencies
ament_export_dependencies(
    rclcpp
    rclpy
    std_msgs
    nav2_msgs
)


# Include directories
include_directories(
    # include
    ${ament_INCLUDE_DIRS}
)



ament_package()
";
            return file;
        }


        public static string GetSetupFilefoxy(string console_main)
        {
            string file = @"

from setuptools import setup

package_name = 'aos_ros2_middleware_auto'

setup(
    name=package_name,
    version='0.0.0',
    packages=[package_name],
    data_files=[
        ('share/ament_index/resource_index/packages',
            ['resource/' + package_name]),
        ('share/' + package_name, ['package.xml']),
    ],
    install_requires=['setuptools'],
    zip_safe=True,
    maintainer='michel',
    maintainer_email='michel1912@github.com',
    description='TODO: Package description',
    license='TODO: License declaration',
    tests_require=['pytest'],
    entry_points={
        'console_scripts': [
            '" + console_main + @"'
        ],
    },
)
";

            return file;
        }


        // private static string GetImportsForMiddlewareNode(PLPsData data, InitializeProject initProj)// NO CHANGES NEED
        // {
        //     string result = "";
        //     Dictionary<string, HashSet<string>> unImports = new Dictionary<string, HashSet<string>>();
        //     List<RosImport> imports = new List<RosImport>();
        //     Console.WriteLine("22222222222222");     
        //     foreach (RosGlue glue in data.RosGlues.Values)
        //     {
        //         Console.WriteLine("112111111111111");
        //         Console.WriteLine(glue.RosActionActivation.Imports[0].From);
        //
        //         foreach (string item in glue.RosActionActivation.Imports[0].Imports)
        //         {
        //             Console.WriteLine(item);   
        //         }
        //         foreach (string item in glue.RosActionActivation.Imports[1].Imports)
        //         {
        //             Console.WriteLine(item);   
        //         }
        //         Console.WriteLine(glue.RosActionActivation.Imports[1].From);
        //         imports.AddRange(glue.RosActionActivation.Imports);
        //         Console.WriteLine("113");
        //
        //
        //         foreach (var lVar in glue.GlueLocalVariablesInitializations)
        //         {
        //             imports.AddRange(lVar.Imports);
        //         }
        //     }
        //
        //     foreach (RosImport im in imports)
        //     {
        //         im.From = im.From == null ? "" : im.From;
        //         if (!unImports.ContainsKey(im.From))
        //         {
        //             unImports.Add(im.From, new HashSet<string>());
        //         }
        //         foreach (string sIm in im.Imports)
        //         {
        //             unImports[im.From].Add(sIm);
        //         }
        //     }
        //
        //     foreach (KeyValuePair<string, HashSet<string>> keyVal in unImports)// NO CHANGES NEED
        //     {
        //         string baseS = keyVal.Key.Replace(" ", "").Length == 0 ? "" : "from " + keyVal.Key + " ";
        //         result += GenerateFilesUtils.GetIndentationStr(0, 0, baseS + "import " + String.Join(",", keyVal.Value));
        //     }
        //     return result;
        // }
        private static string GetImportsForMiddlewareNode(PLPsData data, InitializeProject initProj)
        {
            string result = "";
            Dictionary<string, HashSet<string>> unImports = new Dictionary<string, HashSet<string>>();
            List<RosImport> imports = new List<RosImport>();
            Console.WriteLine("1111111111111111111");
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


        // ros1 
        // private static string GetListenToMongoDbCommandsInitFunction(PLPsData data)//NEED TO CHECK THIS IN ROS2
        // {
        //     string result = "";

        //     result += GenerateFilesUtils.GetIndentationStr(1, 4, "def __init__(self, _topicListener):");
        //     result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.currentActionSequenceID = 1");
        //     result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.currentActionFotExecutionId = None");
        //     result += GenerateFilesUtils.GetIndentationStr(2, 4, "self._topicListener = _topicListener");
        //     result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.readyToActivate = \"\" ");

        //     foreach (RosGlue glue in data.RosGlues.Values)
        //     {
        //         if (glue.RosServiceActivation != null && !string.IsNullOrEmpty(glue.RosServiceActivation.ServiceName))
        //         {
        //             result += GenerateFilesUtils.GetIndentationStr(2, 4, "self." + glue.Name + "ServiceName = \"" + glue.RosServiceActivation.ServicePath + "\"");
        //         }
        //     }

        //     result += Environment.NewLine;

        //     result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.listen_to_mongodb_commands()");
        //     return result;
        // }
        private static string GetListenToMongoDbCommandsInitFunction(PLPsData data)//for ros2
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


        private static CompoundVarTypePLP GetCompundTypeByName(string compTypeName, PLPsData data)// NO CHANGES NEED
        {
            List<CompoundVarTypePLP> cl = data.GlobalCompoundTypes.Where(x => x.TypeName.Equals(compTypeName)).ToList();
            return cl.Count == 0 ? null : cl[0];
        }
        private static CompoundVarTypePLP_Variable GetCompundVariableByName(CompoundVarTypePLP oComp, string subFields, PLPsData data)// NO CHANGES NEED
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


        private static string GetUnderlineLocalVariableNameTypeByVarName(PLPsData data, PLP plp, string variableName)// NO CHANGES NEED 
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
                return oVar == null || !oVar.Type.Equals(PLPsData.ANY_VALUE_TYPE_NAME) ? null : oVar.UnderlineLocalVariableType;
            }
            else//is a parameter
            {
                List<GlobalVariableModuleParameter> temp = plp.GlobalVariableModuleParameters.Where(x => x.Name.Equals(bits[0])).ToList();
                if (temp.Count == 0) return null;
                CompoundVarTypePLP comp = GetCompundTypeByName(temp[0].Type, data);
                CompoundVarTypePLP_Variable oVar = GetCompundVariableByName(comp, string.Join(".", bits.Skip(1)), data);
                return oVar == null || !oVar.Type.Equals(PLPsData.ANY_VALUE_TYPE_NAME) ? null : oVar.UnderlineLocalVariableType;
            }
        }
        private static string GetHandleModuleFunction(PLPsData data)
        {
            string result = "";

            foreach (RosGlue glue in data.RosGlues.Values)
            {
                PLP plp = data.PLPs[glue.Name];
                result += GenerateFilesUtils.GetIndentationStr(1, 4, "def handle_" + glue.Name + "(self, params):");
                result += GenerateFilesUtils.GetIndentationStr(2, 4, "responseNotByLocalVariables = None");
                Dictionary<string, LocalVariablesInitializationFromGlobalVariable> localVarsFromGlobal = new Dictionary<string, LocalVariablesInitializationFromGlobalVariable>();

                bool hasVar = false;

                foreach (LocalVariablesInitializationFromGlobalVariable oGlVar in glue.LocalVariablesInitializationFromGlobalVariables)
                {
                    hasVar = true;
                    result += GenerateFilesUtils.GetIndentationStr(2, 4, oGlVar.InputLocalVariable + " = \"\"");
                }
                if (hasVar)
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
                                //globVarName = "oDesiredLocation.actual_location".replace("oDesiredLocation", params["ParameterLinks"]["oDesiredLocation"])
                                string baseGlobalParameter = plp.GlobalVariableModuleParameters
                                    .Where(x => oGlVar.FromGlobalVariable.StartsWith(x.Name + ".") || oGlVar.FromGlobalVariable.Equals(x.Name))
                                    .Select(x => x.Name).FirstOrDefault();
                                result += GenerateFilesUtils.GetIndentationStr(3, 4, "globVarName = \"" + oGlVar.FromGlobalVariable + "\".replace(\"" + baseGlobalParameter + "\", params[\"ParameterLinks\"][\"" + baseGlobalParameter + "\"], 1)");
                            }

                            result += GenerateFilesUtils.GetIndentationStr(3, 4, "dbVar = aos_GlobalVariablesAssignments_collection.find_one({\"GlobalVariableName\": globVarName})");
                            result += GenerateFilesUtils.GetIndentationStr(3, 4, oGlVar.InputLocalVariable + " = " + underlineType.TypeName + "()");
                            foreach (LocalVariableCompoundTypeField field in underlineType.SubFields)
                            {
                                //obj_location.z=cupAccurateLocation[""LowLevelValue""][""z""]
                                result += GenerateFilesUtils.GetIndentationStr(3, 4, oGlVar.InputLocalVariable + "." + field.FieldName + " = dbVar[\"LowLevelValue\"][\"" + field.FieldName + "\"]");
                            }
                            result += GenerateFilesUtils.GetIndentationStr(3, 4, "self._topicListener.localVarNamesAndValues[\"" + glue.Name + "\"][\"" + oGlVar.InputLocalVariable + "\"] = " + underlineType.TypeName + "ToDict(" + oGlVar.InputLocalVariable + ")");
                        }
                        else
                        {
                            string[] bits = oGlVar.FromGlobalVariable.Split(".");
                            string varDesc = "[\"" + String.Join("\"][\"", bits) + "\"]";
                            result += GenerateFilesUtils.GetIndentationStr(3, 4, oGlVar.InputLocalVariable + " = params[\"ParameterValues\"]" + varDesc);

                            result += GenerateFilesUtils.GetIndentationStr(3, 4, "self._topicListener.updateLocalVariableValue(\""+oGlVar.InputLocalVariable+"\", "+oGlVar.InputLocalVariable+")");
                 
                        }
                      
                    }
                    result += GenerateFilesUtils.GetIndentationStr(2, 4, "except Exception as e:");
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerError(str(e), traceback.format_exc(e), 'Action: "+glue.Name+", illegalActionObs')");
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "responseNotByLocalVariables = \"illegalActionObs\"");
                }

                if (glue.RosServiceActivation != null && !string.IsNullOrEmpty(glue.RosServiceActivation.ServiceName))
                {
                    result += GenerateFilesUtils.GetIndentationStr(2, 4, "try:");
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerLog(\"wait for service: moduleName:"+glue.Name+", serviceName:"+glue.RosServiceActivation.ServiceName+"\")");
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "rospy.wait_for_service(self." + glue.Name + "ServiceName)");
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, glue.Name + "_proxy = rospy.ServiceProxy(self." + glue.Name + "ServiceName, " + glue.RosServiceActivation.ServiceName + ")");

                    string serviceCallParam = string.Join(", ", glue.RosServiceActivation.ParametersAssignments.Select(x => x.MsgFieldName + "=(" + x.AssignServiceFieldCode + ")"));

                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerLog(\"Sending request to service, moduleName:"+ glue.Name+"\")");
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "__input = " + glue.Name + "_proxy(" + serviceCallParam + ")");
                    GlueLocalVariablesInitialization localVarFromServiceReponse = glue.GlueLocalVariablesInitializations.Where(x => x.FromROSServiceResponse.HasValue && x.FromROSServiceResponse.Value).FirstOrDefault();
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerLog(\"Service response received, moduleName:"+ glue.Name+"\")");
                    if (localVarFromServiceReponse != null)
                    {
                        result += GenerateFilesUtils.GetIndentationStr(3, 4, localVarFromServiceReponse.AssignmentCode, true, true);
                        result += GenerateFilesUtils.GetIndentationStr(3, 4, "self._topicListener.updateLocalVariableValue(\"" + localVarFromServiceReponse.LocalVarName + "\"," + localVarFromServiceReponse.LocalVarName + ")");
                    }


                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "if DEBUG:");
                    result += GenerateFilesUtils.GetIndentationStr(4, 4, "print(\"" + glue.Name + " service terminated\")");
                    result += GenerateFilesUtils.GetIndentationStr(2, 4, "except Exception as e:");
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerError(str(e),traceback.format_exc(e),'Action: "+glue.Name+"')");
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "print(\"Service call failed\")");
                    result += GenerateFilesUtils.GetIndentationStr(2, 4, "");
                }


                result += GenerateFilesUtils.GetIndentationStr(2, 4, "return responseNotByLocalVariables");

            }
            return result;
        }

        private static string GetListenToMongoCommandsFunctionPart(PLPsData data)// SELF._TOPICLISTENER MAYBE NEED TO BE CHANGED FROM ROS1 TO ROS2 DIFFRENET COMMAND
        {
            string result = "";
            foreach (PLP plp in data.PLPs.Values)
            {
                result += GenerateFilesUtils.GetIndentationStr(4, 4, "if moduleName == \"" + plp.Name + "\":");
                result += GenerateFilesUtils.GetIndentationStr(5, 4, "print(\"handle " + plp.Name + "\")");
                result += GenerateFilesUtils.GetIndentationStr(5, 4, "responseNotByLocalVariables = self.handle_" + plp.Name + "(actionParameters)");
            }
            return result;
        }
        private static string GetModuleResponseFunctionPart(PLPsData data)
        {
            string result = "";
            foreach (RosGlue glue in data.RosGlues.Values)
            {
                PLP plp = data.PLPs[glue.Name];
                result += GenerateFilesUtils.GetIndentationStr(2, 4, "if moduleName == \"" + glue.Name + "\":");
                HashSet<string> localVarNames = new HashSet<string>();
                foreach (var oVar in glue.GlueLocalVariablesInitializations)
                {
                    localVarNames.Add(oVar.LocalVarName);
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, oVar.LocalVarName + " = self._topicListener.localVarNamesAndValues[\"" + glue.Name + "\"][\"" + oVar.LocalVarName + "\"]");
                }
                foreach (var oVar in glue.LocalVariablesInitializationFromGlobalVariables)
                {
                    localVarNames.Add(oVar.InputLocalVariable);
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, oVar.InputLocalVariable + " = self._topicListener.localVarNamesAndValues[\"" + glue.Name + "\"][\"" + oVar.InputLocalVariable + "\"]");
                }

                result += GenerateFilesUtils.GetIndentationStr(3, 4, "if DEBUG:");
                result += GenerateFilesUtils.GetIndentationStr(4, 4, "print(\"" + glue.Name + " action local variables:\")");
                foreach (var oVar in glue.GlueLocalVariablesInitializations)
                {
                    if(!oVar.IsHeavyVariable)
                    {
                        result += GenerateFilesUtils.GetIndentationStr(4, 4, "print(\"" + oVar.LocalVarName + ":\")");
                        result += GenerateFilesUtils.GetIndentationStr(4, 4, "print(" + oVar.LocalVarName + ")");
                    }
                }

                if(glue.ResponseRules.Count == 0)
                {
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "if moduleResponse == \"\":");
                    result += GenerateFilesUtils.GetIndentationStr(4, 4, "moduleResponse = \"DefaultObservation\"");
                }
                foreach (var responseRule in glue.ResponseRules)
                {
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "if moduleResponse == \"\" and " + (string.IsNullOrEmpty(responseRule.Condition) ? "True" : responseRule.Condition) + ":");
                    result += GenerateFilesUtils.GetIndentationStr(4, 4, "moduleResponse = \"" + glue.Name + "_" + responseRule.Response + "\"");
                }

                foreach (var responseRule in glue.ResponseRules)
                {
                    foreach (var assign in responseRule.ResponseAssignmentsToGlobalVar)
                    {
                        result += GenerateFilesUtils.GetIndentationStr(3, 4, "if moduleResponse == \"" + glue.Name + "_" + responseRule.Response + "\":");
                        result += GenerateFilesUtils.GetIndentationStr(4, 4, "assignGlobalVar[\"" + assign.GlobalVarName + "\"] = " + assign.Value);
                    }
                }
              Console.WriteLine("ssssssssssssssssssssssss");
                if(!String.IsNullOrEmpty(glue.ResponseFromStringLocalVariable))
                {
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "moduleResponse = str(" + glue.ResponseFromStringLocalVariable + ")");
                }
                result += Environment.NewLine;
            }
            return result;
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
        .Where(x => x.LocalVariablesInitializationFromGlobalVariables.Count > 0 || x.GlueLocalVariablesInitializations.Count > 0)
        .ToList();

    HashSet<string> localVarNames = new HashSet<string>();
    foreach (var glue in gluesWithLocalVars)
    {
        result += GenerateFilesUtils.GetIndentationStr(0, 4, "\"" + glue.Name + "\":{", false);

        foreach (var localVar in glue.GlueLocalVariablesInitializations)
        {
            localVarNames.Add(localVar.LocalVarName);
            result += GenerateFilesUtils.GetIndentationStr(0, 4, "\"" + localVar.LocalVarName + "\": " +
                (string.IsNullOrEmpty(localVar.InitialValue) ? "None" : localVar.InitialValue) +
                (localVar == glue.GlueLocalVariablesInitializations.Last() && glue.LocalVariablesInitializationFromGlobalVariables.Count == 0 ? "" : ", "), false);
        }

        foreach (var localFromGlob in glue.LocalVariablesInitializationFromGlobalVariables)
        {
            localVarNames.Add(localFromGlob.InputLocalVariable);
            result += GenerateFilesUtils.GetIndentationStr(0, 4, "\"" + localFromGlob.InputLocalVariable + "\": None" +
                (localFromGlob == glue.LocalVariablesInitializationFromGlobalVariables.Last() ? "" : ", "), false);
        }

        result += GenerateFilesUtils.GetIndentationStr(0, 4, "}" + (glue != gluesWithLocalVars.Last() ? ", " : ""), false);
    }

    result += GenerateFilesUtils.GetIndentationStr(0, 4, "}");
    result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.setListenTarget(\"initTopicListener\")");

    // Replace ROS 1 with ROS 2 dependencies and functions

    Dictionary<string, Dictionary<string, List<GlueLocalVariablesInitialization>>> topicsToListen = new Dictionary<string, Dictionary<string, List<GlueLocalVariablesInitialization>>>();
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
        result += GenerateFilesUtils.GetIndentationStr(2, 4, "rclpy.create_subscription(\"" + topic.Key + "\", " + topic.Value.Values.ToList()[0][0].TopicMessageType + ", self.cb_" + topic.Key.Replace("/", "_") + ", 1000)");

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
                result += GenerateFilesUtils.GetIndentationStr(4, 4, "#-----------------------------------------------------------------------");
                result += GenerateFilesUtils.GetIndentationStr(4, 4, "value = self." + glueTopic.Key + "_get_value_" + localVar.LocalVarName + "(data)");
                result += GenerateFilesUtils.GetIndentationStr(4, 4, "self.updateLocalVariableValue(\"" + localVar.LocalVarName + "\", value)");
            }
        }
        result += GenerateFilesUtils.GetIndentationStr(2, 4, "except Exception as e:");
        result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerError(str(e), traceback.format_exc(e), 'topic "+topic.Key+"')");


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

    result += @"
def initLocalVars(self, moduleNameToInit):
    if DEBUG:
        print(""initLocalVars:"")
        print(moduleNameToInit)
    for moduleName, localVarNamesAndValuesPerModule in self.localVarNamesAndValues.items():
        for localVarName, value in localVarNamesAndValuesPerModule.items():
            if moduleName == moduleNameToInit:
                if DEBUG:
                    print (""init var:"")
                    print(localVarName)
                aos_local_var_collection.replace_one({""Module"": moduleName, ""VarName"": localVarName},
                                                     {""Module"": moduleName, ""VarName"": localVarName, ""Value"": value},
                                                     upsert=True)
                aosStats_local_var_collection.insert_one(
                    {""Module"": moduleName, ""VarName"": localVarName, ""value"": value, ""Time"": datetime.datetime.utcnow()})


def setListenTarget(self, _listenTargetModule):
    self.initLocalVars(_listenTargetModule)
    if DEBUG:
        print('setListenTopicTargetModule:')
        print(_listenTargetModule)
    self.listenTargetModule = _listenTargetModule
";
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
            result += GenerateFilesUtils.GetIndentationStr(1, 4, "def checkParameterValue_" + localParam.LocalVarName + "(self):#TODO:: need to see how to update ROS parameters. using threading disable other topic listeners");
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "if self.listenTargetModule == \"" + glueRosParamLocalVars.Key + "\":");
            result += GenerateFilesUtils.GetIndentationStr(3, 4, "try:");
            result += GenerateFilesUtils.GetIndentationStr(4, 4, "#__input = rospy.get_param('" + localParam.RosParameterPath + "')");
            result += GenerateFilesUtils.GetIndentationStr(4, 4, "#" + localParam.LocalVarName + " = __input");
            result += GenerateFilesUtils.GetIndentationStr(4, 4, "#self.updateLocalVariableValue(\"" + localParam.LocalVarName + "\", " + localParam.LocalVarName + ")");
            result += GenerateFilesUtils.GetIndentationStr(4, 4, "self.updateLocalVariableValue(\"" + localParam.LocalVarName + "\", True)");
            result += GenerateFilesUtils.GetIndentationStr(3, 4, "except:");
            result += GenerateFilesUtils.GetIndentationStr(4, 4, "pass");
            result += GenerateFilesUtils.GetIndentationStr(3, 4, "#threading.Timer(1, self.checkParameterValue_" + localParam.LocalVarName + ").start()");
        }
    }

    result += @"

def updateLocalVariableValue(self, varName, value):
    if DEBUG and varName not in getHeavyLocalVarList(self.listenTargetModule):
        print(""update local var:"")
        print(varName)
        print(value)
    if self.listenTargetModule not in self.localVarNamesAndValues:
        return
    if self.localVarNamesAndValues[self.listenTargetModule][varName] != value:
        if DEBUG:
            print(""ACTUAL UPDATE --------------------------------------------------------------------------"")
        self.localVarNamesAndValues[self.listenTargetModule][varName]=value
        if varName not in getHeavyLocalVarList(self.listenTargetModule):
            aos_local_var_collection.replace_one({""Module"": self.listenTargetModule, ""VarName"":varName}, {""Module"": self.listenTargetModule, ""VarName"":varName, ""Value"":value}, upsert=True)
            aosStats_local_var_collection.insert_one(
                {""Module"": self.listenTargetModule, ""VarName"": varName, ""value"": value, ""Time"": datetime.datetime.utcnow()})
            if DEBUG:
                print(""WAS UPDATED --------------------------------------------------------------------------"")

";
    return result;
}

//         private static string GetAOS_TopicListenerServerClass(PLPsData data)// NEED TO CHANGE TO RCLPY AND CHECK IF WE NEED ANOTHER ROS2 CHANGES
//         {
//             string result = "";
//             result += GenerateFilesUtils.GetIndentationStr(0, 4, "class AOS_TopicListenerServer:");
//             result += GenerateFilesUtils.GetIndentationStr(1, 4, "def __init__(self):");
//             result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.localVarNamesAndValues = {", false);

//             List<RosGlue> gluesWithLocalVars = data.RosGlues.Values.Where(x => x.LocalVariablesInitializationFromGlobalVariables.Count > 0 || x.GlueLocalVariablesInitializations.Count > 0).ToList();

//             HashSet<string> localVarNames = new HashSet<string>();
//             for (int j = 0; gluesWithLocalVars.Count > j; j++)
//             {
//                 RosGlue glue = gluesWithLocalVars[j];

//                 result += GenerateFilesUtils.GetIndentationStr(0, 4, "\"" + glue.Name + "\":{", false);

//                 for (int i = 0; glue.GlueLocalVariablesInitializations.Count > i; i++)
//                 {
//                     var localVar = glue.GlueLocalVariablesInitializations[i];
//                     localVarNames.Add(localVar.LocalVarName);
//                     result += GenerateFilesUtils.GetIndentationStr(0, 4, "\"" + localVar.LocalVarName + "\": " +
//                             (string.IsNullOrEmpty(localVar.InitialValue) ? "None" : localVar.InitialValue) +
//                             (i == glue.GlueLocalVariablesInitializations.Count - 1 && glue.LocalVariablesInitializationFromGlobalVariables.Count == 0 ? "" : ", "), false);
//                 }
//                 for (int i = 0; i < glue.LocalVariablesInitializationFromGlobalVariables.Count; i++)
//                 {
//                     var localFromGlob = glue.LocalVariablesInitializationFromGlobalVariables[i];
//                     localVarNames.Add(localFromGlob.InputLocalVariable);
//                     result += GenerateFilesUtils.GetIndentationStr(0, 4, "\"" + localFromGlob.InputLocalVariable + "\": None" +
//                             (i == glue.LocalVariablesInitializationFromGlobalVariables.Count - 1 ? "" : ", "), false);
//                 }
//                 result += GenerateFilesUtils.GetIndentationStr(0, 4, "}" + (j < gluesWithLocalVars.Count - 1 ? ", " : ""), false);
//             }
//             result += GenerateFilesUtils.GetIndentationStr(0, 4, "}");
//             result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.setListenTarget(\"initTopicListener\")");


//             Dictionary<string, Dictionary<string, List<GlueLocalVariablesInitialization>>> topicsToListen = new Dictionary<string, Dictionary<string, List<GlueLocalVariablesInitialization>>>();
//             foreach (RosGlue glue in data.RosGlues.Values)
//             {
//                 foreach (var oLVar in glue.GlueLocalVariablesInitializations)
//                 {
//                     if (!string.IsNullOrEmpty(oLVar.RosTopicPath))
//                     {
//                         string cbFunc = "cb_" + oLVar.RosTopicPath.Replace("/", "_");
//                         if (!topicsToListen.ContainsKey(oLVar.RosTopicPath))
//                         {
//                             topicsToListen[oLVar.RosTopicPath] = new Dictionary<string, List<GlueLocalVariablesInitialization>>();
//                         }
//                         if (!topicsToListen[oLVar.RosTopicPath].ContainsKey(glue.Name))
//                         {
//                             topicsToListen[oLVar.RosTopicPath][glue.Name] = new List<GlueLocalVariablesInitialization>();
//                         }
//                         topicsToListen[oLVar.RosTopicPath][glue.Name].Add(oLVar);
//                     }
//                 }
//             }

//             foreach (var topic in topicsToListen)
//             {
//                 result += GenerateFilesUtils.GetIndentationStr(2, 4, "rospy.Subscriber(\"" + topic.Key + "\", " + topic.Value.Values.ToList()[0][0].TopicMessageType + ", self.cb_" + topic.Key.Replace("/", "_") + ", queue_size=1000)");

//             }
//             result += Environment.NewLine;

//             foreach (var topic in topicsToListen)
//             {
//                 result += GenerateFilesUtils.GetIndentationStr(1, 4, "def cb_" + topic.Key.Replace("/", "_") + "(self, data):");
//                 result += GenerateFilesUtils.GetIndentationStr(2, 4, "try:");
//                 foreach (var glueTopic in topic.Value)
//                 {
//                     result += GenerateFilesUtils.GetIndentationStr(3, 4, "if self.listenTargetModule == \"" + glueTopic.Key + "\":");
//                     result += GenerateFilesUtils.GetIndentationStr(4, 4, "if DEBUG:");
//                     result += GenerateFilesUtils.GetIndentationStr(5, 4, "print(\"handling topic call:" + glueTopic.Key + "\")");
//                     result += GenerateFilesUtils.GetIndentationStr(5, 4, "print(data)");
//                     foreach (var localVar in glueTopic.Value)
//                     {
//                         result += GenerateFilesUtils.GetIndentationStr(4, 4, "#-----------------------------------------------------------------------");
//                         result += GenerateFilesUtils.GetIndentationStr(4, 4, "value = self." + glueTopic.Key + "_get_value_" + localVar.LocalVarName + "(data)");
//                         result += GenerateFilesUtils.GetIndentationStr(4, 4, "self.updateLocalVariableValue(\"" + localVar.LocalVarName + "\", value)");
//                     }
//                 }
//                 result += GenerateFilesUtils.GetIndentationStr(2, 4, "except Exception as e:");
//                 result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerError(str(e), traceback.format_exc(e), 'topic "+topic.Key+"')");


//                 result += Environment.NewLine;
//             }



//             foreach (var topic in topicsToListen)
//             {
//                 foreach (var glueTopic in topic.Value)
//                 {
//                     foreach (var localVar in glueTopic.Value)
//                     {
//                         result += GenerateFilesUtils.GetIndentationStr(1, 4, "def " + glueTopic.Key + "_get_value_" + localVar.LocalVarName + "(self, __input):");
//                         result += GenerateFilesUtils.GetIndentationStr(2, 4, GetCodeLineWithLocalVarRefference(localVar.AssignmentCode, localVarNames), true, true);

//                         result += Environment.NewLine;
//                     }
//                 }
//             }

//             result += @"
//     def initLocalVars(self, moduleNameToInit):
//         if DEBUG:
//             print(""initLocalVars:"")
//             print(moduleNameToInit)
//         for moduleName, localVarNamesAndValuesPerModule in self.localVarNamesAndValues.items():
//             for localVarName, value in localVarNamesAndValuesPerModule.items():
//                 if moduleName == moduleNameToInit:
//                     if DEBUG:
//                         print (""init var:"")
//                         print(localVarName)
//                     aos_local_var_collection.replace_one({""Module"": moduleName, ""VarName"": localVarName},
//                                                          {""Module"": moduleName, ""VarName"": localVarName, ""Value"": value},
//                                                          upsert=True)
//                     aosStats_local_var_collection.insert_one(
//                         {""Module"": moduleName, ""VarName"": localVarName, ""value"": value, ""Time"": datetime.datetime.utcnow()})


//     def setListenTarget(self, _listenTargetModule):
//         self.initLocalVars(_listenTargetModule)
//         if DEBUG:
//             print('setListenTopicTargetModule:')
//             print(_listenTargetModule)
//         self.listenTargetModule = _listenTargetModule
// ;
//             Dictionary<string, List<GlueLocalVariablesInitialization>> rosParamVariables = new Dictionary<string, List<GlueLocalVariablesInitialization>>();
//             foreach (RosGlue glue in data.RosGlues.Values)
//             {
//                 List<GlueLocalVariablesInitialization> glueParamVariables = glue.GlueLocalVariablesInitializations.Where(x => !string.IsNullOrEmpty(x.RosParameterPath)).ToList();
//                 if (glueParamVariables.Count > 0)
//                 {
//                     rosParamVariables.Add(glue.Name, glueParamVariables);
//                 }
//             }

//             foreach (var glueRosParamLocalVars in rosParamVariables)
//             {
//                 result += GenerateFilesUtils.GetIndentationStr(2, 4, "if self.listenTargetModule == \"" + glueRosParamLocalVars.Key + "\":");
//                 foreach (var localParam in glueRosParamLocalVars.Value)
//                 {
//                     result += GenerateFilesUtils.GetIndentationStr(3, 4, "self.checkParameterValue_" + localParam.LocalVarName + "()");
//                 }
//             }



//             foreach (var glueRosParamLocalVars in rosParamVariables)
//             {
//                 foreach (var localParam in glueRosParamLocalVars.Value)
//                 {
//                     result += GenerateFilesUtils.GetIndentationStr(1, 4, "def checkParameterValue_" + localParam.LocalVarName + "(self):#TODO:: need to see how to update ROS parameters. using threading disable other topic listeners");
//                     result += GenerateFilesUtils.GetIndentationStr(2, 4, "if self.listenTargetModule == \"" + glueRosParamLocalVars.Key + "\":");
//                     result += GenerateFilesUtils.GetIndentationStr(3, 4, "try:");
//                     result += GenerateFilesUtils.GetIndentationStr(4, 4, "#__input = rospy.get_param('" + localParam.RosParameterPath + "')");
//                     result += GenerateFilesUtils.GetIndentationStr(4, 4, "#" + localParam.LocalVarName + " = __input");
//                     result += GenerateFilesUtils.GetIndentationStr(4, 4, "#self.updateLocalVariableValue(\"" + localParam.LocalVarName + "\", " + localParam.LocalVarName + ")");
//                     result += GenerateFilesUtils.GetIndentationStr(4, 4, "self.updateLocalVariableValue(\"" + localParam.LocalVarName + "\", True)");
//                     result += GenerateFilesUtils.GetIndentationStr(3, 4, "except:");
//                     result += GenerateFilesUtils.GetIndentationStr(4, 4, "pass");
//                     result += GenerateFilesUtils.GetIndentationStr(3, 4, "#threading.Timer(1, self.checkParameterValue_" + localParam.LocalVarName + ").start()");
//                 }
//             }




//             result += @"

//     def updateLocalVariableValue(self, varName, value):
//         if DEBUG and varName not in getHeavyLocalVarList(self.listenTargetModule):
//             print(""update local var:"")
//             print(varName)
//             print(value)
//         if self.listenTargetModule not in self.localVarNamesAndValues:
//             return
//         if self.localVarNamesAndValues[self.listenTargetModule][varName] != value:
//             if DEBUG:
//                 print(""ACTUAL UPDATE --------------------------------------------------------------------------"")
//             self.localVarNamesAndValues[self.listenTargetModule][varName]=value
//             if varName not in getHeavyLocalVarList(self.listenTargetModule):
//                 aos_local_var_collection.replace_one({""Module"": self.listenTargetModule, ""VarName"":varName}, {""Module"": self.listenTargetModule, ""VarName"":varName, ""Value"":value}, upsert=True)
//                 aosStats_local_var_collection.insert_one(
//                     {""Module"": self.listenTargetModule, ""VarName"": varName, ""value"": value, ""Time"": datetime.datetime.utcnow()})
//                 if DEBUG:
//                     print(""WAS UPDATED --------------------------------------------------------------------------"")

// ";
//             return result;
//         }


    private static string GetHeavyLocalVariablesList(PLPsData data)// NO NEED TO CHANGE
    {
        string result = "";;
        foreach(PLP plp in data.PLPs.Values)
        {
            string plpHeavyVars = string.Join<String>(",", data.LocalVariablesListings.Where(x=>x.IsHeavyVariable && x.SkillName == plp.Name).Select(x=> "\"" + x.VariableName + "\"" ).ToArray());
            if (plpHeavyVars.Length > 0)
            {
                result += (result.Length > 0 ? ", " : "") + "\"" + plp.Name + "\" : [" + plpHeavyVars + "]";
            }
        } 
        return "HEAVY_LOCAL_VARS={" + result + "}";
    }
    
    // from rclpy.node import Node
    // from geometry_msgs.msg import Point
    // from nav2_msgs.action import NavigateToPose
    // from rcl_interfaces.msg import Log
    // from interfaces_robot.srv import NavigateToCoordinates
        private static string GetHandleModuleFunctionV2(PLPsData data)
{
    string result = "";

    foreach (RosGlue glue in data.RosGlues.Values)
    {
        PLP plp = data.PLPs[glue.Name];
        result += GenerateFilesUtils.GetIndentationStr(1, 4, "def handle_" + glue.Name + "(self, params):");
        result += GenerateFilesUtils.GetIndentationStr(2, 4, "responseNotByLocalVariables = None");

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
                            .Where(x => oGlVar.FromGlobalVariable.StartsWith(x.Name + ".") || oGlVar.FromGlobalVariable.Equals(x.Name))
                            .Select(x => x.Name).FirstOrDefault();
                        result += GenerateFilesUtils.GetIndentationStr(3, 4, "globVarName = \"" + oGlVar.FromGlobalVariable + "\".replace(\"" + baseGlobalParameter + "\", params[\"ParameterLinks\"][\"" + baseGlobalParameter + "\"], 1)");
                    }

                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "dbVar = aos_GlobalVariablesAssignments_collection.find_one({\"GlobalVariableName\": globVarName})");
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, oGlVar.InputLocalVariable + " = " + underlineType.TypeName + "()");
                    foreach (LocalVariableCompoundTypeField field in underlineType.SubFields)
                    {
                        result += GenerateFilesUtils.GetIndentationStr(3, 4, oGlVar.InputLocalVariable + "." + field.FieldName + " = dbVar[\"LowLevelValue\"][\"" + field.FieldName + "\"]");
                    }
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "self._topicListener.localVarNamesAndValues[\"" + glue.Name + "\"][\"" + oGlVar.InputLocalVariable + "\"] = " + underlineType.TypeName + "ToDict(" + oGlVar.InputLocalVariable + ")");
                }
                else
                {
                    string[] bits = oGlVar.FromGlobalVariable.Split(".");
                    string varDesc = "[\"" + String.Join("\"][\"", bits) + "\"]";
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, oGlVar.InputLocalVariable + " = params[\"ParameterValues\"]" + varDesc);
                    result += GenerateFilesUtils.GetIndentationStr(3, 4, "self._topicListener.updateLocalVariableValue(\"" + oGlVar.InputLocalVariable + "\", \"" + oGlVar.Consistency + "\", " + oGlVar.InputLocalVariable + ")");//added
                    
                }
            }
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "except Exception as e:");
            result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerError(str(e), traceback.format_exc(), 'Action: " + glue.Name + ", illegalActionObs')");
            result += GenerateFilesUtils.GetIndentationStr(3, 4, "responseNotByLocalVariables = \"illegalActionObs\"");
            result += GenerateFilesUtils.GetIndentationStr(3, 4, "return responseNotByLocalVariables");
        }

        // Try block for service activation
        if (glue.RosServiceActivation != null && !string.IsNullOrEmpty(glue.RosServiceActivation.ServiceName))
        {
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "try:");
            result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerLog(\"wait for service: moduleName=" + glue.Name + ", serviceName=" + glue.RosServiceActivation.ServiceName + "\")");
            result += GenerateFilesUtils.GetIndentationStr(3, 4, "req = NavigateToCoordinates.Request()");
            result += GenerateFilesUtils.GetIndentationStr(3, 4, "req.x = float(nav_to_x)");
            result += GenerateFilesUtils.GetIndentationStr(3, 4, "req.y = float(nav_to_y)");
            result += GenerateFilesUtils.GetIndentationStr(3, 4, "req.z = float(nav_to_z)");
            result += GenerateFilesUtils.GetIndentationStr(3, 4, "self.get_logger().info(\"Sending request to service, moduleName=" + glue.Name + "\")");
            result += GenerateFilesUtils.GetIndentationStr(3, 4, "future = self.cli.call_async(req)");
            result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerLog(\"Service call made, waiting for response\")");
            result += GenerateFilesUtils.GetIndentationStr(3, 4, "future.add_done_callback(self.navigate_callback)");

            result += GenerateFilesUtils.GetIndentationStr(3, 4, "while not self._topicListener.localVarNamesAndValues[\"" + glue.Name + "\"][\"goal_reached\"]:");
            result += GenerateFilesUtils.GetIndentationStr(4, 4, "rclpy.spin_once(self._topicListener, timeout_sec=0.1)");
            result += GenerateFilesUtils.GetIndentationStr(3, 4, "time.sleep(0.1)");

            result += GenerateFilesUtils.GetIndentationStr(2, 4, "except Exception as e:");
            result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerError(str(e), traceback.format_exc(), 'Action: " + glue.Name + "')");
            result += GenerateFilesUtils.GetIndentationStr(3, 4, "print(\"Service call failed\")");
        }

        result += GenerateFilesUtils.GetIndentationStr(2, 4, "return responseNotByLocalVariables");

        // Adding the callback function
        result += "\n";
        result += GenerateFilesUtils.GetIndentationStr(1, 4, "def navigate_callback(self, future):");
        result += GenerateFilesUtils.GetIndentationStr(2, 4, "try:");
        result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerLog(\"navigate_callback invoked\")");
        result += GenerateFilesUtils.GetIndentationStr(3, 4, "result = future.result()");
        result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerLog(\"Future result obtained\")");
        result += GenerateFilesUtils.GetIndentationStr(3, 4, "if result is not None:");
        result += GenerateFilesUtils.GetIndentationStr(4, 4, "self.get_logger().info(\"Service response received, moduleName=navigate\")");
        result += GenerateFilesUtils.GetIndentationStr(4, 4, "skillSuccess = result.success");
        result += GenerateFilesUtils.GetIndentationStr(4, 4, "registerLog(f\"Service response success: {skillSuccess}\")");
        result += GenerateFilesUtils.GetIndentationStr(4, 4, "self._topicListener.updateLocalVariableValue(\"skillSuccess\", \"DB\", skillSuccess)");//added
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

            result += GenerateFilesUtils.GetIndentationStr(1, 0, "def __init__(self, topic_listener, shared_state):");
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "super().__init__('listen_to_mongodb_commands')");
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.current_action_sequence_id = 1");
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.current_action_for_execution_id = None");
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.shared_state = shared_state");
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.navigate_service_name = \"/navigate_to_coordinates\"");
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "self._topicListener = topic_listener");
            result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.cli = self.create_client(NavigateToCoordinates, self.navigate_service_name)");

            foreach (RosGlue glue in data.RosGlues.Values)
            {
                if (glue.RosServiceActivation != null && !string.IsNullOrEmpty(glue.RosServiceActivation.ServiceName))
                {
                    result += GenerateFilesUtils.GetIndentationStr(2, 4, "self." + glue.Name + "_service_name = \"" + glue.RosServiceActivation.ServicePath + "\"");
                }
            }

            result += GenerateFilesUtils.GetIndentationStr(2, 4, "while not self.cli.wait_for_service(timeout_sec=1.0):");
            result += GenerateFilesUtils.GetIndentationStr(3, 4, "self.get_logger().info('Service not available, waiting again...')");

            result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.timer = self.create_timer(1.0, self.listen_to_mongodb_commands)");

            return result;
        }


     public static string GetAosRos2MiddlewareNodeFile(PLPsData data, InitializeProject initProj)
{
    string pythonVersion = "python3"; // ROS2 supports only Python 3
    string file = @"#!/usr/bin/" + pythonVersion + @"
import datetime
import rclpy
import pymongo
import traceback
import operator
import time

" + GetImportsForMiddlewareNode(data, initProj) + @"


DEBUG = " + (initProj.MiddlewareConfiguration.DebugOn ? "True" : "False") + Environment.NewLine +
GetHeavyLocalVariablesList(data) + @"

aosDbConnection = pymongo.MongoClient(""mongodb://localhost:27017/"")
aosDB = aosDbConnection[""AOS""]
aos_statisticsDB = aosDbConnection[""AOS_Statistics""]
aos_local_var_collection = aosDB[""LocalVariables""]
aosStats_local_var_collection = aos_statisticsDB[""LocalVariables""]
aos_GlobalVariablesAssignments_collection = aosDB[""GlobalVariablesAssignments""]
aos_ModuleResponses_collection = aosDB[""ModuleResponses""]
collActionForExecution = aosDB[""ActionsForExecution""]
collLogs = aosDB[""Logs""]
collActions = aosDB[""Actions""]

aos_local_var_collection1 = aosDB[""LocalVariablesDB""]
aosStats_local_var_collection1 = aos_statisticsDB[""LocalVariablesDB""]




def registerError(errorStr, trace, comments=None):
    error = {
        ""Component"": ""RosMiddleware"", ""Event"": errorStr, ""Advanced"": trace, ""LogLevel"": 2, ""LogLevelDesc"": ""Error"",
        ""Time"": datetime.datetime.utcnow()
    }
    if comments is not None:
        error = {
            ""Component"": ""RosMiddleware"", ""Error"": errorStr, ""Advanced"": str(comments) + "". "" + str(trace),
            ""Time"": datetime.datetime.utcnow()
        }
    collLogs.insert_one(error)

def registerLog(logStr):
    log = {
        ""Component"": ""RosMiddleware"", ""Event"": logStr, ""LogLevel"": 5, ""LogLevelDesc"": ""Debug"", ""Advanced"": """",
        ""Time"": datetime.datetime.utcnow()
    }
    collLogs.insert_one(log)

def getHeavyLocalVarList(moduleName):
    return HEAVY_LOCAL_VARS.get(moduleName, [])



" + GetLocalVariableTypeClasses(data) + @"

class ListenToMongoDbCommands(Node):
    " + GetListenToMongoDbCommandsInitFunctionV2(data) + @"

" + GetHandleModuleFunctionV2(data) + @"
    

    def saveHeavyLocalVariableToDB(self, moduleName):
        for varName in getHeavyLocalVarList(moduleName):
            value = self._topicListener.localVarNamesAndValues[moduleName][varName]
            aos_local_var_collection.replace_one({""Module"": moduleName, ""VarName"": varName},
                                                 {""Module"": moduleName, ""VarName"": varName,
                                                  ""Value"": value}, upsert=True)
            aosStats_local_var_collection.insert_one(
                {""Module"": moduleName, ""VarName"": varName, ""value"": value,
                 ""Time"": datetime.datetime.utcnow()})




    def registerModuleResponse(self, moduleName, startTime, actionSequenceID, responseNotByLocalVariables):
        registerLog(""in the function registerModuleResponse:::::::: "")
        self._topicListener.initLocalVars(moduleName)  # Ensure initialization
        registerLog(""in the function registerModuleResponse222222222222:::::::: "")

        self.saveHeavyLocalVariableToDB(moduleName)
        registerLog(""in the function registerModuleResponse333333333333333:::::::: "")

        filter1 = {""ActionSequenceId"": actionSequenceID}
        # if DEBUG:
            # print(""registerModuleResponse()"")

        if responseNotByLocalVariables is not None:
            moduleResponseItem = {""Module"": moduleName, ""ActionSequenceId"": actionSequenceID,
                                  ""ModuleResponseText"": responseNotByLocalVariables, ""StartTime"": startTime,
                                  ""EndTime"": datetime.datetime.utcnow(),
                                  ""ActionForExecutionId"": self.current_action_for_execution_id}
            aos_ModuleResponses_collection.replace_one(filter1, moduleResponseItem, upsert=True)
            return
        registerLog(""in the function registerModuleResponse444444444:::::::: "")
        # time.sleep(2)
        moduleResponse = """"
        assignGlobalVar = {}


" + GetModuleResponseFunctionPartV2(data) + @"




        registerLog(""this is the response of the navigation : ""+moduleResponse)
        moduleLocalVars = self._topicListener.localVarNamesAndValues.get(moduleName, {})
        moduleResponseItem = {""Module"": moduleName, ""ActionSequenceId"": actionSequenceID,
                              ""ModuleResponseText"": moduleResponse, ""StartTime"": startTime, ""EndTime"": datetime.datetime.utcnow(),
                              ""ActionForExecutionId"": self.current_action_for_execution_id,
                              ""LocalVariables"": moduleLocalVars}

        aos_ModuleResponses_collection.replace_one(filter1, moduleResponseItem, upsert=True)
        for varName, value in assignGlobalVar.items():
            isInit = value is not None
            aos_GlobalVariablesAssignments_collection.replace_one({""GlobalVariableName"": varName},
                                                                  {""GlobalVariableName"": varName, ""LowLevelValue"": value,
                                                                   ""IsInitialized"": isInit, ""UpdatingActionSequenceId"": actionSequenceID,
                                                                   ""ModuleResponseId"": moduleResponseItem[""_id""]}, upsert=True)

    def listen_to_mongodb_commands(self):
        filter1 = {""ActionSequenceId"": self.current_action_sequence_id}
        actionForExecution = collActionForExecution.find_one(filter1)
        if actionForExecution:
            if DEBUG:
                print(""~~"")
                print(""actionID:"", actionForExecution[""ActionID""])
            moduleName = actionForExecution[""ActionName""]
            actionParameters = actionForExecution[""Parameters""]
            self.current_action_for_execution_id = actionForExecution[""_id""]
            registerLog(""navigate start with id :::"" + str(self.current_action_sequence_id))
            self._topicListener.setListenTarget(moduleName)
            time.sleep(0.3)
            moduleActivationStart = datetime.datetime.utcnow()
            responseNotByLocalVariables = None
            print(""module name:"", moduleName)
            registerLog(""Request to call to module: "" + moduleName)
            registerLog(""navigate start:"")


" + GetListenToMongoCommandsFunctionPartV2(data) + @"
            time.sleep(0.3)
            self._topicListener.setListenTarget(""after action"")
            # while not self._topicListener.localVarNamesAndValues[""navigate""][""goal_reached""]:
            #               rclpy.spin_once(self._topicListener, timeout_sec=0.1)
 
            registerLog(""after while loop of goal reached""+ str(self._topicListener.localVarNamesAndValues[""navigate""][""goal_reached""]))    
            self.registerModuleResponse(moduleName, moduleActivationStart, self.current_action_sequence_id,
                                        responseNotByLocalVariables)
            if DEBUG:
                print(""navigate finished"")
            self.current_action_sequence_id += 1
            self.currentActionFotExecutionId = None
    time.sleep(0.1)

        
" + GetAOS_TopicListenerServerClassV2(data) + @"

" + shareClassMashehoKazy(data) + @"







def main(args=None):
    rclpy.init(args=args)
    shared_state = SharedState1()
    topic_listener = AOS_TopicListenerServer(shared_state)
    command_listener = ListenToMongoDbCommands(topic_listener,shared_state)

    topic_listener.get_logger().info(""Nodes initialized and command_listener is about to spin"")
    executor = MultiThreadedExecutor()
    executor.add_node(topic_listener)
    executor.add_node(command_listener)
    try:
        rclpy.spin(command_listener)  # Only spin the command_listener node
    except KeyboardInterrupt:
        pass
    finally:
        executor.shutdown()
        command_listener.destroy_node()
        topic_listener.destroy_node()
        rclpy.shutdown()




";
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
            result += GenerateFilesUtils.GetIndentationStr(3, 4, oVar.LocalVarName + " = self._topicListener.localVarNamesAndValues[\"" + glue.Name + "\"][\"" + oVar.LocalVarName + "\"]");
            result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerLog(\"" + oVar.LocalVarName + ":  \"  +  str(" + oVar.LocalVarName + "))");
        }
        foreach (var oVar in glue.LocalVariablesInitializationFromGlobalVariables)
        {
            localVarNames.Add(oVar.InputLocalVariable);
            result += GenerateFilesUtils.GetIndentationStr(3, 4, oVar.InputLocalVariable + " = self._topicListener.localVarNamesAndValues[\"" + glue.Name + "\"][\"" + oVar.InputLocalVariable + "\"]");
        }

        result += GenerateFilesUtils.GetIndentationStr(3, 4, "nav_to_x = self._topicListener.localVarNamesAndValues[\"" + glue.Name + "\"][\"nav_to_x\"]");
        result += GenerateFilesUtils.GetIndentationStr(3, 4, "nav_to_y = self._topicListener.localVarNamesAndValues[\"" + glue.Name + "\"][\"nav_to_y\"]");
        result += GenerateFilesUtils.GetIndentationStr(3, 4, "nav_to_z = self._topicListener.localVarNamesAndValues[\"" + glue.Name + "\"][\"nav_to_z\"]");

        result += GenerateFilesUtils.GetIndentationStr(3, 4, "if " + string.Join(" and ", localVarNames.Select(varName => varName)) + ":");
        result += GenerateFilesUtils.GetIndentationStr(4, 4, "moduleResponse = \"" + glue.Name + "_eSuccess\"");
        result += GenerateFilesUtils.GetIndentationStr(3, 4, "else:");
        result += GenerateFilesUtils.GetIndentationStr(4, 4, "moduleResponse = \"" + glue.Name + "_eFailed\"");
        
        result += Environment.NewLine;
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
        result += GenerateFilesUtils.GetIndentationStr(5, 4, "responseNotByLocalVariables = self.handle_" + plp.Name + "(actionParameters)");
        result += GenerateFilesUtils.GetIndentationStr(5, 4, "registerLog(\"" + plp.Name + " finished:\")");
    }
    return result;
}

     private static string GetAOS_TopicListenerServerClassV2(PLPsData data)
{
    string result = "";

    // Class definition and constructor
    result += GenerateFilesUtils.GetIndentationStr(0, 4, "class AOS_TopicListenerServer(Node):");
    result += GenerateFilesUtils.GetIndentationStr(1, 4, "def __init__(self, shared_state):");
    result += GenerateFilesUtils.GetIndentationStr(2, 4, "super().__init__('aos_topic_listener_server')");
    result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.shared_state = shared_state");
    result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.localVarNamesAndValues = {", false);

    // Initialize local variables
    List<RosGlue> gluesWithLocalVars = data.RosGlues.Values
        .Where(x => x.LocalVariablesInitializationFromGlobalVariables.Count > 0 || x.GlueLocalVariablesInitializations.Count > 0)
        .ToList();

    for (int j = 0; j < gluesWithLocalVars.Count; j++)
    {
        RosGlue glue = gluesWithLocalVars[j];
        result += GenerateFilesUtils.GetIndentationStr(0, 4, "\"" + glue.Name + "\":{", false);

        for (int i = 0; i < glue.GlueLocalVariablesInitializations.Count; i++)
        {
            var localVar = glue.GlueLocalVariablesInitializations[i];
            result += GenerateFilesUtils.GetIndentationStr(0, 4, "\"" + localVar.LocalVarName + "\": " +
                    (string.IsNullOrEmpty(localVar.InitialValue) ? "None" : localVar.InitialValue) +
                    (i == glue.GlueLocalVariablesInitializations.Count - 1 && glue.LocalVariablesInitializationFromGlobalVariables.Count == 0 ? "" : ", "), false);
        }
        for (int i = 0; i < glue.LocalVariablesInitializationFromGlobalVariables.Count; i++)
        {
            var localFromGlob = glue.LocalVariablesInitializationFromGlobalVariables[i];
            result += GenerateFilesUtils.GetIndentationStr(0, 4, "\"" + localFromGlob.InputLocalVariable + "\": None" +
                    (i == glue.LocalVariablesInitializationFromGlobalVariables.Count - 1 ? "" : ", "), false);
        }
        result += GenerateFilesUtils.GetIndentationStr(0, 4, "}" + (j < gluesWithLocalVars.Count - 1 ? ", " : ""), false);
    }
    result += GenerateFilesUtils.GetIndentationStr(0, 4, "}");
    result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.setListenTarget(\"initTopicListener\")");
    result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.subscription = self.create_subscription(Log, '/rosout', self.cb__rosout, 10)");
    result += GenerateFilesUtils.GetIndentationStr(2, 4, "print(\"LOLO\")");
    result += GenerateFilesUtils.GetIndentationStr(2, 4, "self.get_logger().info(\"AOS_TopicListenerServer initialized and subscribed to /rosout\")");
    result += Environment.NewLine;

    // Topic callbacks
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
                //added
                result += GenerateFilesUtils.GetIndentationStr(4, 4, "self.updateLocalVariableValue(\"" + localVar.LocalVarName + "\", \""+ localVar.Consistency+"\", value)");
                Console.WriteLine(localVar.Consistency+"  coooooooooooooooooooooooooooooooooooooooooooooooooooooooo");
                result += GenerateFilesUtils.GetIndentationStr(4, 4, "self.updateLocalVariableValue(\"skillSuccess\", \"DB\", value)"); // Should be changed if needed
            }
        }
        result += GenerateFilesUtils.GetIndentationStr(2, 4, "except Exception as e:");
        result += GenerateFilesUtils.GetIndentationStr(3, 4, "registerError(str(e), traceback.format_exc(), 'topic " + topic.Key + "')");
        result += Environment.NewLine;
    }

    // Helper methods for local variables
    foreach (var topic in topicsToListen)
    {
        foreach (var glueTopic in topic.Value)
        {
            foreach (var localVar in glueTopic.Value)
            {
                result += GenerateFilesUtils.GetIndentationStr(1, 4, "def " + glueTopic.Key + "_get_value_" + localVar.LocalVarName + "(self, __input):");
                result += GenerateFilesUtils.GetIndentationStr(2, 4, "if 'Goal has been reached.' in __input.msg:");
                result += GenerateFilesUtils.GetIndentationStr(3, 4, "self.shared_state.goal_reached = True");
                result += GenerateFilesUtils.GetIndentationStr(3, 4, "self.skill_success = True"); // Should be changed if needed
                result += GenerateFilesUtils.GetIndentationStr(3, 4, "return True");
                result += GenerateFilesUtils.GetIndentationStr(2, 4, "return False");
                result += Environment.NewLine;
            }
        }
    }

    // initLocalVars method
    result += @"
    def initLocalVars(self, moduleNameToInit):
        for moduleName, localVarNamesAndValuesPerModule in self.localVarNamesAndValues.items():
            for localVarName, value in localVarNamesAndValuesPerModule.items():
                if moduleName == moduleNameToInit:
                    aos_local_var_collection.replace_one({""Module"": moduleName, ""VarName"": localVarName},
                                                         {""Module"": moduleName, ""VarName"": localVarName, ""Value"": value},
                                                         upsert=True)
                    aosStats_local_var_collection.insert_one(
                        {""Module"": moduleName, ""VarName"": localVarName, ""value"": value, ""Time"": datetime.datetime.utcnow()})
";
    result += Environment.NewLine;

    // setListenTarget method
    result += @"
    def setListenTarget(self, _listenTargetModule):
        self.initLocalVars(_listenTargetModule)
        if DEBUG:
            print('setListenTopicTargetModule:')
            print(_listenTargetModule)
        self.listenTargetModule = _listenTargetModule
";
    result += Environment.NewLine;

    // updateLocalVariableValue method
    result += @"
    def updateLocalVariableValue(self, varName, Consistency, value):
        if DEBUG and varName not in getHeavyLocalVarList(self.listenTargetModule):
            print(""update local var:"")
            print(varName)
            print(value)
        if self.listenTargetModule not in self.localVarNamesAndValues:
            return
        if self.localVarNamesAndValues[self.listenTargetModule][varName] != value:
            if DEBUG:
                print(""ACTUAL UPDATE --------------------------------------------------------------------------"")
            self.localVarNamesAndValues[self.listenTargetModule][varName] = value
            if varName not in getHeavyLocalVarList(self.listenTargetModule):
               if Consistency == ""DB"":
                aos_local_var_collection1.replace_one({""Module"": self.listenTargetModule, ""VarName"": varName},
                                                     {""Module"": self.listenTargetModule, ""VarName"": varName, ""Value"": value}, upsert=True)
                aosStats_local_var_collection1.insert_one(
                    {""Module"": self.listenTargetModule, ""VarName"": varName, ""value"": value, ""Time"": datetime.datetime.utcnow()})
               aos_local_var_collection.replace_one(
                {""Module"": self.listenTargetModule, ""VarName"": varName},
                {""Module"": self.listenTargetModule, ""VarName"": varName, ""Value"": value},
                upsert=True
                             ) 
               aosStats_local_var_collection1.insert_one(
                        {""Module"": self.listenTargetModule, ""VarName"": varName, ""value"": value, ""Time"": datetime.datetime.utcnow()})
               if DEBUG:
                    print(""WAS UPDATED --------------------------------------------------------------------------"")
";
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
            List<string> tempCodeLines = data.GlobalVariableDeclarations.Select(x => x.DefaultCode).Where(x => !string.IsNullOrEmpty(x)).ToList();
            List<string> codeLines = new List<string>();
            foreach (string codeLine in tempCodeLines)
            {
                codeLines.AddRange(codeLine.Replace("if", "").Replace("else", "").Replace("{", "").Replace("}", "").Replace(" ", "").Split(";")
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
                value = GenerateFilesUtils.IsPrimitiveType(constants[assignment.Value].Type) ? assignment.Value : constants[assignment.Value].Type + "ToDict(" + constants[assignment.Value].Name + ")";
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


            result += @"
    def updateGlobalVarLowLevelValue(self, varName, value):
        isInit = value is not None
        aos_GlobalVariablesAssignments_collection.replace_one({""GlobalVariableName"": varName},{""GlobalVariableName"": varName, ""LowLevelValue"": value,
                                                                                               ""IsInitialized"": isInit, ""UpdatingActionSequenceId"": ""initialization"",
                                                                                               ""ModuleResponseId"": ""initialization""},upsert=True)


";
            return result;
        }
    }
}