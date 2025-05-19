using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Languages
{
    public interface ILang
    {
        public string InvalidCommandName { get; }
        public string InvalidParameter { get; }
        public string InvalidParamForCreateSolution { get; }
        public string EnterCommand { get; }
        public string ExistDirectory { get; }
        public string ProjectExist { get; }
        public string ConfigExist { get; }
        public string ConfigNotFoundVersionExist { get; }
        public string ConfigNotFound { get; }
        public string YouCanWriteCode { get; }
        public string CreatedConfigFile { get; }
        public string ModuleActive { get; }
        public string GatewayActive { get; }
        public string ModuleName { get; }
        public string ModuleNameIsRequired { get; }
        public string UseOchelot { get; }
        public string Required { get; }
        public string ModuleIsDefined { get; }
        public string CoreFilesCreated { get; }
        public string NugetPackagesCreated { get; }
        public string ClassLibraryCreated { get; }
        public string ClassNotFound { get; }
        public string WebApiCreated { get; }
        public string ConfigurationCreated { get; }
        public string ConfigurationUpdated { get; }
        public string FolderCreated { get; }
        public string EnterProjectName { get; }
        public string EnterModuleName { get; }
        public string EnterEntityName { get; }
        public string NugetPackageCreated { get; }
        public string ModuleCreated { get; }
        public string DirectoryCreated { get; }
        public string FileCreated { get; }
        public string EntityNotFound { get; }
        public string ServiceCreated { get; }
        public string NameSpaceNotFound { get; }

    }
}
