// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using BuberDinner.Application.Common.Results.Auth;
using BuberDinner.Application.Services.Auth;
using BuberDinner.Contracts.Requests.Auth;
using ErrorOr;

namespace BuberDinner.WebAPI.Modules.Auth.Endpoints;

public static partial class AuthEndpoints {
  public static IResult Create(
    HttpRequest request,
    CreateUserRequest body,
    IAuthService authService
  ) {
    (string firstName, string lastName, string email, string password) = body;

    ErrorOr<SuccessfulAuthResult> resultOrError = authService.Create(firstName, lastName, email, password);

    return resultOrError.MatchFirst(
      auth => Results.Created(request.Path.ToUriComponent(), GenerateAuthResponse(auth)),
      error => Results.Problem(error.Description, statusCode: StatusCodes.Status409Conflict)
    );
  }
}
