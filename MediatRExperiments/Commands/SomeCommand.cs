using MediatR;

namespace MediatRExperiments.Commands
{
    public class SomeCommand: IRequest<SomeCommandResult>
    {
        public string Field1 { get; set; }
        
        public int Field2 { get; set; }
    }
}