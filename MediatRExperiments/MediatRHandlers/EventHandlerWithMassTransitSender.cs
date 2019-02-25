using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatRExperiments.Events;
using MediatRExperiments.MassTransitHandlers;

namespace MediatRExperiments.MediatRHandlers
{
    public class EventHandlerWithMassTransitSender : INotificationHandler<AfterSomeCommandFinishedEvent>
    {
        private readonly MassTransitSender _massTransitSender;

        public EventHandlerWithMassTransitSender(MassTransitSender massTransitSender)
        {
            _massTransitSender = massTransitSender;
        }

        public async Task Handle(AfterSomeCommandFinishedEvent notification, CancellationToken cancellationToken)
        {
            // await _massTransitSender.Send();
            await _massTransitSender.Publish();
        }
    }
}