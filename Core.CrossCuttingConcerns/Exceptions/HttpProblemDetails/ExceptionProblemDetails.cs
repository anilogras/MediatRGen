using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails
{
    internal class ExceptionProblemDetails : ProblemDetails
    {
        public ExceptionProblemDetails(string detail)
        {
            Title = "Global Exception Error";
            Detail = detail;
            Status = StatusCodes.Status500InternalServerError;
            Type = "https://example.com/probs/business";
        }

    }
}
