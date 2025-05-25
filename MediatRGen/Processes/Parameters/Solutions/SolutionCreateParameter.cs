using CommandLine;

namespace MediatRGen.Cli.Processes.Parameters.Solutions
{
    public class SolutionCreateParameter
    {
        [Option('n', "name", Required = false)]
        public string ProjectName { get; set; }


        private string _Directory;

        [Option('d', "dir", Required = false)]
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
