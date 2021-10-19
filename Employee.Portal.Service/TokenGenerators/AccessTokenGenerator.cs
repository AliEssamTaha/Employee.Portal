using Employee.Portal.CoreLib.Config;
using Employee.Portal.Service.Interfaces;
using Employee.Portal.Service.Models;
using Employee.Portal.Service.Dto;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Employee.Portal.Service.TokenGenerators
{
    public class AccessTokenGenerator : IAccessTokenGenerator
    {
        private readonly AuthenticationConfiguration _configuration;
        private readonly ITokenGenerator _tokenGenerator;

        public AccessTokenGenerator(AuthenticationConfiguration configuration, ITokenGenerator tokenGenerator)
        {
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
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
            string token = _tokenGenerator.GenerateToken(
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

    }
}
