// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Application;

public static class DependencyInjector {
  public static IServiceCollection AddApplicationServiceCollection(this IServiceCollection serviceCollection) {
    serviceCollection.AddMediatR(typeof(DependencyInjector).Assembly);

    return serviceCollection;
  }
}
