using System;
using System.Linq;
using MediatR;
using MediatRExperiments.Events;
using MediatRExperiments.MediatRHandlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MediatRExperiments.Extensions
{
    public static class ConfigureExtensions
    {
        public static IHostBuilder ConfigureApp(this IHostBuilder hostBuilder)
        {
            return hostBuilder
                .WithCommandSender()
                .WithInMemoryStorage()
                .WithMediator();
        }
        
        public static IHostBuilder WithCustomMediator(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(collection =>
            {
                collection.AddSingleton<IMediator, ParallelNoWaitMediator>();
            });
        }

        public static IHostBuilder WithBadEventConsumer(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(collection =>
            {
                collection.AddTransient<INotificationHandler<AfterSomeCommandFinishedEvent>, EventHandlerFailed>();
            });
        }

        public static SomeCommandSender GetCommandSender(this IHost host)
        {
            return host.Services.GetService<SomeCommandSender>();
        }

        public static InMemoryStorage GetInMemoryStorage(this IHost host)
        {
            return host.Services.GetService<InMemoryStorage>();
        }
        
        private static IHostBuilder WithCommandSender(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(collection => { collection.AddSingleton<SomeCommandSender>(); });
        }

        private static IHostBuilder WithInMemoryStorage(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(collection =>
            {
                collection.AddSingleton<InMemoryStorage>();
            });
        }

        private static IHostBuilder WithMediator(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(collection =>
            {
                collection.AddMediatR();
                var serviceDescriptor = collection.FirstOrDefault(descriptor => descriptor.ImplementationType == typeof(EventHandlerFailed));
                collection.Remove(serviceDescriptor);
            });
        }
    }
}