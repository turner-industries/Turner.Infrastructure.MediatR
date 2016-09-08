using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Turner.Infrastructure.MediatR.Decorators.Handlers;
using Turner.Infrastructure.MediatR.Decorators.Interfaces;
using Turner.Infrastructure.MediatR.Decorators.Tests.Common;

namespace Turner.Infrastructure.MediatR.Decorators.Tests
{
    [TestFixture]
    public class AuthorizeHandlerTests
    {
        [Test]
        public void Handle_InnerIsCalled()
        {
            IRequestHandler<Request, string> inner = Substitute.For<IRequestHandler<Request, string>>();

            var handler = new AuthorizeHandler<Request, string>(inner);
            handler.Handle(new Request());
            
            inner.Received().Handle(Arg.Any<Request>());
        }
    }
}
