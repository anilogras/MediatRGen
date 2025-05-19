namespace MediatRGen.Cli.Helpers
{
    public static class QuestionHelper
    {
        public static bool YesNoQuestion(string question)
        {

            bool answer = false;

            while (true)
            {
                Console.WriteLine(question + " (y / n)");
                string? response = Console.ReadLine();

                if (response?.ToLower() != "y" && response?.ToLower() != "n")
                    continue;

                if (response.ToLower() == "n")
                {
                    answer = false;
                    break;
                }
                else
                {
                    answer = true;
                    break;
                }
            }

            return answer;
        }

        public static void GetAnswer(string question, ref object answer)
        {
            while (true)
            {

                Console.WriteLine(question);
                string moduleName = Console.ReadLine();

                if (string.IsNullOrEmpty(moduleName))
                {
                    Console.WriteLine(LangHandler.Definitions().Required);
                    continue;
                }

                answer = moduleName;
                break;
            }
        }
    }
}
