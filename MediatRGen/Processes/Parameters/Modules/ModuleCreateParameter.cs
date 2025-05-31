using CommandLine;

namespace MediatRGen.Cli.Processes.Parameters.Modules
{
    public class ModuleCreateParameter
    {

        [Option('n', "name", Required = false)]
        public string ModuleName { get; set; }


    }
}
