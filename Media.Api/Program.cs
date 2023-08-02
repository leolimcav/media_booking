using Media.Api.Entities;
using Media.Api.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => 
{
    configuration.ReadFrom.Configuration(context.Configuration);
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
    c.AddDefaultPolicy(p =>
    {
        p.AllowAnyOrigin();
        p.AllowAnyHeader();
        p.AllowAnyMethod();
        
    });
});

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseCors();

=======
>>>>>>> refact: adjust repository extensions. validator and cors
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

app.UseHttpsRedirection();

app.UseCors();

app.Run();
