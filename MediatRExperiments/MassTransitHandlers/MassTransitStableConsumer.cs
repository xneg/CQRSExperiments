using System.Threading.Tasks;
using MassTransit;
using MediatRExperiments.Events;

namespace MediatRExperiments.MassTransitHandlers
{
    public class MassTransitStableConsumer : IConsumer<MassTransitEvent>
    {
        private readonly InMemoryStorage _inMemoryStorage;

        public MassTransitStableConsumer(InMemoryStorage inMemoryStorage)
        {
            _inMemoryStorage = inMemoryStorage;
        }

        public Task Consume(ConsumeContext<MassTransitEvent> context)
        {
            _inMemoryStorage.AddValue("masstransit", "eventReceived");
            return Task.CompletedTask;
        }
    }
}