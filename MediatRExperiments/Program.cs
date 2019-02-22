using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace MediatRExperiments
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureApp()
                .Build();

            await host.StartAsync();
//            await host.RunAsync();
            Console.WriteLine("after start async");
        }
    }
}