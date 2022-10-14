using System;
using Application.Shared.Models;

namespace DefaultAPITests.Static
{
    public static class PetStatic
    {
        public static Pet Create() => new Pet() { Age = 10, Name = "Teste" };
    }
}

