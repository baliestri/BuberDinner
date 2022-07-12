// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BuberDinner.Application.Interfaces.Providers;
using Microsoft.IdentityModel.Tokens;

namespace BuberDinner.Infrastructure.Providers;

public class JwtTokenProvider : IJwtTokenProvider {
  private readonly IDateTimeProvider _dateTime;

  public JwtTokenProvider(IDateTimeProvider dateTime) => _dateTime = dateTime;

  public string Generate(Guid userId, string firstName, string lastName) {
    SigningCredentials signingCredentials = new(
      new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-secret-key")),
      SecurityAlgorithms.HmacSha256
    );

    Claim[] claims = {
      new(JwtRegisteredClaimNames.Sub, userId.ToString()), new(JwtRegisteredClaimNames.GivenName, firstName),
      new(JwtRegisteredClaimNames.FamilyName, lastName), new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

    JwtSecurityToken securityToken = new(
      audience: "BuberDinner",
      issuer: "BuberDinner",
      expires: _dateTime.UtcNow.AddMinutes(60),
      claims: claims,
      signingCredentials: signingCredentials
    );

    return new JwtSecurityTokenHandler().WriteToken(securityToken);
  }
}
