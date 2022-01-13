using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SyncOverAsync.DataStore;

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
app.MapGet("/", () => "Hello Sync Over Async Web API!");
app.MapGet("/weatherforecast", async (WeatherDb db) => await db.Weather.ToListAsync());

app.Run();