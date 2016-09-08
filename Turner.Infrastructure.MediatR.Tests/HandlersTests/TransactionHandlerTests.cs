using MediatR;
using NSubstitute;
using NUnit.Framework;
using Turner.Infrastructure.Logging;
using Turner.Infrastructure.MediatR.Handlers;
using Turner.Infrastructure.MediatR.Tests.Common;

namespace Turner.Infrastructure.MediatR.Tests
{
    [TestFixture]
    public class TransactionTests
    {
        [Test]
        public void Handle_InnerIsCalled()
        {
            IRequestHandler<Request, string> inner = Substitute.For<IRequestHandler<Request, string>>();
            var logger = Substitute.For<ILogger>();

            var handler = new TransactionHandler<Request, string>(inner, logger);
            handler.Handle(new Request());

            inner.Received().Handle(Arg.Any<Request>());
        }
    }
}