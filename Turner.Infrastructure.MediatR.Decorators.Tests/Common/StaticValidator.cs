using FluentValidation;
using FluentValidation.Results;

namespace Turner.Infrastructure.MediatR.Decorators.Tests.Common
{
    public class StaticValidator : AbstractValidator<Request>
    {
        private readonly bool _result;

        public StaticValidator(bool result)
        {
            _result = result;

            Custom(request => !_result ? new ValidationFailure("", "") : null);
        }
    }
}