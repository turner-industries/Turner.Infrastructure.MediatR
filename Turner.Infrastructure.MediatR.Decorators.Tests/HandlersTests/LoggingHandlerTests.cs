using MediatR;
using NSubstitute;
using NUnit.Framework;
using Turner.Infrastructure.MediatR.Decorators.Handlers;
using Turner.Infrastructure.MediatR.Decorators.Interfaces;
using Turner.Infrastructure.MediatR.Decorators.Tests.Common;

namespace Turner.Infrastructure.MediatR.Decorators.Tests
{
    [TestFixture]
    public class LoggingTests
    {
        [Test]
        public void Handle_InnerIsCalled()
        {
            IRequestHandler<Request, string> inner = Substitute.For<IRequestHandler<Request, string>>();
            var logger = Substitute.For<ILogger>();

            var handler = new LoggingHandler<Request, string>(inner, logger);
            handler.Handle(new Request());

            logger.Received().Information(Arg.Is<string>(s => s.Contains("Logging:")));
            inner.Received().Handle(Arg.Any<Request>());
        }
    }
}
