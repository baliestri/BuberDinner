// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using BuberDinner.Application.Common.Results.Auth;
using BuberDinner.Application.Persistence;
using BuberDinner.Application.Providers;
using BuberDinner.Core.Common.Errors;
using BuberDinner.Core.Entities;
using ErrorOr;

namespace BuberDinner.Application.Services.Auth;

public class AuthService : IAuthService {
  private readonly IJwtTokenProvider _jwtToken;
  private readonly IUserRepository _userRepository;

  public AuthService(IJwtTokenProvider jwtToken, IUserRepository userRepository) {
    _jwtToken = jwtToken;
    _userRepository = userRepository;
  }

  public ErrorOr<SuccessfulAuthResult> Create(string firstName, string lastName, string email, string password) {
    if (_userRepository.GetUserByEmail(email) is not null) {
      return Errors.Authentication.EmailAlreadyExists;
    }

    User user = new() { Email = email, FirstName = firstName, LastName = lastName, Password = password };
    _userRepository.Add(user);
    string token = _jwtToken.Generate(user.Id, firstName, lastName);

    return new SuccessfulAuthResult(user.Id, firstName, lastName, email, token);
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
