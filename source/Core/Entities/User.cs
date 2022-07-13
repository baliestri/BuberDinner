// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace BuberDinner.Core.Entities;

public sealed class User {
  public Guid Id { get; set; } = Guid.NewGuid();
  public string FirstName { get; set; } = null!;
  public string LastName { get; set; } = null!;
  public string Email { get; set; } = null!;
  public string Password { get; set; } = null!;

  public void Deconstruct(
    out Guid id, out string firstName,
    out string lastName, out string email
  ) => (id, firstName, lastName, email) = (Id, FirstName, LastName, Email);
}
