using System;

namespace Application.Shared.Models
{
    public class Pet
    {
        public string? Name { get; set; }
        public int Age { get; set; }

        public bool IsValid() => !(Age <= 0);
    }
}

