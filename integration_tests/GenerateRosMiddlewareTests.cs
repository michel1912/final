using Xunit;
using System.IO;
using WebApiCSharp.JsonTextModel;

namespace integration_tests
{
    public class GenerateRosMiddlewareTests
    {
        /////////////////////////////////////// INTEGRATION TEST OF TRANSLATE AM FILE //////////////////////////////////
        [Fact]
        public void TranslateAM_ValidContent_ReturnsCorrectJson()
        {
            // Arrange
            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"../../../../"));
            string amContentPath = Path.Combine(projectRoot, "integration_tests", "TestData", "AMContent.txt");
            string expectedJsonPath = Path.Combine(projectRoot, "integration_tests", "TestData", "AMJson.json");

            string amContent = File.ReadAllText(amContentPath);
            string expectedJson = File.ReadAllText(expectedJsonPath);
            string skillName = "navigate";

            // Act
            string jsonResult = TranslateSdlToJson.TranslateAM(skillName, amContent);

            // Assert
            Assert.Equal(NormalizeJsonString(expectedJson), NormalizeJsonString(jsonResult));
        }



        ////////////////////////////////// INTEGRATION TEST OF TRANSLATE SD FILE ///////////////////////////////////////
        [Fact]
        public void TranslateSD_ValidContent_ReturnsCorrectJson()
        {
            // Arrange
            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"../../../../"));
            string sdContentPath = Path.Combine(projectRoot, "integration_tests", "TestData", "sdContent.txt");
            string expectedJsonPath = Path.Combine(projectRoot, "integration_tests", "TestData", "expectedJson.json");

            string sdContent = File.ReadAllText(sdContentPath);
            string expectedJson = File.ReadAllText(expectedJsonPath);
            string skillName = "navigate";

            // Act
            string jsonResult = TranslateSdlToJson.TranslateSD(skillName, sdContent);

            // Assert
            Assert.Equal(NormalizeJsonString(expectedJson), NormalizeJsonString(jsonResult));
        }

        ///////////////////////////////////// INTEGRATION TEST OF TRANSLATE EF FILE //////////////////////////////////////
        [Fact]
        public void TranslateEnvironment_ValidContent_ReturnsCorrectJson()
        {
            // Arrange
            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"../../../../"));
            string environmentContentPath = Path.Combine(projectRoot, "integration_tests", "TestData", "EFContent.txt");
            string expectedJsonPath = Path.Combine(projectRoot, "integration_tests", "TestData", "EFJson.json");

            string environmentContent = File.ReadAllText(environmentContentPath);
            string expectedJson = File.ReadAllText(expectedJsonPath);
            string skillName = "navigate";

            // Act
            string jsonResult = TranslateSdlToJson.TranslateEF(skillName, environmentContent);

            // Assert
            Assert.Equal(NormalizeJsonString(expectedJson), NormalizeJsonString(jsonResult));
        }


        private string NormalizeJsonString(string json)
        {
            // Remove all whitespace characters and extra commas
            json = json.Replace(" ", "").Replace("\n", "").Replace("\r", "").Replace("\t", "");
            return json;
        }
    }
}