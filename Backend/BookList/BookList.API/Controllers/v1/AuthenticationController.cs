using BookList.Core;
using BookList.DomainService;
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
    public class AuthenticationController(IUserService _userService, IJwtTokenProvider _tokenProvider, ILogger<AuthenticationController> _logger) : ControllerBase
    {

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest loginRequest)
        {
            try
            {
                var user = await _userService.GetUserByUsernameAsync(loginRequest.Username);
                if (user == null || !_userService.VerifyPassword(loginRequest.Password, user.PasswordHash))
                {
                    _logger.LogWarning("Failed login attempt for username: {Username}", loginRequest.Username);
                    return Unauthorized(new { message = "Invalid username or password." });
                }

                var tokenResult = _tokenProvider.GenerateToken(user);

                return Ok(new UserLoginResponse
                {
                    Token = tokenResult.Token,
                    ExpiresAt = tokenResult.ExpiresAt,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login attempt for username: {Username}", loginRequest.Username);
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }
    }
}
