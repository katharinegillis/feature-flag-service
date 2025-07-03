using NSubstitute;
using ConfigShow = Application.UseCases.Config.Show;
using FeatureFlags = Domain.FeatureFlags;

namespace Application.Tests.Unit.UseCases.Config.Show;

[Parallelizable]
[Category("Unit")]
public sealed class UseCaseTests
{
    [Test]
    public void ConfigShowUseCase__Is_An_IUseCase()
    {
        // Arrange
        var repository = Substitute.For<FeatureFlags.IReadRepository>();

        // Act
        var subject = new ConfigShow.UseCase(repository);

        // Assert
        Assert.That(subject, Is.InstanceOf<ConfigShow.IUseCase>());
    }

    [Test]
    public void ConfigShowUseCase__Execute__Ok__Provides_A_Config_Value()
    {
        // Arrange
        const string repositoryName = "Some Repository";

        var repository = Substitute.For<FeatureFlags.IReadRepository>();
        repository.Name.Returns(repositoryName);

        var request = new ConfigShow.RequestModel
        {
            Name = ConfigShow.RequestModel.NameOptions.Datasource
        };

        var presenter = Substitute.For<ConfigShow.IPresenter>();

        // Act
        var subject = new ConfigShow.UseCase(repository);
        subject.Execute(request, presenter);

        // Assert
        presenter.Received().Ok(repositoryName);
    }

    [Test]
    public void ConfigShowUseCase__Execute__Invalid_Request__Throws_Error()
    {
        // Arrange
        var repository = Substitute.For<FeatureFlags.IReadRepository>();

        var request = new ConfigShow.RequestModel
        {
            Name = ConfigShow.RequestModel.NameOptions.Unknown
        };

        var presenter = Substitute.For<ConfigShow.IPresenter>();

        // Act
        var subject = new ConfigShow.UseCase(repository);
        Assert.That(() => subject.Execute(request, presenter), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());

        // Assert
        presenter.DidNotReceive().Ok(Arg.Any<string>());
    }
}