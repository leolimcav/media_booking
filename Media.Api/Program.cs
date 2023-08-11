using System.Globalization;
using Media.Api.Entities;
using Media.Api.Extensions;
using Microsoft.EntityFrameworkCore;
using Prometheus;
using Prometheus.DotNetRuntime;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning);
});

// Add services to the container.
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();
builder.Services.AddDbContext<MediaDbContext>(c => 
{
    c.UseNpgsql(builder.Configuration.GetConnectionString("mediadb"));
});

builder.Services.AddRepositories();

builder.Services.AddCors(c => 
{
    c.AddPolicy("defaultPolicy", p =>
    {
        p.AllowAnyOrigin();
        p.AllowAnyHeader();
        p.AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors();

app.UseFastEndpoints(c => 
{
    c.Endpoints.RoutePrefix = "api";
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerGen();
    app.RunMigrations();
}

app.UseHttpMetrics();

app.UseMetricServer();

app.MapMetrics();

app.UseHttpsRedirection();

app.Run();
