using MediatRGen.Cli.Models;
using MediatRGen.Cli.Processe2s.Parameters.Modules;
using MediatRGen.Cli.States;
using MediatRGen.Core.Concrete;
using MediatRGen.Core.Exceptions;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Models;
using MediatRGen.Core.Services;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes.Module
{
    public class CreateModuleCommand : Command<CreateModuleParameter>
    {
        public override int Execute(CommandContext context, CreateModuleParameter settings)
        {
            ParameterService.GetParameter<ModuleCreateParameter>(command, ref settings);
            ParameterService.GetParameterFromConsole(settings, "ModuleName", LangHandler.Definitions().EnterModuleName);


            CheckModulNameIsExist();
            DirectoryServices.CreateIsNotExist(DirectoryServices.GetCurrentDirectory().Value + "src\\" + settings.ModuleName);

            ClassLibraryService.Create(settings.ModuleName + "." + "Domain", DirectoryServices.GetPath(settings.ModuleName).Value, GlobalState.Instance.ProjectName, GlobalState.Instance.SolutionName);
            ClassLibraryService.Create(settings.ModuleName + "." + "Application", DirectoryServices.GetPath(settings.ModuleName).Value, GlobalState.Instance.ProjectName, GlobalState.Instance.SolutionName);
            ClassLibraryService.Create(settings.ModuleName + "." + "Infrastructure", DirectoryServices.GetPath(settings.ModuleName).Value, GlobalState.Instance.ProjectName, GlobalState.Instance.SolutionName);
            WebApiService.Create(settings.ModuleName + "." + "API", DirectoryServices.GetPath(settings.ModuleName).Value, GlobalState.Instance.ProjectName, GlobalState.Instance.SolutionName);

            SystemProcessService.BuildProject(GlobalState.Instance.ProjectName);

            GlobalState.Instance.Modules.Add(new ProjectModule()
            {
                Name = settings.ModuleName
            });

            FileService.UpdateConfig(GlobalState.ConfigFileName, GlobalState.Instance);

            Console.WriteLine(settings.ModuleName + LangHandler.Definitions().ModuleCreated);
        }

        private void CheckModulNameIsExist()
        {
            if (GlobalState.Instance.Modules.Where(x => x.Name == settings.ModuleName).Count() != 0)
            {
                throw new ModuleException(LangHandler.Definitions().ModuleIsDefined);
            }
        }
    }
}
