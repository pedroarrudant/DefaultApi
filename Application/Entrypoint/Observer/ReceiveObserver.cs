using System;
using System.Diagnostics.CodeAnalysis;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.Entrypoint.Observer
{
    [ExcludeFromCodeCoverage]
    public class ReceiveObserver : IReceiveObserver
    {
        private readonly ILogger<ReceiveObserver> _logger;

        public ReceiveObserver(ILogger<ReceiveObserver> logger)
        {
            _logger = logger;
        }

        public async Task ConsumeFault<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType, Exception exception) where T : class
        {
            _logger.LogInformation($"[RabbitMQ - Observer] Mensagem do tipo {context.Message.GetType().Name} enviada com erro {exception.Message} do consumer.");
        }

        public async Task PostConsume<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType) where T : class
        {
            _logger.LogInformation($"[RabbitMQ - Observer] Mensagem do tipo {context.Message.GetType().Name} id {context.MessageId} consumida.");
        }

        public async Task PostReceive(ReceiveContext context)
        {
            _logger.LogInformation($"[RabbitMQ - Observer] Mensagem {context.GetMessageId()} recebida.");
        }

        public async Task PreReceive(ReceiveContext context)
        {
            _logger.LogInformation($"[RabbitMQ - Observer] Mensagem {context.GetMessageId()} consumida.");
        }

        public async Task ReceiveFault(ReceiveContext context, Exception exception)
        {
            if (context.Redelivered)
            {
                _logger.LogInformation($"[RabbitMQ - Observer] Mensagem {context.GetMessageId()} reenviada.");
            }
            _logger.LogError($"[RabbitMQ - Observer] Mensagem {context.GetMessageId()} enviada porém foi dada excessao {exception.Message} no consumer.");
        }
    }
}

