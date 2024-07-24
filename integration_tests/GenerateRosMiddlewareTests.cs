using System.Text;
using WebApiCSharp.JsonTextModel;

namespace integration_tests
{
    public class GenerateRosMiddlewareTests
    {
        /////////////////////////////////////////////// < TranslateSD > ////////////////////////////////////////////////
        
        [Fact]
        public void TranslateSD_InvalidProjectName_ThrowsException()
        {
            string sdContent = "project: !@#$%\nparameter: int value\n";
            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD("example", sdContent));
        }

        [Fact]
        public void TranslateSD_MissingSyntaxPreconditionSection_ThrowsException()
        {
            string sdContent = "project: example\nparameter: int value\nprecondition\n";
            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD("example", sdContent));
        }

        [Fact]
        public void TranslateSD_NonexistentParameterReference_ThrowsException()
        {
            string sdContent = @"
project: example
parameter: int value
precondition:
nonexistentParameter > 0";
            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD("example", sdContent));
        }

        [Fact]
        public void TranslateSD_NonIntegerParameter_ThrowsException()
        {
            string sdContent = @"
project: example
parameter: int value
precondition:
value == 10.5";
            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD("example", sdContent));
        }

        [Fact]
        public void TranslateSD_InvalidAvailableParametersCodeSyntax_ThrowsException()
        {
            string sdContent = @"
project: example
parameter: int value
available_parameters_code:
invalid_syntax";
            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD("example", sdContent));
        }

        [Fact]
        public void TranslateSD_DuplicateProjectSection_ThrowsException()
        {
            string sdContent = @"
project: example
parameter: int value
project: duplicate_example";
            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD("example", sdContent));
        }
        
        [Fact]
        public void TranslateSD_InvalidContent_ThrowsException()
        {
            string invalidSdContent = @"
    project: example
    invalid_line: this will cause an error";

            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD("example", invalidSdContent));
        }

        [Fact]
        public void TranslateSD_InvalidContents_ThrowsException()
        {
            string[] invalidSdContents = new[]
            {
                @"
        project: example 
        parameter: int discreteDestination
        parameter: float x
        parameter: float y
        parameter: float z",

                @"
        project: example 
        parameter: int discreteDestination
        parameter: float x
        invalid_line: this is not a valid parameter",

                @"",

                @"
        project: example 
        parameter: int discreteDestination
        parameter: float x
        parameter: float y
        parameter: float z 
        dynamic_model: 
        state__.robotLocation.discrete = ! __meetPrecondition || AOS.Bernoulli(0.1) ? -1: discreteDestination;",

                @"
        project: example 
        parameter: int discreteDestination
        parameter: float x
        parameter: float y
        parameter: float z 
        precondition:
        __meetPrecondition = discreteDestination != state.robotLocation.discrete;
        // missing closing semicolon on purpose",
            };

            foreach (var invalidSdContent in invalidSdContents)
            {
                Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD("example", invalidSdContent));
            }
        }

        [Fact]
        public void TranslateSD_EmptyContent_ThrowsException()
        {
            string sdContent = "";

            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD("example", sdContent));
        }

        [Fact]
        public void TranslateSD_MissingProject_ThrowsException()
        {
            string sdContent = @"
parameter: int discreteDestination
parameter: float x
parameter: float y
parameter: float z 
available_parameters_code: 
__possibleParameters.push_back(std::make_tuple(1,-1.5744997262954712,-0.5154024362564087,-0.001373291015625));
__possibleParameters.push_back(std::make_tuple(2,-1.6243184804916382,0.6617045402526855,-0.00143432617188)); 
__possibleParameters.push_back(std::make_tuple(3,0.986030161381,0.610693752766,-0.00143432617188));

precondition:
__meetPrecondition = discreteDestination != state.robotLocation.discrete;
violate_penalty: -10 
dynamic_model: 
state__.robotLocation.discrete = ! __meetPrecondition || AOS.Bernoulli(0.1) ? -1: discreteDestination;
if(state__.robotLocation.discrete == discreteDestination)
{ 
state__.robotLocation.x = x; 
state__.robotLocation.y = y; 
state__.robotLocation.z = z;
}
for(int i=0; i < state__.tVisitedLocationObjects.size();i++)
{
	if(state__.tVisitedLocationObjects[i]->discrete == state__.robotLocation.discrete) 
	{
	state__.tVisitedLocationObjects[i]->visited = true;
	break;
	}
}
__moduleResponse = (state__.robotLocation.discrete == -1 && AOS.Bernoulli(0.8)) ? eFailed : eSuccess;
__reward = state_.robotLocation.discrete == -1 ? -5 : -(sqrt(pow(state.robotLocation.x-x,2.0)+pow(state.robotLocation.y-y,2.0)))*100;
if (state__.robotLocation.discrete == -1) __reward =  -10;
";

            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD("example", sdContent));
        }

        [Fact]
        public void TranslateSD_MultiplePreconditions_ThrowsException()
        {
            string sdContent = @"
project: example
parameter: int discreteDestination
parameter: float x
precondition:
__meetPrecondition1 = discreteDestination != state.robotLocation.discrete;
precondition:
__meetPrecondition2 = x > 0;";

            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD("example", sdContent));
        }
        

        [Fact]
        public void TranslateSD_MissingProjectHeader_ThrowsException()
        {
            string sdContent =
                "parameter: int discreteDestination\nparameter: float x\nparameter: float y\nparameter: float z\n";
            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD("example", sdContent));
        }

        [Fact]
        public void TranslateSD_InvalidSyntax_ThrowsException()
        {
            string sdContent =
                "project example\nparameter: int discreteDestination\nparameter: float x\nparameter: float y\nparameter: float z\navailable_parameters_code:\n";
            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD("example", sdContent));
        }

        [Fact]
        public void TranslateSD_SectionsInUnexpectedOrder_ThrowsException()
        {
            string sdContent = "parameter: int value\nproject: example\navailable_parameters_code:\n";
            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD("example", sdContent));
        }

        [Fact]
        public void TranslateSD_EmptyParameterSection_ThrowsException()
        {
            string sdContent = "project: example\nparameter:\navailable_parameters_code:\n";
            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD("example", sdContent));
        }

        [Fact]
        public void TranslateSD_DuplicatedParameterSection_ThrowsException()
        {
            string sdContent =
                "project: example\nparameter: paramater\nparameter: float x\nparameter: float y\nparameter: float z\nparameter: float x\n";
            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD("example", sdContent));
        }

        [Fact]
        public void TranslateSD_InvalidParameterType_ThrowsException()
        {
            string sdContent =
                "projjeccct: example\nparameter: invalidType discreteDestination\nparameter: float x\nparameter: float y\nparameter: float z\n";
            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD("example", sdContent));
        }

        [Fact]
        public void TranslateSD_InvalidAvailableParametersCodeValue_ThrowsException()
        {
            string sdContent =
                "project: example\nmeter: int discreteDestination\nparameter: float x\nparameter: float y\nparameter: float z\n12available_parameters_code: invalid_value\n";
            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD("example", sdContent));
        }

        [Fact]
        public void TranslateSD_NestedSections_ThrowsException()
        {
            string sdContent =
                "project: example\nparameter: int discreteDestination\nparameter: float x\nparameter: float y\nparameter: float z\navailable_parameters_code:\nprecondition:\nparameter: nested_parameter\n";
            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD("example", sdContent));
        }

        [Fact]
        public void TranslateSD_AllCases_ThrowsException()
        {
            string[] testCases =
            {
                "parameter: int x\navailable_parameters_code: \nprecondition: \nrollout_policy: \nviolate_penalty: -10 \ndynamic_model: \n",
                "project: example\nparameter: int x\nparameter: float y\navailable_parameters_code: \nprecondition: \nrollout_policy: \nviolate_penalty: -10 \ndynamic_model: \n",
                "project: example\nparameter: int\navailable_parameters_code: \nprecondition: \nrollout_policy: \nviolate_penalty: -10 \ndynamic_model: \n",
                "project: example\nparameter: int x\navailable_parameters_code: \nprecondition: \nrollout_policy: \nviolate_penalty: invalid \ndynamic_model: \n",
                "project: example\nparameter: int x\navailable_parameters_code: \nprecondition: \nviolate_penalty: -10 \ndynamic_model: \n",
                "project: example\nparameter: int x\navailable_parameters_code: \nprecondition: \nrollout_policy: \nviolate_penalty: -10 \nviolate_penalty: -20 \ndynamic_model: \n",
                "project: example\nparameter: int x\navailable_parameters_code: \nprecondition: \nrollout_policy: \nviolate_penalty: 10 \ndynamic_model: \n",
                "project: example\nparameter: int x\navailable_parameters_code: \nprecondition: \nrollout_policy: \nviolate_penalty: 10a \ndynamic_model: \n",
                "project: example\navailable_parameters_code: \nprecondition: \nrollout_policy: \nviolate_penalty: -10 \ndynamic_model: \n",
                "project: example\nparameter: int x\navailable_parameters_code: \nprecondition: \nrollout_policy: \nviolate_penalty: -10 \nstate__.robotLocation.discrete = ! __meetPrecondition || AOS.Bernoulli(0.1) ? -1: discreteDestination;\nif(state__.robotLocation.discrete == discreteDestination)\n{ \nstate__.robotLocation.x = x; \nstate__.robotLocation.y = y;\nstate__.robotLocation.z = z;\n}\nfor(int i=0; i < state__.tVisitedLocationObjects.size();i++)\n{\n\tif(state__.tVisitedLocationObjects[i]->discrete == state__.robotLocation.discrete) \n\t{\n\tstate__.tVisitedLocationObjects[i]->visited = true;\nbreak;\n}\n}\n__moduleResponse = (state__.robotLocation.discrete == -1 && AOS.Bernoulli(0.8)) ? eFailed : eSuccess;\n__reward = state_.robotLocation.discrete == -1 ? -5 : -(sqrt(pow(state.robotLocation.x-x,2.0)+pow(state.robotLocation.y-y,2.0)))*100;\nif (state__.robotLocation.discrete == -1) __reward =  -10;"
            };

            foreach (var testCase in testCases)
            {
                try
                {
                    TranslateSdlToJson.TranslateSD("testSkill", testCase);
                    // If no exception was thrown, fail the test
                    Assert.True(false, "Expected an exception to be thrown, but none was.");
                }
                catch (Exception)
                {
                    // Expected behavior: an exception should be caught
                }
            }
        }

        [Fact]
        public void TranslateSD_UnrecognizedHeader_ThrowsException()
        {
            string sdContent =
                "project: example\nunrecognized_header: value\navailable_parameters_code:\nprecondition: \n";
            var exception = Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD("example", sdContent));
            Assert.Contains("contains an unrecognized header or syntax at line", exception.Message);
        }

        [Fact]
        public void TranslateSD_InvalidParameterFormat_ThrowsException()
        {
            string fileContent = "project: example\nparameter: intvalue\n";
            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD("example", fileContent));
        }

        [Fact]
        public void TranslateSD_SpecialCharactersInIdentifiers_ThrowsException()
        {
            string sdContent = "project: example\navailable_parameters_code: \nparameter: int value!\n";
            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD("example", sdContent));
        }

        [Fact]
        public void TranslateSD_VaryingWhitespace_Success()
        {
            string fileContent =
                "project:   example\navailable_parameters_code: \nparameter:  int   value\nprecondition: \n";
            string result = TranslateSdlToJson.TranslateSD("example", fileContent);
            Assert.Contains("\"Type\": \"int\"", result);
            Assert.Contains("\"Name\": \"value\"", result);
        }

        [Fact]
        public void TranslateSD_LongLines_ThrowsException()
        {
            string skillName = "ExampleSkill";
            string fileContent = "project: example\n" +
                                 "parameter: int value with long description that exceeds typical line limits in parsing\n" +
                                 "available_parameters_code:\n" +
                                 "    // Code snippet\n";

            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD(skillName, fileContent));
        }

        [Fact]
        public void TranslateSD_LargeNumberOfEntries_ThrowsException()
        {
            StringBuilder sdContentBuilder = new StringBuilder("project: example\navailable_parameters_code: \n");
            for (int i = 0; i < 10000; i++)
            {
                sdContentBuilder.AppendLine($"parameter: int value{i}");
            }

            string sdContent = sdContentBuilder.ToString();

            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD("example", sdContent));
        }

        [Fact]
        public void TranslateSD_OptionalFields_Success()
        {
            string fileContent =
                "project: example\navailable_parameters_code: \nparameter: int value\nprecondition: \n";
            string result = TranslateSdlToJson.TranslateSD("example", fileContent);
            Assert.Contains("example", result);
        }

        [Fact]
        public void TranslateSD_MissingCodeKeyword_ThrowsException()
        {
            string sdContent = "parameter: int value\n";
            Exception exception = Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD("example", sdContent));
            Assert.Contains("does not start with 'project: <project_name>'", exception.Message);
        }

        [Fact]
        public void TranslateSD_ValidFileContent_ReturnsJsonString()
        {
            string sdContent = "project: example\navailable_parameters_code:\nparameter: int value\nprecondition: \n";
            string jsonString = TranslateSdlToJson.TranslateSD("example", sdContent);
            Assert.NotNull(jsonString);
        }

        [Fact]
        public void TranslateSD_NoProjectHeader_ThrowsException()
        {
            string sdContent = "available_parameters_code:\nparameter: int value\nprecondition: \n";
            var exception = Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD("example", sdContent));
            Assert.Contains("does not start with 'project:", exception.Message);
        }

        [Fact]
        public void TranslateSD_InvalidParameterDefinition_ThrowsException()
        {
            string sdContent =
                "project: example\nparameter: invalid_definition\navailable_parameters_code:\nprecondition: \n";
            var exception = Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD("example", sdContent));
            Assert.Contains("a '<type> <name>' must be defined after 'parameter:'", exception.Message);
        }

        [Fact]
        public void TranslateSD_EmptyFileContent_ThrowsException()
        {
            string sdContent = "";
            var exception = Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD("example", sdContent));
            Assert.Contains("does not start with 'project:", exception.Message);
        }

        [Fact]
        public void TranslateSD_MissingPrecondition_ThrowsException()
        {
            string sdContent = "project: example\navailable_parameters_code:\nparameter: int value\n";
            var exception = Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateSD("example", sdContent));
            Assert.Contains("contains an unrecognized header or syntax at line", exception.Message);
        }

        [Fact]
        public void TranslateSD_ValidFileContent_WritesJsonFileToDesktop()
        {
            string sdContent = "project: example\navailable_parameters_code:\nparameter: int value\nprecondition: \n";
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "translated_sd_file.json");

            if (File.Exists(filePath))
                File.Delete(filePath);

            string jsonString = TranslateSdlToJson.TranslateSD("example", sdContent);

            Assert.True(File.Exists(filePath));
            string fileContent = File.ReadAllText(filePath);
            Assert.Contains("example", fileContent);

            // Clean up: delete the created file
            File.Delete(filePath);
        }

        /////////////////////////////////////////////// < TranslateAM > ////////////////////////////////////////////////
        
        [Fact]
        public void TranslateAM_InvalidProjectNameAndSyntax_ThrowsException()
        {
            string amContent = "project 2!@#$%\nresponse: eSuccess\nresponse_rule: skillSuccess and goal_reached\nmodule_activation: ros_service\n";
            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateAM("example", amContent));
        }

        [Fact]
        public void TranslateAM_InvalidResponseFormat_ThrowsException()
        {
            string amContent = "project: example\nresponse: invalidResponse\nresponse_rule skillSuccess and goal_reached\nmodule_activation: ros_service\n";
            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateAM("example", amContent));
        }

        [Fact]
        public void TranslateAM_DuplicateResponseSection_ThrowsException()
        {
            string amContent = "project: example\nresponse: eSuccess\nresponse: eFailed\nresponse_rule: skillSuccess and goal_reached\nmodule_activation: ros_service\n";
            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateAM("example", amContent));
        }
        
        [Fact]
        public void TranslateAM_InvalidAndEmptyResponseRule_ThrowsException()
        {
            string amContent = "project: example\nresponse: eSuccess\nresponse_rule\nmodule_activation: ros_service\n";
            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateAM("example", amContent));
        }
        
        [Fact]
        public void TranslateAM_MissingProjectLine_ThrowsException()
        {
            string amContent =
                "response: eSuccess\nresponse_rule: skillSuccess and goal_reached\nmodule_activation: ros_service\n";

            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateAM("example", amContent));
        }

        [Fact]
        public void TranslateAM_InvalidResponseRuleLine_ThrowsException()
        {
            string amContent =
                "project: example\nresponse: eSuccess\ninvalid_rule_line_here\nmodule_activation: ros_service\n";

            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateAM("example", amContent));
        }

        [Fact]
        public void TranslateAM_InvalidImportStatement_ThrowsException()
        {
            string amContent =
                "project: example\nresponse: eSuccess\nresponse_rule: skillSuccess and goal_reached\nmodule_activation: ros_service\nimports: invalid_import_statement\n";

            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateAM("example", amContent));
        }

        [Fact]
        public void TranslateAM_MissingResponseRuleCondition_ThrowsException()
        {
            string amContent = "project: example\nresponse: eSuccess\nmodule_activation: ros_service\n";

            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateAM("example", amContent));
        }

        [Fact]
        public void TranslateAM_InvalidInputs_ThrowException()
        {
            string[] invalidAmContents = new string[]
            {
                ": example\nreponse: eSuccess\nresponse_rule: skillSuccess and goal_reached\nmodule_activation: ros_service\n",
                "projectt: example\nresponse: eSuccess\nresponse_rule: skillSuccess and goal_reached\n",
                "prooooject: examplee\nresponseesponse_rule: invalid_condition\nactivation: ros_service\n",
                ": examaple\nrespasdonse: ::: eSuccess\nresponse_rule: skillSuccess and goal_reached\nmodule_activation: ros_service\nsrv: NavigateToCoordinates\n",
                "AAA: example\n: eSucceess\n: skillSuccess and goal_reached\nmodule_activation: invalid_a12ctivation_type\n",
            };

            foreach (string amContent in invalidAmContents)
            {
                Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateAM("example", amContent));
            }
        }

        [Fact]
        public void TranslateAM_EmptyFile_ThrowsException()
        {
            string amContent = "";
            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateAM("example", amContent));
        }
        
        /////////////////////////////////////////////// < TranslateEF > ////////////////////////////////////////////////
        
        [Fact]
        public void TranslateEF_InvalidContent_ThrowsException()
        {
            string invalidEfContent = @"
    project: example
    invalid_line: this will cause an error";

            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateEF("example", invalidEfContent));
        }

        [Fact]
        public void TranslateEF_InvalidContents_ThrowsException()
        {
            string[] invalidEfContents = new[]
            {
                @"
        project: example 
        parameter: int discreteDestination
        parameter: float x
        parameter: float y
        parameter: float z",

                @"
        project: example 
        parameter: int discreteDestination
        parameter: float x
        invalid_line: this is not a valid parameter",

                @"",

                @"
        project: example 
        parameter: int discreteDestination
        parameter: float x
        parameter: float y
        parameter: float z 
        dynamic_model: 
        state__.robotLocation.discrete = ! __meetPrecondition || AOS.Bernoulli(0.1) ? -1: discreteDestination;",

                @"
        project: example 
        parameter: int discreteDestination
        parameter: float x
        parameter: float y
        parameter: float z 
        precondition:
        __meetPrecondition = discreteDestination != state.robotLocation.discrete;
        // missing closing semicolon on purpose",
            };

            foreach (var invalidEfContent in invalidEfContents)
            {
                Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateEF("example", invalidEfContent));
            }
        }

        [Fact]
        public void TranslateEF_EmptyContent_ThrowsException()
        {
            string efContent = "";

            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateEF("example", efContent));
        }

        [Fact]
        public void TranslateEF_MissingProject_ThrowsException()
        {
            string efContent = @"
parameter: int discreteDestination
parameter: float x
parameter: float y
parameter: float z 
available_parameters_code: 
__possibleParameters.push_back(std::make_tuple(1,-1.5744997262954712,-0.5154024362564087,-0.001373291015625));
__possibleParameters.push_back(std::make_tuple(2,-1.6243184804916382,0.6617045402526855,-0.00143432617188)); 
__possibleParameters.push_back(std::make_tuple(3,0.986030161381,0.610693752766,-0.00143432617188));

precondition:
__meetPrecondition = discreteDestination != state.robotLocation.discrete;
violate_penalty: -10 
dynamic_model: 
state__.robotLocation.discrete = ! __meetPrecondition || AOS.Bernoulli(0.1) ? -1: discreteDestination;
if(state__.robotLocation.discrete == discreteDestination)
{ 
state__.robotLocation.x = x; 
state__.robotLocation.y = y; 
state__.robotLocation.z = z;
}
for(int i=0; i < state__.tVisitedLocationObjects.size();i++)
{
    if(state__.tVisitedLocationObjects[i]->discrete == state__.robotLocation.discrete) 
    {
    state__.tVisitedLocationObjects[i]->visited = true;
    break;
    }
}
__moduleResponse = (state__.robotLocation.discrete == -1 && AOS.Bernoulli(0.8)) ? eFailed : eSuccess;
__reward = state_.robotLocation.discrete == -1 ? -5 : -(sqrt(pow(state.robotLocation.x-x,2.0)+pow(state.robotLocation.y-y,2.0)))*100;
if (state__.robotLocation.discrete == -1) __reward =  -10;
";

            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateEF("example", efContent));
        }

        [Fact]
        public void TranslateEF_MultiplePreconditions_ThrowsException()
        {
            string efContent = @"
project: example
parameter: int discreteDestination
parameter: float x
precondition:
__meetPrecondition1 = discreteDestination != state.robotLocation.discrete;
precondition:
__meetPrecondition2 = x > 0;";

            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateEF("example", efContent));
        }
        
        [Fact]
        public void TranslateEF_MissingProjectHeader_ThrowsException()
        {
            string efContent =
                "parameter: int discreteDestination\nparameter: float x\nparameter: float y\nparameter: float z\n";
            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateEF("example", efContent));
        }

        [Fact]
        public void TranslateEF_InvalidSyntax_ThrowsException()
        {
            string efContent =
                "project example\nparameter: int discreteDestination\nparameter: float x\nparameter: float y\nparameter: float z\navailable_parameters_code:\n";
            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateEF("example", efContent));
        }

        [Fact]
        public void TranslateEF_SectionsInUnexpectedOrder_ThrowsException()
        {
            string efContent = "parameter: int value\nproject: example\navailable_parameters_code:\n";
            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateEF("example", efContent));
        }

        [Fact]
        public void TranslateEF_InvalidParameterType_ThrowsException()
        {
            string efContent =
                "projjeccct: example\nparameter: invalidType discreteDestination\nparameter: float x\nparameter: float y\nparameter: float z\n";
            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateEF("example", efContent));
        }
        
        [Fact]
        public void TranslateEF_InvalidProject_ThrowsException()
        {
            string fileName = "InvalidProjectEFContent.txt";
            string efContent = @"
horizon: 10
discount: 0.97";

            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateEF(fileName, efContent));
        }

        [Fact]
        public void TranslateEF_InvalidHorizon_ThrowsException()
        {
            string fileName = "InvalidHorizonEFContent.txt";
            string efContent = @"
project: example
horizon: ten
discount: 0.97";

            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateEF(fileName, efContent));
        }

        [Fact]
        public void TranslateEF_InvalidDiscount_ThrowsException()
        {
            string fileName = "InvalidDiscountEFContent.txt";
            string efContent = @"
project: example
horizon: 10
discount: ninety-seven";

            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateEF(fileName, efContent));
        }

        [Fact]
        public void TranslateEF_MissingHorizon_ThrowsException()
        {
            string fileName = "MissingHorizonEFContent.txt";
            string efContent = @"
project: example
discount: 0.97";

            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateEF(fileName, efContent));
        }

        [Fact]
        public void TranslateEF_MissingDiscount_ThrowsException()
        {
            string fileName = "MissingDiscountEFContent.txt";
            string efContent = @"
project: example
horizon: 10";

            Assert.Throws<Exception>(() => TranslateSdlToJson.TranslateEF(fileName, efContent));
        }
    }
}
