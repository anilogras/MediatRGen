// See https://aka.ms/new-console-template for more information
using CommandLine;
using MediatRGen;
using MediatRGen.Exceptions;
using MediatRGen.Helpers;
using MediatRGen.Languages;
using MediatRGen.Processes.Base;

//args = ["create-config", "-n", "DenemeSolution" , "-d" , "\"d:/creator/ddddd\""];
//dotnet publish -c Release -o C:\MediatRGen\cli

bool type = false;

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