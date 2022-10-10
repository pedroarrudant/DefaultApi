using System;
using Application.Shared.Models;
using MassTransit;

namespace Application.Shared.Services
{
    public class PublishService : IPublisherService
    {
        public async Task Publish(Pet pet, IPublishEndpoint publishEndpoint)
        {
            await publishEndpoint.Publish(pet);
        }
    }
}

