using Domain.Common;

namespace Application.Interactors.Config.Show;

public interface IOutputPort
{
    public void Ok(string value);
}