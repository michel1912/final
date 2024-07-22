using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApiCSharp.JsonTextModel;

public class SdlLineProcessorVisitor : ISdlVisitor
{
    private readonly string[] _lines;
    private int _currentIndex;
    private readonly string _errorStart;
    private AmFile amFile = new AmFile();
    
    public SdlLineProcessorVisitor(string[] lines, string errorStart)
    {
        _lines = lines;
        _currentIndex = 0;
        _errorStart = errorStart;
    }

    public void Visit(GlobalVariableType globalVariableType) { throw new NotImplementedException(); }

    public void Visit(GlobalVariableDeclaration globalVariableDeclaration) { throw new NotImplementedException(); }

    public void Visit(SpecialStateCode specialStateCode) { throw new NotImplementedException(); }

    public void Visit(EnvironmentGeneral environmentGeneral) { throw new NotImplementedException(); }
    
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

    public void Visit(ModuleActivation moduleActivation)
    {
        amFile.ModuleActivation = moduleActivation;
    }

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

    public int CurrentIndex
    {
        get { return _currentIndex; }
    }

    public void Visit(PlpMain plpMain)
    {
        if (_currentIndex == 0 && _lines[_currentIndex].Trim().StartsWith("project:"))
        {
            string project = _lines[_currentIndex].Substring("project:".Length).Replace(" ", "");
            project = RemoveHiddenChar(project);
            plpMain.Project = project;
            _currentIndex++;
        }
        else
        {
            throw new Exception(_errorStart + " does not start with 'project: <project_name>'");
        }
    }

    public void Visit(GlobalVariableModuleParameter globalVariableModuleParameter)
    {
        if (_lines[_currentIndex].Trim().StartsWith("parameter:"))
        {
            string[] delimiters = { " ", ":" };
            List<string> bits = _lines[_currentIndex].Split(delimiters, StringSplitOptions.None).ToList();
            bits = bits.Select(x => x.Replace(" ", "")).Where(x => x.Length > 0 && x != "parameter").ToList();
            
            if (bits.Count != 2)
                throw new Exception(_errorStart + " a '<type> <name>' must be defined after 'parameter:'");
            
            globalVariableModuleParameter.Type = bits[0];
            globalVariableModuleParameter.Name = bits[1];
            _currentIndex++;
        }
    }

    public void Visit(CodeAssignment codeAssignment)
    {
        List<string> codeLines = new List<string>();
        _currentIndex++;
        
        while (_currentIndex < _lines.Length && !IsFirstLevelSavedWord(_lines[_currentIndex]))
            codeLines.Add(_lines[_currentIndex++]);
        
        codeAssignment.AssignmentCode = codeLines.ToArray();
    }

    public void Visit(Preconditions preconditions)
    {
        List<string> codeLines = new List<string>();
        _currentIndex++;
        
        while (_currentIndex < _lines.Length && !IsFirstLevelSavedWord(_lines[_currentIndex]))
            codeLines.Add(_lines[_currentIndex++]);
        
        preconditions.GlobalVariablePreconditionAssignments = new CodeAssignment[]
        {
            new CodeAssignment { AssignmentCode = codeLines.ToArray() }
        };
    }

    public void Visit(DynamicModel dynamicModel)
    {
        List<string> codeLines = new List<string>();
        _currentIndex++;
        
        while (_currentIndex < _lines.Length && !IsFirstLevelSavedWord(_lines[_currentIndex]))
            codeLines.Add(_lines[_currentIndex++]);
        
        dynamicModel.NextStateAssignments = new CodeAssignment[]
        {
            new CodeAssignment { AssignmentCode = codeLines.ToArray() }
        };
    }

    private string RemoveHiddenChar(string str)
    {
        return str.Replace("\t", "");
    }

    private bool IsFirstLevelSavedWord(string line)
    {
        return TranslateSdlToJson.FirstLevelSavedWords.Contains(line.Trim().Split(':')[0] + ":");
    }
}
