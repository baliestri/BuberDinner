// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using BuberDinner.Application.Interfaces.Services.Auth;
using BuberDinner.Application.Results.Auth;
using BuberDinner.Contracts.Requests.Auth;
using BuberDinner.Contracts.Responses.Auth;
using ErrorOr;

namespace BuberDinner.WebAPI.Modules.Auth;

public class AuthModule : ModuleBase {
  public override IEndpointRouteBuilder MapRouteEndpoints(IEndpointRouteBuilder endpoint) {
    endpoint.MapPost("/api/user/create", CreateUserHandler)
      .Produces<SuccessfulAuthResponse>(StatusCodes.Status201Created)
      .ProducesProblem(StatusCodes.Status409Conflict);
    endpoint.MapPost("/api/user/auth", SignInUserHandler)
      .Produces<SuccessfulAuthResponse>()
      .ProducesProblem(StatusCodes.Status409Conflict);

    return endpoint;
  }

  private static IResult CreateUserHandler(
    HttpRequest request,
    CreateUserRequest body,
    IAuthService authService
  ) {
    (string firstName, string lastName, string email, string password) = body;

    ErrorOr<SuccessfulAuthResult> resultOrError = authService.Create(firstName, lastName, email, password);

    return resultOrError.MatchFirst(
      auth => Results.Created(request.Path.ToUriComponent(), AuthResponse(auth)),
      error => Results.Problem(error.Description, statusCode: StatusCodes.Status409Conflict)
    );

    SuccessfulAuthResponse AuthResponse(SuccessfulAuthResult authResult) {
      return new SuccessfulAuthResponse(
        authResult.Id, authResult.FirstName, authResult.LastName,
        authResult.Email, authResult.Token
      );
    }
  }

  private static IResult SignInUserHandler(
    SignInUserRequest body,
    IAuthService authService
  ) {
    (string email, string password) = body;

    ErrorOr<SuccessfulAuthResult> resultOrError = authService.SignIn(email, password);

    return resultOrError.Match(
      auth => Results.Ok(AuthResponse(auth)),
      errors => Results.Extensions.ErrorsProblem(errors)
    );


    SuccessfulAuthResponse AuthResponse(SuccessfulAuthResult authResult) {
      return new SuccessfulAuthResponse(
        authResult.Id, authResult.FirstName, authResult.LastName,
        authResult.Email, authResult.Token
      );
    }
  }
}
