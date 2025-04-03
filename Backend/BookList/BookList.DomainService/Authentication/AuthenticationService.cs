using BookList.DomainService.DTOs;
using BookList.DomainService.JwtProvider;
using BookList.DomainService.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookList.DomainService.Authentication
{
    public class AuthenticationService(IUserService _userService, IJwtTokenProvider _jwtService) : IAuthenticationService
    {
        public async Task<JwtTokenResult?> AuthenticateAsync(string username, string password)
        {
            var user = await _userService.GetUserByUsernameAsync(username);
            if (user == null || !_userService.VerifyPassword(password, user.PasswordHash))
            {
                return null; // Authentication failed
            }

            return  _jwtService.GenerateToken(user);
        }
    }
}
