// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using BuberDinner.Application.Common.Results.Auth;
using BuberDinner.Application.Services.Auth;
using BuberDinner.Contracts.Requests.Auth;
using ErrorOr;

namespace BuberDinner.WebAPI.Modules.Auth.Endpoints;

public static partial class AuthEndpoints {
  public static IResult SignIn(
    SignInUserRequest body,
    IAuthService authService
  ) {
    (string email, string password) = body;

    ErrorOr<SuccessfulAuthResult> resultOrError = authService.SignIn(email, password);

    return resultOrError.Match(
      auth => Results.Ok(GenerateAuthResponse(auth)),
      errors => Results.Extensions.ErrorsProblem(errors)
    );
  }
}
