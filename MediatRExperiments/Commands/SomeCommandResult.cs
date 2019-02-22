namespace MediatRExperiments.Commands
{
    public class SomeCommandResult
    {
        public string ResultField1 { get; }
        
        public int ResultField2 { get; }
        
        public SomeCommandResult(string resultField1, int resultField2)
        {
            ResultField1 = resultField1;
            ResultField2 = resultField2;
        }
    }
}