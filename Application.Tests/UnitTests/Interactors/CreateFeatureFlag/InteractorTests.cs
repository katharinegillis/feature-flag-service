using Domain.FeatureFlags;
using Application.Interactors.CreateFeatureFlag;
using Domain.Common;
using Moq;

namespace Application.Tests.UnitTests.Interactors.CreateFeatureFlag;

public class InteractorTests
{
    [Test]
    public void CreateFeatureFlagInteractor_Is_An_InputPort()
    {
        var featureFlagRepository = Mock.Of<IFeatureFlagRepository>();

        var featureFlagFactory = Mock.Of<IFactory>();

        var interactor = new Interactor(featureFlagRepository, featureFlagFactory);

        Assert.That(interactor, Is.InstanceOf<IInputPort>());
    }

    [Test]
    public async Task CreateFeatureFlagInteractor_Creates_A_Feature_Flag()
    {
        // TODO: Add create method to feature flag repository
        IFeatureFlag? passedFeatureFlag = null;
        var featureFlagRepositoryMock = new Mock<IFeatureFlagRepository>();
        featureFlagRepositoryMock.Setup(repository => repository.Create(It.IsAny<IFeatureFlag>()).Result)
            .Callback<IFeatureFlag>(featureFlag => passedFeatureFlag = featureFlag)
            .Returns(Result<string, Error>.Ok("new_flag"));

        var featureFlagMock = new Mock<IFeatureFlag>();
        featureFlagMock.Setup(model => model.Id).Returns("new_flag");
        featureFlagMock.Setup(model => model.Enabled).Returns(true);
        featureFlagMock.Setup(model => model.Validate()).Returns(Result<bool, IEnumerable<ValidationError>>.Ok(true));

        var featureFlagFactoryMock = new Mock<IFactory>();
        featureFlagFactoryMock.Setup(factory => factory.Create("new_flag", true)).Returns(featureFlagMock.Object);

        var interactor = new Interactor(featureFlagRepositoryMock.Object, featureFlagFactoryMock.Object);

        var request = new RequestModel
        {
            Id = "new_flag",
            Enabled = true
        };
        var presenterMock = new Mock<IOutputPort>();

        await interactor.Execute(request, presenterMock.Object);

        var equalityHelper = new FeatureFlagEqualityComparer();

        Assert.That(equalityHelper.Equals(new FeatureFlag
        {
            Id = "new_flag",
            Enabled = true
        }, passedFeatureFlag));

        featureFlagRepositoryMock.Verify(repository => repository.Create(It.IsAny<IFeatureFlag>()));

        presenterMock.Verify(presenter => presenter.Ok("new_flag"));
    }

    [Test]
    public async Task CreateFeatureFlagInteractor_Should_Return_Validation_Error_If_Id_Is_Too_Long()
    {
        var featureFlagRepository = Mock.Of<IFeatureFlagRepository>();

        // TODO rename domain to model, modelnull, etc.
        var validationErrors = new ValidationError[]
        {
            new()
            {
                Field = "Id",
                Message = "Max length is 100"
            }
        };

        var featureFlagMock = new Mock<IFeatureFlag>();
        featureFlagMock.Setup(model => model.Id)
            .Returns(
                "abcdefghijklmnopqrstuvwxyz1234abcdefghijklmnopqrstuvwxyz1234abcdefghijklmnopqrstuvwxyz1234abcdefghijk");
        featureFlagMock.Setup(model => model.Enabled).Returns(true);
        featureFlagMock.Setup(model => model.Validate())
            .Returns(Result<bool, IEnumerable<ValidationError>>.Err(validationErrors));

        var featureFlagFactory = new Mock<IFactory>();
        featureFlagFactory
            .Setup(factory =>
                factory.Create(
                    "abcdefghijklmnopqrstuvwxyz1234abcdefghijklmnopqrstuvwxyz1234abcdefghijklmnopqrstuvwxyz1234abcdefghijk",
                    true)).Returns(featureFlagMock.Object);

        var interactor = new Interactor(featureFlagRepository, featureFlagFactory.Object);

        var request = new RequestModel
        {
            Id =
                "abcdefghijklmnopqrstuvwxyz1234abcdefghijklmnopqrstuvwxyz1234abcdefghijklmnopqrstuvwxyz1234abcdefghijk",
            Enabled = true
        };
        var presenterMock = new Mock<IOutputPort>();

        await interactor.Execute(request, presenterMock.Object);

        presenterMock.Verify(presenter => presenter.BadRequest(validationErrors));
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

        // TODO Change messages over to localization system or constants
        var featureFlagRepositoryMock = new Mock<IFeatureFlagRepository>();
        featureFlagRepositoryMock.Setup(repository => repository.Create(It.IsAny<IFeatureFlag>()).Result).Returns(
            Result<string, Error>.Err(validationError));

        var featureFlagMock = new Mock<IFeatureFlag>();
        featureFlagMock.Setup(model => model.Id).Returns("new_flag");
        featureFlagMock.Setup(model => model.Enabled).Returns(true);
        featureFlagMock.Setup(model => model.Validate()).Returns(Result<bool, IEnumerable<ValidationError>>.Ok(true));

        var featureFlagFactoryMock = new Mock<IFactory>();
        featureFlagFactoryMock.Setup(factory => factory.Create("new_flag", true)).Returns(featureFlagMock.Object);

        var interactor = new Interactor(featureFlagRepositoryMock.Object, featureFlagFactoryMock.Object);

        // TODO make request models records
        var request = new RequestModel
        {
            Id = "new_flag",
            Enabled = true
        };
        var presenterMock = new Mock<IOutputPort>();

        await interactor.Execute(request, presenterMock.Object);

        var validationErrors = new List<ValidationError> { validationError };
        presenterMock.Verify(presenter => presenter.BadRequest(validationErrors));
    }

    [Test]
    public async Task CreateFeatureFlagInteractor_Should_Return_Error_If_Repository_Errors()
    {
        var error = new Error
        {
            Message = "Unknown error"
        };

        var featureFlagRepositoryMock = new Mock<IFeatureFlagRepository>();
        featureFlagRepositoryMock.Setup(repository => repository.Create(It.IsAny<IFeatureFlag>()).Result)
            .Returns(Result<string, Error>.Err(error));

        var featureFlagMock = new Mock<IFeatureFlag>();
        featureFlagMock.Setup(model => model.Id).Returns("new_flag");
        featureFlagMock.Setup(model => model.Enabled).Returns(true);
        featureFlagMock.Setup(model => model.Validate()).Returns(Result<bool, IEnumerable<ValidationError>>.Ok(true));

        var featureFlagFactoryMock = new Mock<IFactory>();
        featureFlagFactoryMock.Setup(factory => factory.Create("new_flag", true)).Returns(featureFlagMock.Object);

        var interactor = new Interactor(featureFlagRepositoryMock.Object, featureFlagFactoryMock.Object);

        var request = new RequestModel
        {
            Id = "new_flag",
            Enabled = true
        };

        var presenterMock = new Mock<IOutputPort>();

        await interactor.Execute(request, presenterMock.Object);

        presenterMock.Verify(presenter => presenter.Error(error));
    }
}