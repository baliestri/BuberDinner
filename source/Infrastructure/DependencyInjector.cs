// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using BuberDinner.Application.Interfaces.Providers;
using BuberDinner.Infrastructure.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Infrastructure;

public static class DependencyInjector {
  public static IServiceCollection AddInfrastructureServiceCollection(this IServiceCollection serviceCollection) {
    serviceCollection.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    serviceCollection.AddSingleton<IJwtTokenProvider, JwtTokenProvider>();

    return serviceCollection;
  }
}
