using MediatRGen.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Services
{
    public interface IQuestionService
    {
        public ServiceResult<bool> YesNoQuestion(string question);
        public ServiceResult<string> GetAnswer(string question);
    }
}
