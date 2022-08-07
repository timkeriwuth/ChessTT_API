﻿using Labo.IL.Configurations;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Labo.IL.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtConfiguration _config;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public JwtService(JwtConfiguration config, JwtSecurityTokenHandler tokenHandler)
        {
            _config = config;
            _tokenHandler = tokenHandler;
        }

        public string CreateToken(string identifier, string role)
        {
            DateTime now = DateTime.Now;
            JwtSecurityToken token = new(
                _config.Issuer,
                _config.Audience,
                CreateClaims(identifier, role),
                now,
                now.AddSeconds(_config.LifeTime),
                CreateCredentials()
            );

            return _tokenHandler.WriteToken(token);
        }

        private SigningCredentials CreateCredentials()
        {
            return new SigningCredentials(CreateKey(), SecurityAlgorithms.HmacSha256);
        }

        private SecurityKey CreateKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Signature));
        }

        private IEnumerable<Claim> CreateClaims(string identifier, string role)
        {
            yield return new Claim(ClaimTypes.NameIdentifier, identifier);
            yield return new Claim(ClaimTypes.Role, role);
        }
    }
}