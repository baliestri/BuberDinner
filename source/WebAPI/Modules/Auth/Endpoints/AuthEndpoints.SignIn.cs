// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using BuberDinner.Application.Common.Results;
using BuberDinner.Application.Queries.Auth;
using BuberDinner.Contracts.Requests.Auth;
using ErrorOr;
using MediatR;

namespace BuberDinner.WebAPI.Modules.Auth.Endpoints;

public static partial class AuthEndpoints {
  public static async Task<IResult> SignIn(
    SignInUserRequest body,
    ISender mediator
  ) {
    (string email, string password) = body;
    SignInQuery query = new(email, password);

    ErrorOr<SuccessfulAuthResult> resultOrError = await mediator.Send(query);

    return resultOrError.Match(
      auth => Results.Ok(GenerateAuthResponse(auth)),
      errors => Results.Extensions.ErrorsProblem(errors)
    );
  }
}
