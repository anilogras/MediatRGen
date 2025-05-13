using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Application.Pipelines.Transaction
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ITransactionRequest
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            using TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            TResponse response;

            try
            {
                response = await next();
                transactionScope.Complete();
            }
            catch (Exception ex)
            {
                transactionScope.Dispose();
                throw new CrossCuttingConcerns.Exceptions.ExceptionTypes.TransactionException(ex.Message);
            }

            return response;

        }
    }
}
