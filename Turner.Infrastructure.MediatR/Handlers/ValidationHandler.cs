using FluentValidation;
using MediatR;
using Turner.Infrastructure.Exceptions.Core;
using Turner.Infrastructure.Logging;
using Turner.Infrastructure.MediatR.Extensions;
using Turner.Infrastructure.MediatR.Validation;

namespace Turner.Infrastructure.MediatR.Handlers
{
    public class ValidationHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _inner;
        private readonly IValidator<TRequest> _validator;
        private readonly ILogger _log;

        public ValidationHandler(IRequestHandler<TRequest, TResponse> inner,
                                IValidator<TRequest> validator,
                                ILogger log)
        {
            _inner = inner;
            _validator = validator;
            _log = log;
        }

        public TResponse Handle(TRequest request)
        {
            var requestType = request.GetType().GetPrettyName();

            _log.Information($"Validating TRequest of type {requestType}");

            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                var violations = validationResult.GetBusinessViolations();

                _log.Information($"Validating TRequest: {requestType} FAILED with {violations}");
                throw new BusinessRuleException(violations);
            }

            _log.Information($"Validating TRequest: {requestType} PASSED");
            return _inner.Handle(request);
        }
    }
}