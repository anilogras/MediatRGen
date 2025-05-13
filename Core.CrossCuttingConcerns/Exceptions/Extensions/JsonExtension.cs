using Core.Domain.Models.Logger;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.Extensions;

public static class JsonExtension
{
    public static string AsJSON<TValue>(this TValue value) =>
         value switch
         {
             ProblemDetails => JsonSerializer.Serialize(value, new JsonSerializerOptions { WriteIndented = true }),
             LogDetail => JsonSerializer.Serialize(value, new JsonSerializerOptions { WriteIndented = true })
         };
}