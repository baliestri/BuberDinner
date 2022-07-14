// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using BuberDinner.Application.Services.Common.Results;
using BuberDinner.Contracts.Responses.Auth;

namespace BuberDinner.WebAPI.Modules.Auth.Endpoints;

public static partial class AuthEndpoints {
  private static SuccessfulAuthResponse GenerateAuthResponse(SuccessfulAuthResult authResult)
    => new(
      authResult.Id, authResult.FirstName, authResult.LastName,
      authResult.Email, authResult.Token
    );
}
