using MediatRGen.Core.Base;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Services;

namespace MediatRGen.Core.Concrete
{
    internal class QuestionService : IQuestionService
    {
        public ServiceResult<bool> YesNoQuestion(string question)
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

            return new ServiceResult<bool>(answer, true, "");
        }

        public ServiceResult<string> GetAnswer(string question)
        {
            while (true)
            {

                Console.WriteLine(question);
                string answer = Console.ReadLine();

                if (string.IsNullOrEmpty(answer))
                {
                    Console.WriteLine(LangHandler.Definitions().Required);
                    continue;
                }

                return new ServiceResult<string>(answer, true, "");
            }
        }
    }
}
