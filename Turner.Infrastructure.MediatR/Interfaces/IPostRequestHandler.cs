namespace Turner.Infrastructure.MediatR.Interfaces
{
    public interface IPostRequestHandler<in TRequest, in TResponse>
    {
        void Handle(TRequest request, TResponse response);
    }
}