using System.Collections.Generic;
using FluentValidation.Results;
using Turner.Infrastructure.Exceptions.Core;

namespace Turner.Infrastructure.MediatR.Validation
{
    public static class ValidationResultExtensions
    {
        public static List<BusinessRuleViolation> GetBusinessViolations(this ValidationResult validationResult)
        {
            var violations = new List<BusinessRuleViolation>();

            foreach (var error in validationResult.Errors)
            {
                violations.Add(new BusinessRuleViolation(error.ErrorMessage));
            }

            return violations;
        }
    }
}
