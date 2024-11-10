using Application.Interactors.FeatureFlag.Delete;
using Domain.Common;
using Domain.FeatureFlags;
using NSubstitute;

namespace Application.Tests.Unit.Interactors.FeatureFlag.Delete;

public sealed class InteractorTests
{
    [Test]
    public void DeleteFeatureFlagInteractor_Is_An_InputPort()
    {
        var repository = Substitute.For<IRepository>();

        var interactor = new Interactor(repository);

        Assert.That(interactor, Is.InstanceOf<IInputPort>());
    }

    [Test]
    public async Task DeleteFeatureFlagInteractor_Deletes_A_Feature_Flag()
    {
        var repository = Substitute.For<IRepository>();
        repository.Delete("some_flag").Returns(Result<bool, Error>.Ok(true));

        var interactor = new Interactor(repository);

        var request = new RequestModel
        {
            Id = "some_flag"
        };

        var presenter = Substitute.For<IOutputPort>();

        await interactor.Execute(request, presenter);

        presenter.Received().Ok();
    }

    [Test]
    public async Task DeleteFeatureFlagInteractor_Should_Return_NotFound_If_Flag_Not_Found()
    {
        var error = new NotFoundError();

        var repository = Substitute.For<IRepository>();
        repository.Delete("some_flag").Returns(Result<bool, Error>.Err(error));

        var interactor = new Interactor(repository);

        var request = new RequestModel
        {
            Id = "some_flag"
        };

        var presenter = Substitute.For<IOutputPort>();

        await interactor.Execute(request, presenter);

        presenter.Received().NotFound();
    }

    [Test]
    public async Task DeleteFeatureFlagInteractor_Should_Return_Error_If_Repository_Errors()
    {
        var error = new Error
        {
            Message = "Unknown error"
        };

        var repository = Substitute.For<IRepository>();
        repository.Delete("some_flag").Returns(Result<bool, Error>.Err(error));

        var interactor = new Interactor(repository);

        var request = new RequestModel
        {
            Id = "some_flag"
        };

        var presenter = Substitute.For<IOutputPort>();

        await interactor.Execute(request, presenter);

        presenter.Received().Error(error);
    }
}