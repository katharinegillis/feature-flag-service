using Microsoft.Extensions.Logging;
using Moq;
using WebAPI.Controllers;

namespace WebAPI.Tests;

public class Tests
{
    private WeatherForecastController _controller;

    [SetUp]
    public void Setup()
    {
        var mockedLogger = Mock.Of<ILogger<WeatherForecastController>>();
        _controller = new WeatherForecastController(mockedLogger);
    }

    [Test]
    public void Get_Should_Return_Random_Forecast_For_5_Days()
    {
        var result = _controller.Get();

        foreach (var item in result.Select((value, i) => new { i, value }))
        {
            WeatherForecast weatherForecast = item.value;
            int index = item.i;
            Assert.That(weatherForecast.Date, Is.EqualTo(DateOnly.FromDateTime(DateTime.Now.AddDays(index + 1))));
        }
    }
}