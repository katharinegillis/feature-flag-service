using Microsoft.Extensions.Localization;
using Moq;

namespace Console.Tests.UnitTests.Commands.FeatureFlags.Get;

public class ConsolePresenterTests
{
    [Test]
    public void ConsolePresenter_Ok_Should_Display_FeatureFlag()
    {
        var localizerMock = new Mock<IStringLocalizer<ConsolePresenter>>();
        var flagString = new LocalizedString("Id: \"{0}\", Enabled: \"{1}\"", "Id: \"{0}\", Enabled: \"{1}\"");
        var trueString = new LocalizedString("true", "true");
        localizerMock.Setup(localizer => localizer["Id: \"{0}\", Enabled: \"{1}\"", "some_flag", trueString])
            .Returns(flagString);
    }
}