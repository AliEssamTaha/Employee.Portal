using Employee.Portal.CoreLib.Config;
using Employee.Portal.Service.Interfaces;
using Employee.Portal.Service.Models;
using Employee.Portal.Service.Dto;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Employee.Portal.Service.TokenGenerators
{
    public class AccessTokenGenerator : IAccessTokenGenerator
    {
        private readonly AuthenticationConfiguration _configuration;

        public AccessTokenGenerator(AuthenticationConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AccessToken GenerateToken(UserDto user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(nameof(user.Id), user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            DateTime expirationTime = DateTime.UtcNow.AddMinutes(_configuration.AccessTokenExpirationMinutes);
            string token = GenerateToken(
                _configuration.AccessTokenSecret,
                _configuration.Issuer,
                _configuration.Audience,
                expirationTime,
                claims);

            return new AccessToken
            {
                Value = token,
                ExpirationTime = expirationTime
            };
        }

        public string GenerateToken(string secretKey, string issuer, string audience, DateTime utcExpirationTime,
            IEnumerable<Claim> claims = null)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                DateTime.UtcNow,
                utcExpirationTime,
                credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
