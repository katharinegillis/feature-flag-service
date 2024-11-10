using Application.Interactors.FeatureFlag.Create;
using Domain.Common;
using Domain.FeatureFlags;
using NSubstitute;

namespace Application.Tests.Unit.Interactors.FeatureFlag.Create;

public sealed class InteractorTests
{
    [Test]
    public void CreateFeatureFlagInteractor_Is_An_InputPort()
    {
        var repository = Substitute.For<IRepository>();
        var factory = Substitute.For<IFactory>();

        var interactor = new Interactor(repository, factory);

        Assert.That(interactor, Is.InstanceOf<IInputPort>());
    }

    [Test]
    public async Task CreateFeatureFlagInteractor_Creates_A_Feature_Flag()
    {
        var model = Substitute.For<IModel>();
        model.Id.Returns("new_flag");
        model.Enabled.Returns(true);
        model.Validate().Returns(Result<bool, IEnumerable<ValidationError>>.Ok(true));

        var repository = Substitute.For<IRepository>();
        repository.Create(model).Returns(m => m.Arg<IModel>().Id);

        var factory = Substitute.For<IFactory>();
        factory.Create("new_flag", true).Returns(model);

        var interactor = new Interactor(repository, factory);

        var request = new RequestModel
        {
            Id = "new_flag",
            Enabled = true
        };

        var presenter = Substitute.For<IOutputPort>();

        await interactor.Execute(request, presenter);

        presenter.Received().Ok();
    }

    [Test]
    public async Task CreateFeatureFlagInteractor_Should_Return_Any_Validation_Errors()
    {
        var repository = Substitute.For<IRepository>();

        var validationErrors = new ValidationError[]
        {
            new()
            {
                Field = "Id",
                Message = "Some error"
            },
            new()
            {
                Field = "Id",
                Message = "Some other error"
            }
        };

        var model = Substitute.For<IModel>();
        model.Id.Returns("new_flag");
        model.Enabled.Returns(true);
        model.Validate().Returns(Result<bool, IEnumerable<ValidationError>>.Err(validationErrors));

        var factory = Substitute.For<IFactory>();
        factory.Create("new_flag", true).Returns(model);

        var interactor = new Interactor(repository, factory);

        var request = new RequestModel
        {
            Id =
                "new_flag",
            Enabled = true
        };
        var presenter = Substitute.For<IOutputPort>();

        await interactor.Execute(request, presenter);

        presenter.Received().BadRequest(validationErrors);
    }

    [Test]
    public async Task
        CreateFeatureFlagInteractor_Should_Return_Validation_Error_If_Id_Already_Used()
    {
        var validationError = new ValidationError
        {
            Field = "Id",
            Message = "Id already exists"
        };

        var model = Substitute.For<IModel>();
        model.Id.Returns("new_flag");
        model.Enabled.Returns(true);
        model.Validate().Returns(Result<bool, IEnumerable<ValidationError>>.Ok(true));

        var repository = Substitute.For<IRepository>();
        repository.Create(model).Returns(Result<string, Error>.Err(validationError));

        var factory = Substitute.For<IFactory>();
        factory.Create("new_flag", true).Returns(model);

        var interactor = new Interactor(repository, factory);

        var request = new RequestModel
        {
            Id = "new_flag",
            Enabled = true
        };

        var presenter = Substitute.For<IOutputPort>();

        await interactor.Execute(request, presenter);

        var validationErrors = new List<ValidationError> { validationError };
        presenter.Received().BadRequest(Arg.Is<List<ValidationError>>(x => x.SequenceEqual(validationErrors)));
    }

    [Test]
    public async Task CreateFeatureFlagInteractor_Should_Return_Error_If_Repository_Errors()
    {
        var error = new Error
        {
            Message = "Unknown error"
        };

        var model = Substitute.For<IModel>();
        model.Id.Returns("new_flag");
        model.Enabled.Returns(true);
        model.Validate().Returns(Result<bool, IEnumerable<ValidationError>>.Ok(true));

        var repository = Substitute.For<IRepository>();
        repository.Create(model).Returns(Result<string, Error>.Err(error));

        var factory = Substitute.For<IFactory>();
        factory.Create("new_flag", true).Returns(model);

        var interactor = new Interactor(repository, factory);

        var request = new RequestModel
        {
            Id = "new_flag",
            Enabled = true
        };

        var presenter = Substitute.For<IOutputPort>();

        await interactor.Execute(request, presenter);

        presenter.Received().Error(error);
    }
}