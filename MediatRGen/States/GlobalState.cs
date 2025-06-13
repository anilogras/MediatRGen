using MediatRGen.Core.Base;
using MediatRGen.Core.Concrete;
using MediatRGen.Core.Services;
using System.Text.Json.Serialization;

namespace MediatRGen.Cli.States
{
    public class GlobalState
    {

        private readonly IDirectoryServices _directoryServices;


        public static string ConfigFileName = "mediatr-config.json";

        private static GlobalState _instance;

        public  GlobalState Instance
        {
            get
            {

                if (_instance == null)
                {
                    

                }
                return _instance;
            }
        }

        public static void UpdateInstance()
        {
            
        }

        public string ProjectName { get; set; }

        public string Lang { get; set; }

        [JsonIgnore]
        public string[] Commands { get; set; }
        public string SolutionName { get; set; }
        public bool? Modul { get; set; }
        public string Version { get; set; }
        public bool UseModule { get; set; }
        public bool UseGateway { get; set; }
        public List<ProjectModule> Modules { get; set; }
    }
}
