using CommandLine;
using Spectre.Console.Cli;

namespace MediatRGen.Cli.Models
{
    public class CreateSolutionSchema : CommandSettings
    {

        [CommandArgument(0, "<Name>")]
        public string ProjectName { get; set; }


        private string _Directory;

        [CommandArgument(1, "<Directory>")]
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

    }
}
