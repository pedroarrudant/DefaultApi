using System;
using System.Data;
using Application.Features.GetPetsByName.Models;
using Application.Shared.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Features.GetPetsByName.Repositories
{
    public class PetsRepository : BaseSqlRepository, IPetsRepository
    {
        private readonly ILogger<PetsRepository> _logger;

        public PetsRepository(IDbConnection connection, ILogger<PetsRepository> logger) : base(connection)
        {
            _logger = logger;
        }

        public async Task<Pet?> GetPetByNameAsync(string PetName, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[PetsRepository][GetPetByNameAsync] Efetuando a consulta no repositorio.");

            try
            {
                var result = await base.QueryFirstOrDefault<Pet>("SELECT_PET", new { PetName });
                _logger.LogInformation("[PetsRepository][GetPetByNameAsync] Consulta efetuada com sucesso!");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[PetsRepository][GetPetByNameAsync] Erro ao efetuar a consulta");
            }

            return default;
        }
    }
}

