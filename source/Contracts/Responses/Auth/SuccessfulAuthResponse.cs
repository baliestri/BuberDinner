// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace BuberDinner.Contracts.Responses.Auth;

public sealed record SuccessfulAuthResponse(Guid Id, string FirstName, string LastName, string Email, string Token);
