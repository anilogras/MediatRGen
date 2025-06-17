using MediatRGen.Core.Base;
using MediatRGen.Core.Languages;
using MediatRGen.Core.Services;

namespace MediatRGen.Core.Concrete
{
    internal class QuestionService : IQuestionService
    {
        private readonly IOutputService _OutputService;

        public QuestionService(IOutputService outputService)
        {
            _OutputService = outputService;
        }

        public ServiceResult<bool> YesNoQuestion(string question)
        {

            bool answer = false;

            while (true)
            {
                _OutputService.Info(question + " (y / n)");
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

                _OutputService.Info(question);
                string answer = Console.ReadLine();

                if (string.IsNullOrEmpty(answer))
                {
                    _OutputService.Info(LangHandler.Definitions().Required);
                    continue;
                }

                return new ServiceResult<string>(answer, true, "");
            }
        }
    }
}
