using MediatRGen.Cli.Processes.Base;

namespace MediatRGen.Cli.Processes.Nuget
{
    public class CreateNugetPackages : BaseProcess
    {
        public CreateNugetPackages()
        {
            new UpdateNugetPackageProcess();
        }
    }
}
