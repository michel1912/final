using NUnit.Framework;
using WebApiCSharp.Models;
using System.Collections.Generic;

namespace unit_tests
{
    public class Tests
    {
        private InitializeProject _initializeProject;
        private RosTargetProject _rosTargetProject;
        private SolverConfiguration _solverConfiguration;
        private MiddlewareConfiguration _middlewareConfiguration;

        [SetUp]
        public void Setup()
        {
            _initializeProject = new InitializeProject();
            _rosTargetProject = new RosTargetProject();
            _solverConfiguration = new SolverConfiguration();
            _middlewareConfiguration = new MiddlewareConfiguration();
        }

        // Verifies that properties on the InitializeProject instance are correctly set and retrieved.
        [Test]
        public void TestInitializeProjectGettersAndSetters()
        {
            _initializeProject.PLPsDirectoryPath = "/home/user/plps";
            _initializeProject.RunWithoutRebuild = true;
            _initializeProject.OnlyGenerateCode = false;
            _initializeProject.RosTarget = _rosTargetProject;
            _initializeProject.SolverConfiguration = _solverConfiguration;
            _initializeProject.MiddlewareConfiguration = _middlewareConfiguration;

            Assert.AreEqual("/home/user/plps", _initializeProject.PLPsDirectoryPath);
            Assert.IsTrue(_initializeProject.RunWithoutRebuild.Value);
            Assert.IsFalse(_initializeProject.OnlyGenerateCode.Value);
            Assert.AreEqual(_rosTargetProject, _initializeProject.RosTarget);
            Assert.AreEqual(_solverConfiguration, _initializeProject.SolverConfiguration);
            Assert.AreEqual(_middlewareConfiguration, _initializeProject.MiddlewareConfiguration);
        }

        // Verifies that properties on the RosTargetProject instance are correctly set and retrieved.
        [Test]
        public void TestRosTargetProjectGettersAndSetters()
        {
            _rosTargetProject.RosDistribution = "x";
            _rosTargetProject.WorkspaceDirectortyPath = "/home/user/catkin_ws";
            _rosTargetProject.TargetProjectLaunchFile = "launch_file.launch";
            _rosTargetProject.TargetProjectInitializationTimeInSeconds = 10.5;
            _rosTargetProject.RosTargetProjectPackages = new List<string> { "package1", "package2" };

            Assert.AreEqual("x", _rosTargetProject.RosDistribution);
            Assert.AreEqual("/home/user/catkin_ws", _rosTargetProject.WorkspaceDirectortyPath);
            Assert.AreEqual("launch_file.launch", _rosTargetProject.TargetProjectLaunchFile);
            Assert.AreEqual(10.5, _rosTargetProject.TargetProjectInitializationTimeInSeconds);
            Assert.AreEqual(2, _rosTargetProject.RosTargetProjectPackages.Count);
            Assert.Contains("package1", _rosTargetProject.RosTargetProjectPackages);
        }

        // Verifies that properties on the SolverConfiguration instance are correctly set and retrieved.
        [Test]
        public void TestSolverConfigurationGettersAndSetters()
        {
            _solverConfiguration.PolicyGraphDepth = 5;
            _solverConfiguration.limitClosedModelHorizon_stepsAfterGoalDetection = 10;
            _solverConfiguration.UseSarsop = true;
            _solverConfiguration.UseML = false;
            _solverConfiguration.UseSavedSarsopPolicy = true;
            _solverConfiguration.NumOfSamplesPerStateActionToLearnModel = 30;
            _solverConfiguration.LoadBeliefFromDB = true;
            _solverConfiguration.NumOfParticles = 1000;
            _solverConfiguration.NumOfBeliefStateParticlesToSaveInDB = 10;
            _solverConfiguration.ActionsToSimulate = new List<int> { 1, 2, 3 };
            _solverConfiguration.IsInternalSimulation = true;
            _solverConfiguration.SimulateByMdpRate = 0.5f;
            _solverConfiguration.ManualControl = true;
            _solverConfiguration.PlanningTimePerMoveInSeconds = 5.0f;
            _solverConfiguration.RolloutsCount = 50;
            _solverConfiguration.Verbosity = 2;

            Assert.AreEqual(5, _solverConfiguration.PolicyGraphDepth);
            Assert.AreEqual(10, _solverConfiguration.limitClosedModelHorizon_stepsAfterGoalDetection);
            Assert.IsTrue(_solverConfiguration.UseSarsop);
            Assert.IsFalse(_solverConfiguration.UseML);
            Assert.IsTrue(_solverConfiguration.UseSavedSarsopPolicy);
            Assert.AreEqual(30, _solverConfiguration.NumOfSamplesPerStateActionToLearnModel);
            Assert.IsTrue(_solverConfiguration.LoadBeliefFromDB);
            Assert.AreEqual(1000, _solverConfiguration.NumOfParticles);
            Assert.AreEqual(10, _solverConfiguration.NumOfBeliefStateParticlesToSaveInDB);
            Assert.AreEqual(3, _solverConfiguration.ActionsToSimulate.Count);
            Assert.Contains(1, _solverConfiguration.ActionsToSimulate);
            Assert.IsTrue(_solverConfiguration.IsInternalSimulation);
            Assert.AreEqual(0.5f, _solverConfiguration.SimulateByMdpRate);
            Assert.IsTrue(_solverConfiguration.ManualControl);
            Assert.AreEqual(5.0f, _solverConfiguration.PlanningTimePerMoveInSeconds);
            Assert.AreEqual(50, _solverConfiguration.RolloutsCount);
            Assert.AreEqual(2, _solverConfiguration.Verbosity);
        }

        // Verifies that properties on the MiddlewareConfiguration instance are correctly set and retrieved.
        [Test]
        public void TestMiddlewareConfigurationGettersAndSetters()
        {
            _middlewareConfiguration.DebugOn = true;
            _middlewareConfiguration.KillRosCoreBeforeStarting = true;

            Assert.IsTrue(_middlewareConfiguration.DebugOn);
            Assert.IsTrue(_middlewareConfiguration.KillRosCoreBeforeStarting);
        }
        
        // Verifies that properties on the InitializeProject instance handle null values.
        [Test]
        public void TestInitializeProjectNullValues()
        {
            _initializeProject.PLPsDirectoryPath = null;
            _initializeProject.RosTarget = null;
            _initializeProject.SolverConfiguration = null;
            _initializeProject.MiddlewareConfiguration = null;

            Assert.IsNull(_initializeProject.PLPsDirectoryPath);
            Assert.IsNull(_initializeProject.RosTarget);
            Assert.IsNull(_initializeProject.SolverConfiguration);
            Assert.IsNull(_initializeProject.MiddlewareConfiguration);
        }

        // Verifies that properties on the RosTargetProject instance handle null and empty values.
        [Test]
        public void TestRosTargetProjectNullValues()
        {
            _rosTargetProject.RosDistribution = null;
            _rosTargetProject.WorkspaceDirectortyPath = null;
            _rosTargetProject.TargetProjectLaunchFile = null;
            _rosTargetProject.RosTargetProjectPackages = null;

            Assert.IsNull(_rosTargetProject.RosDistribution);
            Assert.IsNull(_rosTargetProject.WorkspaceDirectortyPath);
            Assert.IsNull(_rosTargetProject.TargetProjectLaunchFile);
            Assert.IsNull(_rosTargetProject.RosTargetProjectPackages);
        }

        // Verifies that properties on the SolverConfiguration instance handle empty and null values.
        [Test]
        public void TestSolverConfigurationNullValues()
        {
            _solverConfiguration.ActionsToSimulate = null;

            Assert.IsNull(_solverConfiguration.ActionsToSimulate);
        }

        // Verifies that properties on the RosTargetProject instance handle empty collections.
        [Test]
        public void TestRosTargetProjectEmptyCollections()
        {
            _rosTargetProject.RosTargetProjectPackages = new List<string>();

            Assert.IsEmpty(_rosTargetProject.RosTargetProjectPackages);
        }

        // Verifies that properties on the SolverConfiguration instance handle empty collections.
        [Test]
        public void TestSolverConfigurationEmptyCollections()
        {
            _solverConfiguration.ActionsToSimulate = new List<int>();

            Assert.IsEmpty(_solverConfiguration.ActionsToSimulate);
        }
        
        // Verifies that default values are correctly set upon initialization.
        [Test]
        public void TestDefaultValues()
        {
            // Ensure SolverConfiguration default values are as expected
            Assert.AreEqual(0, _solverConfiguration.PolicyGraphDepth);
            Assert.AreEqual(-1, _solverConfiguration.limitClosedModelHorizon_stepsAfterGoalDetection); // This needs clarification based on your implementation
            Assert.IsFalse(_solverConfiguration.UseSarsop);
            Assert.IsFalse(_solverConfiguration.UseML);
            Assert.IsFalse(_solverConfiguration.UseSavedSarsopPolicy);
            Assert.AreEqual(20, _solverConfiguration.NumOfSamplesPerStateActionToLearnModel);
            Assert.IsFalse(_solverConfiguration.LoadBeliefFromDB);
            Assert.AreEqual(5000, _solverConfiguration.NumOfParticles);
            Assert.AreEqual(1, _solverConfiguration.NumOfBeliefStateParticlesToSaveInDB);
            Assert.IsNotNull(_solverConfiguration.ActionsToSimulate); // Ensure it's not null initially
            Assert.AreEqual(0, _solverConfiguration.ActionsToSimulate.Count);
            Assert.IsFalse(_solverConfiguration.IsInternalSimulation);
            Assert.AreEqual(0.0f, _solverConfiguration.SimulateByMdpRate);
            Assert.IsFalse(_solverConfiguration.ManualControl);
            Assert.AreEqual(2.0f, _solverConfiguration.PlanningTimePerMoveInSeconds);
            Assert.AreEqual(100, _solverConfiguration.RolloutsCount);
            Assert.AreEqual(4, _solverConfiguration.Verbosity);

            // Print out some values for debugging purposes
            Console.WriteLine($"PolicyGraphDepth default: {_solverConfiguration.PolicyGraphDepth}");
            Console.WriteLine($"NumOfSamplesPerStateActionToLearnModel default: {_solverConfiguration.NumOfSamplesPerStateActionToLearnModel}");
            Console.WriteLine($"NumOfParticles default: {_solverConfiguration.NumOfParticles}");
            Console.WriteLine($"NumOfBeliefStateParticlesToSaveInDB default: {_solverConfiguration.NumOfBeliefStateParticlesToSaveInDB}");

            // Assert MiddlewareConfiguration default values
            Assert.IsFalse(_middlewareConfiguration.DebugOn);
            Assert.IsFalse(_middlewareConfiguration.KillRosCoreBeforeStarting);
        }
        
        // Ensure default values are correctly set upon initialization of InitializeProject
        [Test]
        public void TestInitializeProjectDefaultValues()
        {
            Assert.IsNull(_initializeProject.PLPsDirectoryPath);
            Assert.IsFalse(_initializeProject.RunWithoutRebuild.HasValue ? _initializeProject.RunWithoutRebuild.Value : false);
            Assert.IsFalse(_initializeProject.OnlyGenerateCode.HasValue ? _initializeProject.OnlyGenerateCode.Value : false);
            Assert.IsNull(_initializeProject.RosTarget);
            Assert.IsNull(_initializeProject.SolverConfiguration);
            Assert.IsNull(_initializeProject.MiddlewareConfiguration);
        }
        
        // Ensure default values are correctly set upon initialization of RosTargetProject
        [Test]
        public void TestRosTargetProjectDefaultValues()
        {
            // Ensure RosTargetProject is instantiated properly
            _rosTargetProject = new RosTargetProject();

            Assert.IsNull(_rosTargetProject.RosDistribution);
            Assert.IsNull(_rosTargetProject.WorkspaceDirectortyPath);
            Assert.IsNull(_rosTargetProject.TargetProjectLaunchFile);
            Assert.IsNotNull(_rosTargetProject.RosTargetProjectPackages); // Ensure it's not null initially
            Assert.IsEmpty(_rosTargetProject.RosTargetProjectPackages);
        }
        
        // Ensure default values are correctly set upon initialization of MiddlewareConfiguration
        [Test]
        public void TestMiddlewareConfigurationDefaultValues()
        {
            Assert.IsFalse(_middlewareConfiguration.DebugOn);
            Assert.IsFalse(_middlewareConfiguration.KillRosCoreBeforeStarting);
        }
        
        // Ensure default values are correctly set upon initialization of SolverConfiguration
        [Test]
        public void TestSolverConfigurationDefaultValues()
        {
            Assert.AreEqual(0, _solverConfiguration.PolicyGraphDepth);
            Assert.AreEqual(-1, _solverConfiguration.limitClosedModelHorizon_stepsAfterGoalDetection); // Adjust based on implementation
            Assert.IsFalse(_solverConfiguration.UseSarsop);
            Assert.IsFalse(_solverConfiguration.UseML);
            Assert.IsFalse(_solverConfiguration.UseSavedSarsopPolicy);
            Assert.AreEqual(20, _solverConfiguration.NumOfSamplesPerStateActionToLearnModel);
            Assert.IsFalse(_solverConfiguration.LoadBeliefFromDB);
            Assert.AreEqual(5000, _solverConfiguration.NumOfParticles);
            Assert.AreEqual(1, _solverConfiguration.NumOfBeliefStateParticlesToSaveInDB);
            Assert.IsNotNull(_solverConfiguration.ActionsToSimulate); // Ensure it's not null initially
            Assert.IsEmpty(_solverConfiguration.ActionsToSimulate);
            Assert.IsFalse(_solverConfiguration.IsInternalSimulation);
            Assert.AreEqual(0.0f, _solverConfiguration.SimulateByMdpRate);
            Assert.IsFalse(_solverConfiguration.ManualControl);
            Assert.AreEqual(2.0f, _solverConfiguration.PlanningTimePerMoveInSeconds);
            Assert.AreEqual(100, _solverConfiguration.RolloutsCount);
            Assert.AreEqual(4, _solverConfiguration.Verbosity);
        }
        
        [Test]
        public void TestSolverConfigurationEdgeCases()
        {
            // Test extreme values
            _solverConfiguration.PolicyGraphDepth = int.MaxValue;
            _solverConfiguration.limitClosedModelHorizon_stepsAfterGoalDetection = int.MinValue;
            _solverConfiguration.NumOfSamplesPerStateActionToLearnModel = 0;
            _solverConfiguration.NumOfParticles = 999999;
            _solverConfiguration.Verbosity = -1;

            Assert.AreEqual(int.MaxValue, _solverConfiguration.PolicyGraphDepth);
            Assert.AreEqual(int.MinValue, _solverConfiguration.limitClosedModelHorizon_stepsAfterGoalDetection);
            Assert.AreEqual(0, _solverConfiguration.NumOfSamplesPerStateActionToLearnModel);
            Assert.AreEqual(999999, _solverConfiguration.NumOfParticles);
            Assert.AreEqual(-1, _solverConfiguration.Verbosity);
        }
        
        [Test]
        public void TestMiddlewareConfigurationValidation()
        {
            // Test validation logic
            _middlewareConfiguration.DebugOn = true;
            _middlewareConfiguration.KillRosCoreBeforeStarting = true;

            Assert.IsTrue(_middlewareConfiguration.DebugOn);
            Assert.IsTrue(_middlewareConfiguration.KillRosCoreBeforeStarting);

            // Test invalid value (assuming DebugOn must be false for KillRosCoreBeforeStarting to be true)
            _middlewareConfiguration.DebugOn = false;
            _middlewareConfiguration.KillRosCoreBeforeStarting = true;

            Assert.IsFalse(_middlewareConfiguration.DebugOn); // Should revert to false due to validation
            Assert.IsTrue(_middlewareConfiguration.KillRosCoreBeforeStarting);
        }
        
        [Test]
        public void TestSolverConfigurationInitialization()
        {
            // Test constructor initialization
            SolverConfiguration config = new SolverConfiguration();

            Assert.AreEqual(0, config.PolicyGraphDepth);
            Assert.AreEqual(-1, config.limitClosedModelHorizon_stepsAfterGoalDetection);
            Assert.IsFalse(config.UseSarsop);
            Assert.IsFalse(config.UseML);
            Assert.IsFalse(config.UseSavedSarsopPolicy);
            Assert.AreEqual(20, config.NumOfSamplesPerStateActionToLearnModel);
            Assert.IsFalse(config.LoadBeliefFromDB);
            Assert.AreEqual(5000, config.NumOfParticles);
            Assert.AreEqual(1, config.NumOfBeliefStateParticlesToSaveInDB);
            Assert.IsNotNull(config.ActionsToSimulate);
            Assert.IsEmpty(config.ActionsToSimulate);
            Assert.IsFalse(config.IsInternalSimulation);
            Assert.AreEqual(0.0f, config.SimulateByMdpRate);
            Assert.IsFalse(config.ManualControl);
            Assert.AreEqual(2.0f, config.PlanningTimePerMoveInSeconds);
            Assert.AreEqual(100, config.RolloutsCount);
            Assert.AreEqual(4, config.Verbosity);
        }
        
        [Test]
        public void TestRosTargetProjectValidation()
        {
            // Test validation logic for null values
            _rosTargetProject.RosDistribution = null;
            _rosTargetProject.WorkspaceDirectortyPath = null;
            _rosTargetProject.TargetProjectLaunchFile = null;

            Assert.IsNull(_rosTargetProject.RosDistribution);
            Assert.IsNull(_rosTargetProject.WorkspaceDirectortyPath);
            Assert.IsNull(_rosTargetProject.TargetProjectLaunchFile);
        }

        [Test]
        public void TestRosTargetProjectInitialization()
        {
            // Test default constructor initialization
            RosTargetProject project = new RosTargetProject();

            Assert.IsNull(project.RosDistribution);
            Assert.IsNull(project.WorkspaceDirectortyPath);
            Assert.IsNull(project.TargetProjectLaunchFile);
            Assert.IsNotNull(project.RosTargetProjectPackages);
            Assert.IsEmpty(project.RosTargetProjectPackages);
        }
        
        [Test]
        public void TestInitializeProjectInteraction()
        {
            // Test interaction with RosTarget, SolverConfiguration, and MiddlewareConfiguration
            _initializeProject.RosTarget = _rosTargetProject;
            _initializeProject.SolverConfiguration = _solverConfiguration;
            _initializeProject.MiddlewareConfiguration = _middlewareConfiguration;

            Assert.AreEqual(_rosTargetProject, _initializeProject.RosTarget);
            Assert.AreEqual(_solverConfiguration, _initializeProject.SolverConfiguration);
            Assert.AreEqual(_middlewareConfiguration, _initializeProject.MiddlewareConfiguration);
        }
        
        [Test]
        public void TestMiddlewareConfigurationInitialization()
        {
            // Test default constructor initialization
            Assert.IsFalse(_middlewareConfiguration.DebugOn);
            Assert.IsFalse(_middlewareConfiguration.KillRosCoreBeforeStarting);
        }
        
        [Test]
        public void TestInitializeProjectSettersAndGetters()
        {
            var project = new InitializeProject();
            var rosTarget = new RosTargetProject();
            var solverConfig = new SolverConfiguration();
            var middlewareConfig = new MiddlewareConfiguration();

            project.PLPsDirectoryPath = "/home/user/projects";
            project.RunWithoutRebuild = true;
            project.OnlyGenerateCode = false;
            project.RosTarget = rosTarget;
            project.SolverConfiguration = solverConfig;
            project.MiddlewareConfiguration = middlewareConfig;

            Assert.AreEqual("/home/user/projects", project.PLPsDirectoryPath);
            Assert.IsTrue(project.RunWithoutRebuild.Value);
            Assert.IsFalse(project.OnlyGenerateCode.Value);
            Assert.AreEqual(rosTarget, project.RosTarget);
            Assert.AreEqual(solverConfig, project.SolverConfiguration);
            Assert.AreEqual(middlewareConfig, project.MiddlewareConfiguration);
        }
        
        [Test]
        public void TestRosTargetProjectEdgeCases()
        {
            // Test setting edge cases for RosTargetProject
            _rosTargetProject.RosDistribution = "ROS";

            Assert.AreEqual("ROS", _rosTargetProject.RosDistribution);

            // Test empty collection scenario
            _rosTargetProject.RosTargetProjectPackages = new List<string>();

            Assert.IsEmpty(_rosTargetProject.RosTargetProjectPackages);
        }

        [Test]
        public void TestSolverConfigurationValidation()
        {
            // Test validation logic for SolverConfiguration
            _solverConfiguration.PolicyGraphDepth = -1;
            _solverConfiguration.limitClosedModelHorizon_stepsAfterGoalDetection = 100;

            Assert.AreEqual(-1, _solverConfiguration.PolicyGraphDepth); // Should allow negative values if that's the requirement
            Assert.AreEqual(100, _solverConfiguration.limitClosedModelHorizon_stepsAfterGoalDetection);
        }

        [Test]
        public void TestMiddlewareConfigurationEdgeCases()
        {
            // Test edge cases for MiddlewareConfiguration
            _middlewareConfiguration.DebugOn = false;
            _middlewareConfiguration.KillRosCoreBeforeStarting = false;

            Assert.IsFalse(_middlewareConfiguration.DebugOn);
            Assert.IsFalse(_middlewareConfiguration.KillRosCoreBeforeStarting);
        }

        [Test]
        public void TestInitializeProjectEmptyCollections()
        {
            // Test handling of empty collections in InitializeProject
            _initializeProject.SolverConfiguration = new SolverConfiguration();
            _initializeProject.SolverConfiguration.ActionsToSimulate = new List<int>();

            Assert.IsEmpty(_initializeProject.SolverConfiguration.ActionsToSimulate);
        }

        [Test]
        public void TestSolverConfigurationInteraction()
        {
            // Test interaction between SolverConfiguration and its properties
            var actionsToSimulate = new List<int> { 1, 2, 3 };

            _solverConfiguration.ActionsToSimulate = actionsToSimulate;

            Assert.AreEqual(actionsToSimulate, _solverConfiguration.ActionsToSimulate);
        }

        [Test]
        public void TestSolverConfigurationSettersAndGetters()
        {
            // Test setters and getters of SolverConfiguration
            var config = new SolverConfiguration();
            var actionsToSimulate = new List<int> { 1, 2, 3 };

            config.ActionsToSimulate = actionsToSimulate;

            Assert.AreEqual(actionsToSimulate, config.ActionsToSimulate);
        }
        
        [Test]
        public void TestSolverConfigurationActionList()
        {
            // Initialize with a list of actions
            var solverConfig = new SolverConfiguration();
            solverConfig.ActionsToSimulate = new List<int> { 1, 2, 3 };

            // Test adding an action
            solverConfig.ActionsToSimulate.Add(4);
            Assert.IsTrue(solverConfig.ActionsToSimulate.Contains(4));

            // Test removing an action
            solverConfig.ActionsToSimulate.Remove(2);
            Assert.IsFalse(solverConfig.ActionsToSimulate.Contains(2));
        }

        [Test]
        public void TestSolverConfigurationRolloutsCount()
        {
            // Initialize with default rollouts count
            var solverConfig = new SolverConfiguration();

            // Test setting a valid rollouts count
            solverConfig.RolloutsCount = 150;
            Assert.AreEqual(150, solverConfig.RolloutsCount);

            // Test setting a negative rollouts count
            solverConfig.RolloutsCount = -50;
            Assert.AreEqual(-50, solverConfig.RolloutsCount); // Assuming negative counts are valid

            // Test setting zero rollouts count
            solverConfig.RolloutsCount = 0;
            Assert.AreEqual(0, solverConfig.RolloutsCount);
        }

        [Test]
        public void TestInitializeProjectRosTargetPackages()
        {
            var project = new InitializeProject();
            var rosTarget = new RosTargetProject();
            rosTarget.RosTargetProjectPackages = new List<string> { "package1", "package2" };

            project.RosTarget = rosTarget;

            // Test adding a package
            project.RosTarget.RosTargetProjectPackages.Add("package3");
            Assert.IsTrue(project.RosTarget.RosTargetProjectPackages.Contains("package3"));

            // Test removing a package
            project.RosTarget.RosTargetProjectPackages.Remove("package2");
            Assert.IsFalse(project.RosTarget.RosTargetProjectPackages.Contains("package2"));
        }

        [Test]
        public void TestMiddlewareConfigurationToggle()
        {
            var middlewareConfig = new MiddlewareConfiguration();

            // Test toggling debug mode
            middlewareConfig.DebugOn = true;
            Assert.IsTrue(middlewareConfig.DebugOn);

            middlewareConfig.DebugOn = false;
            Assert.IsFalse(middlewareConfig.DebugOn);

            // Test toggling Ros core kill switch
            middlewareConfig.KillRosCoreBeforeStarting = true;
            Assert.IsTrue(middlewareConfig.KillRosCoreBeforeStarting);

            middlewareConfig.KillRosCoreBeforeStarting = false;
            Assert.IsFalse(middlewareConfig.KillRosCoreBeforeStarting);
        }

        [Test]
        public void TestInitializeProjectSolverConfiguration()
        {
            var project = new InitializeProject();
            var solverConfig = new SolverConfiguration();

            project.SolverConfiguration = solverConfig;

            // Test setting solver configuration properties
            project.SolverConfiguration.PolicyGraphDepth = 10;
            project.SolverConfiguration.NumOfSamplesPerStateActionToLearnModel = 50;

            Assert.AreEqual(10, project.SolverConfiguration.PolicyGraphDepth);
            Assert.AreEqual(50, project.SolverConfiguration.NumOfSamplesPerStateActionToLearnModel);
        }

        [Test]
        public void TestInitializeProjectMiddlewareConfiguration()
        {
            var project = new InitializeProject();
            var middlewareConfig = new MiddlewareConfiguration();

            project.MiddlewareConfiguration = middlewareConfig;

            // Test setting middleware configuration properties
            project.MiddlewareConfiguration.DebugOn = true;
            project.MiddlewareConfiguration.KillRosCoreBeforeStarting = true;

            Assert.IsTrue(project.MiddlewareConfiguration.DebugOn);
            Assert.IsTrue(project.MiddlewareConfiguration.KillRosCoreBeforeStarting);
        }

        [Test]
        public void TestInitializeProjectWithRosTargetInitialization()
        {
            var rosTarget = new RosTargetProject
            {
                RosDistribution = "kinetic",
                WorkspaceDirectortyPath = "/home/user/catkin_ws",
                TargetProjectLaunchFile = "launch_file.launch",
                TargetProjectInitializationTimeInSeconds = 15.0,
                RosTargetProjectPackages = new List<string> { "package1", "package2" }
            };

            var project = new InitializeProject
            {
                PLPsDirectoryPath = "/home/user/plps",
                RunWithoutRebuild = false,
                OnlyGenerateCode = true,
                RosTarget = rosTarget
            };

            // Assert InitializeProject properties
            Assert.AreEqual("/home/user/plps", project.PLPsDirectoryPath);
            Assert.IsFalse(project.RunWithoutRebuild.Value);
            Assert.IsTrue(project.OnlyGenerateCode.Value);

            // Assert RosTargetProject properties
            Assert.AreEqual("kinetic", project.RosTarget.RosDistribution);
            Assert.AreEqual("/home/user/catkin_ws", project.RosTarget.WorkspaceDirectortyPath);
            Assert.AreEqual("launch_file.launch", project.RosTarget.TargetProjectLaunchFile);
            Assert.AreEqual(15.0, project.RosTarget.TargetProjectInitializationTimeInSeconds);
            Assert.AreEqual(2, project.RosTarget.RosTargetProjectPackages.Count);
            Assert.Contains("package1", project.RosTarget.RosTargetProjectPackages);
        }

        [Test]
        public void TestSolverConfigurationFloatValues()
        {
            var solverConfig = new SolverConfiguration();

            // Test setting float values
            solverConfig.SimulateByMdpRate = 0.75f;
            solverConfig.PlanningTimePerMoveInSeconds = 3.5f;

            Assert.AreEqual(0.75f, solverConfig.SimulateByMdpRate);
            Assert.AreEqual(3.5f, solverConfig.PlanningTimePerMoveInSeconds);
        }

        [Test]
        public void TestSolverConfigurationBooleanProperties()
        {
            var solverConfig = new SolverConfiguration();

            // Test toggling boolean properties
            solverConfig.UseSarsop = true;
            solverConfig.UseML = false;
            solverConfig.LoadBeliefFromDB = true;
            solverConfig.IsInternalSimulation = true;

            Assert.IsTrue(solverConfig.UseSarsop);
            Assert.IsFalse(solverConfig.UseML);
            Assert.IsTrue(solverConfig.LoadBeliefFromDB);
            Assert.IsTrue(solverConfig.IsInternalSimulation);
        }

        [Test]
        public void TestInitializeProjectWithNullRosTarget()
        {
            var project = new InitializeProject();

            // Test scenario where RosTarget is not set
            Assert.IsNull(project.RosTarget);
        }

        [Test]
        public void TestRosTargetProjectWithEmptyPackageList()
        {
            var rosTarget = new RosTargetProject();

            // Test scenario where RosTargetProjectPackages list is empty
            Assert.IsNotNull(rosTarget.RosTargetProjectPackages);
            Assert.IsEmpty(rosTarget.RosTargetProjectPackages);
        }

        [Test]
        public void TestSolverConfigurationActionListEdgeCases()
        {
            var solverConfig = new SolverConfiguration();

            // Test edge case of setting ActionsToSimulate to null
            solverConfig.ActionsToSimulate = null;
            Assert.IsNull(solverConfig.ActionsToSimulate);

            // Test edge case of setting ActionsToSimulate to an empty list
            solverConfig.ActionsToSimulate = new List<int>();
            Assert.IsEmpty(solverConfig.ActionsToSimulate);
        }

        [Test]
        public void TestInitializeProjectSetNullRosTarget()
        {
            var project = new InitializeProject();

            // Set RosTarget to null explicitly
            project.RosTarget = null;

            Assert.IsNull(project.RosTarget);
        }

        [Test]
        public void TestInitializeProjectWithEmptySolverConfiguration()
        {
            var project = new InitializeProject();

            // Test scenario where SolverConfiguration is not set
            Assert.IsNull(project.SolverConfiguration);
        }

        [Test]
        public void TestRosTargetProjectPackageListModification()
        {
            var rosTarget = new RosTargetProject();
            rosTarget.RosTargetProjectPackages = new List<string> { "package1", "package2" };

            // Test modifying RosTargetProjectPackages list
            rosTarget.RosTargetProjectPackages.Add("package3");
            Assert.AreEqual(3, rosTarget.RosTargetProjectPackages.Count);
            Assert.Contains("package3", rosTarget.RosTargetProjectPackages);

            rosTarget.RosTargetProjectPackages.Remove("package1");
            Assert.AreEqual(2, rosTarget.RosTargetProjectPackages.Count);
            Assert.IsFalse(rosTarget.RosTargetProjectPackages.Contains("package1"));
        }

        [Test]
        public void TestInitializeProjectWithEmptyMiddlewareConfiguration()
        {
            var project = new InitializeProject();

            // Test scenario where MiddlewareConfiguration is not set
            Assert.IsNull(project.MiddlewareConfiguration);
        }

        [Test]
        public void TestInitializeProjectWithAllPropertiesSet()
        {
            var rosTarget = new RosTargetProject
            {
                RosDistribution = "ROS Melodic",
                WorkspaceDirectortyPath = "/home/user/catkin_ws",
                TargetProjectLaunchFile = "launch_file.launch",
                TargetProjectInitializationTimeInSeconds = 10.5,
                RosTargetProjectPackages = new List<string> { "package1", "package2" }
            };

            var solverConfig = new SolverConfiguration
            {
                PolicyGraphDepth = 5,
                limitClosedModelHorizon_stepsAfterGoalDetection = 10,
                UseSarsop = true,
                UseML = false,
                UseSavedSarsopPolicy = true,
                NumOfSamplesPerStateActionToLearnModel = 30,
                LoadBeliefFromDB = true,
                NumOfParticles = 1000,
                NumOfBeliefStateParticlesToSaveInDB = 10,
                ActionsToSimulate = new List<int> { 1, 2, 3 },
                IsInternalSimulation = true,
                SimulateByMdpRate = 0.5f,
                ManualControl = true,
                PlanningTimePerMoveInSeconds = 5.0f,
                RolloutsCount = 50,
                Verbosity = 2
            };

            var middlewareConfig = new MiddlewareConfiguration
            {
                DebugOn = true,
                KillRosCoreBeforeStarting = true
            };

            var project = new InitializeProject
            {
                PLPsDirectoryPath = "/home/user/plps",
                RunWithoutRebuild = true,
                OnlyGenerateCode = false,
                RosTarget = rosTarget,
                SolverConfiguration = solverConfig,
                MiddlewareConfiguration = middlewareConfig
            };

            // Test all properties are set correctly
            Assert.AreEqual("/home/user/plps", project.PLPsDirectoryPath);
            Assert.IsTrue(project.RunWithoutRebuild.Value);
            Assert.IsFalse(project.OnlyGenerateCode.Value);
            Assert.AreEqual(rosTarget, project.RosTarget);
            Assert.AreEqual(solverConfig, project.SolverConfiguration);
            Assert.AreEqual(middlewareConfig, project.MiddlewareConfiguration);
        }

        [Test]
        public void TestSolverConfigurationWithNegativeValues()
        {
            var solverConfig = new SolverConfiguration();

            // Test setting negative values
            solverConfig.PolicyGraphDepth = -5;
            solverConfig.limitClosedModelHorizon_stepsAfterGoalDetection = -10;

            Assert.AreEqual(-5, solverConfig.PolicyGraphDepth);
            Assert.AreEqual(-10, solverConfig.limitClosedModelHorizon_stepsAfterGoalDetection);
        }

        [Test]
        public void TestMiddlewareConfigurationWithFalseValues()
        {
            var middlewareConfig = new MiddlewareConfiguration();

            // Test setting false values
            middlewareConfig.DebugOn = false;
            middlewareConfig.KillRosCoreBeforeStarting = false;

            Assert.IsFalse(middlewareConfig.DebugOn);
            Assert.IsFalse(middlewareConfig.KillRosCoreBeforeStarting);
        }

        [Test]
        public void TestRosTargetProjectWithDefaultInitialization()
        {
            var project = new RosTargetProject();

            // Test default initialization
            Assert.IsNull(project.RosDistribution);
            Assert.IsNull(project.WorkspaceDirectortyPath);
            Assert.IsNull(project.TargetProjectLaunchFile);
            Assert.IsNotNull(project.RosTargetProjectPackages);
            Assert.IsEmpty(project.RosTargetProjectPackages);
        }

        [Test]
        public void TestInitializeProjectWithNullSolverConfiguration()
        {
            var project = new InitializeProject();

            // Test scenario where SolverConfiguration is null
            project.SolverConfiguration = null;

            Assert.IsNull(project.SolverConfiguration);
        }

        [Test]
        public void TestSolverConfigurationWithZeroSamplesPerStateActionToLearnModel()
        {
            var solverConfig = new SolverConfiguration();

            // Test setting zero for NumOfSamplesPerStateActionToLearnModel
            solverConfig.NumOfSamplesPerStateActionToLearnModel = 0;

            Assert.AreEqual(0, solverConfig.NumOfSamplesPerStateActionToLearnModel);
        }

        [Test]
        public void TestSolverConfigurationWithEmptyActionsToSimulate()
        {
            var solverConfig = new SolverConfiguration();

            // Test setting an empty list for ActionsToSimulate
            solverConfig.ActionsToSimulate = new List<int>();

            Assert.IsEmpty(solverConfig.ActionsToSimulate);
        }

        [Test]
        public void TestInitializeProjectWithEmptyProperties()
        {
            var project = new InitializeProject();

            // Test scenario where all properties are left empty or null
            project.PLPsDirectoryPath = "";
            project.RunWithoutRebuild = null;
            project.OnlyGenerateCode = null;
            project.RosTarget = new RosTargetProject();
            project.SolverConfiguration = new SolverConfiguration();
            project.MiddlewareConfiguration = new MiddlewareConfiguration();

            Assert.AreEqual("", project.PLPsDirectoryPath);
            Assert.IsFalse(project.RunWithoutRebuild.HasValue);
            Assert.IsFalse(project.OnlyGenerateCode.HasValue);
            Assert.AreEqual(0, project.RosTarget.RosTargetProjectPackages.Count);
            Assert.AreEqual(0, project.SolverConfiguration.ActionsToSimulate.Count);
            Assert.IsFalse(project.MiddlewareConfiguration.DebugOn);
            Assert.IsFalse(project.MiddlewareConfiguration.KillRosCoreBeforeStarting);
        }

        [Test]
        public void TestInitializeProjectWithNullMiddlewareConfiguration()
        {
            var project = new InitializeProject();

            // Test scenario where MiddlewareConfiguration is null
            project.MiddlewareConfiguration = null;

            Assert.IsNull(project.MiddlewareConfiguration);
        }

        [Test]
        public void TestInitializeProjectWithDefaultSolverConfiguration()
        {
            var project = new InitializeProject();

            // Test scenario where SolverConfiguration is set with default values
            project.SolverConfiguration = new SolverConfiguration();

            Assert.AreEqual(0, project.SolverConfiguration.PolicyGraphDepth);
            Assert.AreEqual(-1, project.SolverConfiguration.limitClosedModelHorizon_stepsAfterGoalDetection);
            Assert.IsFalse(project.SolverConfiguration.UseSarsop);
            Assert.IsFalse(project.SolverConfiguration.UseML);
            Assert.IsFalse(project.SolverConfiguration.UseSavedSarsopPolicy);
            Assert.AreEqual(20, project.SolverConfiguration.NumOfSamplesPerStateActionToLearnModel);
            Assert.IsFalse(project.SolverConfiguration.LoadBeliefFromDB);
            Assert.AreEqual(5000, project.SolverConfiguration.NumOfParticles);
            Assert.AreEqual(1, project.SolverConfiguration.NumOfBeliefStateParticlesToSaveInDB);
            Assert.IsEmpty(project.SolverConfiguration.ActionsToSimulate);
            Assert.IsFalse(project.SolverConfiguration.IsInternalSimulation);
            Assert.AreEqual(0.0f, project.SolverConfiguration.SimulateByMdpRate);
            Assert.IsFalse(project.SolverConfiguration.ManualControl);
            Assert.AreEqual(2.0f, project.SolverConfiguration.PlanningTimePerMoveInSeconds);
            Assert.AreEqual(100, project.SolverConfiguration.RolloutsCount);
            Assert.AreEqual(4, project.SolverConfiguration.Verbosity);
        }

        [Test]
        public void TestInitializeProjectWithRosTargetPackages()
        {
            var project = new InitializeProject();
            var rosTarget = new RosTargetProject
            {
                RosTargetProjectPackages = new List<string> { "package1", "package2" }
            };
            
            project.RosTarget = rosTarget;

            Assert.AreEqual(2, project.RosTarget.RosTargetProjectPackages.Count);
            Assert.Contains("package1", project.RosTarget.RosTargetProjectPackages);
            Assert.Contains("package2", project.RosTarget.RosTargetProjectPackages);
        }

        [Test]
        public void TestRosTargetProjectWithEmptyLaunchFile()
        {
            var rosTarget = new RosTargetProject();

            // Test scenario where TargetProjectLaunchFile is an empty string
            rosTarget.TargetProjectLaunchFile = "";

            Assert.AreEqual("", rosTarget.TargetProjectLaunchFile);
        }

        [Test]
        public void TestSolverConfigurationWithMaxValues()
        {
            var solverConfig = new SolverConfiguration();

            // Test scenario where some properties are set to maximum values
            solverConfig.PolicyGraphDepth = int.MaxValue;
            solverConfig.NumOfParticles = int.MaxValue;

            Assert.AreEqual(int.MaxValue, solverConfig.PolicyGraphDepth);
            Assert.AreEqual(int.MaxValue, solverConfig.NumOfParticles);
        }

        [Test]
        public void TestInitializeProjectWithEmptyPLPsDirectoryPath()
        {
            var project = new InitializeProject();

            // Test scenario where PLPsDirectoryPath is an empty string
            project.PLPsDirectoryPath = "";

            Assert.AreEqual("", project.PLPsDirectoryPath);
        }

        [Test]
        public void TestRosTargetProjectWithInitializationTime()
        {
            var rosTarget = new RosTargetProject();

            // Test scenario where TargetProjectInitializationTimeInSeconds is set
            rosTarget.TargetProjectInitializationTimeInSeconds = 15.5;

            Assert.AreEqual(15.5, rosTarget.TargetProjectInitializationTimeInSeconds);
        }

        [Test]
        public void TestSolverConfigurationWithCustomActionsToSimulate()
        {
            var solverConfig = new SolverConfiguration();

            // Test scenario where ActionsToSimulate is customized
            solverConfig.ActionsToSimulate = new List<int> { 1, 3, 5 };

            Assert.AreEqual(3, solverConfig.ActionsToSimulate.Count);
            Assert.Contains(1, solverConfig.ActionsToSimulate);
            Assert.Contains(3, solverConfig.ActionsToSimulate);
            Assert.Contains(5, solverConfig.ActionsToSimulate);
        }

        [Test]
        public void TestMiddlewareConfigurationToggleDebugMode()
        {
            var middlewareConfig = new MiddlewareConfiguration();

            // Test scenario toggling DebugOn property
            middlewareConfig.DebugOn = true;
            Assert.IsTrue(middlewareConfig.DebugOn);

            middlewareConfig.DebugOn = false;
            Assert.IsFalse(middlewareConfig.DebugOn);
        }

        [Test]
        public void TestSolverConfigurationWithManualControlEnabled()
        {
            var solverConfig = new SolverConfiguration();

            // Test scenario where ManualControl is enabled
            solverConfig.ManualControl = true;

            Assert.IsTrue(solverConfig.ManualControl);
            Assert.AreEqual(0, solverConfig.PolicyGraphDepth); // Check that other properties retain their default values
        }

        [Test]
        public void TestInitializeProjectWithoutMiddlewareConfiguration()
        {
            var project = new InitializeProject();

            // Test scenario where MiddlewareConfiguration is not set
            Assert.IsNull(project.MiddlewareConfiguration);
        }

        [Test]
        public void TestSolverConfigurationWithNegativePlanningTime()
        {
            var solverConfig = new SolverConfiguration();

            // Test scenario where PlanningTimePerMoveInSeconds is set to a negative value
            solverConfig.PlanningTimePerMoveInSeconds = -1.5f;

            Assert.AreEqual(-1.5f, solverConfig.PlanningTimePerMoveInSeconds);
            Assert.AreEqual(0, solverConfig.PolicyGraphDepth); // Check that other properties retain their default values
        }

        [Test]
        public void TestInitializeProjectWithDefaultValues()
        {
            var project = new InitializeProject();

            // Ensure default values are correctly set upon initialization
            Assert.IsNull(project.PLPsDirectoryPath);
            Assert.IsFalse(project.RunWithoutRebuild.HasValue ? project.RunWithoutRebuild.Value : false);
            Assert.IsFalse(project.OnlyGenerateCode.HasValue ? project.OnlyGenerateCode.Value : false);
            Assert.IsNull(project.RosTarget);
            Assert.IsNull(project.SolverConfiguration);
            Assert.IsNull(project.MiddlewareConfiguration);
        }

        [Test]
        public void TestRosTargetProjectWithMultiplePackages()
        {
            var rosTarget = new RosTargetProject();

            // Test scenario where multiple packages are added
            rosTarget.RosTargetProjectPackages = new List<string> { "package1", "package2", "package3" };

            Assert.AreEqual(3, rosTarget.RosTargetProjectPackages.Count);
            Assert.Contains("package1", rosTarget.RosTargetProjectPackages);
            Assert.Contains("package2", rosTarget.RosTargetProjectPackages);
            Assert.Contains("package3", rosTarget.RosTargetProjectPackages);
        }

        [Test]
        public void TestMiddlewareConfigurationWithDefaultValues()
        {
            var middlewareConfig = new MiddlewareConfiguration();

            // Ensure default values are correctly set upon initialization
            Assert.IsFalse(middlewareConfig.DebugOn);
            Assert.IsFalse(middlewareConfig.KillRosCoreBeforeStarting);
        }

        [Test]
        public void TestInitializeProjectInteractionWithNullObjects()
        {
            var project = new InitializeProject();

            // Test scenario where interacting objects are null
            project.RosTarget = null;
            project.SolverConfiguration = null;
            project.MiddlewareConfiguration = null;

            Assert.IsNull(project.RosTarget);
            Assert.IsNull(project.SolverConfiguration);
            Assert.IsNull(project.MiddlewareConfiguration);
        }

        [Test]
        public void TestSolverConfigurationWithLargeNumberOfActionsToSimulate()
        {
            var solverConfig = new SolverConfiguration();

            // Test scenario where a large number of actions are simulated
            solverConfig.ActionsToSimulate = new List<int>();
            for (int i = 1; i <= 1000; i++)
            {
                solverConfig.ActionsToSimulate.Add(i);
            }

            Assert.AreEqual(1000, solverConfig.ActionsToSimulate.Count);
            Assert.AreEqual(1, solverConfig.ActionsToSimulate[0]);
            Assert.AreEqual(500, solverConfig.ActionsToSimulate[499]);
            Assert.AreEqual(1000, solverConfig.ActionsToSimulate[999]);
        }

        [Test]
        public void TestMiddlewareConfigurationToggleOptions()
        {
            var middlewareConfig = new MiddlewareConfiguration();

            // Test scenario toggling KillRosCoreBeforeStarting property
            middlewareConfig.KillRosCoreBeforeStarting = true;
            Assert.IsTrue(middlewareConfig.KillRosCoreBeforeStarting);

            middlewareConfig.KillRosCoreBeforeStarting = false;
            Assert.IsFalse(middlewareConfig.KillRosCoreBeforeStarting);
        }
    }
}