using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatRExperiments.Events;

namespace MediatRExperiments.Handlers
{
    public class EventHandlerSuccessful : INotificationHandler<AfterSomeCommandFinishedEvent>
    {
        private readonly InMemoryStorage _inMemoryStorage;

        public EventHandlerSuccessful(InMemoryStorage inMemoryStorage)
        {
            _inMemoryStorage = inMemoryStorage;
        }

        public Task Handle(AfterSomeCommandFinishedEvent notification, CancellationToken cancellationToken)
        {
            _inMemoryStorage.Increment();
            _inMemoryStorage.AddValue(notification.Message, "ok");
            return Task.CompletedTask;
        }
    }
}