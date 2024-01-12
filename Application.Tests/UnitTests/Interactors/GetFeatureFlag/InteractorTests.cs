using Application.Interactors.GetFeatureFlag;
using Domain.FeatureFlags;
using Moq;

namespace Application.Tests.UnitTests.Interactors.GetFeatureFlag;

public sealed class InteractorTests
{
    [Test]
    public void GetFeatureFlagInteractor_Is_An_InputPort()
    {
        var repository = Mock.Of<IRepository>();

        var interactor = new Interactor(repository);

        Assert.That(interactor, Is.InstanceOf<IInputPort>());
    }

    [Test]
    public async Task GetFeatureFlagInteractor_Returns_A_Feature_Flag()
    {
        var model = new Model
        {
            Id = "some_flag",
            Enabled = true
        };

        var repositoryMock = new Mock<IRepository>();
        repositoryMock.Setup(r => r.Get(It.IsAny<string>()).Result).Returns(model);

        var interactor = new Interactor(repositoryMock.Object);

        var request = new RequestModel
        {
            Id = "some_flag"
        };
        var presenterMock = new Mock<IOutputPort>();
        presenterMock.SetupProperty(p => p.Request);

        await interactor.Execute(request, presenterMock.Object);

        repositoryMock.Verify(r => r.Get("some_flag"));

        presenterMock.Verify(p => p.Ok(model));

        Assert.That(presenterMock.Object.Request, Is.EqualTo(request));
    }

    [Test]
    public async Task GetFeatureFlagInteractor_Returns_NotFound()
    {
        var repositoryMock = new Mock<IRepository>();
        repositoryMock.Setup(r => r.Get(It.IsAny<string>()).Result)
            .Returns(new NullModel());

        var interactor = new Interactor(repositoryMock.Object);

        var request = new RequestModel
        {
            Id = "some_flag"
        };
        var presenterMock = new Mock<IOutputPort>();

        await interactor.Execute(request, presenterMock.Object);

        repositoryMock.Verify(r => r.Get("some_flag"));

        presenterMock.Verify(p => p.NotFound());
    }
}