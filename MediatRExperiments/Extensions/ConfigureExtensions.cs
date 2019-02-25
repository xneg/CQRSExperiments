using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using MediatRExperiments.Events;
using MediatRExperiments.MassTransitHandlers;
using MediatRExperiments.MediatRHandlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IHost = Microsoft.Extensions.Hosting.IHost;

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

        public static IHostBuilder WithMassTransitSupport(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(collection =>
            {
                var consumerFactory = new DefaultConstructorConsumerFactory();
                var busFactoryConfiguration = new BusFactoryConfiguration(consumerFactory);
                collection.AddSingleton(Bus.Factory.CreateUsingInMemory(busFactoryConfiguration.Configure));
                collection.AddHostedService<MassTransitHostedService>();
                
                collection.AddTransient<INotificationHandler<AfterSomeCommandFinishedEvent>, EventHandlerWithMassTransitSender>();
                collection.AddTransient<MassTransitSender>();
                collection.AddTransient<MassTransitStableConsumer>();
            });
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
                serviceDescriptor = collection.FirstOrDefault(descriptor =>
                    descriptor.ImplementationType == typeof(EventHandlerWithMassTransitSender));
                collection.Remove(serviceDescriptor);
            });
        }
    }
}