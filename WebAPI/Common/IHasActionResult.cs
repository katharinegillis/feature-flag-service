using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Common;

public interface IHasActionResult
{
    public IActionResult ActionResult { get; }
}