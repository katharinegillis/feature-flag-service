using Application.Interactors.IsFeatureFlagEnabled;
using WebAPI.Controllers.Enabled;

namespace WebAPI.Tests.Controllers.Enabled;

public sealed class ActionResultPresenterFactoryTests
{
    [Test]
    public void ActionResultPresenterFactory_Should_Be_An_IActionResultPresenterFactory()
    {
        var factory = new ActionResultPresenterFactory();

        Assert.That(factory, Is.InstanceOf<IActionResultPresenterFactory>());
    }

    [Test]
    public void ActionResultPresenterFactory_Create_Should_Create_ActionResultPresenter_With_Request()
    {
        var factory = new ActionResultPresenterFactory();

        var request = new RequestModel
        {
            Id = "some_flag"
        };

        var presenter = factory.Create(request);

        Assert.That(presenter.Request, Is.EqualTo(request));
    }
}