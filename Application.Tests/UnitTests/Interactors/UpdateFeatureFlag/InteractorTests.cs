using Application.Interactors.UpdateFeatureFlag;
using Domain.Common;
using Domain.FeatureFlags;
using Moq;

namespace Application.Tests.UnitTests.Interactors.UpdateFeatureFlag;

public sealed class InteractorTests
{
    [Test]
    public void UpdateFeatureFlagInteractor_Is_An_InputPort()
    {
        var repository = Mock.Of<IRepository>();

        var factory = Mock.Of<IFactory>();

        var interactor = new Interactor(repository, factory);

        Assert.That(interactor, Is.InstanceOf<IInputPort>());
    }

    [Test]
    public async Task UpdateFeatureFlagInteractor_Updates_A_Feature_Flag()
    {
        IModel? passedModel = null;
        var repositoryMock = new Mock<IRepository>();
        repositoryMock.Setup(r => r.Update(It.IsAny<IModel>()).Result).Callback<IModel>(m => passedModel = m)
            .Returns(Result<bool, Error>.Ok(true));

        var modelMock = new Mock<IModel>();
        modelMock.Setup(m => m.Id).Returns("some_flag");
        modelMock.Setup(m => m.Enabled).Returns(false);
        modelMock.Setup(m => m.Validate()).Returns(Result<bool, IEnumerable<ValidationError>>.Ok(true));

        var factoryMock = new Mock<IFactory>();
        factoryMock.Setup(f => f.Create("some_flag", false)).Returns(modelMock.Object);

        var interactor = new Interactor(repositoryMock.Object, factoryMock.Object);

        var request = new RequestModel
        {
            Id = "some_flag",
            Enabled = false
        };

        var presenterMock = new Mock<IOutputPort>();

        await interactor.Execute(request, presenterMock.Object);

        var equalityComparer = new EqualityComparer();

        Assert.That(equalityComparer.Equals(new Model
        {
            Id = "some_flag",
            Enabled = false
        }, passedModel));

        repositoryMock.Verify(r => r.Update(It.IsAny<IModel>()));

        presenterMock.Verify(p => p.Ok());
    }

    [Test]
    public async Task UpdateFeatureFlagInteractor_Should_Return_BadRequest_If_Validation_Error_Occurs()
    {
        var repository = Mock.Of<IRepository>();

        var validationErrors = new ValidationError[]
        {
            new()
            {
                Field = "Id",
                Message = "Some validation error"
            }
        };

        var modelMock = new Mock<IModel>();
        modelMock.Setup(m => m.Id).Returns("some_flag");
        modelMock.Setup(m => m.Enabled).Returns(false);
        modelMock.Setup(m => m.Validate()).Returns(Result<bool, IEnumerable<ValidationError>>.Err(validationErrors));

        var factoryMock = new Mock<IFactory>();
        factoryMock.Setup(f => f.Create("some_flag", false)).Returns(modelMock.Object);

        var interactor = new Interactor(repository, factoryMock.Object);

        var request = new RequestModel
        {
            Id = "some_flag",
            Enabled = false
        };
        var presenterMock = new Mock<IOutputPort>();

        await interactor.Execute(request, presenterMock.Object);

        presenterMock.Verify(p => p.BadRequest(validationErrors));
    }

    [Test]
    public async Task UpdateFeatureFlagInteractor_Should_Call_Error_If_Repository_Errors()
    {
        var error = new Error
        {
            Message = "Unknown error"
        };

        var repositoryMock = new Mock<IRepository>();
        repositoryMock.Setup(r => r.Update(It.IsAny<IModel>()).Result).Returns(Result<bool, Error>.Err(error));

        var modelMock = new Mock<IModel>();
        modelMock.Setup(m => m.Id).Returns("some_flag");
        modelMock.Setup(m => m.Enabled).Returns(false);
        modelMock.Setup(m => m.Validate()).Returns(Result<bool, IEnumerable<ValidationError>>.Ok(true));

        var factoryMock = new Mock<IFactory>();
        factoryMock.Setup(f => f.Create("some_flag", false)).Returns(modelMock.Object);

        var interactor = new Interactor(repositoryMock.Object, factoryMock.Object);

        var request = new RequestModel
        {
            Id = "some_flag",
            Enabled = false
        };

        var presenterMock = new Mock<IOutputPort>();

        await interactor.Execute(request, presenterMock.Object);

        presenterMock.Verify(p => p.Error(error));
    }
}