using System.Collections.Generic;
using System.Linq;
using WebApiCSharp.JsonTextModel;

public interface ISdlVisitor
{
    void Visit(PlpMain plpMain);
    void Visit(GlobalVariableModuleParameter parameter);
    void Visit(CodeAssignment codeAssignment);
    void Visit(Preconditions preconditions);
    void Visit(DynamicModel dynamicModel);
    void Visit(ModuleResponse moduleResponse);
    void Visit(ResponseRule responseRule);
    void Visit(ModuleActivation moduleActivation);
    void Visit(RosService rosService);
    void Visit(RosAction rosAction);
    void Visit(LocalVariableInitialization localVariableInitialization);
    void Visit(EnvironmentGeneral environmentGeneral);
    void Visit(GlobalVariableType globalVariableType);
    void Visit(GlobalVariableDeclaration globalVariableDeclaration);
    void Visit(SpecialStateCode specialStateCode);
}

public class SdFileToJsonVisitor : ISdlVisitor
{
    private SdFile sdFile = new SdFile();
    private AmFile amFile = new AmFile();

    public void Visit(EnvironmentGeneral environmentGeneral) {}

    public void Visit(GlobalVariableType globalVariableType) {}

    public void Visit(GlobalVariableDeclaration globalVariableDeclaration) {}

    public void Visit(SpecialStateCode specialStateCode) {}

    public void Visit(ModuleResponse moduleResponse) { amFile.ModuleResponse = moduleResponse; }

    public void Visit(ResponseRule responseRule)
    {
        if (amFile.ModuleResponse == null)
        {
            amFile.ModuleResponse = new ModuleResponse();
        }

        var responseRules = amFile.ModuleResponse.ResponseRules?.ToList() ?? new List<ResponseRule>();
        responseRules.Add(responseRule);
        amFile.ModuleResponse.ResponseRules = responseRules.ToArray();
    }

    public void Visit(ModuleActivation moduleActivation) { amFile.ModuleActivation = moduleActivation; }

    public void Visit(RosService rosService)
    {
        if (amFile.ModuleActivation == null)
        {
            amFile.ModuleActivation = new ModuleActivation();
        }

        amFile.ModuleActivation.RosService = rosService;
    }

    public void Visit(RosAction rosAction)
    {
        if (amFile.ModuleActivation == null)
        {
            amFile.ModuleActivation = new ModuleActivation();
        }

        amFile.ModuleActivation.RosAction = rosAction;
    }

    public void Visit(LocalVariableInitialization localVariableInitialization)
    {
        if (amFile.LocalVariablesInitialization == null)
        {
            amFile.LocalVariablesInitialization = new List<LocalVariableInitialization>();
        }

        amFile.LocalVariablesInitialization.Add(localVariableInitialization);
    }

    public void Visit(PlpMain plpMain)
    {
        sdFile.PlpMain = plpMain;
    }

    public void Visit(GlobalVariableModuleParameter parameter)
    {
        if (sdFile.GlobalVariableModuleParameters == null)
        {
            sdFile.GlobalVariableModuleParameters = new List<GlobalVariableModuleParameter>().ToArray();
        }

        var parameters = sdFile.GlobalVariableModuleParameters.ToList();
        parameters.Add(parameter);
        sdFile.GlobalVariableModuleParameters = parameters.ToArray();
    }

    public void Visit(CodeAssignment codeAssignment)
    {
        if (sdFile.PossibleParametersValue == null)
        {
            sdFile.PossibleParametersValue = new CodeAssignment[] { codeAssignment };
        }
        else
        {
            var codeAssignments = sdFile.PossibleParametersValue.ToList();
            codeAssignments.Add(codeAssignment);
            sdFile.PossibleParametersValue = codeAssignments.ToArray();
        }
    }

    public void Visit(Preconditions preconditions) { sdFile.Preconditions = preconditions; }

    public void Visit(DynamicModel dynamicModel) { sdFile.DynamicModel = dynamicModel; }

    public SdFile GetSdFile() { return sdFile; }
}
