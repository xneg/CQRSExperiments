using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MediatRExperiments
{
    public static class CommandSenderExtensions
    {
        public static IHostBuilder WithCommandSender(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(collection => { collection.AddSingleton<SomeCommandSender>(); });
        }

        public static SomeCommandSender GetCommandSender(this IHost host)
        {
            return host.Services.GetService <SomeCommandSender>();
        }
    }
}