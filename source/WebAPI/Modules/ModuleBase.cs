// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace BuberDinner.WebAPI.Modules;

public class ModuleBase {
  public virtual IEndpointRouteBuilder MapRouteEndpoints(IEndpointRouteBuilder endpoint) => endpoint;
  public virtual IServiceCollection MapServiceCollection(IServiceCollection serviceCollection) => serviceCollection;
}
