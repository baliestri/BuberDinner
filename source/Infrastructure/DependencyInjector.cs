// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using BuberDinner.Application.Interfaces.Providers;
using BuberDinner.Application.Interfaces.Repositories;
using BuberDinner.Infrastructure.Common.Settings;
using BuberDinner.Infrastructure.Providers;
using BuberDinner.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Infrastructure;

public static class DependencyInjector {
  public static IServiceCollection AddInfrastructureServiceCollection(
    this IServiceCollection serviceCollection,
    IConfiguration configuration
  ) {
    serviceCollection.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

    serviceCollection.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    serviceCollection.AddSingleton<IJwtTokenProvider, JwtTokenProvider>();

    serviceCollection.AddScoped<IUserRepository, UserRepository>();

    return serviceCollection;
  }
}
