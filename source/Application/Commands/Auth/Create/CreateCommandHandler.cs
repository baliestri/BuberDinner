// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using BuberDinner.Application.Common.Results;
using BuberDinner.Application.Persistence;
using BuberDinner.Application.Providers;
using BuberDinner.Core.Common.Errors;
using BuberDinner.Core.Entities;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Commands.Auth.Create;

public class CreateCommandHandler : IRequestHandler<CreateCommand, ErrorOr<SuccessfulAuthResult>> {
  private readonly IJwtTokenProvider _tokenProvider;
  private readonly IUserRepository _userRepository;

  public CreateCommandHandler(IJwtTokenProvider tokenProvider, IUserRepository userRepository) {
    _tokenProvider = tokenProvider;
    _userRepository = userRepository;
  }

  public Task<ErrorOr<SuccessfulAuthResult>> Handle(CreateCommand command, CancellationToken cancellationToken) {
    (string email, string firstName, string lastName, string password) = command;

    if (_userRepository.GetUserByEmail(email) is not null) {
      return Task.FromResult<ErrorOr<SuccessfulAuthResult>>(Errors.Authentication.EmailAlreadyExists);
    }

    User user = new() { Email = email, FirstName = firstName, LastName = lastName, Password = password };
    _userRepository.Add(user);
    string token = _tokenProvider.Generate(user.Id, firstName, lastName);

    return Task.FromResult<ErrorOr<SuccessfulAuthResult>>(new SuccessfulAuthResult(user.Id, firstName, lastName, email,
      token));
  }
}
