using System;
using Application.Shared.Models;
using MassTransit;
using MassTransit.Transports;

namespace Application.Shared.Services
{
    public interface IPublisherServices
    {

        async Task Publish(Pet pet, IPublishEndpoint publishEndpoint)
        {
            await publishEndpoint.Publish(pet);
        }
    }
}

