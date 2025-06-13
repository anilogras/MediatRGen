using MediatRGen.Core.Base;
using MediatRGen.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MediatRGen.Core.Services
{
    public interface ISettings
    {
        public Settings Get();
        public Settings Update();

        public string ConfigFileName => "mediatr-config.json";

        public string ProjectName { get; set; }
        public string Lang { get; set; }
        public string SolutionName { get; set; }
        public bool? Modul { get; set; }
        public string Version { get; set; }
        public bool UseModule { get; set; }
        public bool UseGateway { get; set; }
        public List<ProjectModule> Modules { get; set; }
    }
}
