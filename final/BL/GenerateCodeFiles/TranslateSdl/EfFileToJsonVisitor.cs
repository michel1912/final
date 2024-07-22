using System.Collections.Generic;
using System.Linq;

namespace WebApiCSharp.JsonTextModel;

public class EfFileToJsonVisitor : ISdlVisitor
{
    private EfFile efFile = new EfFile();

    public void Visit(EnvironmentGeneral environmentGeneral)
    {
        efFile.EnvironmentGeneral = environmentGeneral;
    }

    public void Visit(GlobalVariableType globalVariableType)
    {
        var types = efFile.GlobalVariableTypes?.ToList() ?? new List<GlobalVariableType>();
        types.Add(globalVariableType);
        efFile.GlobalVariableTypes = types.ToArray();
    }

    public void Visit(GlobalVariableDeclaration globalVariableDeclaration)
    {
        var declarations = efFile.GlobalVariablesDeclaration?.ToList() ?? new List<GlobalVariableDeclaration>();
        declarations.Add(globalVariableDeclaration);
        efFile.GlobalVariablesDeclaration = declarations.ToArray();
    }

    public void Visit(SpecialStateCode specialStateCode)
    {
        var codes = efFile.SpecialStates?.ToList() ?? new List<SpecialStateCode>();
        codes.Add(specialStateCode);
        efFile.SpecialStates = codes.ToArray();
    }

    public void Visit(PlpMain plpMain) { efFile.PlpMain = plpMain; }

    public void Visit(GlobalVariableModuleParameter parameter) {}

    public void Visit(CodeAssignment codeAssignment) {}

    public void Visit(Preconditions preconditions) {}

    public void Visit(DynamicModel dynamicModel) {}

    public void Visit(ModuleResponse moduleResponse) {}

    public void Visit(ResponseRule responseRule) {}

    public void Visit(ModuleActivation moduleActivation) {}

    public void Visit(RosService rosService) {}

    public void Visit(RosAction rosAction) {}

    public void Visit(LocalVariableInitialization localVariableInitialization) {}

    public EfFile GetEfFile() { return efFile; }
}
