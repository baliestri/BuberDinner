// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using BuberDinner.Core.Entities;

namespace BuberDinner.Application.Interfaces.Repositories;

public interface IUserRepository {
  void Add(User user);
  User? GetUserByEmail(string email);
}
