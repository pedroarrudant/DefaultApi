using Application.Features.GetPetsByName.Models;
using Application.Shared.Models;

namespace Application.Shared.Repositories
{
    public interface IPetsRepository
    {
        Task<Pet?> GetPetByNameAsync(string PetName, CancellationToken cancellationToken);

        Task<bool> InsertPetAsync(Pet pet, CancellationToken cancellationToken);
    }
}