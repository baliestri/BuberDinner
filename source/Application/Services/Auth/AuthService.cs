// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using BuberDinner.Application.Interfaces.Providers;
using BuberDinner.Application.Interfaces.Repositories;
using BuberDinner.Application.Interfaces.Services.Auth;
using BuberDinner.Application.Results.Auth;
using BuberDinner.Core.Entities;

namespace BuberDinner.Application.Services.Auth;

public class AuthService : IAuthService {
  private readonly IJwtTokenProvider _jwtToken;
  private readonly IUserRepository _userRepository;

  public AuthService(IJwtTokenProvider jwtToken, IUserRepository userRepository) {
    _jwtToken = jwtToken;
    _userRepository = userRepository;
  }

  public SuccessfulAuthResult Create(string firstName, string lastName, string email, string password) {
    if (_userRepository.GetUserByEmail(email) is not null) {
      throw new Exception("An user with the provided email already exists");
    }

    User user = new() { Email = email, FirstName = firstName, LastName = lastName, Password = password };
    _userRepository.Add(user);
    string token = _jwtToken.Generate(user.Id, firstName, lastName);

    return new SuccessfulAuthResult(user.Id, firstName, lastName, email, token);
  }

  public SuccessfulAuthResult SignIn(string email, string password) {
    if (_userRepository.GetUserByEmail(email) is not User user) {
      throw new Exception("An user with the provided email was not found");
    }

    if (user.Password != password) {
      throw new Exception("The provided password does not match");
    }

    string token = _jwtToken.Generate(user.Id, user.FirstName, user.LastName);

    return new SuccessfulAuthResult(user.Id, user.FirstName, user.LastName, email, token);
  }
}
