using BookList.DomainService.DTOs;
using BookList.DomainService.JwtProvider;
using BookList.DomainService.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookList.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IUserService _userService, IJwtTokenProvider _tokenProvider) : ControllerBase
    {

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest loginRequest)
        {
            var user = await _userService.GetUserByUsernameAsync(loginRequest.Username);
            if (user == null || !_userService.VerifyPassword(loginRequest.Password, user.PasswordHash))
            {
                return Unauthorized(new { message = "Invalid username or password." });
            }

            var tokenResult = _tokenProvider.GenerateToken(user);

            return Ok(new UserLoginResponse
            {
                Token = tokenResult.Token,
                ExpiresAt = tokenResult.ExpiresAt,
            });
        }
    }
}
