using System.Collections.Generic;
using System.Linq;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Turner.Infrastructure.MediatR.Handlers;
using Turner.Infrastructure.MediatR.Tests.Common;

namespace Turner.Infrastructure.MediatR.Tests
{
    [TestFixture]
    public class PipelineTests
    {
        [Test]
        public void Handle_InnerHandleIsCalled()
        {
            IRequestHandler<Request, string> inner = Substitute.For<IRequestHandler<Request, string>>();
            IPreRequestHandler<Request>[] preRequest = Substitute.For<List<IPreRequestHandler<Request>>>().ToArray();
            IPostRequestHandler<Request, string>[] postRequest = Substitute.For<List<IPostRequestHandler<Request, string>>>().ToArray();

            MediatorPipeline<Request, string> pipeline = new MediatorPipeline<Request, string>(
                inner,
                preRequest,
                postRequest
                );

            pipeline.Handle(new Request());

            inner.Received().Handle(Arg.Any<Request>());
        }

        [Test]
        public void Handle_PreRequestInnerIsCalled()
        {
            IRequestHandler<Request, string> inner = Substitute.For<IRequestHandler<Request, string>>();
            IList<IPreRequestHandler<Request>> preRequest = Substitute.For<List<IPreRequestHandler<Request>>>();
            IList<IPostRequestHandler <Request, string>> postRequest = Substitute.For<List<IPostRequestHandler<Request, string>>>();

            preRequest.Add(Substitute.For<IPreRequestHandler<Request>>());

            MediatorPipeline<Request, string> pipeline = new MediatorPipeline<Request, string>(
                inner,
                preRequest.ToArray(),
                postRequest.ToArray()
                );

            pipeline.Handle(new Request());

            preRequest[0].Received().Handle(Arg.Any<Request>());
        }

        [Test]
        public void Handle_PostRequestInnerIsCalled()
        {
            IRequestHandler<Request, string> inner = Substitute.For<IRequestHandler<Request, string>>();
            IList<IPreRequestHandler<Request>> preRequest = Substitute.For<List<IPreRequestHandler<Request>>>();
            IList<IPostRequestHandler<Request, string>> postRequest = Substitute.For<List<IPostRequestHandler<Request, string>>>();

            postRequest.Add(Substitute.For<IPostRequestHandler<Request, string>>());

            MediatorPipeline<Request, string> pipeline = new MediatorPipeline<Request, string>(
                inner,
                preRequest.ToArray(),
                postRequest.ToArray()
                );

            pipeline.Handle(new Request());

            postRequest[0].Received().Handle(Arg.Any<Request>(), Arg.Any<string>());
        }
    }
}
