using System;
using Application.Shared.Models;
using MassTransit;

namespace Application.Entrypoint.DeadLetter
{
    public class PetDeadLetterConsumer : IConsumer<Pet>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PetDeadLetterConsumer(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<Pet> context)
        {
            int retries = context.GetRetryAttempt();

            if (retries > 3)
            {
                throw new Exception("Mensagem excedeu a quatidade de tentativas.");
            }
        }
    }
}

