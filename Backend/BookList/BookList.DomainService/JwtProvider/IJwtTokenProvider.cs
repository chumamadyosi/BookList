using BookList.Data.Entities;
using BookList.DomainService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookList.DomainService.JwtProvider
{
    public interface IJwtTokenProvider
    {
        JwtTokenResult GenerateToken(User user);
    }
}
