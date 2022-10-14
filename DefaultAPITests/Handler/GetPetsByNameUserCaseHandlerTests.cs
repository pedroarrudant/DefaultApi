using Application.Features.GetPetsByName.Models;
using Application.Features.GetPetsByName.UseCase;
using Application.Shared.Repositories;
using DefaultAPITests.Static;
using Moq;

namespace DefaultAPITests.Handler;

public class GetPetsByNameUseCaseHandlerTests
{
    private readonly Mock<IPetsRepository> _repository;

    public GetPetsByNameUseCaseHandlerTests()
    {
        _repository = new Mock<IPetsRepository>();
    }

    [Fact]
    public async void GetPetsByNameShouldPass()
    {
        //Arrange
        _repository.Setup(m => m.GetPetByNameAsync(It.IsAny<string>(), new CancellationToken())).ReturnsAsync(PetStatic.Create());
        var input = new GetPetsByNameInput() { Name = "Teste" }; 

        //Act
        var handler = new GetPetsByNameUseCaseHandler(_repository.Object);
        var result = await handler.Handle(input, new CancellationToken());

        //Assert
        Assert.NotNull(result.Pet);
    }
}
