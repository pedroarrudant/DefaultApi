using System;
using Application.Shared.Models;
using Application.Shared.Repositories;
using MassTransit;

namespace Application.Entrypoint.Consumer
{
    public class InsertPetConsumer : IConsumer<Pet>, IInsertPetConsumer
    {
        private readonly IPetsRepository _repository;

        public InsertPetConsumer(IPetsRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<Pet> context)
        {
            await _repository.InsertPetAsync(context.Message, new CancellationToken());
        }
    }
}

