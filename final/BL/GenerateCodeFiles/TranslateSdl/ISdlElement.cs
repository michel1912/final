namespace WebApiCSharp.JsonTextModel;

public interface ISdlElement
{
    void Accept(ISdlVisitor visitor);
}

