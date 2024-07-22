using WebApiCSharp.Models;

namespace WebApiCSharp.GenerateCodeFiles
{
    public class Ros2MiddlewareGenerator : MiddlewareGenerator
    {
        public override void Generate(PLPsData data, InitializeProject initProj)
        {
            new GenerateRos2Middleware(data, initProj);
        }
    }
}