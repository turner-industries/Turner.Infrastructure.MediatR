using FluentValidation;
using FluentValidation.Results;

namespace Turner.Infrastructure.MediatR.Tests.Common
{
    public class StaticValidator : AbstractValidator<Request>
    {
        private readonly bool _result;

        public StaticValidator(bool result)
        {
            _result = result;

            Custom(request => !_result ? new ValidationFailure("foo", "bar") : null);
        }
    }
}