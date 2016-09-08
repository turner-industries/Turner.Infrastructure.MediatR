using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Turner.Infrastructure.MediatR.Decorators.Handlers
{
    public class AuthorizeHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _inner;

        public AuthorizeHandler(IRequestHandler<TRequest, TResponse> inner)
        {
            this._inner = inner;
        }

        public TResponse Handle(TRequest request)
        {
            var response = _inner.Handle(request);

            return response;
        }
    }
}
