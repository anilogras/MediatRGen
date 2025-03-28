// See https://aka.ms/new-console-template for more information
using CommandLine;
using MediatRGen;
using MediatRGen.Exceptions;
using MediatRGen.Helpers;
using MediatRGen.Languages;
using MediatRGen.Processes;

//args = ["create-config", "-n", "DenemeSolution" , "-d" , "\"d:/creator/ddddd\""];
//dotnet publish -c Release -o C:\Tools\MyCLI

bool type = true;

if (type)
{
    while (true)
    {
        //string? input = string.Join(" ", args);
        string? input = Console.ReadLine();
        try
        {
            CommandProcessor.ProcessHandler(input);
            Console.WriteLine("\n\n");

            args = null;
        }
        catch (Exception ex)
        {
            Console.WriteLine("\n\n");
            BaseException.ExceptionHandler(ex);
            Console.WriteLine("\n\n");
        }

    }
}
else
{

    if (args.Length > 0)
    {
        string? input = string.Join(" ", args);
        //string? input = Console.ReadLine();
        try
        {
            CommandProcessor.ProcessHandler(input);

            args = null;
        }
        catch (Exception ex)
        {
            BaseException.ExceptionHandler(ex);
        }
    }
    else
    {
        Console.WriteLine(LangHandler.Definitions().EnterCommand);
    }
}