using Application.Interactors.IsFeatureFlagEnabled;
using IGetFeatureFlagInputPort = Application.Interactors.GetFeatureFlag.IInputPort;
using IGetFeatureFlagOutputPort = Application.Interactors.GetFeatureFlag.IOutputPort;
using IGetFeatureFlagCodePresenter = Application.Interactors.GetFeatureFlag.ICodePresenter;
using IGetFeatureFlagCodePresenterFactory = Application.Interactors.GetFeatureFlag.ICodePresenterFactory;
using GetFeatureFlagRequestModel = Application.Interactors.GetFeatureFlag.RequestModel;
using Domain.FeatureFlags;
using Moq;

namespace Application.Tests.UnitTests.Interactors.IsFeatureFlagEnabled;

public sealed class InteractorTests
{
    [Test]
    public void IsFeatureFlagEnabledInteractor_An_InputPort()
    {
        var getInteractor = Mock.Of<IGetFeatureFlagInputPort>();
        var getPresenterFactory = Mock.Of<IGetFeatureFlagCodePresenterFactory>();

        var interactor = new Interactor(getPresenterFactory, getInteractor);

        Assert.That(interactor, Is.InstanceOf<IInputPort>());
    }

    [Test]
    public async Task IsFeatureFlagEnabledInteractor_Should_Return_True_If_Flag_Is_Enabled()
    {
        var getInteractor = new Mock<IGetFeatureFlagInputPort>();
        getInteractor.Setup(i =>
            i.Execute(It.IsAny<GetFeatureFlagRequestModel>(), It.IsAny<IGetFeatureFlagOutputPort>()));

        var getPresenter = new Mock<IGetFeatureFlagCodePresenter>();
        getPresenter.Setup(p => p.IsNotFound).Returns(false);
        getPresenter.Setup(p => p.FeatureFlag)
            .Returns(new Model { Id = "some_flag", Enabled = true });

        var getPresenterFactory = new Mock<IGetFeatureFlagCodePresenterFactory>();
        getPresenterFactory.Setup(f => f.Create()).Returns(getPresenter.Object);

        var interactor = new Interactor(getPresenterFactory.Object, getInteractor.Object);

        var request = new RequestModel
        {
            Id = "some_flag"
        };
        var presenterMock = new Mock<IOutputPort>();

        await interactor.Execute(request, presenterMock.Object);

        presenterMock.Verify(p => p.Ok(true));
    }

    [Test]
    public async Task IsFeatureFlagEnabledInteractor_Should_Return_False_If_Flag_Is_Not_Enabled()
    {
        var getInteractor = new Mock<IGetFeatureFlagInputPort>();
        getInteractor.Setup(i =>
            i.Execute(It.IsAny<GetFeatureFlagRequestModel>(), It.IsAny<IGetFeatureFlagOutputPort>()));

        var getPresenter = new Mock<IGetFeatureFlagCodePresenter>();
        getPresenter.Setup(p => p.IsNotFound).Returns(false);
        getPresenter.Setup(p => p.FeatureFlag)
            .Returns(new Model { Id = "some_flag", Enabled = false });

        var getPresenterFactory = new Mock<IGetFeatureFlagCodePresenterFactory>();
        getPresenterFactory.Setup(f => f.Create()).Returns(getPresenter.Object);

        var interactor = new Interactor(getPresenterFactory.Object, getInteractor.Object);

        var request = new RequestModel
        {
            Id = "some_flag"
        };
        var presenterMock = new Mock<IOutputPort>();

        await interactor.Execute(request, presenterMock.Object);

        presenterMock.Verify(presenter => presenter.Ok(false));
    }

    [Test]
    public async Task IsFeatureFlagEnabledInteractor_Should_Return_NotFound_If_Flag_Not_Found()
    {
        var getInteractor = new Mock<IGetFeatureFlagInputPort>();
        getInteractor.Setup(i =>
                i.Execute(It.IsAny<GetFeatureFlagRequestModel>(), It.IsAny<IGetFeatureFlagOutputPort>()))
            // ReSharper disable once UnusedParameter.Local
            .Callback((GetFeatureFlagRequestModel r,
                    IGetFeatureFlagOutputPort p) =>
                p.NotFound());

        var getPresenter = new Mock<IGetFeatureFlagCodePresenter>();
        getPresenter.Setup(p => p.IsNotFound).Returns(true);
        getPresenter.Setup(p => p.FeatureFlag).Returns(NullModel.Instance);

        var getPresenterFactory = new Mock<IGetFeatureFlagCodePresenterFactory>();
        getPresenterFactory.Setup(f => f.Create()).Returns(getPresenter.Object);

        var interactor = new Interactor(getPresenterFactory.Object, getInteractor.Object);

        var request = new RequestModel
        {
            Id = "not_found_flag"
        };
        var presenterMock = new Mock<IOutputPort>();

        await interactor.Execute(request, presenterMock.Object);

        presenterMock.Verify(p => p.NotFound());
    }
}