using MediatR;
using SimpleInjector;
using System.Reflection;
using Turner.Infrastructure.MediatR.Handlers;
using Turner.Infrastructure.MediatR.Interfaces;

namespace Turner.Infrastructure.MediatR.Extensions
{
    public static class ContainerExtensions
    {
        public static void ConfigureMediatR(this Container container, Lifestyle lifestyle)
        {
            var assembly = Assembly.GetCallingAssembly();

            container.RegisterCollection(typeof(INotificationHandler<>), assembly);
            container.Register(typeof(IRequestHandler<,>), new[] { assembly }, lifestyle);
            container.RegisterCollection(typeof(IPreRequestHandler<>), assembly);
            container.RegisterCollection(typeof(IPostRequestHandler<,>), assembly);

            container.RegisterDecorator(
                typeof(IRequestHandler<,>),
                typeof(ValidationHandler<,>),
                Lifestyle.Transient,
                x => !x.ImplementationType.ContainsAttribute(typeof(DoNotValidate)));

            container.RegisterDecorator(
                typeof(IRequestHandler<,>),
                typeof(MediatorPipeline<,>),
                Lifestyle.Transient);

            container.RegisterDecorator(typeof(IRequestHandler<,>),
                typeof(TransactionHandler<,>),
                Lifestyle.Transient,
                x => !x.ImplementationType.ContainsAttribute(typeof(DoNotCommit)));
        }
    }
}