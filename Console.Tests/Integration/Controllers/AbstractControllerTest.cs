using Console.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Utilities.LocalizationService;

namespace Console.Tests.Integration.Controllers;

public abstract class AbstractControllerTest
{
    protected readonly ILocalizationService<SharedResource> SharedResourceLocalizationService;

    protected AbstractControllerTest()
    {
        var options = Options.Create(new LocalizationOptions { ResourcesPath = "Resources" });
        var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
        var localizer = new StringLocalizer<SharedResource>(factory);
        SharedResourceLocalizationService = new StringLocalizerLocalizationService<SharedResource>(localizer);
    }
}