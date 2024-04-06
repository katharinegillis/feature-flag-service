using Application.Interactors.IsFeatureFlagEnabled;
using IGetFeatureFlagInputPort = Application.Interactors.GetFeatureFlag.IInputPort;
using IGetFeatureFlagCodePresenter = Application.Interactors.GetFeatureFlag.ICodePresenter;
using IGetFeatureFlagCodePresenterFactory = Application.Interactors.GetFeatureFlag.ICodePresenterFactory;
using Domain.FeatureFlags;
using NSubstitute;

namespace Application.Tests.Unit.Interactors.IsFeatureFlagEnabled;

[Category("Unit")]
public sealed class InteractorTests
{
    [Test]
    public void IsFeatureFlagEnabledInteractor_An_InputPort()
    {
        var getInteractor = Substitute.For<IGetFeatureFlagInputPort>();
        var getPresenterFactory = Substitute.For<IGetFeatureFlagCodePresenterFactory>();

        var interactor = new Interactor(getPresenterFactory, getInteractor);

        Assert.That(interactor, Is.InstanceOf<IInputPort>());
    }

    [Test]
    public async Task IsFeatureFlagEnabledInteractor_Should_Return_True_If_Flag_Is_Enabled()
    {
        var getPresenter = Substitute.For<IGetFeatureFlagCodePresenter>();
        getPresenter.IsNotFound.Returns(false);
        getPresenter.FeatureFlag.Returns(new Model
        {
            Id = "some_flag",
            Enabled = true
        });

        var getInteractor = Substitute.For<IGetFeatureFlagInputPort>();

        var getPresenterFactory = Substitute.For<IGetFeatureFlagCodePresenterFactory>();
        getPresenterFactory.Create().Returns(getPresenter);

        var interactor = new Interactor(getPresenterFactory, getInteractor);

        var request = new RequestModel
        {
            Id = "some_flag"
        };

        var presenter = Substitute.For<IOutputPort>();

        await interactor.Execute(request, presenter);

        presenter.Received().Ok(true);
    }

    [Test]
    public async Task IsFeatureFlagEnabledInteractor_Should_Return_False_If_Flag_Is_Not_Enabled()
    {
        var getInteractor = Substitute.For<IGetFeatureFlagInputPort>();

        var getPresenter = Substitute.For<IGetFeatureFlagCodePresenter>();
        getPresenter.IsNotFound.Returns(false);
        getPresenter.FeatureFlag.Returns(new Model
        {
            Id = "some_flag",
            Enabled = false
        });

        var getPresenterFactory = Substitute.For<IGetFeatureFlagCodePresenterFactory>();
        getPresenterFactory.Create().Returns(getPresenter);

        var interactor = new Interactor(getPresenterFactory, getInteractor);

        var request = new RequestModel
        {
            Id = "some_flag"
        };

        var presenter = Substitute.For<IOutputPort>();

        await interactor.Execute(request, presenter);

        presenter.Received().Ok(false);
    }

    [Test]
    public async Task IsFeatureFlagEnabledInteractor_Should_Return_NotFound_If_Flag_Not_Found()
    {
        var getInteractor = Substitute.For<IGetFeatureFlagInputPort>();

        var getPresenter = Substitute.For<IGetFeatureFlagCodePresenter>();
        getPresenter.IsNotFound.Returns(true);
        getPresenter.FeatureFlag.Returns(NullModel.Instance);

        var getPresenterFactory = Substitute.For<IGetFeatureFlagCodePresenterFactory>();
        getPresenterFactory.Create().Returns(getPresenter);

        var interactor = new Interactor(getPresenterFactory, getInteractor);

        var request = new RequestModel
        {
            Id = "not_found_flag"
        };

        var presenter = Substitute.For<IOutputPort>();

        await interactor.Execute(request, presenter);

        presenter.Received().NotFound();
    }
}