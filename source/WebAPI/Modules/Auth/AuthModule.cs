// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using BuberDinner.Contracts.Requests.Auth;
using BuberDinner.Contracts.Responses.Auth;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.WebAPI.Modules.Auth;

public class AuthModule : ModuleBase {
  public override IEndpointRouteBuilder MapRouteEndpoints(IEndpointRouteBuilder endpoint) {
    endpoint.MapPost("/api/user/create", CreateUserHandler)
      .Produces<SuccessfulAuthResponse>(StatusCodes.Status201Created);
    endpoint.MapPost("/api/user/auth", SignInUserHandler)
      .Produces<SuccessfulAuthResponse>();

    return endpoint;
  }

  private IResult CreateUserHandler(
    HttpContext ctx,
    [FromBody] CreateUserRequest body
  ) => Results.Created(
    ctx.Request.Path,
    new SuccessfulAuthResponse(Guid.NewGuid(), body.FirstName, body.LastName, body.Email, "token")
  );

  private IResult SignInUserHandler([FromBody] SignInUserRequest body)
    => Results.Ok(new SuccessfulAuthResponse(Guid.NewGuid(), "firstName", "lastName", body.Email, "token"));
}
