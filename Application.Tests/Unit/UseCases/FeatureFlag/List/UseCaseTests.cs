using NSubstitute;
using FeatureFlagList = Application.UseCases.FeatureFlag.List;
using FeatureFlags = Domain.FeatureFlags;

namespace Application.Tests.Unit.UseCases.FeatureFlag.List;

[Parallelizable]
public sealed class UseCaseTests
{
    [Test]
    public void ListFeatureFlagsUseCase__Is_An_IUseCase()
    {
        // Arrange
        var repository = Substitute.For<FeatureFlags.IReadRepository>();

        // Act
        var subject = new FeatureFlagList.UseCase(repository);

        // Assert
        Assert.That(subject, Is.InstanceOf<FeatureFlagList.IUseCase>());
    }

    [Test]
    public async Task ListFeatureFlagsUseCase__Execute__Ok__Provides_A_List_Of_Feature_Flags()
    {
        // Arrange
        var featureFlags = new List<FeatureFlags.IModel>
        {
            Substitute.For<FeatureFlags.IModel>(),
            Substitute.For<FeatureFlags.IModel>()
        };

        var repository = Substitute.For<FeatureFlags.IReadRepository>();
        repository.List().Returns(featureFlags);

        var presenter = Substitute.For<FeatureFlagList.IPresenter>();

        // Act
        var subject = new FeatureFlagList.UseCase(repository);
        await subject.Execute(presenter);

        // Arrange
        presenter.Received().Ok(featureFlags);
    }
}