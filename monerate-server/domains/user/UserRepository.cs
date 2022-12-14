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
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _db;
        private string _secretKey;
        public UserRepository(MyDbContext db, IConfiguration configuration)
        {
            _db = db;
            _secretKey = configuration.GetValue<string>("ApiSettings:JwtSecret");
        }

        public User? FindUserByUsername(string username)
        {
            return _db.Users.FirstOrDefault(x => x.Username == username);
        }

        public async Task<User> Register(RegisterPostDto registerDto)
        {
            var user = new User()
            {
                Username = registerDto.Username,
                Password = registerDto.Password,
                Email = registerDto.Email
            };


            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return user;
        }

        // PE 1/3 - move logic to AuthService
        public async Task<LoginResponseDto> Login(LoginPostDto loginPostDto)
        {
            var foundUser = _db.Users.FirstOrDefault(u => u.Username == loginPostDto.UsernameOrEmail || u.Email == loginPostDto.UsernameOrEmail);
            if (foundUser == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    ReasonPhrase = "User not found"
                };
                throw new HttpResponseException(resp);
            }
            if (foundUser.Password != loginPostDto.Password)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    ReasonPhrase = "Wrong password"
                };
                throw new HttpResponseException(resp);
            }

            // if user was found, generate jwt 
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, foundUser.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            foundUser.Password = "";
            var loginResponseDto = new LoginResponseDto()
            {
                Token = tokenHandler.WriteToken(token),
                User = foundUser
            };


            return loginResponseDto;
        }

        public User? FindUserById(int id)
        {
            var user = _db.Users.FirstOrDefault(x => x.Id == id);
            user.Password = "";
            return user;
        }
    }

}
