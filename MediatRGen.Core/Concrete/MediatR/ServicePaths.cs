using MediatRGen.Core.Concrete;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using PluralizeService.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Processes.MediatR
{
    public class ServicePaths
    {

        private readonly IDirectoryServices _directoryService;
        private readonly ISettings _settings;
        private readonly IFileService _fileService;

        public ServicePaths(string moduleName, string entityName)
        {

            _directoryService = GlobalServices.Provider?.GetService<IDirectoryServices>();
            _settings = GlobalServices.Provider?.GetService<ISettings>();
            _fileService = GlobalServices.Provider?.GetService<IFileService>();

            Module = moduleName;
            EntityName = entityName;

        }

        private string Root { get { return _directoryService.GetCurrentDirectory().Value; } }

        private string Application { get { return "Application"; } }
        private string ApplicationFolderName { get { return $"{_settings.SolutionName}.{Module}.{Application}"; } }
        private string ApplicationFolderPath { get { return $"{Root}src\\{Module}\\{ApplicationFolderName}"; } }


        private string Domain { get { return "Domain"; } }
        private string DomainFolderName { get { return $"{_settings.SolutionName}.{Module}.{Domain}"; } }
        private string DomainFolderPath { get { return $"{Root}src\\{Module}\\{DomainFolderName}"; } }

        private string Api { get { return "API"; } }
        private string ApiFolderName { get { return $"{_settings.SolutionName}.{Module}.{Api}"; } }
        private string ApiFolderPath { get { return $"{Root}src\\{Module}\\{ApiFolderName}"; } }
        private string EntityLocalPath
        {
            get
            {
                string _entityPath = _fileService.FindFileRecursive(this.DomainFolderPath, EntityName + ".cs").Value;

                if (string.IsNullOrEmpty(_entityPath))
                    throw new FileNotFoundException(this.EntityName + " " + LangHandler.Definitions().FileNotFound);

                _entityPath = _entityPath.Replace(this.DomainFolderPath + "\\", "");

                _entityPath = _entityPath.Replace($"\\{this.EntityName}.cs", "");

                return _entityPath;
            }
        }

        public string Module { get; set; }
        private string _EntityName;
        public string EntityName
        {
            get { return _EntityName; }
            set
            {
                if (value.Contains(".cs"))
                {
                    value = value.Replace(".cs", "");
                }

                _EntityName = value;
            }
        }
        public string EntityNamePlural { get { return PluralizationProvider.Pluralize(EntityName); } }

        public string EntityFolder { get { return $"{DomainFolderPath}\\{EntityLocalPath}"; } }
        public string EntityNamespace { get { return $"{_settings.SolutionName}.{Module}.{Domain}.{EntityLocalPath.Replace("\\", ".")}"; } }
        public string ApplicationFeaturesWithEntityNameSpace { get { return $"{_settings.SolutionName}.{Module}.{Application}.Features.{EntityLocalPath.Replace("\\", ".")}.{EntityName}CQRS"; } }
        public string ApplicationDirectory
        {
            get
            {
                return $"{Root}src\\{Module}\\{_settings.SolutionName}.{Module}.Application\\Features\\{EntityLocalPath}\\{EntityNamePlural}CQRS";
            }
        }

        public string ControllerDirectory
        {
            get
            {
                return $"{Root}src\\{Module}\\{_settings.SolutionName}.{Module}.API\\Controllers\\{EntityLocalPath}";
            }
        }

    }
}
