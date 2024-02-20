using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using CoddingTest.Domain.Mapping;
using CoddingTest.Domain.Services;
using CoddingTest.Domain.Services.Abstraction;
using CodingTest.Application.Controllers;
using CodingTest.Application.Handlers;
using CodingTest.Application.Mapping;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

IServiceCollection serviceCollection = builder.Services;
ConfigureServices(serviceCollection);
serviceCollection.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Hacker News APIs" });
});

builder.Services.AddProblemDetails(options =>
    options.CustomizeProblemDetails = (context) =>
    {
        context.ProblemDetails.Status = 400;
        context.ProblemDetails.Title = "Bad Request";
        context.ProblemDetails.Detail = "Oops, something went wrong...";
    }
);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler();
app.UseStatusCodePages();

app.UseHttpsRedirection();
app.UseRouting();

app.MapControllers();
app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddControllers()
        .AddApplicationPart(typeof(HackerNewsController).Assembly);

    services.AddHttpContextAccessor();
    services.AddHttpClient();
    RegisterServices(services);
    RegisterHandlers(services);

    services.AddAutoMapper(configAction => configAction.AddProfile(new ApplicationMappingsProfile()), typeof(Program));
    services.AddAutoMapper(configAction => configAction.AddProfile(new DomainMappingsProfile()), typeof(Program));
}

static void RegisterHandlers(IServiceCollection services)
{
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<HackerNewsHandler>());
}

static void RegisterServices(IServiceCollection services)
{
    services.AddScoped<IHackerNewsService, HackerNewsService>();
}