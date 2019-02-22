using MediatR;

namespace MediatRExperiments.Events
{
    public class AfterSomeCommandFinishedEvent : INotification
    {
        public AfterSomeCommandFinishedEvent(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}