// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using BuberDinner.Application.Interfaces.Repositories;
using BuberDinner.Core.Entities;

namespace BuberDinner.Infrastructure.Repositories;

public class UserRepository : IUserRepository {
  private static readonly List<User> _users = new();

  public void Add(User user) => _users.Add(user);

  public User? GetUserByEmail(string email) => _users.SingleOrDefault(x => x.Email == email);
}
