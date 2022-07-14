// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using BuberDinner.Application.Services.Common.Results;
using ErrorOr;

namespace BuberDinner.Application.Services.Auth.Queries;

public interface IAuthQueryService {
  ErrorOr<SuccessfulAuthResult> SignIn(string email, string password);
}
