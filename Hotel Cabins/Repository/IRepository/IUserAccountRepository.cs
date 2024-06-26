﻿using Hotel_Cabins.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Hotel_Cabins.Repository.IRepository
{
    public interface IUserAccountRepository
    {
        public Task AddRoleToUser(ApplicationUser user);
        public JwtSecurityToken CreateToken(List<Claim> claims);
        public Task<IdentityResult> CreateUser(ApplicationUser user, string password);
        public Task<JwtSecurityToken> Login(string email, string password);
        public Task<ApplicationUser> GetUserById(string name);
        public Task<List<ApplicationUser>> GetAllUser();
    }
}
