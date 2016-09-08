namespace Turner.Infrastructure.MediatR.Decorators.Interfaces
{
    public interface IPostRequestHandler<in TRequest, in TResponse>
    {
        void Handle(TRequest request, TResponse response);
    }
}