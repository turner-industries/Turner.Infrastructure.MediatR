namespace Turner.Infrastructure.MediatR.Handlers
{
    public interface IPostRequestHandler<in TRequest, in TResponse>
    {
        void Handle(TRequest request, TResponse response);
    }
}