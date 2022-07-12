// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using BuberDinner.Application.Interfaces.Providers;

namespace BuberDinner.Infrastructure.Providers;

public class DateTimeProvider : IDateTimeProvider {
  public DateTime UtcNow => DateTime.UtcNow;
}
