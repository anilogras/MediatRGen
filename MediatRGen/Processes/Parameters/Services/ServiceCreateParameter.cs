using CommandLine;

namespace MediatRGen.Cli.Processes.Parameters.Services
{
    public class ServiceCreateParameter
    {
        [Option('e', "entity", Required = false)]
        public string EntityName { get; set; }


        [Option('m', "module", Required = false)]
        public string ModuleName { get; set; }

    }
}
