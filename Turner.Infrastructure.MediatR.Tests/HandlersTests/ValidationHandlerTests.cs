using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Turner.Infrastructure.Exceptions;
using Turner.Infrastructure.Logging;
using Turner.Infrastructure.MediatR.Handlers;
using Turner.Infrastructure.MediatR.Tests.Common;

namespace Turner.Infrastructure.MediatR.Tests.HandlersTests
{
    [TestFixture]
    public class ValidationTests
    {
        [Test]
        public void Handle_InnerIsCalled()
        {
            IRequestHandler<Request, string> inner = Substitute.For<IRequestHandler<Request, string>>();
            var logger = Substitute.For<ILogger>();
            var validator = Substitute.For<IValidator<Request>>();
            validator.Validate(Arg.Any<Request>()).Returns(v => new ValidationResult());

            var handler = new ValidationHandler<Request, string>(inner, validator, logger);
            handler.Handle(new Request());

            inner.Received().Handle(Arg.Any<Request>());
        }

        [Test]
        public void Handle_ThrowsBusniessRulexception()
        {
            IRequestHandler<Request, string> inner = Substitute.For<IRequestHandler<Request, string>>();
            var logger = Substitute.For<ILogger>();
            var validator = new StaticValidator(false);

            var handler = new ValidationHandler<Request, string>(inner, validator, logger);

            var e = Assert.Throws<BusinessRuleException>(() => handler.Handle(new Request()));
            Assert.AreEqual(e.Violations.First().PropertyName, "foo");
            Assert.AreEqual(e.Violations.First().Violation, "bar");

            inner.DidNotReceive().Handle(Arg.Any<Request>());
        }
    }
}