using Media.Api.Entities;
using Media.Api.Extensions;
using Media.Api.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();
builder.Services.AddDbContext<MediaDbContext>(c => 
{
    c.UseNpgsql(builder.Configuration.GetConnectionString("mediadb"));
});

builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

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

app.UseHttpsRedirection();

app.Run();
