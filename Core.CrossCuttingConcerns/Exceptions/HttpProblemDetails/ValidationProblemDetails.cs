using Core.CrossCuttingConcerns.Exceptions.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails
{
    public class ValidationProblemDetails : ProblemDetails
    {
        public IEnumerable<ValidationExceptionModel>? Errors { get; }

        public ValidationProblemDetails(IEnumerable<ValidationExceptionModel>? errors)
        {
            Title = "Validation errors";
            Detail = "One or more validation errors occurred.";
            Errors = errors;
            Status = StatusCodes.Status400BadRequest;
            Type = "https://example.com/probs/business";
        }
    }
}
