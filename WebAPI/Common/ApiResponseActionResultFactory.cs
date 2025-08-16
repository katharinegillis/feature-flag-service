using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Common;

public static class ApiResponseActionResultFactory
{
    public static OkObjectResult Ok<T>(T data)
    {
        return new OkObjectResult(new ApiResponse<T>
        {
            Data = data,
            Successful = true
        });
    }

    public static OkObjectResult Err<T>(List<string> errors)
    {
        return new OkObjectResult(new ApiResponse<T>
        {
            Errors = errors,
            Successful = false
        });
    }
}