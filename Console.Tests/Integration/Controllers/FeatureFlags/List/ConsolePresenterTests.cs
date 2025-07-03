using Console.Common;
using Console.Controllers.FeatureFlags.List;
using Domain.FeatureFlags;

namespace Console.Tests.Integration.Controllers.FeatureFlags.List;

[Parallelizable]
[Category("Integration")]
public sealed class ConsolePresenterTests : AbstractControllerTest
{
    [Test]
    public void ConsolePresenter__Is_An_IConsolePresenter()
    {
        // Act
        var subject = new ConsolePresenter(SharedResourceLocalizationService);

        // Assert
        Assert.That(subject, Is.InstanceOf<IConsolePresenter>());
    }

    [Test]
    public void ConsolePresenter__Ok__Provides_Successful_ExitCode_And_Lists_Flags()
    {
        // Arrange
        var expectedLines = new List<string>
        {
            "Id: \"some_flag\", Enabled: \"true\"",
            "Id: \"another_flag\", Enabled: \"false\""
        };

        // Act 
        var subject = new ConsolePresenter(SharedResourceLocalizationService);
        subject.Ok(new List<IModel>
        {
            new Model
            {
                Id = "some_flag",
                Enabled = true
            },
            new Model
            {
                Id = "another_flag",
                Enabled = false
            }
        });

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(subject.ActionResult.ExitCode, Is.EqualTo((int)ExitCode.Success));
            Assert.That(subject.ActionResult.Lines, Is.EqualTo(expectedLines));
        });
    }
}