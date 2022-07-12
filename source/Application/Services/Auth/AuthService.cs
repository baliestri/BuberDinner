// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using BuberDinner.Application.Interfaces.Providers;
using BuberDinner.Application.Interfaces.Services.Auth;
using BuberDinner.Application.Results.Auth;

namespace BuberDinner.Application.Services.Auth;

public class AuthService : IAuthService {
  private readonly IJwtTokenProvider _jwtToken;

  public AuthService(IJwtTokenProvider jwtToken) => _jwtToken = jwtToken;

  public SuccessfulAuthResult Create(string firstName, string lastName, string email, string password) {
    Guid userId = Guid.NewGuid();
    string token = _jwtToken.Generate(userId, firstName, lastName);

    return new SuccessfulAuthResult(userId, firstName, lastName, email, token);
  }

  public SuccessfulAuthResult SignIn(string email, string password) {
    Guid userId = Guid.NewGuid();
    string token = _jwtToken.Generate(userId, "firstName", "lastName");

    return new SuccessfulAuthResult(userId, "firstName", "lastName", email, token);
  }
}
