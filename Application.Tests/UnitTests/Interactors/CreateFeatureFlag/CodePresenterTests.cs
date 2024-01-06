using Application.Interactors.CreateFeatureFlag;
using Domain.Common;

namespace Application.Tests.UnitTests.Interactors.CreateFeatureFlag;

public sealed class CodePresenterTests
{
    [Test]
    public void CodePresenter_Ok_Should_Set_Id()
    {
        var presenter = new CodePresenter();

        presenter.Ok("some_flag");

        Assert.Multiple(() =>
        {
            Assert.That(presenter.IsOk);
            Assert.That(presenter.Id, Is.EqualTo("some_flag"));
        });
    }

    [Test]
    public void CodePresenter_BadRequest_Should_Set_Error()
    {
        var presenter = new CodePresenter();

        presenter.BadRequest(new List<ValidationError>
        {
            new()
            {
                Field = "Id",
                Message = "Required"
            },
            new()
            {
                Field = "Enabled",
                Message = "Required"
            }
        });

        var errors = presenter.Errors.ToList();

        Assert.Multiple(() =>
        {
            Assert.That(presenter.IsOk, Is.False);
            Assert.That(presenter.Errors.Count(), Is.EqualTo(2));
            Assert.That(presenter.Errors.ToList()[0], Is.EqualTo(new ValidationError
            {
                Field = "Id",
                Message = "Required"
            }));
            Assert.That(presenter.Errors.ToList()[1], Is.EqualTo(new ValidationError
            {
                Field = "Enabled",
                Message = "Required"
            }));
        });
    }

    [Test]
    public void CodePresenter_Error_Should_Set_Error()
    {
        var presenter = new CodePresenter();

        presenter.Error(new Error
        {
            Message = "Unknown error"
        });

        Assert.Multiple(() =>
        {
            Assert.That(presenter.IsOk, Is.False);
            Assert.That(presenter.Errors.Count(), Is.EqualTo(1));
            Assert.That(presenter.Errors.ToList()[0], Is.EqualTo(new Error
            {
                Message = "Unknown error"
            }));
        });
    }
}