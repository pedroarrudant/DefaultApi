using System;
using MediatR;

namespace Application.Features.GetPetsByName.Models
{
    public class GetPetsByNameInput : IRequest<GetPetsByNameOutput>
    {
        public string? Name { get; set; }
    }
}

