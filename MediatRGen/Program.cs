// See https://aka.ms/new-console-template for more information
using CommandLine;
using MediatRGen;
using MediatRGen.Exceptions;
using MediatRGen.Languages;
using MediatRGen.Processes;

//args = ["create-solution", "-n", "DenemeSolution" , "-d" , "\"d:/deneme/ddddd\""];

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