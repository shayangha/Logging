using System.Collections;
using Logging.Logger;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILoggerService<WeatherForecastController> _logger;

    public WeatherForecastController(ILoggerService<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogTrace("Trace 1");
        _logger.LogDebug("Debug 1");
        _logger.LogInformation("Info 1");
        _logger.LogWarning("Warn 1");
        _logger.LogError(message: "Error 1");
        _logger.LogFatal(message: "Fatal 1");


        var innerException = new Exception("Inner Exception Message (1)");
        var exception = new Exception("Exception Message (1)", innerException);

        var hashtable = new Hashtable();
        hashtable.Add("Key1", "Value1");
        hashtable.Add("Key2", "Value2");

        _logger.LogFatal(exception, "Fatal 1", hashtable);

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}