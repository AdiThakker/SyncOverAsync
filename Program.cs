using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SyncOverAsync.DataStore;
using SyncOverAsync.Models;

// Create builder
var builder = WebApplication.CreateBuilder(args);

// register services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<WeatherDb>(options => options.UseInMemoryDatabase("weather"));
builder.Services.AddSwaggerGen(_ => _.SwaggerDoc("v1", new OpenApiInfo { Title = "Sync Over Async API", Description = "Example showing use of TCS to control task completion", Version = "v1" }));

// build
var app = builder.Build();

// use services
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sync Over Async API V1"));

// Add routes
app.MapGet("/weatherforecastSync", (WeatherDb db) => db.GetWeather());

// Initialize Data
var weatherDb = app.Services.GetService<WeatherDb>();
var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
weatherDb?.Weather.AddRange(Enumerable.Range(1, 5).Select(index => new WeatherForecast(index, Random.Shared.Next(-20, 55), summaries[Random.Shared.Next(summaries.Length)])).ToArray());
weatherDb?.SaveChanges();


// Run
app.Run();