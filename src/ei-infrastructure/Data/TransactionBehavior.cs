using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using MediatR;

namespace ei_infrastructure.Data
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IDbConnection _dbConnection;

        public TransactionBehavior(IDbConnection dbConnection) => _dbConnection = dbConnection;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                TResponse response;
                using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    using (_dbConnection)
                    {
                        response = await next();
                    }
                    transactionScope.Complete();
                }

                return response;
            }
            catch (Exception)
            {
                // The TransactionScope.Complete method commits the transaction. If an exception has been thrown,
                // Complete is not  called, therefore the transaction is automatically rolled back.

                // TODO: Add a logging entry here with feature #164 (https://dev.azure.com/Apptelier/Entrenamiento%20Imaginativo/_workitems/edit/164).
                throw;
            }
        }
    }
}