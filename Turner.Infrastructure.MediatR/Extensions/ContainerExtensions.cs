using System;
using MediatR;
using SimpleInjector;
using System.Reflection;
using FluentValidation;
using Turner.Infrastructure.MediatR.Handlers;

namespace Turner.Infrastructure.MediatR.Extensions
{
    public static class ContainerExtensions
    {
        public static void ConfigureMediatR(this Container container, Lifestyle lifestyle)
        {
            var entry = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();

            foreach (var assembly in entry.GetReferencedAssemblies())
            {
                Assembly.Load(assembly);
            }

            container.RegisterCollection(typeof(INotificationHandler<>), AppDomain.CurrentDomain.GetAssemblies());
            container.Register(typeof(IRequestHandler<,>), AppDomain.CurrentDomain.GetAssemblies(), lifestyle);
            container.RegisterCollection(typeof(IPreRequestHandler<>), AppDomain.CurrentDomain.GetAssemblies());
            container.RegisterCollection(typeof(IPostRequestHandler<,>), AppDomain.CurrentDomain.GetAssemblies());

            container.Register(typeof(IValidator<>), AppDomain.CurrentDomain.GetAssemblies());

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
