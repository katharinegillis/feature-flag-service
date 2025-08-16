using WebAPI.Common;

namespace WebAPI.Tests.Unit.Common;

[Parallelizable]
[Category("Unit")]
public sealed class ApiResponseActionResultFactoryTests
{
    [Test]
    public void ApiResponseActionResultFactory__Ok__Returns_Successful_ApiResponse_With_Data()
    {
        // Act
        var result = ApiResponseActionResultFactory.Ok<string>("some_data");

        // Assert
        Assert.That(result.Value, Is.InstanceOf<ApiResponse<string>>());
        var apiResponseResult = result.Value as ApiResponse<string>;

        using (Assert.EnterMultipleScope())
        {
            Assert.That(apiResponseResult!.Successful, Is.True);
            Assert.That(apiResponseResult.Data, Is.EqualTo("some_data"));
        }
    }

    [Test]
    public void ApiResponseActionResultFactory__Err__Returns_Unsuccessful_ApiResponse_With_Errors()
    {
        // Act
        var result = ApiResponseActionResultFactory.Err<string>(["some error", "some other error"]);

        // Assert
        Assert.That(result.Value, Is.InstanceOf<ApiResponse<string>>());
        var apiResponseResult = result.Value as ApiResponse<string>;

        using (Assert.EnterMultipleScope())
        {
            Assert.That(apiResponseResult!.Successful, Is.False);
            Assert.That(apiResponseResult.Errors, Is.EqualTo(new List<string>
            {
                "some error",
                "some other error"
            }));
        }
    }
}