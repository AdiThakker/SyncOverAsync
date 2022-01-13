using Microsoft.OpenApi.Models;
using System.Collections.Concurrent;

// Create builder
var builder = WebApplication.CreateBuilder(args);

// register services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(_ => _.SwaggerDoc("v1", new OpenApiInfo { Title = "Sync Over Async API", Description = "Example showing use of TCS to control task completion", Version = "v1" }));

// build
var app = builder.Build();

// use services
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sync Over Async API V1"));

// Add Routes
app.MapGet("/weatherforecastsync", () => WeatherForecast.GetWeather());

app.MapGet("/weatherforecastasync", async () => await WeatherForecast.GetWeatherAsync(Random.Shared.Next(int.MaxValue).ToString()));

// Run
app.Run();

// Data
internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    private static string[] summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public static WeatherForecast[] GetWeather()
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
       new WeatherForecast
       (
           DateTime.Now.AddDays(index),
           Random.Shared.Next(-20, 55),
           summaries[Random.Shared.Next(summaries.Length)]
       ))
        .ToArray();
        return forecast;
    }

    static ConcurrentDictionary<string, TaskCompletionSource<WeatherForecast[]>> requests = new ConcurrentDictionary<string, TaskCompletionSource<WeatherForecast[]>>();

    public static async Task<WeatherForecast[]> GetWeatherAsync(string requestId)
    {
        TaskCompletionSource<WeatherForecast[]> tcs = new TaskCompletionSource<WeatherForecast[]>(requestId);
        requests.TryAdd(requestId, tcs);
        GetWeatherAsyncCompletion(requestId);
       return await tcs.Task;
    }

    private static async Task GetWeatherAsyncCompletion(string requestId)
    {
        // simulate background process such as listening on Service Bus topic / external call back, etc.
        await Task.Delay(10000);

        // Get the tcs that matches the requestId
        if (requests.TryGetValue(requestId, out TaskCompletionSource<WeatherForecast[]> tcs))
        {
            tcs.SetResult(GetWeather());
        }

    }

}





