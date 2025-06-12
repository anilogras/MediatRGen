using MediatRGen.Cli.Processes.Base;
using MediatRGen.Cli.Processes.Parameters.Modules;
using MediatRGen.Cli.States;
using MediatRGen.Core.Exceptions;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Models;
using MediatRGen.Core.Services;

namespace MediatRGen.Cli.Processes.Module
{
    public class CreateModuleProcess : BaseProcess
    {
        private ModuleCreateParameter _parameter;



        public CreateModuleProcess(string command)
        {
            ParameterService.GetParameter<ModuleCreateParameter>(command, ref _parameter);
            ParameterService.GetParameterFromConsole(_parameter, "ModuleName", LangHandler.Definitions().EnterModuleName);
            Execute();
        }

        private void Execute()
        {

            CheckModulNameIsExist();
            DirectoryServices.CreateIsNotExist(DirectoryServices.GetCurrentDirectory().Value + "src\\" + _parameter.ModuleName);

            ClassLibraryService.Create(_parameter.ModuleName + "." + "Domain", DirectoryServices.GetPath(_parameter.ModuleName).Value, GlobalState.Instance.ProjectName, GlobalState.Instance.SolutionName);
            ClassLibraryService.Create(_parameter.ModuleName + "." + "Application", DirectoryServices.GetPath(_parameter.ModuleName).Value, GlobalState.Instance.ProjectName, GlobalState.Instance.SolutionName);
            ClassLibraryService.Create(_parameter.ModuleName + "." + "Infrastructure", DirectoryServices.GetPath(_parameter.ModuleName).Value, GlobalState.Instance.ProjectName, GlobalState.Instance.SolutionName);
            WebApiService.Create(_parameter.ModuleName + "." + "API", DirectoryServices.GetPath(_parameter.ModuleName).Value, GlobalState.Instance.ProjectName, GlobalState.Instance.SolutionName);

            SystemProcessService.BuildProject(GlobalState.Instance.ProjectName);

            GlobalState.Instance.Modules.Add(new ProjectModule()
            {
                Name = _parameter.ModuleName
            });

            FileService.UpdateConfig(GlobalState.ConfigFileName, GlobalState.Instance);

            Console.WriteLine(_parameter.ModuleName + LangHandler.Definitions().ModuleCreated);
        }

        private void CheckModulNameIsExist()
        {
            if (GlobalState.Instance.Modules.Where(x => x.Name == _parameter.ModuleName).Count() != 0)
            {
                throw new ModuleException(LangHandler.Definitions().ModuleIsDefined);
            }
        }
    }
}
