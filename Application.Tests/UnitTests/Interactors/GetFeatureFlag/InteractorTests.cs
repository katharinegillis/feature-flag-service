using Application.Interactors.GetFeatureFlag;
using Domain.FeatureFlags;
using Moq;

namespace Application.Tests.UnitTests.Interactors.GetFeatureFlag;

public class InteractorTests
{
    [Test]
    public void GetFeatureFlagInteractor_Is_An_InputPort()
    {
        var featureFlagRepository = Mock.Of<IFeatureFlagRepository>();

        var interactor = new Interactor(featureFlagRepository);

        Assert.That(interactor, Is.InstanceOf<IInputPort>());
    }

    [Test]
    public async Task GetFeatureFlagInteractor_Returns_A_Feature_Flag()
    {
        var featureFlag = new FeatureFlag
        {
            Id = "some_flag",
            Enabled = true
        };

        var featureFlagRepositoryMock = new Mock<IFeatureFlagRepository>();
        featureFlagRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>()).Result).Returns(featureFlag);

        var interactor = new Interactor(featureFlagRepositoryMock.Object);

        var request = new RequestModel
        {
            Id = "some_flag"
        };
        var presenterMock = new Mock<IOutputPort>();

        await interactor.Execute(request, presenterMock.Object);

        featureFlagRepositoryMock.Verify(repository => repository.Get("some_flag"));

        presenterMock.Verify(presenter => presenter.Ok(featureFlag));
    }

    [Test]
    public async Task GetFeatureFlagInteractor_Returns_NotFound()
    {
        var featureFlagRepositoryMock = new Mock<IFeatureFlagRepository>();
        featureFlagRepositoryMock.Setup(repository => repository.Get(It.IsAny<string>()).Result)
            .Returns(new FeatureFlagNull());

        var interactor = new Interactor(featureFlagRepositoryMock.Object);

        var request = new RequestModel
        {
            Id = "some_flag"
        };
        var presenterMock = new Mock<IOutputPort>();

        await interactor.Execute(request, presenterMock.Object);

        featureFlagRepositoryMock.Verify(repository => repository.Get("some_flag"));

        presenterMock.Verify(presenter => presenter.NotFound());
    }
}