using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;

namespace MediatRExperiments
{
    public class ParallelNoWaitMediator : Mediator
    {
        public ParallelNoWaitMediator(ServiceFactory serviceFactory) : base(serviceFactory)
        {
        }
        
        protected override Task PublishCore(IEnumerable<Func<Task>> allHandlers)
        {
            return ParallelNoWait(allHandlers);
        }
        
        private Task ParallelNoWait(IEnumerable<Func<Task>> handlers)
        {
            foreach (var handler in handlers)
            {
                Task.Run(() => handler());
            }

            return Task.CompletedTask;
        }
    }
}