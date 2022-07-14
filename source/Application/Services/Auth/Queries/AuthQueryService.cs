// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using BuberDinner.Application.Persistence;
using BuberDinner.Application.Providers;
using BuberDinner.Application.Services.Common.Results;
using BuberDinner.Core.Common.Errors;
using ErrorOr;

namespace BuberDinner.Application.Services.Auth.Queries;

public class AuthQueryService : IAuthQueryService {
  private readonly IJwtTokenProvider _jwtToken;
  private readonly IUserRepository _userRepository;

  public AuthQueryService(IJwtTokenProvider jwtToken, IUserRepository userRepository) {
    _jwtToken = jwtToken;
    _userRepository = userRepository;
  }

  public ErrorOr<SuccessfulAuthResult> SignIn(string email, string password) {
    if (_userRepository.GetUserByEmail(email) is not { } user) {
      return Errors.Authentication.InvalidCredentials;
    }

    if (user.Password != password) {
      return Errors.Authentication.InvalidCredentials;
    }

    string token = _jwtToken.Generate(user.Id, user.FirstName, user.LastName);

    return new SuccessfulAuthResult(user.Id, user.FirstName, user.LastName, email, token);
  }
}
