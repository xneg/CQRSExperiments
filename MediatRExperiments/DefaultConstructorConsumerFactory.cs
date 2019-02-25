using System;

namespace MediatRExperiments
{
    public class DefaultConstructorConsumerFactory : IConsumerFactory
    {
        public object Create(Type typeToCreate)
        {
            return Activator.CreateInstance(typeToCreate);
        }
    }
}