// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace BuberDinner.Contracts.Requests.Auth;

public sealed record SignInUserRequest(string Email, string Password);
