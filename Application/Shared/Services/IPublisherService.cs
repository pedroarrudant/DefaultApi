using System;
using Application.Shared.Models;
using MassTransit;
using MassTransit.Transports;

namespace Application.Shared.Services
{
    public interface IPublisherService
    {

        Task Publish(Pet pet, IPublishEndpoint publishEndpoint);
    }
}

