using MediatR;
using Newtonsoft.Json;
using Turner.Infrastructure.Logging;
using Turner.Infrastructure.MediatR.Extensions;

namespace Turner.Infrastructure.MediatR.Handlers
{
    public class LoggingHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _inner;
        private readonly ILogger _log;

        public LoggingHandler(IRequestHandler<TRequest, TResponse> inner,
                                ILogger log)
        {
            _inner = inner;
            _log = log;
        }

        public TResponse Handle(TRequest request)
        {
            var type = request.GetType();
            _log.Information($"Logging: TRequest: {type.GetPrettyName()}");

            if (type.GenericTypeArguments.Length == 0)
            {
                _log.Information($"JSON {type.GetPrettyName()}: {JsonConvert.SerializeObject(request)}");
            }

            var response = _inner.Handle(request);

            var log = response == null
                ? "Logging TResponse: null"
                : $"Logging: TResponse: {response.GetType().GetPrettyName()}";

            _log.Information(log);

            return response;
        }
    }
}