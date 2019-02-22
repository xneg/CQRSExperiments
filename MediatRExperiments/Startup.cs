using System;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace MediatRExperiments
{
    public static class Startup
    {
        public static IHostBuilder ConfigureApp(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices(collection =>
            {
                collection.AddMediatR();
                Console.WriteLine("Ok");
            });
        }
    }
}