using NUnit.Framework;
using System.Collections.Generic;
using Turner.Infrastructure.MediatR.Extensions;
using Turner.Infrastructure.MediatR.Tests.Common;

namespace Turner.Infrastructure.MediatR.Tests.ExtensionsTests
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