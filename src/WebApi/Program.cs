using Application;
using Application.Abstractions;
using Application.Services;
using Domain.Repositories;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Presentation;
using Presentation.Middlewares;
using Serilog;
using System.Text.Json.Serialization;
using System.Text.Json;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("cs");
        builder.Services.AddDbContext<LibraryManagerDbContext>(options =>
            options.UseSqlite(connectionString));

        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));

        builder.Logging.ClearProviders();

        builder.Services
            .AddApplication()
            .AddInfrastructure()
            .AddPresentation();

        builder.Services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web", Version = "v1" }));

        builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
        builder.Services.AddScoped<IServiceManager, ServiceManager>();

        builder.Services.AddTransient<ExceptionHandlingMiddleware>();


        var app = builder.Build();
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<LibraryManagerDbContext>();
            dbContext.Database.Migrate();
        }

        // TODO : Limiter l'accès à l'API en production
        app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.MapControllers();

        app.Run();
    }
}