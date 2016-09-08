using MediatR;
using System.Transactions;
using Turner.Infrastructure.Logging;
using Turner.Infrastructure.MediatR.Decorators.Interfaces;

namespace Turner.Infrastructure.MediatR.Decorators.Handlers
{
    public class TransactionHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _inner;
        private readonly ILogger _log;

        public TransactionHandler(IRequestHandler<TRequest, TResponse> inner,
                                    ILogger log)
        {
            this._inner = inner;
            this._log = log;
        }

        public TResponse Handle(TRequest request)
        {
            _log.Information(string.Format($"Entering transaction Request: {request}"));
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            };

            using (var transaction = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
            {
                var response = _inner.Handle(request);

                transaction.Complete();

                _log.Information(string.Format($"Completed transaction Response: {response}"));
                return response;
            }
        }
    }
}