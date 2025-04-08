using BookList.Data.Entities;
using BookList.DomainService.DTOs;
using BookList.DomainService.Middleware;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookList.DomainService.JwtProvider
{
    public class JwtTokenProvider : IJwtTokenProvider
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<JwtTokenProvider> _logger;

        public JwtTokenProvider(IOptions<JwtSettings> jwtSettings, ILogger<JwtTokenProvider> logger)
        {
            _jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings), "JWT settings are not configured correctly.");
            _logger = logger;
        }

        public JwtTokenResult GenerateToken(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user), "User cannot be null");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);
            var expiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes);  

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiresAt,
                Issuer = _jwtSettings.Issuer,  
                Audience = _jwtSettings.Audience,  
                IssuedAt = DateTime.UtcNow, 
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            try
            {
                _logger.LogInformation("Create user Token");
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return new JwtTokenResult
                {
                    Token = tokenHandler.WriteToken(token),
                    ExpiresAt = expiresAt
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw new Exception("Error generating the JWT token", ex);
            }
        }
    }
}
