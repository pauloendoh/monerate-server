using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using monerate_server.Data;
using monerate_server.domains.auth.types;
using monerate_server.domains.saving.types;
using monerate_server.domains.user;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace monerate_server.domains.auth
{
    public interface IUserRepository
    {

        public User? FindUserByUsername(string username);

        public Task<User> Register(RegisterPostDto registerDto);

        public Task<LoginResponseDto> Login(LoginPostDto loginPostDto);

        public User? FindUserById(int id);
    }

}
