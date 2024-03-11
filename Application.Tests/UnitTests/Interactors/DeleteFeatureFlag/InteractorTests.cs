using Application.Interactors.DeleteFeatureFlag;
using Domain.Common;
using Domain.FeatureFlags;
using Moq;

namespace Application.Tests.UnitTests.Interactors.DeleteFeatureFlag;

[Category("Unit")]
public sealed class InteractorTests
{
    [Test]
    public void DeleteFeatureFlagInteractor_Is_An_InputPort()
    {
        var repositoryMock = new Mock<IRepository>();

        var interactor = new Interactor(repositoryMock.Object);

        Assert.That(interactor, Is.InstanceOf<IInputPort>());
    }

    [Test]
    public async Task DeleteFeatureFlagInteractor_Deletes_A_Feature_Flag()
    {
        var repositoryMock = new Mock<IRepository>();
        repositoryMock.Setup(r => r.Delete("some_flag").Result).Returns(Result<bool, Error>.Ok(true));

        var interactor = new Interactor(repositoryMock.Object);

        var request = new RequestModel
        {
            Id = "some_flag"
        };

        var presenterMock = new Mock<IOutputPort>();

        await interactor.Execute(request, presenterMock.Object);

        repositoryMock.Verify(r => r.Delete("some_flag"));
        presenterMock.Verify(p => p.Ok());
    }

    [Test]
    public async Task DeleteFeatureFlagInteractor_Should_Return_NotFound_If_Flag_Not_Found()
    {
        var error = new NotFoundError();

        var repositoryMock = new Mock<IRepository>();
        repositoryMock.Setup(r => r.Delete("some_flag").Result).Returns(Result<bool, Error>.Err(error));

        var interactor = new Interactor(repositoryMock.Object);

        var request = new RequestModel
        {
            Id = "some_flag"
        };

        var presenterMock = new Mock<IOutputPort>();

        await interactor.Execute(request, presenterMock.Object);

        presenterMock.Verify(p => p.NotFound());
    }

    [Test]
    public async Task DeleteFeatureFlagInteractor_Should_Return_Error_If_Repository_Errors()
    {
        var error = new Error
        {
            Message = "Unknown error"
        };

        var repositoryMock = new Mock<IRepository>();
        repositoryMock.Setup(r => r.Delete("some_flag").Result).Returns(Result<bool, Error>.Err(error));

        var interactor = new Interactor(repositoryMock.Object);

        var request = new RequestModel
        {
            Id = "some_flag"
        };

        var presenterMock = new Mock<IOutputPort>();

        await interactor.Execute(request, presenterMock.Object);

        presenterMock.Verify(p => p.Error(error));
    }
}