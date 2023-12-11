using Application.Interactors.GetFeatureFlag;
using Domain.FeatureFlag;
using Moq;

namespace Application.UnitTests.Interactors.GetFeatureFlag;

public class InteractorTests
{
    [Test]
    public void GetFeatureFlagInterceptor_Is_A_InputPort()
    {
        var featureFlagRepository = Mock.Of<IFeatureFlagRepository>();

        var interactor = new Interactor(featureFlagRepository);

        Assert.That(interactor, Is.InstanceOf<IInputPort>());
    }

    [Test]
    public async Task GetFeatureFlagInterceptor_Returns_A_Feature_Flag()
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

        presenterMock.Verify(presenter => presenter.Ok(featureFlag));
    }

    [Test]
    public async Task GetFeatureFlagInterceptor_Returns_NotFound()
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

        presenterMock.Verify(presenter => presenter.NotFound());
    }
}