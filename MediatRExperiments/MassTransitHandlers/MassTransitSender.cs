using System.Threading.Tasks;
using MassTransit;
using MediatRExperiments.Commands;
using MediatRExperiments.Events;

namespace MediatRExperiments.MassTransitHandlers
{
    public class MassTransitSender
    {
        private readonly IBusControl _busControl;

        public MassTransitSender(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public async Task Send()
        {
            await _busControl.Send(new MassTransitCommand());
        }
        
        public async Task Publish()
        {
            await _busControl.Publish(new MassTransitEvent());
        }
    }
}