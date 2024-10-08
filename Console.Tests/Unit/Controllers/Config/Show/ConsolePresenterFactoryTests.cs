using Application.Interactors.Config.Show;
using Console.Controllers.Config.Show;

namespace Console.Tests.Unit.Controllers.Config.Show;

public sealed class ConsolePresenterFactoryTests
{
    [Test]
    public void ConsolePresenterFactory_Should_Be_An_IConsolePresenterFactory()
    {
        var factory = new ConsolePresenterFactory();
        
        Assert.That(factory, Is.InstanceOf<IConsolePresenterFactory>());
    }

    [Test]
    public void ConsolePresenterFactory_Should_Create_ConsolePresenter_With_Request()
    {
        var request = new RequestModel
        {
            Name = RequestModel.NameOptions.Datasource
        };

        var factory = new ConsolePresenterFactory();

        var presenter = factory.Create(request);
        
        Assert.That(presenter.Request, Is.EqualTo(request));
    }
}