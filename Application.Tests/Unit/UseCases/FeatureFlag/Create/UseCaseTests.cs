using Domain.Common;
using NSubstitute;
using FeatureFlagCreate = Application.UseCases.FeatureFlag.Create;
using FeatureFlags = Domain.FeatureFlags;

namespace Application.Tests.Unit.UseCases.FeatureFlag.Create;

[Parallelizable]
public sealed class UseCaseTests
{
    [Test]
    public void CreateFeatureFlagUseCase__Is_An_IUseCase()
    {
        // Arrange
        var repository = Substitute.For<FeatureFlags.IRepository>();
        var factory = Substitute.For<FeatureFlags.IFactory>();

        // Act
        var subject = new FeatureFlagCreate.UseCase(repository, factory);

        // Assert
        Assert.That(subject, Is.InstanceOf<FeatureFlagCreate.IUseCase>());
    }

    [Test]
    public async Task CreateFeatureFlagUseCase__Execute__Ok__Creates_A_Feature_Flag()
    {
        // Arrange
        const string flagId = "new_flag";
        const bool enabled = true;

        var model = Substitute.For<FeatureFlags.IModel>();
        model.Validate().Returns(Result<bool, IEnumerable<ValidationError>>.Ok(true));

        var repository = Substitute.For<FeatureFlags.IRepository>();
        repository.Create(model).Returns(m => Result<string, Error>.Ok(m.Arg<FeatureFlags.IModel>().Id));

        var factory = Substitute.For<FeatureFlags.IFactory>();
        factory.Create(Arg.Any<string>(), Arg.Any<bool>()).Returns(model);

        var request = new FeatureFlagCreate.RequestModel
        {
            Id = flagId,
            Enabled = enabled
        };

        var presenter = Substitute.For<FeatureFlagCreate.IPresenter>();

        // Act
        var subject = new FeatureFlagCreate.UseCase(repository, factory);
        await subject.Execute(request, presenter);

        // Assert
        presenter.Received().Ok();
    }

    [Test]
    public async Task CreateFeatureFlagUseCase__Execute__BadRequest__Provides_Validation_Errors_From_Model_Validation()
    {
        // Arrange
        const string flagId = "new_flag";
        const bool enabled = true;

        var repository = Substitute.For<FeatureFlags.IRepository>();

        var validationErrors = new ValidationError[]
        {
            new()
            {
                Field = "Id",
                Message = "Some error"
            },
            new()
            {
                Field = "Id",
                Message = "Some other error"
            }
        };

        var model = Substitute.For<FeatureFlags.IModel>();
        model.Validate().Returns(Result<bool, IEnumerable<ValidationError>>.Err(validationErrors));

        var factory = Substitute.For<FeatureFlags.IFactory>();
        factory.Create(Arg.Any<string>(), Arg.Any<bool>()).Returns(model);

        var request = new FeatureFlagCreate.RequestModel
        {
            Id = flagId,
            Enabled = enabled
        };

        var presenter = Substitute.For<FeatureFlagCreate.IPresenter>();

        // Act
        var subject = new FeatureFlagCreate.UseCase(repository, factory);
        await subject.Execute(request, presenter);

        // Assert
        presenter.Received().BadRequest(validationErrors);
    }

    [Test]
    public async Task CreateFeatureFlagUseCase__Execute__BadRequest__Provides_Validation_Error_If_Id_Already_Used()
    {
        // Arrange
        const string flagId = "new_flag";
        const bool enabled = true;

        var validationError = new ValidationError
        {
            Field = "Id",
            Message = "Id already exists"
        };

        var repository = Substitute.For<FeatureFlags.IRepository>();
        repository.Create(Arg.Any<FeatureFlags.IModel>()).Returns(Result<string, Error>.Err(validationError));

        // The model validate needs to pass to get to the repository create error
        var model = Substitute.For<FeatureFlags.IModel>();
        model.Validate().Returns(Result<bool, IEnumerable<ValidationError>>.Ok(true));
        var factory = Substitute.For<FeatureFlags.IFactory>();
        factory.Create(Arg.Any<string>(), Arg.Any<bool>()).Returns(model);

        var request = new FeatureFlagCreate.RequestModel
        {
            Id = flagId,
            Enabled = enabled
        };

        var presenter = Substitute.For<FeatureFlagCreate.IPresenter>();

        // Act
        var subject = new FeatureFlagCreate.UseCase(repository, factory);
        await subject.Execute(request, presenter);

        // Assert
        var validationErrors = new List<ValidationError> { validationError };
        presenter.Received().BadRequest(Arg.Is<List<ValidationError>>(x => x.SequenceEqual(validationErrors)));
    }

    [Test]
    public async Task CreateFeatureFlagUseCase__Execute__Error__Provides_Error_If_Repository_Errors()
    {
        // Arrange
        const string flagId = "new_flag";
        const bool enabled = true;

        var error = new Error
        {
            Message = "Unknown error"
        };

        // The model validate needs to pass to get to the repository create error
        var model = Substitute.For<FeatureFlags.IModel>();
        model.Validate().Returns(Result<bool, IEnumerable<ValidationError>>.Ok(true));
        var repository = Substitute.For<FeatureFlags.IRepository>();
        repository.Create(model).Returns(Result<string, Error>.Err(error));

        var factory = Substitute.For<FeatureFlags.IFactory>();
        factory.Create(Arg.Any<string>(), Arg.Any<bool>()).Returns(model);

        var request = new FeatureFlagCreate.RequestModel
        {
            Id = flagId,
            Enabled = enabled
        };

        var presenter = Substitute.For<FeatureFlagCreate.IPresenter>();

        // Act
        var subject = new FeatureFlagCreate.UseCase(repository, factory);
        await subject.Execute(request, presenter);

        // Assert
        presenter.Received().Error(error);
    }
}