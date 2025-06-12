using MediatRGen.Cli.Processes.Config;
using MediatRGen.Cli.Processes.MediatR;
using MediatRGen.Cli.Processes.Module;
using MediatRGen.Cli.Processes.Nuget;
using MediatRGen.Cli.Processes.Solution;
using MediatRGen.Core.Exceptions;
using MediatRGen.Core.Languages;
using Spectre.Console.Cli;

//args = ["create-config", "-n", "DenemeSolution" , "-d" , "\"d:/creator/ddddd\""];
//dotnet publish -c Release -o C:\MediatRGen\cli

bool type = true;


var app = new CommandApp();

app.Configure(config =>
{
    config.AddCommand<CreateSolutionCommand>("create-solution");
    config.AddCommand<CreateConfigCommand>("create-config");
    config.AddCommand<UpdateConfigCommand>("update-config");
    config.AddCommand<CreateNugetCommand>("create-nuget");
    config.AddCommand<UpdateNugetCommand>("update-nuget");
    config.AddCommand<CreateModuleCommand>("create-module");
    config.AddCommand<CreateMediatRCommand>("create-service");
});


if (type)
{
    while (true)
    {
        string? input = Console.ReadLine();
        try
        {
            await app.RunAsync(args);
            args = null;
        }
        catch (Exception ex)
        {
            Console.WriteLine("**************************");
            BaseException.ExceptionHandler(ex);
            Console.WriteLine("**************************");
        }
    }
}
else
{

    if (args.Length > 0)
    {
        string? input = string.Join(" ", args);
        try
        {
            await app.RunAsync(args);
            args = null;
        }
        catch (Exception ex)
        {
            Console.WriteLine("**************************");
            BaseException.ExceptionHandler(ex);
            Console.WriteLine("**************************");
        }
    }
    else
    {
        Console.WriteLine(LangHandler.Definitions().EnterCommand);
    }
}