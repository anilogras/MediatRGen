// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

int selectedIndex = 0;
string[] options = { "Proje Oluştur", "Servis Oluştur", "Repository Oluştur", "Çıkış" };


while (true)
{

    Console.Clear();
    Console.WriteLine("Lütfen bir seçenek seçin (⬆️ / ⬇️ ile, Enter ile onaylayın):\n");

    for (int i = 0; i < options.Length; i++)
    {
        if (i == selectedIndex)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"> {options[i]}"); // Seçili öğe
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine($"  {options[i]}");
        }
    }

    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

    switch (keyInfo.Key)
    {
        case ConsoleKey.UpArrow:
            selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
            break;

        case ConsoleKey.DownArrow:
            selectedIndex = (selectedIndex == options.Length - 1) ? 0 : selectedIndex + 1;
            break;

        case ConsoleKey.Enter:
            HandleSelection(options[selectedIndex]);
            break;
    }

    //ProcessCommand(inputArgs);
}


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