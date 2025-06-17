using CommandLine;
using MediatRGen.Core.Schemas;
using Spectre.Console.Cli;

namespace MediatRGen.Cli.Models
{
    public class CreateSolutionSchema : CommandSettings
    {

        [CommandOption("-n|--name")]
        public string ProjectName { get; set; }


        private string _Directory;

        [CommandOption("-d|--dir")]
        public string Directory
        {
            get { return _Directory; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    value = "./";
                }

                _Directory = value;
            }
        }

        public CreateSolutionBaseSchema OptionsSet() => new(ProjectName, Directory);


    }
}
