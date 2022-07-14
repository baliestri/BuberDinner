// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using BuberDinner.Application.Services.Auth.Queries;
using BuberDinner.Application.Services.Common.Results;
using BuberDinner.Contracts.Requests.Auth;
using ErrorOr;

namespace BuberDinner.WebAPI.Modules.Auth.Endpoints;

public static partial class AuthEndpoints {
  public static IResult SignIn(
    SignInUserRequest body,
    IAuthQueryService authQueryService
  ) {
    (string email, string password) = body;

    ErrorOr<SuccessfulAuthResult> resultOrError = authQueryService.SignIn(email, password);

    return resultOrError.Match(
      auth => Results.Ok(GenerateAuthResponse(auth)),
      errors => Results.Extensions.ErrorsProblem(errors)
    );
  }
}
