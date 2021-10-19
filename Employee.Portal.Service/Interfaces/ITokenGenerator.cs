using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Employee.Portal.Service.Interfaces
{
    public interface ITokenGenerator
    {
        string GenerateToken(string secretKey, string issuer, string audience, DateTime utcExpirationTime,
            IEnumerable<Claim> claims = null);
    }
}
