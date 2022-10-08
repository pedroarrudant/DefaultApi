using Application.Features.GetPetsByName.Models;

namespace Application.Features.GetPetsByName.Repositories
{
    public interface IPetsRepository
    {
        Task<Pet?> GetPetByNameAsync(string PetName, CancellationToken cancellationToken);
    }
}