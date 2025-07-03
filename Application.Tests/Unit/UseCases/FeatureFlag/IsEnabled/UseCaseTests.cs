using NSubstitute;
using FeatureFlagGet = Application.UseCases.FeatureFlag.Get;
using FeatureFlagIsEnabled = Application.UseCases.FeatureFlag.IsEnabled;
using FeatureFlags = Domain.FeatureFlags;

namespace Application.Tests.Unit.UseCases.FeatureFlag.IsEnabled;

[Parallelizable]
[Category("Unit")]
public sealed class UseCaseTests
{
    [Test]
    public void IsFeatureFlagEnabledUseCase__Is_An_IUseCase()
    {
        // Arrange
        var getInteractor = Substitute.For<FeatureFlagGet.IUseCase>();
        var getPresenterFactory = Substitute.For<FeatureFlagGet.ICodePresenterFactory>();

        // Act
        var subject = new FeatureFlagIsEnabled.UseCase(getPresenterFactory, getInteractor);

        // Assert 
        Assert.That(subject, Is.InstanceOf<FeatureFlagIsEnabled.IUseCase>());
    }

    [Test]
    public async Task IsFeatureFlagEnabledUseCase__Ok__Provides_True_If_Flag_Is_Enabled()
    {
        // Arrange
        const string flagId = "some_flag";

        var model = Substitute.For<FeatureFlags.IModel>();
        model.Enabled.Returns(true);

        var getPresenter = Substitute.For<FeatureFlagGet.ICodePresenter>();
        getPresenter.IsNotFound.Returns(false);
        getPresenter.FeatureFlag.Returns(model);

        var getInteractor = Substitute.For<FeatureFlagGet.IUseCase>();

        var getPresenterFactory = Substitute.For<FeatureFlagGet.ICodePresenterFactory>();
        getPresenterFactory.Create().Returns(getPresenter);

        var request = new FeatureFlagIsEnabled.RequestModel
        {
            Id = flagId
        };

        var presenter = Substitute.For<FeatureFlagIsEnabled.IPresenter>();

        // Act
        var subject = new FeatureFlagIsEnabled.UseCase(getPresenterFactory, getInteractor);
        await subject.Execute(request, presenter);

        // Assert
        presenter.Received().Ok(true);
    }

    [Test]
    public async Task IsFeatureFlagEnabledUseCase__Execute__Ok__Provides_False_If_Flag_Is_Not_Enabled()
    {
        // Arrange
        const string flagId = "some_flag";

        var getInteractor = Substitute.For<FeatureFlagGet.IUseCase>();

        var model = Substitute.For<FeatureFlags.IModel>();
        model.Enabled.Returns(false);

        var getPresenter = Substitute.For<FeatureFlagGet.ICodePresenter>();
        getPresenter.IsNotFound.Returns(false);
        getPresenter.FeatureFlag.Returns(model);

        var getPresenterFactory = Substitute.For<FeatureFlagGet.ICodePresenterFactory>();
        getPresenterFactory.Create().Returns(getPresenter);

        var request = new FeatureFlagIsEnabled.RequestModel
        {
            Id = flagId
        };

        var presenter = Substitute.For<FeatureFlagIsEnabled.IPresenter>();

        // Act
        var subject = new FeatureFlagIsEnabled.UseCase(getPresenterFactory, getInteractor);
        await subject.Execute(request, presenter);

        // Assert
        presenter.Received().Ok(false);
    }

    [Test]
    public async Task IsFeatureFlagEnabledUseCase__Execute__NotFound__Calls_NotFound_If_Flag_Not_Found()
    {
        // Arrange
        const string flagId = "some_flag";

        var getInteractor = Substitute.For<FeatureFlagGet.IUseCase>();

        var getPresenter = Substitute.For<FeatureFlagGet.ICodePresenter>();
        getPresenter.IsNotFound.Returns(true);

        var getPresenterFactory = Substitute.For<FeatureFlagGet.ICodePresenterFactory>();
        getPresenterFactory.Create().Returns(getPresenter);

        var request = new FeatureFlagIsEnabled.RequestModel
        {
            Id = flagId
        };

        var presenter = Substitute.For<FeatureFlagIsEnabled.IPresenter>();

        // Act
        var subject = new FeatureFlagIsEnabled.UseCase(getPresenterFactory, getInteractor);
        await subject.Execute(request, presenter);

        // Assert
        presenter.Received().NotFound();
    }
}