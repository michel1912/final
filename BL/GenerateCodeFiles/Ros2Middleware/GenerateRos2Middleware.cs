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
        public const string console_main ="aos_ros2_middleware_auto_node = aos_ros2_middleware_auto.aos_ros2_middleware_auto_node:main";


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

          GenerateFilesUtils.DeleteDirectory2(rosMiddlewareDirectory, true);

            try
            {
                //we have to change this command to be :
                //ros2 pkg create aos_ros2_middleware_auto --build-type ament_python --dependencies rclpy
                // rosWorkspaceSrcDirPath
                Console.WriteLine(rosWorkspaceSrcDirPath + " aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            GenerateFilesUtils.RunApplicationUntilEnd("ros2" , rosWorkspaceSrcDirPath, $"pkg create {"aos_ros2_middleware_auto"} --build-type ament_python --dependencies rclpy" );
            }
            catch(Exception e)
            {
                Console.WriteLine("besharafak" + " aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
                Console.WriteLine(e);
                throw new Exception("ROS2 workspace path not found: '"+rosWorkspaceSrcDirPath+"'");
            }
            Console.WriteLine(rosMiddlewareDirectory + "bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb");
            GenerateFilesUtils.WriteTextFile(rosMiddlewareDirectory + "/setup.py", Ros2MiddlewareFileTemplate.GetSetupFilefoxy(console_main));

            GenerateFilesUtils.WriteTextFile(rosMiddlewareDirectory + "/package.xml",  Ros2MiddlewareFileTemplate.GetPackageFilefoxy(initProj));

            // Directory.CreateDirectory(rosMiddlewareDirectory + "/mic3");
            GenerateFilesUtils.WriteTextFile(rosMiddlewareDirectory + "/aos_ros2_middleware_auto/" + ROS2_MIDDLEWARE_PACKAGE_NAME + "_node.py", Ros2MiddlewareFileTemplate.GetAosRos2MiddlewareNodeFile(data, initProj), true);

        }


    }
}