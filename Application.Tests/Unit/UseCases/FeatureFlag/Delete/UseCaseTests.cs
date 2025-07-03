using Domain.Common;
using NSubstitute;
using FeatureFlagDelete = Application.UseCases.FeatureFlag.Delete;
using FeatureFlags = Domain.FeatureFlags;

namespace Application.Tests.Unit.UseCases.FeatureFlag.Delete;

[Parallelizable]
public sealed class UseCaseTests
{
    [Test]
    public void DeleteFeatureFlagUseCase__Is_An_IUseCase()
    {
        // Arrange
        var repository = Substitute.For<FeatureFlags.IRepository>();

        // Act
        var subject = new FeatureFlagDelete.UseCase(repository);

        // Arrange
        Assert.That(subject, Is.InstanceOf<FeatureFlagDelete.IUseCase>());
    }

    [Test]
    public async Task DeleteFeatureFlagUseCase__Execute__Ok__Deletes_A_Feature_Flag()
    {
        // Arrange
        const string flagId = "some_flag";

        var repository = Substitute.For<FeatureFlags.IRepository>();
        repository.Delete(Arg.Any<string>()).Returns(Result<bool, Error>.Ok(true));

        var request = new FeatureFlagDelete.RequestModel
        {
            Id = flagId
        };

        var presenter = Substitute.For<FeatureFlagDelete.IPresenter>();

        // Act
        var subject = new FeatureFlagDelete.UseCase(repository);
        await subject.Execute(request, presenter);

        // Assert
        presenter.Received().Ok();
    }

    [Test]
    public async Task DeleteFeatureFlagUseCase__Execute__NotFound__Calls_NotFound_If_Flag_Not_Found()
    {
        // Arrange
        const string flagId = "some_flag";

        var error = new NotFoundError();

        var repository = Substitute.For<FeatureFlags.IRepository>();
        repository.Delete(Arg.Any<string>()).Returns(Result<bool, Error>.Err(error));

        var request = new FeatureFlagDelete.RequestModel
        {
            Id = flagId
        };

        var presenter = Substitute.For<FeatureFlagDelete.IPresenter>();

        // Act
        var subject = new FeatureFlagDelete.UseCase(repository);
        await subject.Execute(request, presenter);

        // Arrange
        presenter.Received().NotFound();
    }

    [Test]
    public async Task DeleteFeatureFlagUseCase__Execute__Error__Provides_Error_If_Repository_Errors()
    {
        // Arrange
        const string flagId = "some_flag";

        var error = new Error
        {
            Message = "Unknown error"
        };

        var repository = Substitute.For<FeatureFlags.IRepository>();
        repository.Delete(Arg.Any<string>()).Returns(Result<bool, Error>.Err(error));

        var request = new FeatureFlagDelete.RequestModel
        {
            Id = flagId
        };

        var presenter = Substitute.For<FeatureFlagDelete.IPresenter>();

        // Act
        var subject = new FeatureFlagDelete.UseCase(repository);
        await subject.Execute(request, presenter);

        // Assert
        presenter.Received().Error(error);
    }
}