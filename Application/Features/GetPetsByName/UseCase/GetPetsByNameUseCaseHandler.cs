using System;
using Application.Features.GetPetsByName.Models;
using Application.Features.GetPetsByName.Repositories;
using MediatR;

namespace Application.Features.GetPetsByName.UseCase
{
    public class GetPetsByNameUseCaseHandler : IRequestHandler<GetPetsByNameInput, GetPetsByNameOutput>
    {
        private readonly IPetsRepository _repository;

        public GetPetsByNameUseCaseHandler(IPetsRepository repository)
        {
            _repository = repository;
        }
        public async Task<GetPetsByNameOutput> Handle(GetPetsByNameInput request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetPetByNameAsync(request.Name, cancellationToken);

            return new GetPetsByNameOutput() { Pet = result };
        }
    }
}

