using NUnit.Framework;
using WebApiCSharp.Models;
using WebApiCSharp.BL;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
//using AutoFixture.NUnit3;
using WebApiCSharp.GenerateCodeFiles;

namespace unit_tests
{
    public class InitializeProjectBLTests
    {
        private InitializeProject _initializeProject;
        private PLPsData _plpsData;
        private InitializeProjectBL _initializeProjectBL;

        [SetUp]
        public void Setup()
        {
            _initializeProject = new InitializeProject
            {
                RosTarget = new RosTargetProject
                {
                    WorkspaceDirectortyPath = "/path/to/workspace",
                    RosDistribution = "noetic",
                    TargetProjectLaunchFile = "launch_file.launch",
                    TargetProjectInitializationTimeInSeconds = 5
                },
                RunWithoutRebuild = true,
                OnlyGenerateCode = false,
                SolverConfiguration = new SolverConfiguration
                {
                    IsInternalSimulation = false,
                    PlanningTimePerMoveInSeconds = 10
                }
            };
            
            _plpsData = new PLPsData(out var errors)
            {
                ProjectName = "TestProject"
            };

            _initializeProjectBL = new InitializeProjectBL();
        }

        [Test]
        public void Test_GetRunSolverBashFile_ReturnsCorrectScript()
        {
            _plpsData = new PLPsData(out var errors)
            {
                ProjectName = "TestProject"
            };

            Assert.That(_plpsData, Is.Not.Null, "PLPsData is null");
            Assert.That(_plpsData.ProjectName, Is.Not.Null, "PLPsData.ProjectName is null");

            string result = InitializeProjectBL.GetRunSolverBashFile(_plpsData);

            Assert.That(result, Is.Not.Null, "Result from GetRunSolverBashFile is null");

            string expectedScriptPart = 
                @"#!/bin/bash\n" +
                @"cd ../../AOS-Solver/build/examples/cpp_models/TestProject";

            Assert.That(result, Does.Contain("pwd"), "Expected script to contain 'pwd'");
            Assert.That(result, Does.Contain($"./despot_{_plpsData.ProjectName}"),
                $"Expected script to contain './despot_{_plpsData.ProjectName}'");
        }
        
        [Test]
        public void Test_GetBuildRosMiddlewareBashFile_ReturnsCorrectScript()
        {
            string result = (string)typeof(InitializeProjectBL)
                .GetMethod("GetBuildRosMiddlewareBashFile",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                .Invoke(null, new object[] { _initializeProject });

            string expectedScript = "#!/bin/bash\n\ncd /path/to/workspace\ncatkin_make\nsource ~/.bashrc";
            Assert.That(result.Trim(), Is.EqualTo(expectedScript));
        }

        [Test]
        public void Test_GetBuildRos2MiddlewareBashFile_ReturnsCorrectScript()
        {
            _initializeProject.RosTarget.WorkspaceDirectortyPath = "/path/to/workspace";

            string result = (string)typeof(InitializeProjectBL)
                .GetMethod("GetBuildRos2MiddlewareBashFile",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                .Invoke(null, new object[] { _initializeProject });

            string expectedScript = "#!/bin/bash\n\ncd /path/to/workspace\ncolcon build\nsource ~/.bashrc";
            Assert.That(result, Is.EqualTo(expectedScript));
        }

        [Test]
        public void Test_GetBuildSolverBashFile_ReturnsCorrectScript()
        {
            string result = (string)typeof(InitializeProjectBL)
                .GetMethod("GetBuildSolverBashFile",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                .Invoke(null, new object[] { "TestProject" });

            string expectedScript = "#!/bin/bash\n\ncmake --build " + Environment.GetEnvironmentVariable("HOME") +
                                    "/AOS/AOS-Solver/build --config Release --target despot_TestProject -j 10 --";
            Assert.That(result.Trim(), Is.EqualTo(expectedScript));
        }
        
        // method RunSolver - ensures that invoking RunSolver behaves as expected.
        [Test]
        public void Test_RunSolver_CallsRunBashCommand()
        {
            var plpsData = new PLPsData(out var errors)
            {
                ProjectName = "TestProject"
            };

            typeof(InitializeProjectBL).GetMethod("RunSolver",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                .Invoke(null, new object[] { plpsData });
        }

        //LoadPLPs method ; InitializeProjectBL - checking for error handling when loading PLPs from an invalid directory path.
        [Test]
        public void Test_InitializeProject_HandlesErrors()
        {
            List<string> errors;
            List<string> remarks;
            string buildOutput;
            string buildRosMiddlewareOutput;

            InitializeProjectBL.InitializeProject(_initializeProject, out errors, out remarks, out buildOutput, out buildRosMiddlewareOutput);

            // no directory 
            Assert.That(errors, Is.Not.Empty);
            Assert.That(remarks, Is.Empty);
            Assert.That(buildOutput, Is.Empty);
            Assert.That(buildRosMiddlewareOutput, Is.Empty);
        }

        // LoadPLPs ; InitializeProjectBL 
        [Test]
        public void Test_LoadPLPs_ReturnsErrorsForInvalidDirectory()
        {
            string invalidDirectoryPath = "/invalid/path";
            var parameters = new object[] { invalidDirectoryPath, null };
            List<string> result = (List<string>)typeof(InitializeProjectBL)
                .GetMethod("LoadPLPs", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                .Invoke(null, parameters);

            Assert.That(result, Is.Not.Empty);
            Assert.That(result[0], Is.EqualTo("The PLPs directory '/invalid/path', is not a Directory!"));
        }

        [Test]
        public void IsValidPLP_ValidPLP_ReturnsTrue()
        {
            string validJson = "{\"PlpMain\": { \"example\": \"data\" }}";
            JsonDocument jsonDocument = JsonDocument.Parse(validJson);
            List<string> errorMessages;
            bool isValid = InitializeProjectBL.IsValidPLP(jsonDocument, out errorMessages);

            Assert.That(isValid, Is.True);
            Assert.That(errorMessages, Is.Empty);
        }

        [Test]
        public void Test_IsValidPLP_ReturnsTrueForValidPLP()
        {
            string jsonContent = @"{ ""PlpMain"": {} }";
            JsonDocument plp = JsonDocument.Parse(jsonContent);
            var errors = new List<string>(); 
            bool result = InitializeProjectBL.IsValidPLP(plp, out errors);
            
            Assert.That(result, Is.EqualTo(true));
            Assert.That(errors, Is.Empty);
        }
    }
}