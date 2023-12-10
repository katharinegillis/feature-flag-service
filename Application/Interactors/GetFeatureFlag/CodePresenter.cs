using Domain.Models;

namespace Application.Interactors.GetFeatureFlag;

public class CodePresenter : IOutputPort
{
    public FeatureFlag? FeatureFlag { get; private set; }

    public void Ok(FeatureFlag featureFlag)
    {
        FeatureFlag = featureFlag;
    }

    public void NotFound()
    {
        FeatureFlag = null;
    }
}