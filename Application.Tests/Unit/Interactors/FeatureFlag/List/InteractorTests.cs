using Application.Interactors.FeatureFlag.List;
using Domain.FeatureFlags;
using NSubstitute;

namespace Application.Tests.Unit.Interactors.FeatureFlag.List;

public sealed class InteractorTests
{
    [Test]
    public void ListFeatureFlagsInteractor_Is_An_InputPort()
    {
        var repository = Substitute.For<IReadRepository>();

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

        var repository = Substitute.For<IReadRepository>();
        repository.List().Returns(featureFlags);

        var interactor = new Interactor(repository);

        var presenter = Substitute.For<IOutputPort>();

        await interactor.Execute(presenter);

        await repository.Received().List();

        presenter.Received().Ok(featureFlags);
    }
}