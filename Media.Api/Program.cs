using Media.Api.Entities;
using Media.Api.Extensions;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.Logging.AddOpenTelemetry(options =>
{
    options.SetResourceBuilder(
        ResourceBuilder.CreateDefault()
        .AddService("media-api"))
        .AddConsoleExporter()
        .AddOtlpExporter();
});

// Add services to the container.
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();
builder.Services.AddDbContext<MediaDbContext>(c =>
{
    c.UseNpgsql(builder.Configuration.GetConnectionString("mediadb"));
});

builder.Services.AddOpenTelemetry()
.ConfigureResource(resource => resource.AddService("media-api"))
.WithTracing(tracing => tracing.AddAspNetCoreInstrumentation().AddConsoleExporter().AddOtlpExporter())
.WithMetrics(metrics => metrics.AddAspNetCoreInstrumentation().AddConsoleExporter().AddOtlpExporter());

builder.Services.AddRepositories();

builder.Services.AddCors(c =>
{
    c.AddDefaultPolicy(p =>
    {
        p.AllowAnyOrigin();
        p.AllowAnyHeader();
        p.AllowAnyMethod();
    });
});

builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseDefaultExceptionHandler()
.UseFastEndpoints(c =>
{
    c.Endpoints.RoutePrefix = "api";
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwaggerGen();
    app.RunMigrations();
}

app.UseHttpsRedirection();

app.UseHealthChecks("/health");

app.UseCors();

await app.RunAsync().ConfigureAwait(false);
