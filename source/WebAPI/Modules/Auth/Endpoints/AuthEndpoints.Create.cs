// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using BuberDinner.Application.Services.Auth.Commands;
using BuberDinner.Application.Services.Common.Results;
using BuberDinner.Contracts.Requests.Auth;
using ErrorOr;

namespace BuberDinner.WebAPI.Modules.Auth.Endpoints;

public static partial class AuthEndpoints {
  public static IResult Create(
    HttpRequest request,
    CreateUserRequest body,
    IAuthCommandService authCommandService
  ) {
    (string firstName, string lastName, string email, string password) = body;

    ErrorOr<SuccessfulAuthResult> resultOrError = authCommandService.Create(firstName, lastName, email, password);

    return resultOrError.MatchFirst(
      auth => Results.Created(request.Path.ToUriComponent(), GenerateAuthResponse(auth)),
      error => Results.Problem(error.Description, statusCode: StatusCodes.Status409Conflict)
    );
  }
}
