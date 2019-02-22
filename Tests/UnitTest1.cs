using System;
using System.Threading.Tasks;
using MediatRExperiments;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            var host = new HostBuilder()
                .ConfigureApp()
                .WithCommandSender()
                .Build();

            var commandSender = host.GetCommandSender();

            await host.StartAsync();

            var commandResult = await commandSender.SendCommand();

            await host.StopAsync();
            
            Assert.Equal("Test_Result", commandResult.ResultField1);
        }
    }
}