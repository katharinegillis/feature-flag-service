using Domain.Models;

namespace Application.Interactors.GetFeatureFlag;

public interface IOutputPort
{
    public void Ok(FeatureFlag featureFlag);

    public void NotFound();
}