// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using BuberDinner.Application.Common.Results;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Commands.Auth.Create;

public record CreateCommand(
  string FirstName, string LastName,
  string Email, string Password
) : IRequest<ErrorOr<SuccessfulAuthResult>>;
