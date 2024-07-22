using WebApiCSharp.BL;

namespace WebApiCSharp.GenerateCodeFiles;

public class pythonContainer
{

    public static string GetSetUpFilefoxy()
    {
        return @"
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
            '";
    }

    public static string GetPackageFilefoxy()
    {
        return @"
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
    }

    public static string timer_function()
    {
        return @"
            self.create_timer(0.1, self.check_goal_reached) 
    def check_goal_reached(self):

";
    }

    public static string GetCMakeListsFilyfoxy()
    {
        return @")
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
    }

    public static string GetAOS_TopicListenerServerClass()
    {
        return @"
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
    }

    public static string GetAOS_TopicListenerServerClass1()
    {
        return @"
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
    }

    public static string getImports()
    {
        return @"
import datetime
import rclpy
import pymongo
import traceback
import operator
import time
import threading  # Import threading module

";
    }
    
    public static string Register_ModuleResponse()
    {
        return @"
            self.registerModuleResponse(self._topicListener.listenTargetModule, datetime.datetime.utcnow(), self.current_action_sequence_id, self.responseNotByLocalVariables)

";
    }

    public static string getFromMiddleWare()
    {
        return @"
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
";
    }

    public static string getFromNodeFile()
    {
        return @"
    
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
        self._topicListener.initLocalVars(moduleName)  # Ensure initialization
        self.saveHeavyLocalVariableToDB(moduleName)
        filter1 = {""ActionSequenceId"": actionSequenceID}
        
        if responseNotByLocalVariables is not None:
            moduleResponseItem = {""Module"": moduleName, ""ActionSequenceId"": actionSequenceID,
                                  ""ModuleResponseText"": responseNotByLocalVariables, ""StartTime"": startTime,
                                  ""EndTime"": datetime.datetime.utcnow(),
                                  ""ActionForExecutionId"": self.current_action_for_execution_id}
            aos_ModuleResponses_collection.replace_one(filter1, moduleResponseItem, upsert=True)
            return
        time.sleep(2)
        moduleResponse = """"
        assignGlobalVar = {}
";
    }

    public static string getRegister()
    {
        return @"
        registerLog(""this is the response of the navigation : ""+moduleResponse)
        moduleLocalVars = self._topicListener.localVarNamesAndValues.get(moduleName, {})
        moduleResponseItem = {""Module"": moduleName, ""ActionSequenceId"": actionSequenceID,
                              ""ModuleResponseText"": moduleResponse, ""StartTime"": startTime, ""EndTime"": datetime.datetime.utcnow(),
                              ""ActionForExecutionId"": self.current_action_for_execution_id,
                              ""LocalVariables"": moduleLocalVars}
        aos_ModuleResponses_collection.replace_one(filter1, moduleResponseItem, upsert=True)
        self._topicListener.localVarNamesAndValues[""navigate""][""goal_reached""] = False

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
";
    }

    public static string getTopicListener()
    {
        return @"
            self.current_action_sequence_id += 1
            self.currentActionFotExecutionId = None        
";
    }

    public static string getMain()
    {
        return @"
def main(args=None):
    rclpy.init(args=args)
    topic_listener = AOS_TopicListenerServer()
    command_listener = ListenToMongoDbCommands(topic_listener)

    topic_listener.get_logger().info(""Nodes initialized and command_listener is about to spin"")
    executor = MultiThreadedExecutor()
    executor.add_node(topic_listener)
    executor.add_node(command_listener)
    try:
        executor.spin()
    except KeyboardInterrupt:
        pass
    finally:
        executor.shutdown()
        command_listener.destroy_node()
        topic_listener.destroy_node()
        rclpy.shutdown()
";
    }

    public static string initialVars()
    {
        return @"
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
    }

    public static string setListenTarget()
    {
        return @"
    def setListenTarget(self, _listenTargetModule):
        self.initLocalVars(_listenTargetModule)
        if DEBUG:
            print('setListenTopicTargetModule:')
            print(_listenTargetModule)
        self.listenTargetModule = _listenTargetModule
";
    }

    public static string getListenTarget()
    {
        return @"
    def updateLocalVariableValue(self, varName, Consistency, value):
     with self.lock:
        if DEBUG and varName not in getHeavyLocalVarList(self.listenTargetModule):
            print(""update local var:"")
            print(varName)
            print(value)
        if self.listenTargetModule not in self.localVarNamesAndValues:
            return
        if Consistency != ""ROS"":
         if varName == ""goal_reached"":
            if self.localVarNamesAndValues[self.listenTargetModule][varName] != value :
                if DEBUG:
                    print(""ACTUAL UPDATE --------------------------------------------------------------------------"")
                self.localVarNamesAndValues[self.listenTargetModule][varName] = value
                registerLog(""this is the s_g_r: "" + str(self.localVarNamesAndValues[self.listenTargetModule][varName]) + "" and this is the varname "" + str(varName) + "" the consis "" + str(Consistency))
                if varName not in getHeavyLocalVarList(self.listenTargetModule):
                    if Consistency == ""DB"":
                        document = aos_local_var_collection1.find_one({""VarName"": ""goal_reached""})
                        if document:
                            index = document.get(""Index"") + 1
                        else:
                            index = 1

                        registerLog(""this is the index of goal_reached: "" + str(index))

                        aos_local_var_collection1.replace_one({""Module"": self.listenTargetModule, ""VarName"": varName},
                                                              {""Module"": self.listenTargetModule, ""VarName"": varName, ""Index"": index, ""Value"": value}, upsert=True)
                        aosStats_local_var_collection1.insert_one(
                            {""Module"": self.listenTargetModule, ""VarName"": varName, ""value"": value, ""Time"": datetime.datetime.utcnow()})
                    else:
                        aos_local_var_collection.replace_one({""Module"": self.listenTargetModule, ""VarName"": varName},
                                                             {""Module"": self.listenTargetModule, ""VarName"": varName, ""Value"": value}, upsert=True)
                        aosStats_local_var_collection.insert_one(
                            {""Module"": self.listenTargetModule, ""VarName"": varName, ""value"": value, ""Time"": datetime.datetime.utcnow()})
                    if DEBUG:
                        print(""WAS UPDATED --------------------------------------------------------------------------"")
         else:
            if self.localVarNamesAndValues[self.listenTargetModule][varName] != value:
                if DEBUG:
                    print(""ACTUAL UPDATE --------------------------------------------------------------------------"")
                self.localVarNamesAndValues[self.listenTargetModule][varName] = value
                registerLog(""this is the s_g_r: "" + str(self.localVarNamesAndValues[self.listenTargetModule][varName]) + "" and this is the varname "" + str(varName) + "" the consis "" + str(Consistency))
                if varName not in getHeavyLocalVarList(self.listenTargetModule):
                    if Consistency == ""DB"":
                        aos_local_var_collection1.replace_one({""Module"": self.listenTargetModule, ""VarName"": varName},
                                                              {""Module"": self.listenTargetModule, ""VarName"": varName, ""Value"": value}, upsert=True)
                        aosStats_local_var_collection1.insert_one(
                            {""Module"": self.listenTargetModule, ""VarName"": varName, ""value"": value, ""Time"": datetime.datetime.utcnow()})
                    else:
                        registerLog(""this is the index of skill_success:"")
                        aos_local_var_collection.replace_one({""Module"": self.listenTargetModule, ""VarName"": varName},
                                                             {""Module"": self.listenTargetModule, ""VarName"": varName, ""Value"": value}, upsert=True)
                        aosStats_local_var_collection.insert_one(
                            {""Module"": self.listenTargetModule, ""VarName"": varName, ""value"": value, ""Time"": datetime.datetime.utcnow()})
                    if DEBUG:
                        print(""WAS UPDATED --------------------------------------------------------------------------"")
        else:
         current_value = self.localVarNamesAndValues[self.listenTargetModule][varName]
         if current_value != value:
          self.localVarNamesAndValues[self.listenTargetModule][varName] = value
          registerLog(f'Updated {varName} to {value} for module {self.listenTargetModule} with consistency {Consistency}')
          registerLog(""this is goal reached check1111111111111111111111"")
          self.set_parameters([rclpy.parameter.Parameter(varName, rclpy.Parameter.Type.BOOL, value)])
          self.verify_param_update(varName, value)
          self.get_logger().info(f'This is the GOAL_REACHED SHARED value: {value}')
            
          
          
    def verify_param_update(self, param_name, expected_value):
     time.sleep(0.1)  # Short delay to allow parameter server to update
     param_value = self.get_parameter(param_name).get_parameter_value().bool_value
     if param_value == expected_value:
        self.get_logger().info(f'{param_name} successfully updated to {expected_value}')
     else:
        self.get_logger().error(f'{param_name} update failed: expected {expected_value}, got {param_value}')

     # Add additional logging to trace the flow and values
     self.get_logger().info(f'Current value of goal_reached in parameter server: {param_value}')
";
    }

    public static string updateLocalVars()
    {
        return @"
    def updateGlobalVarLowLevelValue(self, varName, value):
        isInit = value is not None
        aos_GlobalVariablesAssignments_collection.replace_one({""GlobalVariableName"": varName},{""GlobalVariableName"": varName, ""LowLevelValue"": value,
                                                                                               ""IsInitialized"": isInit, ""UpdatingActionSequenceId"": ""initialization"",
                                                                                               ""ModuleResponseId"": ""initialization""},upsert=True)
";
        
    }
    
}