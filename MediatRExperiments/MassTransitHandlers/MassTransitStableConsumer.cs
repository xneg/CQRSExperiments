using System.Threading.Tasks;
using MassTransit;
using MediatRExperiments.Commands;

namespace MediatRExperiments.MassTransitHandlers
{
    public class MassTransitStableConsumer : IConsumer<MassTransitCommand>
    {
        public Task Consume(ConsumeContext<MassTransitCommand> context)
        {
            throw new System.NotImplementedException();
        }
    }
}