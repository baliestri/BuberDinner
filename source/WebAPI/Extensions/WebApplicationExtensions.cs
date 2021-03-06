// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Reflection;
using BuberDinner.Application;
using BuberDinner.Infrastructure;
using BuberDinner.WebAPI.Modules;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;

namespace BuberDinner.WebAPI.Extensions;

internal static class WebApplicationExtensions {
  private static readonly List<ModuleBase> _loadedModules;

  static WebApplicationExtensions()
    => _loadedModules = LoadModules();

  private static List<ModuleBase> LoadModules() => typeof(WebApplicationExtensions)
    .Assembly
    .GetTypes().Where(type => type.IsClass && type.IsAssignableTo(typeof(ModuleBase)))
    .Select(Activator.CreateInstance)
    .Cast<ModuleBase>()
    .ToList();

  private static void AddModuleServices(this IServiceCollection serviceCollection)
    => _loadedModules.ForEach(module => module.MapServiceCollection(serviceCollection));

  private static void UseModuleEndpoints(this IEndpointRouteBuilder endpoint)
    => _loadedModules.ForEach(module => module.MapRouteEndpoints(endpoint));

  public static WebApplication RegisterPipelines(this WebApplication application) {
    application
      .UseExceptionHandler("/api/error")
      .UseSwagger()
      .UseSwaggerUI(options
        => options.SwaggerEndpoint("/swagger/v1/swagger.json", "BuberDinner v1"))
      .UseHttpsRedirection()
      .UseAuthorization();
    application.UseModuleEndpoints();
    application.Map("/api/error", (HttpContext ctx) => {
      Exception? exception = ctx.Features.Get<IExceptionHandlerFeature>()?.Error;

      (int statusCode, string message) = exception switch {
        _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
      };

      return Results.Problem(message, statusCode: statusCode);
    });

    return application;
  }

  public static WebApplicationBuilder RegisterComponents(this WebApplicationBuilder builder) {
    IConfiguration configuration = builder.Configuration;
    IServiceCollection serviceCollection = builder.Services;

    serviceCollection
      .AddAuthorization()
      .AddEndpointsApiExplorer()
      .AddSwaggerGen(options => {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "BuberDinner", Version = "v1" });
        options.IncludeXmlComments(Path.Combine(
          AppContext.BaseDirectory,
          $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"
        ));

        OpenApiSecurityScheme securityScheme = new() {
          Name = "Authorization",
          Description = "Get 'Bearer {token}'",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.ApiKey
        };
        OpenApiSecurityRequirement securityRequirement = new() { { securityScheme, Array.Empty<string>() } };
        options.AddSecurityDefinition(
          "Bearer",
          securityScheme
        );
        options.AddSecurityRequirement(securityRequirement);
      })
      .AddApplicationServiceCollection()
      .AddInfrastructureServiceCollection(configuration);
    serviceCollection.AddModuleServices();

    return builder;
  }
}
