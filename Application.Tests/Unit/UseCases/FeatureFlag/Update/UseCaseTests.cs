using Domain.Common;
using NSubstitute;
using FeatureFlagUpdate = Application.UseCases.FeatureFlag.Update;
using FeatureFlags = Domain.FeatureFlags;

namespace Application.Tests.Unit.UseCases.FeatureFlag.Update;

[Parallelizable]
[Category("Unit")]
public sealed class UseCaseTests
{
    [Test]
    public void UpdateFeatureFlagUseCase__Is_An_IUseCase()
    {
        // Arrange
        var repository = Substitute.For<FeatureFlags.IRepository>();
        var factory = Substitute.For<FeatureFlags.IFactory>();

        // Act
        var subject = new FeatureFlagUpdate.UseCase(repository, factory);

        // Assert
        Assert.That(subject, Is.InstanceOf<FeatureFlagUpdate.IUseCase>());
    }

    [Test]
    public async Task UpdateFeatureFlagUseCase__Execute__Ok__Updates_A_Feature_Flag()
    {
        // Arrange
        const string flagId = "some_flag";
        const bool enabled = false;

        var model = Substitute.For<FeatureFlags.IModel>();
        model.Validate().Returns(Result<bool, IEnumerable<ValidationError>>.Ok(true));

        var repository = Substitute.For<FeatureFlags.IRepository>();
        repository.Update(model).Returns(Result<bool, Error>.Ok(true));

        var factory = Substitute.For<FeatureFlags.IFactory>();
        factory.Create(Arg.Any<string>(), Arg.Any<bool>()).Returns(model);

        var request = new FeatureFlagUpdate.RequestModel
        {
            Id = flagId,
            Enabled = enabled
        };

        var presenter = Substitute.For<FeatureFlagUpdate.IPresenter>();

        // Act
        var subject = new FeatureFlagUpdate.UseCase(repository, factory);
        await subject.Execute(request, presenter);

        // Arrange
        presenter.Received().Ok();
    }

    [Test]
    public async Task UpdateFeatureFlagUseCase__Execute__BadRequest__Provides_Validation_Error()
    {
        // Arrange
        const string flagId = "some_flag";
        const bool enabled = false;

        var repository = Substitute.For<FeatureFlags.IRepository>();

        var validationErrors = new ValidationError[]
        {
            new()
            {
                Field = "Id",
                Message = "Some validation error"
            }
        };

        var model = Substitute.For<FeatureFlags.IModel>();
        model.Validate().Returns(Result<bool, IEnumerable<ValidationError>>.Err(validationErrors));

        var factory = Substitute.For<FeatureFlags.IFactory>();
        factory.Create(Arg.Any<string>(), Arg.Any<bool>()).Returns(model);

        var request = new FeatureFlagUpdate.RequestModel
        {
            Id = flagId,
            Enabled = enabled
        };

        var presenter = Substitute.For<FeatureFlagUpdate.IPresenter>();

        // Act
        var subject = new FeatureFlagUpdate.UseCase(repository, factory);
        await subject.Execute(request, presenter);

        // Assert
        presenter.Received().BadRequest(validationErrors);
    }

    [Test]
    public async Task UpdateFeatureFlagUseCase__Execute__Error__Calls_Error_If_Repository_Errors()
    {
        // Arrange
        const string flagId = "some_flag";
        const bool enabled = false;

        var error = new Error
        {
            Message = "Unknown error"
        };

        var model = Substitute.For<FeatureFlags.IModel>();
        model.Validate().Returns(Result<bool, IEnumerable<ValidationError>>.Ok(true));

        var repository = Substitute.For<FeatureFlags.IRepository>();
        repository.Update(model).Returns(Result<bool, Error>.Err(error));

        var factory = Substitute.For<FeatureFlags.IFactory>();
        factory.Create(Arg.Any<string>(), Arg.Any<bool>()).Returns(model);

        var request = new FeatureFlagUpdate.RequestModel
        {
            Id = flagId,
            Enabled = enabled
        };

        var presenter = Substitute.For<FeatureFlagUpdate.IPresenter>();

        // Act
        var subject = new FeatureFlagUpdate.UseCase(repository, factory);
        await subject.Execute(request, presenter);

        // Assert
        presenter.Received().Error(error);
    }

    [Test]
    public async Task UpdateFeatureFlagUseCase__Execute__NotFound___Calls_NotFound_If_Flag_Not_Found()
    {
        // Arrange
        const string flagId = "some_flag";
        const bool enabled = false;

        var error = new NotFoundError();

        var model = Substitute.For<FeatureFlags.IModel>();
        model.Validate().Returns(Result<bool, IEnumerable<ValidationError>>.Ok(true));

        var repository = Substitute.For<FeatureFlags.IRepository>();
        repository.Update(model).Returns(Result<bool, Error>.Err(error));

        var factory = Substitute.For<FeatureFlags.IFactory>();
        factory.Create(Arg.Any<string>(), Arg.Any<bool>()).Returns(model);

        var request = new FeatureFlagUpdate.RequestModel
        {
            Id = flagId,
            Enabled = enabled
        };

        var presenter = Substitute.For<FeatureFlagUpdate.IPresenter>();

        // Act
        var subject = new FeatureFlagUpdate.UseCase(repository, factory);
        await subject.Execute(request, presenter);

        // Arrange
        presenter.Received().NotFound();
    }
}