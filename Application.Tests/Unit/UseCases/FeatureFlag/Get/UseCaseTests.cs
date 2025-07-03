using NSubstitute;
using FeatureFlagGet = Application.UseCases.FeatureFlag.Get;
using FeatureFlags = Domain.FeatureFlags;

namespace Application.Tests.Unit.UseCases.FeatureFlag.Get;

[Parallelizable]
[Category("Unit")]
public sealed class UseCaseTests
{
    [Test]
    public void GetFeatureFlagUseCase__Is_An_IUseCase()
    {
        // Arrange
        var repository = Substitute.For<FeatureFlags.IReadRepository>();

        // Act
        var subject = new FeatureFlagGet.UseCase(repository);

        // Arrange
        Assert.That(subject, Is.InstanceOf<FeatureFlagGet.IUseCase>());
    }

    [Test]
    public async Task GetFeatureFlagUseCase__Execute__OK__Provides_A_Feature_Flag()
    {
        // Arrange
        const string flagId = "some_flag";

        var model = Substitute.For<FeatureFlags.IModel>();

        var repository = Substitute.For<FeatureFlags.IReadRepository>();
        repository.Get(Arg.Any<string>()).Returns(model);

        var request = new FeatureFlagGet.RequestModel
        {
            Id = flagId
        };

        var presenter = Substitute.For<FeatureFlagGet.IPresenter>();

        // Act
        var subject = new FeatureFlagGet.UseCase(repository);
        await subject.Execute(request, presenter);

        // Arrange
        presenter.Received().Ok(model);
    }

    [Test]
    public async Task GetFeatureFlag__Execute__NotFound__Calls_NotFound_If_Flag_Not_Found()
    {
        // Arrange
        const string flagId = "some_flag";

        var model = Substitute.For<FeatureFlags.IModel>();
        model.IsNull.Returns(true);
        var repository = Substitute.For<FeatureFlags.IReadRepository>();
        repository.Get(Arg.Any<string>()).Returns(model);

        var request = new FeatureFlagGet.RequestModel
        {
            Id = flagId
        };

        var presenter = Substitute.For<FeatureFlagGet.IPresenter>();

        // Act
        var subject = new FeatureFlagGet.UseCase(repository);
        await subject.Execute(request, presenter);

        // Assert
        presenter.Received().NotFound();
    }
}