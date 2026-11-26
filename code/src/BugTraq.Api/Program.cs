using BugTraq.Api.Behaviors;
using BugTraq.Api.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHealthChecks();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Program>();
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddDbContext<BugTraqContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BugTraq")));

var app = builder.Build();

app.UseExceptionHandler();
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseStaticFiles();
app.MapControllers();
app.MapHealthChecks("/health");
app.MapFallbackToFile("index.html");

app.Run();

public partial class Program;
