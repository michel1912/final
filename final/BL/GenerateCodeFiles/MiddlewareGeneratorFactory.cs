using System;

namespace WebApiCSharp.GenerateCodeFiles
{
    public class MiddlewareGeneratorFactory
    {
        public static MiddlewareGenerator Create(string middlewareType)
        {
            switch (middlewareType)
            {
                case "ROS2":
                    return new Ros2MiddlewareGenerator();
                case "ROS":
                    return new RosMiddlewareGenerator();
                default:
                    throw new ArgumentException("Invalid middleware type");
            }
        }
    }
}