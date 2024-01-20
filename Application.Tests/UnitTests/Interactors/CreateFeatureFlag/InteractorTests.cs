using Domain.FeatureFlags;
using Application.Interactors.CreateFeatureFlag;
using Domain.Common;
using Moq;

namespace Application.Tests.UnitTests.Interactors.CreateFeatureFlag;

public sealed class InteractorTests
{
    [Test]
    public void CreateFeatureFlagInteractor_Is_An_InputPort()
    {
        var repository = Mock.Of<IRepository>();

        var factory = Mock.Of<IFactory>();

        var interactor = new Interactor(repository, factory);

        Assert.That(interactor, Is.InstanceOf<IInputPort>());
    }

    [Test]
    public async Task CreateFeatureFlagInteractor_Creates_A_Feature_Flag()
    {
        IModel? passedFeatureFlag = null;
        var repositoryMock = new Mock<IRepository>();
        repositoryMock.Setup(r => r.Create(It.IsAny<IModel>()).Result)
            .Callback<IModel>(featureFlag => passedFeatureFlag = featureFlag)
            .Returns(Result<string, Error>.Ok("new_flag"));

        var modelMock = new Mock<IModel>();
        modelMock.Setup(m => m.Id).Returns("new_flag");
        modelMock.Setup(m => m.Enabled).Returns(true);
        modelMock.Setup(m => m.Validate()).Returns(Result<bool, IEnumerable<ValidationError>>.Ok(true));

        var factoryMock = new Mock<IFactory>();
        factoryMock.Setup(f => f.Create("new_flag", true)).Returns(modelMock.Object);

        var interactor = new Interactor(repositoryMock.Object, factoryMock.Object);

        var request = new RequestModel
        {
            Id = "new_flag",
            Enabled = true
        };
        var presenterMock = new Mock<IOutputPort>();

        await interactor.Execute(request, presenterMock.Object);

        var equalityComparer = new EqualityComparer();

        Assert.Multiple(() =>
        {
            Assert.That(equalityComparer.Equals(new Model
            {
                Id = "new_flag",
                Enabled = true
            }, passedFeatureFlag));
        });

        repositoryMock.Verify(r => r.Create(It.IsAny<IModel>()));

        presenterMock.Verify(p => p.Ok());
    }

    [Test]
    public async Task CreateFeatureFlagInteractor_Should_Return_Validation_Error_If_Id_Is_Too_Long()
    {
        var repository = Mock.Of<IRepository>();

        var validationErrors = new ValidationError[]
        {
            new()
            {
                Field = "Id",
                Message = "Max length is 100"
            }
        };

        var modelMock = new Mock<IModel>();
        modelMock.Setup(m => m.Id)
            .Returns(
                "abcdefghijklmnopqrstuvwxyz1234abcdefghijklmnopqrstuvwxyz1234abcdefghijklmnopqrstuvwxyz1234abcdefghijk");
        modelMock.Setup(m => m.Enabled).Returns(true);
        modelMock.Setup(m => m.Validate())
            .Returns(Result<bool, IEnumerable<ValidationError>>.Err(validationErrors));

        var factoryMock = new Mock<IFactory>();
        factoryMock
            .Setup(f =>
                f.Create(
                    "abcdefghijklmnopqrstuvwxyz1234abcdefghijklmnopqrstuvwxyz1234abcdefghijklmnopqrstuvwxyz1234abcdefghijk",
                    true)).Returns(modelMock.Object);

        var interactor = new Interactor(repository, factoryMock.Object);

        var request = new RequestModel
        {
            Id =
                "abcdefghijklmnopqrstuvwxyz1234abcdefghijklmnopqrstuvwxyz1234abcdefghijklmnopqrstuvwxyz1234abcdefghijk",
            Enabled = true
        };
        var presenterMock = new Mock<IOutputPort>();

        await interactor.Execute(request, presenterMock.Object);

        presenterMock.Verify(p => p.BadRequest(validationErrors));
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

        var repositoryMock = new Mock<IRepository>();
        repositoryMock.Setup(r => r.Create(It.IsAny<IModel>()).Result).Returns(
            Result<string, Error>.Err(validationError));

        var modelMock = new Mock<IModel>();
        modelMock.Setup(model => model.Id).Returns("new_flag");
        modelMock.Setup(model => model.Enabled).Returns(true);
        modelMock.Setup(model => model.Validate()).Returns(Result<bool, IEnumerable<ValidationError>>.Ok(true));

        var factoryMock = new Mock<IFactory>();
        factoryMock.Setup(factory => factory.Create("new_flag", true)).Returns(modelMock.Object);

        var interactor = new Interactor(repositoryMock.Object, factoryMock.Object);

        var request = new RequestModel
        {
            Id = "new_flag",
            Enabled = true
        };
        var presenterMock = new Mock<IOutputPort>();

        await interactor.Execute(request, presenterMock.Object);

        var validationErrors = new List<ValidationError> { validationError };
        presenterMock.Verify(p => p.BadRequest(validationErrors));
    }

    [Test]
    public async Task CreateFeatureFlagInteractor_Should_Return_Error_If_Repository_Errors()
    {
        var error = new Error
        {
            Message = "Unknown error"
        };

        var repositoryMock = new Mock<IRepository>();
        repositoryMock.Setup(r => r.Create(It.IsAny<IModel>()).Result)
            .Returns(Result<string, Error>.Err(error));

        var modelMock = new Mock<IModel>();
        modelMock.Setup(model => model.Id).Returns("new_flag");
        modelMock.Setup(model => model.Enabled).Returns(true);
        modelMock.Setup(model => model.Validate()).Returns(Result<bool, IEnumerable<ValidationError>>.Ok(true));

        var factoryMock = new Mock<IFactory>();
        factoryMock.Setup(factory => factory.Create("new_flag", true)).Returns(modelMock.Object);

        var interactor = new Interactor(repositoryMock.Object, factoryMock.Object);

        var request = new RequestModel
        {
            Id = "new_flag",
            Enabled = true
        };

        var presenterMock = new Mock<IOutputPort>();

        await interactor.Execute(request, presenterMock.Object);

        presenterMock.Verify(p => p.Error(error));
    }
}