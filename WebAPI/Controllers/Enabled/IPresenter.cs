using Application.Interactors.IsFeatureFlagEnabled;
using WebAPI.Common;

namespace WebAPI.Controllers.Enabled;

public interface IPresenter : IOutputPort, IHasActionResult, IHasIsError
{
}