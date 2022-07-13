// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BuberDinner.Application.Interfaces.Providers;
using BuberDinner.Infrastructure.Common.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BuberDinner.Infrastructure.Providers;

public class JwtTokenProvider : IJwtTokenProvider {
  private readonly IDateTimeProvider _dateTime;
  private readonly JwtSettings _settings;

  public JwtTokenProvider(IDateTimeProvider dateTime, IOptions<JwtSettings> settings) {
    _dateTime = dateTime;
    _settings = settings.Value;
  }

  public string Generate(Guid userId, string firstName, string lastName) {
    SigningCredentials signingCredentials = new(
      new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret)),
      SecurityAlgorithms.HmacSha256
    );

    Claim[] claims = {
      new(JwtRegisteredClaimNames.Sub, userId.ToString()), new(JwtRegisteredClaimNames.GivenName, firstName),
      new(JwtRegisteredClaimNames.FamilyName, lastName), new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

    JwtSecurityToken securityToken = new(
      audience: _settings.Audience,
      issuer: _settings.Issuer,
      expires: _dateTime.UtcNow.AddMinutes(_settings.ExpirationTimeInMinutes),
      claims: claims,
      signingCredentials: signingCredentials
    );

    return new JwtSecurityTokenHandler().WriteToken(securityToken);
  }
}
