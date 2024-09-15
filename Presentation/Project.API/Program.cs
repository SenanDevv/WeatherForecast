using Project.Core;
using Project.Geocoding.Utilities.DependencyResolvers;
using Project.Service.Utilities.DependencyResolvers;
using Project.WeatherbitApi.Utilities.DependencyResolvers;
using Project.WeatherVisualcrossingApi.Utilities.DependencyResolvers;
using Project.WorldWeatherOnlineApi.Utilities.DependencyResolvers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProjectDependencies();
builder.Services.AddGeocoding();
builder.Services.AddWeatherbitApi();
builder.Services.AddWeatherVisualcrossing();
builder.Services.AddWorldWeatherOnline();
builder.Services.Configure<ThirdPartyConnectionOptions>(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
