using Application.UseCases.Config.Show;
using Console.Common;
using Console.Controllers.Config.Show;
using Console.Localization;
using Domain.Common;
using NSubstitute;
using Utilities.LocalizationService;

namespace Console.Tests.Integration.Controllers.Config.Show;

[Parallelizable]
[Category("Integration")]
public class ConsolePresenterTests : AbstractControllerTest
{
    [Test]
    public void ConsolePresenter__Is_An_IConsolePresenter()
    {
        // Arrange
        var request = new RequestModel();

        var localizer = Substitute.For<ILocalizationService<SharedResource>>();

        // Act
        var subject = new ConsolePresenter(request, localizer);

        // Assert
        Assert.That(subject, Is.InstanceOf<IConsolePresenter>());
    }

    [Test]
    public void ConsolePresenter__Ok__Provides_Successful_ExitCode_And_Lines()
    {
        // Arrange
        const string repositoryName = "Some datasource";
        var expectedLines = new List<string>
        {
            $"Datasource \"{repositoryName}\""
        };

        var request = new RequestModel
        {
            Name = RequestModel.NameOptions.Datasource
        };

        // Act
        var subject = new ConsolePresenter(request, SharedResourceLocalizationService);
        subject.Ok(repositoryName);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(subject.ActionResult.ExitCode, Is.EqualTo((int)ExitCode.Success));
            Assert.That(subject.ActionResult.Lines, Is.EqualTo(expectedLines));
        }
    }

    [Test]
    public void ConsolePresenter__BadRequest__Provides_OptionsError_ExitCode()
    {
        // Arrange
        const string fieldName = "Id";
        const string message = "Invalid";
        var expectedLines = new List<string>
        {
            $"{fieldName}: {message}."
        };

        // Act
        var subject = new ConsolePresenter(null, SharedResourceLocalizationService);
        subject.BadRequest(new List<ValidationError>
        {
            new()
            {
                Field = fieldName,
                Message = message
            }
        });

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(subject.ActionResult.ExitCode, Is.EqualTo((int)ExitCode.OptionsError));
            Assert.That(subject.ActionResult.Lines, Is.EqualTo(expectedLines));
        }
    }
}