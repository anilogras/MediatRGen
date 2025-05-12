using MediatRGen.Exceptions;
using MediatRGen.Helpers;
using MediatRGen.Languages;
using MediatRGen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MediatRGen.States
{
    public class GlobalState
    {

        //todo : eğer create-solution komutu çalışırsa global stateyi resetle / yoksa eski projede kalıyor
        public GlobalState()
        {
            Lang = "tr";
            Commands = ["create-solution", "create-repository", "create-config", "create-module"];
            ProjectName = "DenemeSolution";
            Modules = new List<ProjectModule>();

            Console.WriteLine("GLOBALLLLL");



        }
        public static string ConfigFileName = "mediatr-config.json";

        private static GlobalState _instance;

        public static GlobalState Instance
        {
            get
            {

                if (_instance == null)
                {
                    string _configPath = PathHelper.GetPath(DirectoryHelpers.GetCurrentDirectory(), ConfigFileName);
                    bool _fileExist = FileHelpers.CheckFile("./", ConfigFileName);

                    if (_fileExist == false)
                        _instance = new GlobalState();
                    else
                    {
                        string _file = FileHelpers.Get(_configPath);
                        _instance = System.Text.Json.JsonSerializer.Deserialize<GlobalState>(_file);
                    }

                }
                return _instance;
            }
        }

        public string ProjectName { get; set; }

        [JsonIgnore]
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
