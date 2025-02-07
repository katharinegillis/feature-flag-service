using Application.Interactors.Config.Show;
using Domain.FeatureFlags;
using NSubstitute;

namespace Application.Tests.Unit.Interactors.Config.Show;

[Category("Unit")]
public sealed class InteractorTests
{
    [Test]
    public void ConfigShowInteractor_Is_An_InputPort()
    {
        var repository = Substitute.For<IReadRepository>();

        var interactor = new Interactor(repository);

        Assert.That(interactor, Is.InstanceOf<IInputPort>());
    }

    [Test]
    public async Task ConfigShowInteractor_Returns_A_Config_Value_For_Datasource()
    {
        var repository = Substitute.For<IReadRepository>();
        repository.Name.Returns("Some Repository");

        var interactor = new Interactor(repository);

        var request = new RequestModel
        {
            Name = RequestModel.NameOptions.Datasource
        };

        var presenter = Substitute.For<IOutputPort>();

        await interactor.Execute(request, presenter);

        presenter.Received().Ok("Some Repository");
    }
}