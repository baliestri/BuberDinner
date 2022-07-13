// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using BuberDinner.Application.Interfaces.Services.Auth;
using BuberDinner.Application.Results.Auth;
using BuberDinner.Contracts.Requests.Auth;
using BuberDinner.Contracts.Responses.Auth;

namespace BuberDinner.WebAPI.Modules.Auth;

public class AuthModule : ModuleBase {
  public override IEndpointRouteBuilder MapRouteEndpoints(IEndpointRouteBuilder endpoint) {
    endpoint.MapPost("/api/user/create", CreateUserHandler)
      .Produces<SuccessfulAuthResponse>(StatusCodes.Status201Created)
      .ProducesProblem(StatusCodes.Status400BadRequest);
    endpoint.MapPost("/api/user/auth", SignInUserHandler)
      .Produces<SuccessfulAuthResponse>()
      .ProducesProblem(StatusCodes.Status400BadRequest);

    return endpoint;
  }

  private static IResult CreateUserHandler(
    HttpRequest request,
    CreateUserRequest body,
    IAuthService authService
  ) {
    (string firstName, string lastName, string email, string password) = body;

    SuccessfulAuthResult result = authService.Create(firstName, lastName, email, password);
    SuccessfulAuthResponse response = new(result.Id, firstName, lastName, email, result.Token);

    return Results.Created(request.Path.ToUriComponent(), response);
  }

  private static IResult SignInUserHandler(
    SignInUserRequest body,
    IAuthService authService
  ) {
    (string email, string password) = body;

    SuccessfulAuthResult result = authService.SignIn(email, password);
    SuccessfulAuthResponse response = new(result.Id, result.FirstName, result.LastName, email, result.Token);

    return Results.Ok(response);
  }
}
