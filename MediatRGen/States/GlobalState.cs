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
            ConfigFileName = "mediatr-config.json";
            ProjectName = "DenemeSolution";
        }

        private static GlobalState _instance;

        public static GlobalState Instance
        {
            get
            {

                if (_instance == null)
                    _instance = new GlobalState();

                return _instance;
            }
        }
        
        [JsonIgnore]
        public string ConfigFileName { get; set; }
        
        public string ProjectName { get; set; }
        
        [JsonIgnore]
        public string Lang { get; set; }
        
        [JsonIgnore]
        public string[] Commands { get; set; }
    }
}
