using System;
using Application.Features.InsertPet.Models;
using Application.Shared.Models;
using MediatR;

namespace Application.Features.InsertPet.Models
{
    public class InsertPetInput : IRequest<InsertPetOutput>
    {
        public Pet? Pet { get; set; }
    }
}

