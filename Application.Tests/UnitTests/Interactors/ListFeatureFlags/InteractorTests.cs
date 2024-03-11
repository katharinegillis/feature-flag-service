using Application.Interactors.ListFeatureFlags;
using Domain.FeatureFlags;
using Moq;

namespace Application.Tests.UnitTests.Interactors.ListFeatureFlags;

[Category("Unit")]
public sealed class InteractorTests
{
    [Test]
    public void ListFeatureFlagsInteractor_Is_An_InputPort()
    {
        var repository = Mock.Of<IRepository>();

        var interactor = new Interactor(repository);

        Assert.That(interactor, Is.InstanceOf<IInputPort>());
    }

    [Test]
    public async Task ListFeatureFlagsInteractor_Returns_A_List_Of_Feature_Flags()
    {
        var featureFlags = new List<IModel>
        {
            new Model
            {
                Id = "some_flag",
                Enabled = true
            },
            new Model
            {
                Id = "another_flag",
                Enabled = false
            }
        };

        var repositoryMock = new Mock<IRepository>();
        repositoryMock.Setup(r => r.List().Result).Returns(featureFlags);

        var interactor = new Interactor(repositoryMock.Object);

        var presenterMock = new Mock<IOutputPort>();

        await interactor.Execute(presenterMock.Object);

        repositoryMock.Verify(r => r.List());

        presenterMock.Verify(p => p.Ok(featureFlags));
    }
}