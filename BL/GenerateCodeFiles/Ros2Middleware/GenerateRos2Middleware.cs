using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using WebApiCSharp.Services;
using WebApiCSharp.Models;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace WebApiCSharp.GenerateCodeFiles
{
    public class GenerateRos2Middleware
    {
        public const string ROS2_MIDDLEWARE_PACKAGE_NAME = "aos_ros2_middleware_auto";

        private static Configuration conf;
        static GenerateRos2Middleware()
        {
            conf = ConfigurationService.Get();
        }



        public GenerateRos2Middleware(PLPsData data, InitializeProject initProj)
        {
            if(String.IsNullOrEmpty(initProj.RosTarget.WorkspaceDirectortyPath)) return;
            string rosWorkspaceSrcDirPath = GenerateFilesUtils.AppendPath(initProj.RosTarget.WorkspaceDirectortyPath, "src");
            string rosMiddlewareDirectory = GenerateFilesUtils.AppendPath(rosWorkspaceSrcDirPath, ROS2_MIDDLEWARE_PACKAGE_NAME);

            GenerateFilesUtils.DeleteAndCreateDirectory(rosMiddlewareDirectory, true);

            try
            {
            GenerateFilesUtils.RunApplicationUntilEnd(ROS2_MIDDLEWARE_PACKAGE_NAME, rosWorkspaceSrcDirPath, "ros2 pkg create" + "--build-type ament_python --dependencies std_msgs rclpy rclcpp");
            }
            catch(Exception e)
            {
                throw new Exception("ROS2 workspace path not found: '"+rosWorkspaceSrcDirPath+"'");
            }
            GenerateFilesUtils.WriteTextFile(rosMiddlewareDirectory + "/CMakeLists.txt", Ros2MiddlewareFileTemplate.GetCMakeListsFilefoxy());

            GenerateFilesUtils.WriteTextFile(rosMiddlewareDirectory + "/package.xml",  Ros2MiddlewareFileTemplate.GetPackageFilefoxy(initProj));

            Directory.CreateDirectory(rosMiddlewareDirectory + "/scripts");
            GenerateFilesUtils.WriteTextFile(rosMiddlewareDirectory + "/scripts/" + ROS2_MIDDLEWARE_PACKAGE_NAME + "_node.py", Ros2MiddlewareFileTemplate.GetAosRos2MiddlewareNodeFile(data, initProj), true);

        }


    }
}