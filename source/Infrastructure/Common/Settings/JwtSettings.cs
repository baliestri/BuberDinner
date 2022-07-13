// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace BuberDinner.Infrastructure.Common.Settings;

public class JwtSettings {
  public const string SectionName = "JwtSettings";

  public string Secret { get; init; } = null!;
  public string Issuer { get; init; } = null!;
  public string Audience { get; init; } = null!;
  public int ExpirationTimeInMinutes { get; init; }
}
