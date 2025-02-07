using Application.Interactors.FeatureFlag.Update;
using Domain.Common;
using Domain.FeatureFlags;
using NSubstitute;

namespace Application.Tests.Unit.Interactors.FeatureFlag.Update;

[Category("Unit")]
public sealed class InteractorTests
{
    [Test]
    public void UpdateFeatureFlagInteractor_Is_An_InputPort()
    {
        var repository = Substitute.For<IRepository>();
        var factory = Substitute.For<IFactory>();

        var interactor = new Interactor(repository, factory);

        Assert.That(interactor, Is.InstanceOf<IInputPort>());
    }

    [Test]
    public async Task UpdateFeatureFlagInteractor_Updates_A_Feature_Flag()
    {
        var model = Substitute.For<IModel>();
        model.Id.Returns("some_flag");
        model.Enabled.Returns(false);
        model.Validate().Returns(Result<bool, IEnumerable<ValidationError>>.Ok(true));

        var repository = Substitute.For<IRepository>();
        repository.Update(model).Returns(Result<bool, Error>.Ok(true));

        var factory = Substitute.For<IFactory>();
        factory.Create("some_flag", false).Returns(model);

        var interactor = new Interactor(repository, factory);

        var request = new RequestModel
        {
            Id = "some_flag",
            Enabled = false
        };

        var presenter = Substitute.For<IOutputPort>();

        await interactor.Execute(request, presenter);

        await repository.Received().Update(model);
        presenter.Received().Ok();
    }

    [Test]
    public async Task UpdateFeatureFlagInteractor_Should_Return_BadRequest_If_Validation_Error_Occurs()
    {
        var repository = Substitute.For<IRepository>();

        var validationErrors = new ValidationError[]
        {
            new()
            {
                Field = "Id",
                Message = "Some validation error"
            }
        };

        var model = Substitute.For<IModel>();
        model.Id.Returns("some_flag");
        model.Enabled.Returns(false);
        model.Validate().Returns(Result<bool, IEnumerable<ValidationError>>.Err(validationErrors));

        var factory = Substitute.For<IFactory>();
        factory.Create("some_flag", false).Returns(model);

        var interactor = new Interactor(repository, factory);

        var request = new RequestModel
        {
            Id = "some_flag",
            Enabled = false
        };

        var presenter = Substitute.For<IOutputPort>();

        await interactor.Execute(request, presenter);

        presenter.Received().BadRequest(validationErrors);
    }

    [Test]
    public async Task UpdateFeatureFlagInteractor_Should_Call_Error_If_Repository_Errors()
    {
        var error = new Error
        {
            Message = "Unknown error"
        };

        var model = Substitute.For<IModel>();
        model.Id.Returns("some_flag");
        model.Enabled.Returns(false);
        model.Validate().Returns(Result<bool, IEnumerable<ValidationError>>.Ok(true));

        var repository = Substitute.For<IRepository>();
        repository.Update(model).Returns(Result<bool, Error>.Err(error));

        var factory = Substitute.For<IFactory>();
        factory.Create("some_flag", false).Returns(model);

        var interactor = new Interactor(repository, factory);

        var request = new RequestModel
        {
            Id = "some_flag",
            Enabled = false
        };

        var presenter = Substitute.For<IOutputPort>();

        await interactor.Execute(request, presenter);

        presenter.Received().Error(error);
    }

    [Test]
    public async Task UpdateFeatureFlagInteractor_Should_Call_NotFound_If_Flag_Not_Found()
    {
        var error = new NotFoundError();

        var model = Substitute.For<IModel>();
        model.Id.Returns("some_flag");
        model.Enabled.Returns(false);
        model.Validate().Returns(Result<bool, IEnumerable<ValidationError>>.Ok(true));

        var repository = Substitute.For<IRepository>();
        repository.Update(model).Returns(Result<bool, Error>.Err(error));

        var factory = Substitute.For<IFactory>();
        factory.Create("some_flag", false).Returns(model);

        var interactor = new Interactor(repository, factory);

        var request = new RequestModel
        {
            Id = "some_flag",
            Enabled = false
        };

        var presenter = Substitute.For<IOutputPort>();

        await interactor.Execute(request, presenter);

        presenter.Received().NotFound();
    }
}