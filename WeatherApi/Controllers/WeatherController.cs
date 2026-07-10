using Microsoft.AspNetCore.Mvc;
using WeatherApi.Models;

namespace WeatherApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild",
        "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];
 
    private static readonly List<WeatherForecast> Forecasts = [];

    static WeatherController()
    {
        Forecasts = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            {
                Id = index,
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToList();
    }

    /// <summary>
    /// Tamame peygoohi haya havaye ro bargardunad
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<WeatherForecast>> GetAll()
    {
        return Ok(Forecasts);
    }

    /// <summary>
    /// Yek peygoohi hava ba ID bargardunad
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<WeatherForecast> GetById(int id)
    {
        var forecast = Forecasts.FirstOrDefault(f => f.Id == id);
        if (forecast is null)
            return NotFound($"Forecast ba ID-ye {id} payda nashod.");
        return Ok(forecast);
    }

    /// <summary>
    /// Peygoohi hava-ye jadid besazad
    /// </summary>
    [HttpPost]
    public ActionResult<WeatherForecast> Create([FromBody] WeatherForecast forecast)
    {
        forecast.Id = Forecasts.Count > 0 ? Forecasts.Max(f => f.Id) + 1 : 1;
        Forecasts.Add(forecast);
        return CreatedAtAction(nameof(GetById), new { id = forecast.Id }, forecast);
    }

    /// <summary>
    /// Peygoohi hava-ye vojoudi ro beروزرسانی konad
    /// </summary>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] WeatherForecast updated)
    {
        var existing = Forecasts.FirstOrDefault(f => f.Id == id);
        if (existing is null)
            return NotFound();

        existing.TemperatureC = updated.TemperatureC;
        existing.Summary = updated.Summary;
        existing.Date = updated.Date;

        return NoContent();
    }

    /// <summary>
    /// Peygoohi hava-ye vojoudi ro hazf konad
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var forecast = Forecasts.FirstOrDefault(f => f.Id == id);
        if (forecast is null)
            return NotFound();

        Forecasts.Remove(forecast);
        return NoContent();
    }
}
