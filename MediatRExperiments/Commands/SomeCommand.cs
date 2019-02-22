using MediatR;

namespace MediatRExperiments.Commands
{
    public class SomeCommand: IRequest<SomeCommandResult>
    {
        public string Field1 { get; }
        
        public int Field2 { get; }
        
        public SomeCommand(string field1, int field2)
        {
            Field1 = field1;
            Field2 = field2;
        }
    }
}