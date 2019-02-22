using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatRExperiments.Commands;
using MediatRExperiments.Events;

namespace MediatRExperiments.Handlers
{ 
    public class SomeCommandHandler : IRequestHandler<SomeCommand, SomeCommandResult>
    {
        private readonly IMediator _mediator;

        public SomeCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<SomeCommandResult> Handle(SomeCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Publish(new AfterSomeCommandFinishedEvent(request.Field1), cancellationToken);
            return new SomeCommandResult(request.Field1 + "_Result", -request.Field2);
        }
    }
}