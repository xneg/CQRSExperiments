using MassTransit;
using MediatRExperiments.Commands;
using MediatRExperiments.Events;

namespace MediatRExperiments
{
    public class BusFactoryConfiguration
    {
        private readonly IConsumerFactory _consumerFactory;
        private const string QueueName = "myQueue";
        private const string ErrorQueueName = "myQueue_error";

        public BusFactoryConfiguration(IConsumerFactory consumerFactory)
        {
            _consumerFactory = consumerFactory;
        }

        public void Configure(IBusFactoryConfigurator busFactoryConfigurator)
        {
            ConfigureReceiveEndpoints(busFactoryConfigurator);
        }

        private void ConfigureReceiveEndpoints(IBusFactoryConfigurator busFactoryConfigurator)
        {
            ConfigureConsumersListeningOnMainQueue(busFactoryConfigurator);
//            ConfigureConsumersListeningOnErrorQueue(busFactoryConfigurator);
        }

        private void ConfigureConsumersListeningOnMainQueue(IBusFactoryConfigurator busFactoryConfigurator)
        {
            busFactoryConfigurator.ReceiveEndpoint(QueueName,
                receiveEndpointConfigurator =>
                {
                    receiveEndpointConfigurator.Consumer(typeof(MassTransitCommand), _consumerFactory.Create);
                    receiveEndpointConfigurator.Consumer(typeof(MassTransitEvent), _consumerFactory.Create);
                });
        }

//        private void ConfigureConsumersListeningOnErrorQueue(IBusFactoryConfigurator busFactoryConfigurator)
//        {
//            busFactoryConfigurator.ReceiveEndpoint(ErrorQueueName,
//                receiveEndpointConfigurator =>
//                {
//                    receiveEndpointConfigurator.Consumer(typeof(MyCommandFaultConsumer), _consumerFactory.Create);
//                    receiveEndpointConfigurator.Consumer(typeof(MyEventFaultConsumer), _consumerFactory.Create);
//                });
//        }
    }
}