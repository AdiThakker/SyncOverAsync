using Microsoft.OpenApi.Models;

// Create builder
var builder = WebApplication.CreateBuilder(args);

// register services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(_ => _.SwaggerDoc("V1", new OpenApiInfo { Title = "Sync Over Async API", Description = "Example showing use of TCS to control task completion", Version = "V1" }));

// build
var app = builder.Build();


// use services
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sync Over Async API V1"));

app.MapGet("/", () => "Hello World!");

app.Run();