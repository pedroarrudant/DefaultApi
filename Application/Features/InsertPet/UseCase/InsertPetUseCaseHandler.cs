using System;
using Application.Features.GetPetsByName.Models;
using Application.Features.InsertPet.Models;
using Application.Shared.Models;
using Application.Shared.Repositories;
using MassTransit;
using MediatR;

namespace Application.Features.InsertPet.UseCase
{
    public class InsertPetUseCaseHandler : IRequestHandler<InsertPetInput, InsertPetOutput>
    {
        private readonly IPetsRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;

        public InsertPetUseCaseHandler(IPetsRepository repository, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<InsertPetOutput> Handle(InsertPetInput request, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish<Pet>(request.Pet, cancellationToken);

            return new InsertPetOutput();
        }
    }
}

