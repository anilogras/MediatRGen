// See https://aka.ms/new-console-template for more information
using CommandLine;
using MediatRGen;
using MediatRGen.Exceptions;
using MediatRGen.Languages;
using MediatRGen.Processes;

int selectedIndex = 0;
//string[] options = { "Proje Oluştur", "Servis Oluştur", "Repository Oluştur", "Çıkış" };
//COmmandValidator.Equals("deneme");

args = ["create-solutiona", "-d22", "D:\\Deneme\\den", "-n", "mysol"];

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
        BaseException.ExceptionHandler(ex);
    }
}
else
{
    Console.WriteLine(LangHandler.Definitions().EnterCommand);
}

//ParserResult<ProjectOptions> _parsedOptions = Parser.Default.ParseArguments<ProjectOptions>(commandArgs);

//ProjectOptions opt = _parsedOptions.Value;

//Console.Clear();
//Console.WriteLine("Lütfen bir seçenek seçin (⬆️ / ⬇️ ile, Enter ile onaylayın):\n");



//for (int i = 0; i < options.Length; i++)
//{
//    if (i == selectedIndex)
//    {
//        Console.ForegroundColor = ConsoleColor.Green;
//        Console.WriteLine($"> {options[i]}"); // Seçili öğe
//        Console.ResetColor();
//    }
//    else
//    {
//        Console.WriteLine($"  {options[i]}");
//    }
//}

//ConsoleKeyInfo keyInfo = Console.ReadKey(true);

//switch (keyInfo.Key)
//{
//    case ConsoleKey.UpArrow:
//        selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
//        break;

//    case ConsoleKey.DownArrow:
//        selectedIndex = (selectedIndex == options.Length - 1) ? 0 : selectedIndex + 1;
//        break;

//    case ConsoleKey.Enter:
//        HandleSelection(options[selectedIndex]);
//        break;
//}

//ProcessCommand(inputArgs);




static void HandleSelection(string selection)
{
    Console.Clear();
    Console.WriteLine($"Seçilen işlem: {selection}");

    if (selection == "Çıkış")
    {
        Console.WriteLine("Çıkış yapılıyor...");
        Environment.Exit(0);
    }
    else
    {
        Console.WriteLine("Devam etmek için bir tuşa basın...");
        Console.ReadKey();
    }
}

static void ProcessCommand(string[] inputArgs)
{
    CreateProject();
}

static void CreateProject()
{
    Console.Write("Proje adını girin: ");
    string? projectName = Console.ReadLine()?.Trim();

    Console.Write("Dizin (boş bırakılırsa mevcut dizin): ");
    string? path = Console.ReadLine()?.Trim();
    if (string.IsNullOrWhiteSpace(path)) path = Directory.GetCurrentDirectory();

    string projectPath = Path.Combine(path, projectName ?? "YeniProje");
    Directory.CreateDirectory(projectPath);

    Console.WriteLine($"Proje '{projectPath}' oluşturuldu!");
}


public class ProjectOptions
{
    [Option('d', "dir", Required = false, HelpText = "Dizin yolu.")]
    public string Directory { get; set; }

    [Option('r', "rep", Required = false, HelpText = "Repository var mı?")]
    public bool HasRepository { get; set; }

    [Option('i', "img", Required = false, HelpText = "Resim var mı?")]
    public bool HasImage { get; set; }
}

