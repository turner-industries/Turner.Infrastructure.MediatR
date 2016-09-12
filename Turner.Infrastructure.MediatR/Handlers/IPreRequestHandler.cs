namespace Turner.Infrastructure.MediatR.Handlers
{
    public interface IPreRequestHandler<in TRequest>
    {
        void Handle(TRequest request);
    }
}