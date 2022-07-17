// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using BuberDinner.Application.Commands.Auth.Create;
using BuberDinner.Application.Common.Results;
using BuberDinner.Contracts.Requests.Auth;
using ErrorOr;
using MediatR;

namespace BuberDinner.WebAPI.Modules.Auth.Endpoints;

public static partial class AuthEndpoints {
  public static async Task<IResult> Create(
    HttpRequest request,
    CreateUserRequest body,
    ISender mediator
  ) {
    (string firstName, string lastName, string email, string password) = body;
    CreateCommand command = new(firstName, lastName, email, password);

    ErrorOr<SuccessfulAuthResult> resultOrError = await mediator.Send(command);

    return resultOrError.Match(
      auth => Results.Created(request.Path.ToUriComponent(), GenerateAuthResponse(auth)),
      errors => Results.Extensions.Errors(errors)
    );
  }
}
