using Application.Shared.Models;
using MassTransit;

namespace Application.Entrypoint.Consumer
{
    public interface IInsertPetConsumer
    {
        Task Consume(ConsumeContext<Pet> context);
    }
}