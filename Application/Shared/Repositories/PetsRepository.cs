using System;
using System.Data;
using Application.Features.GetPetsByName.Models;
using Application.Shared.Models;
using Application.Shared.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Shared.Repositories
{
    public class PetsRepository : BaseSqlRepository, IPetsRepository
    {
        private readonly ILogger<PetsRepository> _logger;

        public PetsRepository(IDbConnection connection, ILogger<PetsRepository> logger) : base(connection)
        {
            _logger = logger;
        }

        public async Task<Pet?> GetPetByNameAsync(string petName, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[PetsRepository][GetPetByNameAsync] Efetuando a consulta no repositorio.");

            try
            {
                var result = await base.QueryFirstOrDefault<Pet>("SELECT_PET", cancellationToken, new { petName });

                _logger.LogInformation("[PetsRepository][GetPetByNameAsync] Consulta efetuada com sucesso!");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[PetsRepository][GetPetByNameAsync] Erro ao efetuar a consulta");
            }

            return default;
        }

        public async Task<bool> InsertPetAsync(Pet pet, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[PetsRepository][InsertPetasync] Efetuando a consulta no repositorio.");

            try
            {
                var result = await base.ExecuteAsync("INSERT_PET", cancellationToken, new { Name = pet.Name, Age = pet.Age });

                _logger.LogInformation("[PetsRepository][InsertPetasync] Consulta efetuada com sucesso!");

                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[PetsRepository][InsertPetasync] Erro ao efetuar a consulta");
            }

            return false;
        }
    }
}

