using System;
using System.Threading.Tasks;
using MediatRExperiments.Extensions;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Tests
{
    public class MyDemoProgramShould
    {
        [Fact]
        public async Task SendCommandThroughMediatRAndReturnResult()
        {
            var host = new HostBuilder()
                .ConfigureApp()
                .Build();

            var commandSender = host.GetCommandSender();

            await host.StartAsync();

            var commandResult = await commandSender.SendCommand("test1");

            await host.StopAsync();
            
            Assert.Equal("test1_Result", commandResult.ResultField1);
        }

        [Fact]
        public async Task ThrowExceptionWhenOneOfHandlersIsBad()
        {
            var host = new HostBuilder()
                .ConfigureApp()
                .WithBadEventConsumer()
                .Build();

            var commandSender = host.GetCommandSender();

            await host.StartAsync();

            await Assert.ThrowsAsync<NotImplementedException>(() => commandSender.SendCommand("test2"));
            
            await host.StopAsync();
        }
        
        [Fact]
        public async Task UseParallelPublishWithoutHandlingExceptions()
        {
            var host = new HostBuilder()
                .ConfigureApp()
                .WithBadEventConsumer()
                .WithCustomMediator()
                .Build();

            var commandSender = host.GetCommandSender();
            var inMemoryStorage = host.GetInMemoryStorage();

            await host.StartAsync();

            var commandResult = await commandSender.SendCommand("test3");

            await WaitUntilConditionMetOrTimedOut(() => inMemoryStorage.Count == 1);
            
            await host.StopAsync();
            
            Assert.Equal(1, inMemoryStorage.Count);
        }

        [Fact]
        public async Task X()
        {
            var host = new HostBuilder()
                .ConfigureApp()
                .WithMassTransitSupport()
                .Build();
            
            var commandSender = host.GetCommandSender();
            var inMemoryStorage = host.GetInMemoryStorage();
            
            await host.StartAsync();
            
            await commandSender.SendCommand("test4");

            await Task.Delay(5000);
            
            await WaitUntilConditionMetOrTimedOut(() => inMemoryStorage.GetValue("masstransit") == "eventReceived");
            
            await host.StopAsync();
            
            Assert.Equal("eventReceived", inMemoryStorage.GetValue("masstransit"));
        }
        
        private async Task WaitUntilConditionMetOrTimedOut(Func<bool> conditionMet)
        {
            var timeoutExpired = false;
            var startTime = DateTime.Now;
            while (!conditionMet() && !timeoutExpired)
            {
                await Task.Delay(100);
                timeoutExpired = DateTime.Now - startTime > TimeSpan.FromSeconds(5);
            }
        }
    }
}