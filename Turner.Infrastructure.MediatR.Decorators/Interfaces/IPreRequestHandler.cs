namespace Turner.Infrastructure.MediatR.Decorators.Interfaces
{
    public interface IPreRequestHandler<in TRequest>
    {
        void Handle(TRequest request);
    }
}