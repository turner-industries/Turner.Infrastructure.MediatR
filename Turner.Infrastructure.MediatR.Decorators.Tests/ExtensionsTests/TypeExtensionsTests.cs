using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NSubstitute;
using NUnit.Framework.Internal;
using NUnit.Framework;
using Turner.Infrastructure.MediatR.Decorators.Extensions;
using Turner.Infrastructure.MediatR.Decorators.Handlers;
using Turner.Infrastructure.MediatR.Decorators.Tests.Common;
using ILogger = Turner.Infrastructure.MediatR.Decorators.Interfaces.ILogger;

namespace Turner.Infrastructure.MediatR.Decorators.Tests.ExtensionsTests
{
    [TestFixture]
    public class TypeExtensionsTests
    {
        [Test]
        public void GetPrettyName_GenericPrettyName()
        {
            IList<Request> listRequest = new List<Request>();

            string prettyName = listRequest.GetType().GetPrettyName();

            Assert.AreEqual("List<Request>", prettyName);
        }

        [Test]
        public void GetPrettyName_NotGenericPrettyName()
        {
            Request request = new Request();

            string prettyName = request.GetType().GetPrettyName();

            Assert.AreEqual("Request", prettyName);
        }
    }
}
