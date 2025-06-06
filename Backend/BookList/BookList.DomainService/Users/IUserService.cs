﻿using BookList.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookList.DomainService.Users
{
    public interface IUserService
    {
        Task<User?> GetUserByUsernameAsync(string username);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
