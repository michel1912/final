using WebApiCSharp.Models;

namespace WebApiCSharp.GenerateCodeFiles
{
    public class RosMiddlewareGenerator : MiddlewareGenerator
    {
        public override void Generate(PLPsData data, InitializeProject initProj)
        {
            new GenerateRosMiddleware(data, initProj);
        }
    }
}