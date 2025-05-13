using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails
{
    public class TransactionProblemDetails : ProblemDetails
    {
        public TransactionProblemDetails(string message)
        {
            Title = "Transaction Error";
            Detail = message;
            Type = "https://example.com/transaction-error";
            Status = 500;
        }
    }
}
