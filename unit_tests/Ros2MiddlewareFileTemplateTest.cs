using NUnit.Framework;
using WebApiCSharp.GenerateCodeFiles;
using WebApiCSharp.Models;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using MongoDB.Bson.IO;
using Moq;
using NUnit.Framework;

namespace unit_tests
{
    public class Ros2MiddlewareFileTemplateTests
    {
        private InitializeProject _initializeProject;
        private PLPsData _data; 
        private Ros2MiddlewareFileTemplate _template;
    
        [SetUp]
        public void Setup()
        {
            _initializeProject = new InitializeProject
            {
                RosTarget = new RosTargetProject
                {
                    RosTargetProjectPackages = new List<string> { "package1", "package2" }
                }
            };

            List<string> errors;
            _data = new PLPsData(out errors)
            {
                LocalVariableTypes = new List<LocalVariableTypePLP>
                {
                    new LocalVariableTypePLP
                    {
                        TypeName = "TestType",
                        SubFields = new List<LocalVariableCompoundTypeField>
                        {
                            new LocalVariableCompoundTypeField { FieldName = "field1", FieldType = "int" },
                            new LocalVariableCompoundTypeField { FieldName = "field2", FieldType = "string" }
                        }
                    },
                    new LocalVariableTypePLP
                    {
                        TypeName = "AnotherType",
                        SubFields = new List<LocalVariableCompoundTypeField>
                        {
                            new LocalVariableCompoundTypeField { FieldName = "param1", FieldType = "float" },
                            new LocalVariableCompoundTypeField { FieldName = "param2", FieldType = "bool" }
                        }
                    }
                },
                RosGlues = new Dictionary<string, RosGlue>()
            };

            var rosGlue1 = new RosGlue
            {
                Name = "glue1",
                RosActionActivation = new RosActionActivation
                {
                    ActionPath = "/test/action/path",
                    ActionName = "TestAction"
                }
            };

            _data.RosGlues.Add("glue1", rosGlue1);

            var rosGlue2 = new RosGlue
            {
                Name = "glue2",
                RosActionActivation = new RosActionActivation
                {
                    ActionPath = "/another/action/path",
                    ActionName = "AnotherAction"
                }
            };

            _data.RosGlues.Add("glue2", rosGlue2);

            // Initialize compound types with variables
            var compoundType1 = new CompoundVarTypePLP
            {
                TypeName = "Type1",
                Variables = new List<CompoundVarTypePLP_Variable>
                {
                    new CompoundVarTypePLP_Variable { Name = "field1", Type = "Type2" }
                }
            };

            var compoundType2 = new CompoundVarTypePLP
            {
                TypeName = "Type2",
                Variables = new List<CompoundVarTypePLP_Variable>
                {
                    new CompoundVarTypePLP_Variable { Name = "subField", Type = "int" }
                }
            };

            _data.GlobalCompoundTypes = new List<CompoundVarTypePLP> { compoundType1, compoundType2 };
            
            _template = new Ros2MiddlewareFileTemplate();
        }
        
        private PLPsData CreatePLPsDataWithSingleRosServiceImport(string module, string func)
        {
            List<string> errors;
            PLPsData data = new PLPsData(out errors);

            var rosGlue = new RosGlue
            {
                RosServiceActivation = new RosServiceActivation
                {
                    Imports = new List<RosImport>
                    {
                        new RosImport { From = module, Imports = new List<string> { func } }
                    }
                },
                GlueLocalVariablesInitializations = new List<GlueLocalVariablesInitialization>()
            };

            data.RosGlues.Add("glue1", rosGlue);

            return data;
        }

        private PLPsData CreatePLPsData()
        {
            List<string> errors;
            return new PLPsData(out errors);
        }

        private PLPsData CreatePLPsDataWithMultipleImports()
        {
            List<string> errors;
            PLPsData data = new PLPsData(out errors);

            var rosGlue = new RosGlue
            {
                RosServiceActivation = new RosServiceActivation
                {
                    Imports = new List<RosImport>
                    {
                        new RosImport { From = "module1", Imports = new List<string> { "func1", "func2" } },
                        new RosImport { From = "module2", Imports = new List<string> { "func3" } },
                        new RosImport { From = "module3", Imports = new List<string> { "func4" } }
                    }
                },
                GlueLocalVariablesInitializations = new List<GlueLocalVariablesInitialization>()
            };

            data.RosGlues.Add("glue1", rosGlue);

            return data;
        }
        
        /////////////////////////////////// <GetPackageFileTargetProjectDependencies> //////////////////////////////////
        [Test]
        public void TestGetPackageFileTargetProjectDependencies()
        {
            string expected =
                "<build_depend>package1</build_depend>\n" +
                "<exec_depend>package1</exec_depend>\n" +
                "<build_depend>package2</build_depend>\n" +
                "<exec_depend>package2</exec_depend>";

            string result = Ros2MiddlewareFileTemplate.GetPackageFileTargetProjectDependencies(_initializeProject)
                .Trim();

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestGetPackageFileTargetProjectDependencies_EmptyPackages()
        {
            _initializeProject.RosTarget.RosTargetProjectPackages = new List<string>();

            string result = Ros2MiddlewareFileTemplate.GetPackageFileTargetProjectDependencies(_initializeProject);

            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void TestGetPackageFileTargetProjectDependencies_NullRosTarget()
        {
            _initializeProject.RosTarget = null;

            Assert.Throws<System.NullReferenceException>(() =>
            {
                Ros2MiddlewareFileTemplate.GetPackageFileTargetProjectDependencies(_initializeProject);
            });
        }

        [Test]
        public void TestGetPackageFileTargetProjectDependencies_NullProject()
        {
            InitializeProject nullProject = null;

            Assert.Throws<System.NullReferenceException>(() =>
            {
                Ros2MiddlewareFileTemplate.GetPackageFileTargetProjectDependencies(nullProject);
            });
        }

        [Test]
        public void TestGetPackageFileTargetProjectDependencies_EmptyRosTargetPackages_ShouldReturnEmptyString()
        {
            _initializeProject.RosTarget.RosTargetProjectPackages = new List<string>(); // Simulating empty RosTargetProjectPackages

            string result = Ros2MiddlewareFileTemplate.GetPackageFileTargetProjectDependencies(_initializeProject);

            Assert.AreEqual(string.Empty, result);
        }
        
        ///////////////////////////////////////// <GetImportsForMiddlewareNode> ////////////////////////////////////////
        [Test]
        public void TestGetImportsForMiddlewareNode_SingleRosServiceActivationImport()
        {
            PLPsData data = CreatePLPsDataWithSingleRosServiceImport("module1", "func1");

            string result = Ros2MiddlewareFileTemplate.GetImportsForMiddlewareNode(data, _initializeProject);

            StringAssert.Contains("from module1 import func1", result);
        }

        [Test]
        public void TestGetImportsForMiddlewareNode_NoImports()
        {
            PLPsData data = CreatePLPsData();

            string result = Ros2MiddlewareFileTemplate.GetImportsForMiddlewareNode(data, _initializeProject).Trim();

            // Split the result by newlines and filter out any empty or whitespace-only lines
            var importLines = result.Split('\n').Select(line => line.Trim())
                .Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();

            // Check for known default imports that are always included
            var expectedImports = new[]
            {
                "from geometry_msgs.msg import Point",
                "from rclpy.node import Node",
                "from rclpy.executors import MultiThreadedExecutor",
                "from nav2_msgs.action import NavigateToPose",
                "from interfaces_robot.srv import NavigateToCoordinates",
                "from std_msgs.msg import Bool",
                "from rcl_interfaces.msg import Log"
            };

            // Assert each expected import is in the result
            foreach (var expectedImport in expectedImports)
            {
                StringAssert.Contains(expectedImport, result);
            }

            // Ensure no other imports are included
            Assert.AreEqual(expectedImports.Length, importLines.Length);
        }

        [Test]
        public void TestGetImportsForMiddlewareNode_MultipleImports()
        {
            PLPsData data = CreatePLPsDataWithMultipleImports();

            string result = Ros2MiddlewareFileTemplate.GetImportsForMiddlewareNode(data, _initializeProject);

            StringAssert.Contains("from module1 import func1,func2", result);
            StringAssert.Contains("from module2 import func3", result);
            StringAssert.Contains("from module3 import func4", result);
        }

        [Test]
        public void TestGetImportsForMiddlewareNode_EmptyFrom()
        {
            List<string> errors;
            PLPsData data = new PLPsData(out errors);
            var rosGlue = new RosGlue
            {
                RosServiceActivation = new RosServiceActivation
                {
                    Imports = new List<RosImport>
                    {
                        new RosImport { From = null, Imports = new List<string> { "func1" } }
                    }
                },
                GlueLocalVariablesInitializations = new List<GlueLocalVariablesInitialization>()
            };
            data.RosGlues.Add("glue1", rosGlue);

            string result = Ros2MiddlewareFileTemplate.GetImportsForMiddlewareNode(data, _initializeProject);

            StringAssert.Contains("import func1", result);
        }
        
        /////////////////////////////////// <GetListenToMongoDbCommandsInitFunction> ///////////////////////////////////
        [Test]
        public void TestGetListenToMongoDbCommandsInitFunction()
        {
            string expected = @"
def _init_(self, _topic_listener):
    self.current_action_sequence_id = 1
    self.current_action_for_execution_id = None
    self._topic_listener = _topic_listener
    self.ready_to_activate = "" ""
    self.glue1_service_name = ""/test/action/path""
    self.glue2_service_name = ""/another/action/path""

    self.listen_to_mongodb_commands()";

            string result = Ros2MiddlewareFileTemplate.GetListenToMongoDbCommandsInitFunction(_data);
            
            Assert.IsTrue(result.Contains("def _init_(self, _topic_listener):"));
            Assert.IsTrue(result.Contains("    self.current_action_sequence_id = 1"));
            Assert.IsTrue(result.Contains("    self.current_action_for_execution_id = None"));
            Assert.IsTrue(result.Contains("    self._topic_listener = _topic_listener"));
            Assert.IsTrue(result.Contains("    self.ready_to_activate = \"\""));
            Assert.IsTrue(result.Contains("    self.glue1_service_name = \"/test/action/path\""));
            Assert.IsTrue(result.Contains("    self.glue2_service_name = \"/another/action/path\""));
            Assert.IsTrue(result.Contains("    self.listen_to_mongodb_commands()"));
        }
        
        [Test]
        public void TestGetListenToMongoDbCommandsInitFunction_NullTopicListener()
        {
            List<string> errors;
            PLPsData data = new PLPsData(out errors);

            string result = Ros2MiddlewareFileTemplate.GetListenToMongoDbCommandsInitFunction(data);
            
            Assert.NotNull(result);  //result is not null
            Assert.IsTrue(result.Contains("_init_(self, _topic_listener):"));  //check method header 
            Assert.IsTrue(result.Contains("self.listen_to_mongodb_commands()"));  //check method call 
        }
        
        [Test]
        public void TestGetListenToMongoDbCommandsInitFunction_CustomTopicListener()
        {
            string expected = @"
def _init_(self, _topic_listener):
    self.current_action_sequence_id = 1
    self.current_action_for_execution_id = None
    self._topic_listener = _topic_listener
    self.ready_to_activate = "" ""
    self.glue1_service_name = ""/test/action/path""
    self.glue2_service_name = ""/another/action/path""

    self.listen_to_mongodb_commands()";

            string result = Ros2MiddlewareFileTemplate.GetListenToMongoDbCommandsInitFunction(_data);

            Assert.IsTrue(result.Contains("_init_(self, _topic_listener):"));
            Assert.IsTrue(result.Contains("self.current_action_sequence_id = 1"));
            Assert.IsTrue(result.Contains("self.current_action_for_execution_id = None"));
            Assert.IsTrue(result.Contains("self._topic_listener = _topic_listener"));
            Assert.IsTrue(result.Contains("self.ready_to_activate = \"\""));
            Assert.IsTrue(result.Contains("self.glue1_service_name = \"/test/action/path\""));
            Assert.IsTrue(result.Contains("self.glue2_service_name = \"/another/action/path\""));
            Assert.IsTrue(result.Contains("self.listen_to_mongodb_commands()"));
        }
        
        //////////////////////////////////////////// <GetCompundTypeByName> ////////////////////////////////////////////
        [Test]
        public void TestGetCompundTypeByName_WhenExists_ShouldReturnCorrectObject()
        {
            string compTypeName = "Type1";  //existing type in _data.GlobalCompoundTypes
            var result = Ros2MiddlewareFileTemplate.GetCompundTypeByName(compTypeName, _data);

            Assert.IsNotNull(result);
            Assert.AreEqual(compTypeName, result.TypeName); 
        }

        [Test]
        public void TestGetCompundTypeByName_WhenNotExists_ShouldReturnNull()
        {
            string compTypeName = "NonExistentType";
            var result = Ros2MiddlewareFileTemplate.GetCompundTypeByName(compTypeName, _data);

            Assert.IsNull(result);
        }
        
        // [Test]
        // public void TestGetCompundTypeByName_NullData_ShouldReturnNull()
        // {
        //     string compTypeName = "Type1"; // existing type in _data.GlobalCompoundTypes
        //     var result = Ros2MiddlewareFileTemplate.GetCompundTypeByName(compTypeName, null);
        //
        //     Assert.IsNull(result);
        // }
        
        [Test]
        public void TestGetCompundTypeByName_EmptyTypeName_ShouldReturnNull()
        {
            string compTypeName = "";
            var result = Ros2MiddlewareFileTemplate.GetCompundTypeByName(compTypeName, _data);

            Assert.IsNull(result);
        }
        
        /////////////////////////////////////////// <GetCompundVariableByName> /////////////////////////////////////////
        [Test]
        public void TestGetCompundVariableByName_NullCompoundType_ShouldReturnNull()
        {
            CompoundVarTypePLP oComp = null;
            string subFields = "field1.subField";
            PLPsData data = _data;
            var result = Ros2MiddlewareFileTemplate.GetCompundVariableByName(oComp, subFields, data);

            Assert.IsNull(result);
        }

        [Test]
        public void TestGetCompundVariableByName_EmptySubFields_ShouldReturnNull()
        {
            CompoundVarTypePLP oComp = new CompoundVarTypePLP();
            string subFields = "";
            PLPsData data = _data;
            var result = Ros2MiddlewareFileTemplate.GetCompundVariableByName(oComp, subFields, data);

            Assert.IsNull(result);
        }

        [Test]
        public void TestGetCompundVariableByName_NonExistentField_ShouldReturnNull()
        {
            CompoundVarTypePLP oComp = new CompoundVarTypePLP
            {
                Variables = new List<CompoundVarTypePLP_Variable>
                {
                    new CompoundVarTypePLP_Variable { Name = "field1", Type = "int" }
                }
            };
            string subFields = "nonExistentField";
            PLPsData data = _data;

            var result = Ros2MiddlewareFileTemplate.GetCompundVariableByName(oComp, subFields, data);

            Assert.IsNull(result);
        }

        [Test]
        public void TestGetCompundVariableByName_SingleField_ShouldReturnVariable()
        {
            CompoundVarTypePLP oComp = new CompoundVarTypePLP
            {
                Variables = new List<CompoundVarTypePLP_Variable>
                {
                    new CompoundVarTypePLP_Variable { Name = "field1", Type = "int" }
                }
            };
            string subFields = "field1";
            PLPsData data = _data;

            var result = Ros2MiddlewareFileTemplate.GetCompundVariableByName(oComp, subFields, data);

            Assert.IsNotNull(result);
            Assert.AreEqual("field1", result.Name);
            Assert.AreEqual("int", result.Type);
        }

        [Test]
        public void TestGetCompundVariableByName_ComplexFields_ShouldReturnVariable()
        {
            string subFields = "field1.subField";

            // Ensure compoundType1 and subFields are valid before calling GetCompundVariableByName
            Assert.IsNotNull(_data.GlobalCompoundTypes[0], "compoundType1 should not be null");
            Assert.IsFalse(string.IsNullOrEmpty(subFields), "subFields should not be null or empty");

            var result = Ros2MiddlewareFileTemplate.GetCompundVariableByName(_data.GlobalCompoundTypes[0], subFields, _data);

            Assert.IsNotNull(result, "Expected result to be not null");
            Assert.AreEqual("subField", result.Name);
            Assert.AreEqual("int", result.Type); 
        }
        //
        // [Test]
        // public void TestGetCompundVariableByName_NullData_ShouldReturnNull()
        // {
        //     CompoundVarTypePLP oComp = new CompoundVarTypePLP
        //     {
        //         Variables = new List<CompoundVarTypePLP_Variable>
        //         {
        //             new CompoundVarTypePLP_Variable { Name = "field1", Type = "int" }
        //         }
        //     };
        //     string subFields = "field1.subField";
        //     PLPsData data = null;
        //
        //     var result = Ros2MiddlewareFileTemplate.GetCompundVariableByName(oComp, subFields, data);
        //
        //     Assert.IsNull(result);
        // }

        ////////////////////////////////// <GetUnderlineLocalVariableNameTypeByVarName> ////////////////////////////////
        [Test]
        public void TestGetUnderlineLocalVariableNameTypeByVarName_PrimitiveType_ShouldReturnNull()
        {
            _data.GlobalVariableDeclarations = new List<GlobalVariableDeclaration>
            {
                new GlobalVariableDeclaration { Name = "globalVar", Type = "int" }
            };

            PLP plp = new PLP();
            string variableName = "state.globalVar";

            string result = Ros2MiddlewareFileTemplate.GetUnderlineLocalVariableNameTypeByVarName(_data, plp, variableName);

            Assert.IsNull(result);
        }

        [Test]
        public void TestGetUnderlineLocalVariableNameTypeByVarName_AnyValueType_ShouldReturnUnderlineLocalVariableType()
        {
            _data.GlobalVariableDeclarations = new List<GlobalVariableDeclaration>
            {
                new GlobalVariableDeclaration { Name = "globalVar", Type = PLPsData.ANY_VALUE_TYPE_NAME, UnderlineLocalVariableType = "int" }
            };

            PLP plp = new PLP();
            string variableName = "state.globalVar";

            string result = Ros2MiddlewareFileTemplate.GetUnderlineLocalVariableNameTypeByVarName(_data, plp, variableName);

            Assert.AreEqual("int", result);
        }

        [Test]
        public void TestGetUnderlineLocalVariableNameTypeByVarName_CompoundType_ShouldReturnNull()
        {
            _data.GlobalVariableDeclarations = new List<GlobalVariableDeclaration>
            {
                new GlobalVariableDeclaration { Name = "globalVar", Type = "Type1" }
            };

            PLP plp = new PLP();
            string variableName = "state.globalVar.field1";

            string result = Ros2MiddlewareFileTemplate.GetUnderlineLocalVariableNameTypeByVarName(_data, plp, variableName);

            Assert.IsNull(result);
        }

        [Test]
        public void TestGetUnderlineLocalVariableNameTypeByVarName_ModuleParameter_ShouldReturnUnderlineLocalVariableType()
        {
            PLP plp = new PLP
            {
                GlobalVariableModuleParameters = new List<GlobalVariableModuleParameter>
                {
                    new GlobalVariableModuleParameter { Name = "param1", Type = "Type1" }
                }
            };

            string variableName = "param1.field1.subField";

            string result = Ros2MiddlewareFileTemplate.GetUnderlineLocalVariableNameTypeByVarName(_data, plp, variableName);

            Assert.IsNull(result);
        }

        [Test]
        public void TestGetUnderlineLocalVariableNameTypeByVarName_NonExistingGlobalVariable_ShouldReturnNull()
        {
            PLP plp = new PLP();
            string variableName = "state.nonExistingVar";

            string result = Ros2MiddlewareFileTemplate.GetUnderlineLocalVariableNameTypeByVarName(_data, plp, variableName);

            Assert.IsNull(result);
        }
        
        [Test]
        public void TestGetUnderlineLocalVariableNameTypeByVarName_EmptyVariableName_ShouldReturnNull()
        {
            _data.GlobalVariableDeclarations = new List<GlobalVariableDeclaration>
            {
                new GlobalVariableDeclaration { Name = "globalVar", Type = PLPsData.ANY_VALUE_TYPE_NAME, UnderlineLocalVariableType = "int" }
            };

            PLP plp = new PLP();
            string variableName = "";

            string result = Ros2MiddlewareFileTemplate.GetUnderlineLocalVariableNameTypeByVarName(_data, plp, variableName);

            Assert.IsNull(result);
        }
        
        [Test]
        public void TestGetUnderlineLocalVariableNameTypeByVarName_GlobalVarDeclared_ShouldReturnUnderlineLocalVariableType()
        {
            _data.GlobalVariableDeclarations = new List<GlobalVariableDeclaration>
            {
                new GlobalVariableDeclaration { Name = "globalVar", Type = PLPsData.ANY_VALUE_TYPE_NAME, UnderlineLocalVariableType = "int" }
            };

            PLP plp = new PLP();
            string variableName = "state.globalVar";

            string result = Ros2MiddlewareFileTemplate.GetUnderlineLocalVariableNameTypeByVarName(_data, plp, variableName);

            Assert.AreEqual("int", result);
        }
        
    }
}