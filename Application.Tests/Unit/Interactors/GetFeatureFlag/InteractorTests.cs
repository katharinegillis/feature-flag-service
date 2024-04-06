using Application.Interactors.GetFeatureFlag;
using Domain.FeatureFlags;
using NSubstitute;

namespace Application.Tests.Unit.Interactors.GetFeatureFlag;

[Category("Unit")]
public sealed class InteractorTests
{
    [Test]
    public void GetFeatureFlagInteractor_Is_An_InputPort()
    {
        var repository = Substitute.For<IRepository>();

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

        var repository = Substitute.For<IRepository>();
        repository.Get("some_flag").Returns(model);

        var interactor = new Interactor(repository);

        var request = new RequestModel
        {
            Id = "some_flag"
        };

        var presenter = Substitute.For<IOutputPort>();

        await interactor.Execute(request, presenter);

        presenter.Received().Ok(model);
    }

    [Test]
    public async Task GetFeatureFlagInteractor_Returns_NotFound()
    {
        var repository = Substitute.For<IRepository>();
        repository.Get("some_flag").Returns(new NullModel());

        var interactor = new Interactor(repository);

        var request = new RequestModel
        {
            Id = "some_flag"
        };

        var presenter = Substitute.For<IOutputPort>();

        await interactor.Execute(request, presenter);

        presenter.Received().NotFound();
    }
}