using System;
using System.Net;
using Application.Features.GetPetsByName.Models;
using Application.Features.InsertPet.Models;
using Application.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DefaultAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetsController
    {
        private readonly ILogger<PetsController> _logger;
        private readonly IMediator _mediator;

        public PetsController(ILogger<PetsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{petName}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<Pet> GetPetsByName([FromRoute]string petName, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetPetsByNameInput() { Name = petName }, cancellationToken);

            return result?.Pet;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task InsertPet([FromBody] Pet pet, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new InsertPetInput() { Pet = pet }, cancellationToken);
        }
    }
}

