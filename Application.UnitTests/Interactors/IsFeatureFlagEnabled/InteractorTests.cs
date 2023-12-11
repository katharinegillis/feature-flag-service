using Application.Interactors.IsFeatureFlagEnabled;
using IGetFeatureFlagInputPort = Application.Interactors.GetFeatureFlag.IInputPort;
using IGetFeatureFlagOutputPort = Application.Interactors.GetFeatureFlag.IOutputPort;
using GetFeatureFlagRequestModel = Application.Interactors.GetFeatureFlag.RequestModel;
using Domain.FeatureFlag;
using Moq;

namespace Application.UnitTests.Interactors.IsFeatureFlagEnabled;

public class InteractorTests
{
    [Test]
    public void IsFeatureFlagEnabledInterceptor_An_InputPort()
    {
        var getFeatureFlagInteractor = Mock.Of<IGetFeatureFlagInputPort>();

        var interactor = new Interactor(getFeatureFlagInteractor);

        Assert.That(interactor, Is.InstanceOf<IInputPort>());
    }

    [Test]
    public async Task IsFeatureFlagEnabledInterceptor_Should_Return_True_If_Flag_Is_Enabled()
    {
        var getFeatureFlagInteractor = new Mock<IGetFeatureFlagInputPort>();
        getFeatureFlagInteractor.Setup(interactor =>
                interactor.Execute(It.IsAny<GetFeatureFlagRequestModel>(), It.IsAny<IGetFeatureFlagOutputPort>()))
            // ReSharper disable once UnusedParameter.Local
            .Callback((GetFeatureFlagRequestModel request, IGetFeatureFlagOutputPort getFeatureFlagPresenter) =>
                getFeatureFlagPresenter.Ok(new FeatureFlag { Id = "some_flag", Enabled = true }));

        var interactor = new Interactor(getFeatureFlagInteractor.Object);

        var request = new RequestModel
        {
            Id = "some_flag"
        };
        var presenterMock = new Mock<IOutputPort>();

        await interactor.Execute(request, presenterMock.Object);

        presenterMock.Verify(presenter => presenter.Ok(true));
    }

    [Test]
    public async Task IsFeatureFlagEnabledInterceptor_Should_Return_False_If_Flag_Is_Not_Enabled()
    {
        var getFeatureFlagInteractor = new Mock<IGetFeatureFlagInputPort>();
        getFeatureFlagInteractor.Setup(interactor =>
                interactor.Execute(It.IsAny<GetFeatureFlagRequestModel>(), It.IsAny<IGetFeatureFlagOutputPort>()))
            // ReSharper disable once UnusedParameter.Local
            .Callback((GetFeatureFlagRequestModel request, IGetFeatureFlagOutputPort getFeatureFlagPresenter) =>
                getFeatureFlagPresenter.Ok(new FeatureFlag { Id = "some_flag", Enabled = false }));

        var interactor = new Interactor(getFeatureFlagInteractor.Object);

        var request = new RequestModel
        {
            Id = "some_flag"
        };
        var presenterMock = new Mock<IOutputPort>();

        await interactor.Execute(request, presenterMock.Object);

        presenterMock.Verify(presenter => presenter.Ok(false));
    }

    [Test]
    public async Task IsFeatureFlagEnabledInterceptor_Should_Return_NotFound_If_Flag_Not_Found()
    {
        var getFeatureFlagInteractor = new Mock<IGetFeatureFlagInputPort>();
        getFeatureFlagInteractor.Setup(interactor =>
                interactor.Execute(It.IsAny<GetFeatureFlagRequestModel>(), It.IsAny<IGetFeatureFlagOutputPort>()))
            // ReSharper disable once UnusedParameter.Local
            .Callback((GetFeatureFlagRequestModel getFeatureFlagRequestModel,
                    IGetFeatureFlagOutputPort getFeatureFlagPresenter) =>
                getFeatureFlagPresenter.NotFound());

        var interactor = new Interactor(getFeatureFlagInteractor.Object);

        var request = new RequestModel
        {
            Id = "not_found_flag"
        };
        var presenterMock = new Mock<IOutputPort>();

        await interactor.Execute(request, presenterMock.Object);

        presenterMock.Verify(presenter => presenter.NotFound());
    }
}