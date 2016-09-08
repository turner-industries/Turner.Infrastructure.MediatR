namespace Turner.Infrastructure.MediatR.Interfaces
{
    public interface IPreRequestHandler<in TRequest>
    {
        void Handle(TRequest request);
    }
}