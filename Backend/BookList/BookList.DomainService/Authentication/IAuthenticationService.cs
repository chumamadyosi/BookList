using BookList.DomainService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookList.DomainService.Authentication
{
    public interface IAuthenticationService
    {
        Task<JwtTokenResult?> AuthenticateAsync(string username, string password);
    }
}
