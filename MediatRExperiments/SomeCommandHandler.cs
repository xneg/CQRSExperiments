using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatRExperiments.Commands;

namespace MediatRExperiments
{ 
    public class SomeCommandHandler : IRequestHandler<SomeCommand, SomeCommandResult>
    {
        public Task<SomeCommandResult> Handle(SomeCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new SomeCommandResult
            {
                ResultField1 = request.Field1 + "_Result",
                ResultField2 = -request.Field2
            });
        }
    }
}