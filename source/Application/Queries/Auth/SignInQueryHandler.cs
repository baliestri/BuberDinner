// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using BuberDinner.Application.Common.Results;
using BuberDinner.Application.Persistence;
using BuberDinner.Application.Providers;
using BuberDinner.Core.Common.Errors;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Queries.Auth;

public class SignInQueryHandler : IRequestHandler<SignInQuery, ErrorOr<SuccessfulAuthResult>> {
  private readonly IJwtTokenProvider _tokenProvider;
  private readonly IUserRepository _userRepository;

  public SignInQueryHandler(IJwtTokenProvider tokenProvider, IUserRepository userRepository) {
    _tokenProvider = tokenProvider;
    _userRepository = userRepository;
  }

  public Task<ErrorOr<SuccessfulAuthResult>> Handle(SignInQuery query, CancellationToken cancellationToken) {
    (string email, string password) = query;

    if (_userRepository.GetUserByEmail(email) is not { } user) {
      return Task.FromResult<ErrorOr<SuccessfulAuthResult>>(Errors.Authentication.InvalidCredentials);
    }

    if (user.Password != password) {
      return Task.FromResult<ErrorOr<SuccessfulAuthResult>>(Errors.Authentication.InvalidCredentials);
    }

    string token = _tokenProvider.Generate(user.Id, user.FirstName, user.LastName);

    return Task.FromResult<ErrorOr<SuccessfulAuthResult>>(
      new SuccessfulAuthResult(user.Id, user.FirstName, user.LastName, email, token)
    );
  }
}
