// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using BuberDinner.Contracts.Responses.Auth;
using BuberDinner.WebAPI.Modules.Auth.Endpoints;

namespace BuberDinner.WebAPI.Modules.Auth;

public class AuthModule : ModuleBase {
  public override IEndpointRouteBuilder MapRouteEndpoints(IEndpointRouteBuilder endpoint) {
    endpoint.MapPost("/api/user/create", AuthEndpoints.Create)
      .Produces<SuccessfulAuthResponse>(StatusCodes.Status201Created)
      .ProducesProblem(StatusCodes.Status409Conflict);
    endpoint.MapPost("/api/user/auth", AuthEndpoints.SignIn)
      .Produces<SuccessfulAuthResponse>()
      .ProducesProblem(StatusCodes.Status409Conflict);

    return endpoint;
  }
}
