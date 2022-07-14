// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace BuberDinner.Application.Providers;

public interface IJwtTokenProvider {
  string Generate(Guid userId, string firstName, string lastName);
}
