using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatRExperiments.Commands;

namespace MediatRExperiments
{
    public class SomeCommandSender
    {
        private readonly IMediator _mediator;

        public SomeCommandSender(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<SomeCommandResult> SendCommand()
        {
            var commandResult = await _mediator.Send(new SomeCommand
            {
                Field1 = "Test",
                Field2 = 5
            }, CancellationToken.None);

            Console.WriteLine($"Command result: Field1 = {commandResult.ResultField1}, Field2 = {commandResult.ResultField2}");

            return commandResult;
        }
    }
}