using Application;
using Application.Abstractions;
using Application.Services;
using Domain.Repositories;
using Infrastructure;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Presentation;
using Presentation.Middlewares;
using Serilog;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web", Version = "v1" }));

        builder.Services
            .AddApplication()
            .AddInfrastructure()
            .AddPresentation();

        builder.Services.AddTransient<ExceptionHandlingMiddleware>();

        builder.Services.AddDbContext<LibraryManagerDbContext>(options =>
        {
            var connectionStrings = builder.Configuration["LibraryManagerDatabase:ConnectionString"] ?? "mongodb://localhost:27017";
            var databaseName = builder.Configuration["LibraryManagerDatabase:DatabaseName"] ?? "LibraryManager";

            options.UseMongoDB(connectionStrings, databaseName);
        });


        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseSerilogRequestLogging();

        app.UseHttpsRedirection();

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.Run();
    }

    private static async Task ApplyMigrations(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        await using LibraryManagerDbContext context = scope.ServiceProvider.GetRequiredService<LibraryManagerDbContext>();

        await context.Database.MigrateAsync();
    }
}