// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using ErrorOr;

namespace BuberDinner.Core.Common.Errors;

public static class Errors {
  public static class Authentication {
    public static Error EmailAlreadyExists
      => Error.Conflict("ERR_EMAIL_ALREADY_EXISTS", "An user with the provided email already exists");

    public static Error InvalidCredentials
      => Error.Custom(CustomErrorType.Unauthorized, "ERR_INVALID_CREDENTIALS", "The email/password does not match");
  }
}
