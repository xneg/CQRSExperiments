using System;

namespace MediatRExperiments
{
    public interface IConsumerFactory
    {
        object Create(Type typeToCreate);
    }
}