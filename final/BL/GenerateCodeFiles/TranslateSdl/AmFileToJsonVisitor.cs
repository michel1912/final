using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApiCSharp.JsonTextModel;

public class AmFileToJsonVisitor : ISdlVisitor
{
    private AmFile amFile = new AmFile();
    
    public void Visit(PlpMain plpMain) { amFile.PlpMain = plpMain; }

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

    public void Visit(GlobalVariableModuleParameter parameter) {}

    public void Visit(CodeAssignment codeAssignment) {}

    public void Visit(Preconditions preconditions) {}

    public void Visit(DynamicModel dynamicModel) {}

    public void Visit(GlobalVariableType globalVariableType) { throw new NotImplementedException(); }

    public void Visit(GlobalVariableDeclaration globalVariableDeclaration) { throw new NotImplementedException(); }

    public void Visit(SpecialStateCode specialStateCode) { throw new NotImplementedException(); }

    public void Visit(EnvironmentGeneral environmentGeneral) { throw new NotImplementedException(); }
    
    public AmFile GetAmFile() { return amFile; }
}
