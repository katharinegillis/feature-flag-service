using Application.Interactors.GetFeatureFlag;
using Domain.Models;

namespace Application.UnitTests.Interactors.GetFeatureFlag;

public class CodePresenterTests
{
    [Test]
    public void Ok_Should_Store_FeatureFlag()
    {
        var featureFlag = new FeatureFlag
        {
            Id = "some_flag",
            Enabled = true
        };
        
        var presenter = new CodePresenter();

        presenter.Ok(featureFlag);

        Assert.That(presenter.FeatureFlag, Is.EqualTo(featureFlag));
    }
}