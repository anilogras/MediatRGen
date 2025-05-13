using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.SensitiveProperty
{
    public class SensitivePropertyBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            MaskSensitiveData(request);

            var response = await next();

            MaskSensitiveData(response);

            return response;
        }

        private void MaskSensitiveData(object data)
        {
            if (data == null) return;

            var properties = data.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    var value = (string)property.GetValue(data);
                    if (value != null && IsSensitiveData(property.Name))
                    {
                        property.SetValue(data, new string('*', value.Length));
                    }
                }
            }
        }

        private bool IsSensitiveData(string propertyName)
        {
            // Todo : bunu bir attr ile entity üzerinden işaretle eğer varsa * la , string olarak olmaz
            var sensitiveFields = new List<string> { "Password", "TCKN", "PassportNumber" };
            return sensitiveFields.Contains(propertyName, StringComparer.OrdinalIgnoreCase);
        }

    }
}
