using System;
using System.Linq;
using System.Reflection;
using Individuals.Decorators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Individuals.Api
{
    public static class DecoratorRegistrar
    {
        public static IServiceCollection RegisterDecorators(this IServiceCollection serviceCollection,Assembly[] decoratorAssemblies)
        {
            foreach (var assembly in decoratorAssemblies)
            {
                RegisterDecoratorsFromAssembly(serviceCollection,assembly);
            }

            return serviceCollection;
        }

        public static void RegisterDecoratorsFromAssembly(IServiceCollection collection, Assembly assembly)
        {
            try
            {
                var handlers = assembly.GetTypes().Where(x =>
                    x.GetInterfaces().Any(z =>z.IsGenericType &&( z.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))).ToList();

                foreach (var handler in handlers)
                {

                    ProcessHandlerAttributes(collection,handler);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void ProcessHandlerAttributes(IServiceCollection serviceCollection, Type handler)
        {
            var attributes = handler.GetCustomAttributes(false).Where(x=>x.GetType() == typeof(BaseDecoratorAttribute)).ToList();

            foreach (var attribute in attributes)
            {
                var arguments = handler.GetInterfaces().FirstOrDefault(x=>x.IsGenericType && 
                                                                          x.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)).GetGenericArguments();
                var requestType = arguments[0];
                var responseType = arguments[1];
                var decoratorType = (attribute as BaseDecoratorAttribute).DecoratorType;
                
                var decoratorNonGenericType = decoratorType.MakeGenericType(requestType, responseType);
                var pipelineNonGenericType = typeof(IPipelineBehavior<,>).MakeGenericType(requestType, responseType);
                serviceCollection.AddTransient(pipelineNonGenericType, decoratorNonGenericType);
            }
        }
    }
}
