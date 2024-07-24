using System;
using System.Collections.Generic;
using System.Text.Json;
using MongoDB.Bson;
using NUnit.Framework;
using WebApiCSharp.BL;
using WebApiCSharp.GenerateCodeFiles;
using WebApiCSharp.Models;
using WebApiCSharp.Services;

namespace WebApiCSharp.Tests
{
    [TestFixture]
    public class InitializeProjectBLTests
    {
        private static readonly Random Random = new Random();

        private PLPsData GenerateRandomPLPsData(out List<string> errors)
        {
            var fakePLPs = new List<BsonDocument>
            {
                new BsonDocument { { "name", "PLP1" }, { "type", "PLP" } },
                new BsonDocument { { "name", "PLP2" }, { "type", "Environment" } }
            };

            PLPsService.SetFakePLPs(fakePLPs);

            return new PLPsData(out errors);
        }

        public static class PLPsService
        {
            private static List<BsonDocument> _fakePLPs;

            public static void SetFakePLPs(List<BsonDocument> fakePLPs)
            {
                _fakePLPs = fakePLPs;
            }

            public static List<BsonDocument> GetAll()
            {
                return _fakePLPs ?? new List<BsonDocument>();
            }

            public static BsonDocument Add(JsonDocument plp)
            {
                return BsonDocument.Parse(plp.RootElement.GetRawText());
            }
        }

        private string GenerateRandomJsonDocument(bool valid)
        {
            if (valid)
            {
                return "{\"PlpMain\": {\"name\": \"TestPLP" + Random.Next(1000, 9999) + "\"}}";
            }
            else
            {
                return "{\"name\": \"TestPLP" + Random.Next(1000, 9999) + "\"}";
            }
        }

        [Test]
        public void TestGetRunSolverBashFile()
        {
            List<string> errors;
            var data = GenerateRandomPLPsData(out errors);

            string result = InitializeProjectBL.GetRunSolverBashFile(data);

            Assert.That(result, Does.Contain("cd "));
            Assert.That(result, Does.Contain("./despot_" + data.ProjectName));
        }

        [Test]
        public void TestIsValidPLP()
        {
            var validJson = GenerateRandomJsonDocument(true);
            var invalidJson = GenerateRandomJsonDocument(false);

            using (JsonDocument validPlp = JsonDocument.Parse(validJson))
            {
                bool isValid = InitializeProjectBL.IsValidPLP(validPlp, out List<string> errorMessages);
                Assert.That(isValid, Is.True);
                Assert.That(errorMessages, Is.Empty);
            }

            using (JsonDocument invalidPlp = JsonDocument.Parse(invalidJson))
            {
                bool isValid = InitializeProjectBL.IsValidPLP(invalidPlp, out List<string> errorMessages);
                Assert.That(isValid, Is.False);
                Assert.That(errorMessages, Is.Not.Empty);
                Assert.That(errorMessages[0], Is.EqualTo("no 'PlpMain' element"));
            }
        }  
    }
}
