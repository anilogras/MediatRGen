// See https://aka.ms/new-console-template for more information
using CommandLine;
using MediatRGen;
using MediatRGen.Cli.Processes.Base;
using MediatRGen.Cli.Helpers;
using MediatRGen.Core.Exceptions;
using MediatRGen.Core.Languages;
using MediatRGen.Cli;

//args = ["create-config", "-n", "DenemeSolution" , "-d" , "\"d:/creator/ddddd\""];
//dotnet publish -c Release -o C:\MediatRGen\cli

bool type = true;

if (type)
{
    while (true)
    {
        string? input = Console.ReadLine();
        try
        {
            CommandProcessor.ProcessHandler(input);
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
            CommandProcessor.ProcessHandler(input);
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