// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace BuberDinner.Contracts.Requests.Auth;

public sealed record CreateUserRequest(string FirstName, string LastName, string Email, string Password);
