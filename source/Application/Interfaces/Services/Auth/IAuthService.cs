// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using BuberDinner.Application.Results.Auth;

namespace BuberDinner.Application.Interfaces.Services.Auth;

public interface IAuthService {
  SuccessfulAuthResult Create(string firstName, string lastName, string email, string password);
  SuccessfulAuthResult SignIn(string email, string password);
}
