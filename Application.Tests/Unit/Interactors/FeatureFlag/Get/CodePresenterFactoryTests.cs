using Application.Interactors.FeatureFlag.Get;

namespace Application.Tests.Unit.Interactors.FeatureFlag.Get;

public sealed class CodePresenterFactoryTests
{
    [Test]
    public void CodePresenterFactory_Should_Be_An_ICodePresenterFactory()
    {
        var factory = new CodePresenterFactory();

        Assert.That(factory, Is.InstanceOf<ICodePresenterFactory>());
    }

    [Test]
    public void CodePresenterFactory_Should_Create_CodePresenter()
    {
        var factory = new CodePresenterFactory();

        var presenter = factory.Create();

        Assert.That(presenter, Is.InstanceOf<ICodePresenter>());
    }
}