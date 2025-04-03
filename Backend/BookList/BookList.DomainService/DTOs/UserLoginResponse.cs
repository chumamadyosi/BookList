using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookList.DomainService.DTOs
{
    public class UserLoginResponse
    {
        public string? Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public  string? RefreshToken { get; set; }
    }
}
