using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatRExperiments.Events;

namespace MediatRExperiments.MediatRHandlers
{
    public class EventHandlerFailed : INotificationHandler<AfterSomeCommandFinishedEvent>
    {
        public Task Handle(AfterSomeCommandFinishedEvent notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}