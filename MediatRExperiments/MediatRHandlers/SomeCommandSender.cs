using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatRExperiments.Commands;

namespace MediatRExperiments.MediatRHandlers
{
    public class SomeCommandSender
    {
        private readonly IMediator _mediator;

        public SomeCommandSender(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<SomeCommandResult> SendCommand(string message)
        {
            var commandResult = await _mediator.Send(new SomeCommand(message, 5), CancellationToken.None);

            Console.WriteLine($"Command result: Field1 = {commandResult.ResultField1}, Field2 = {commandResult.ResultField2}");

            return commandResult;
        }
    }
}