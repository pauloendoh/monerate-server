using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using monerate_server.Data;
using monerate_server.domains.auth.types;
using monerate_server.domains.saving.types;
using monerate_server.domains.user;
using System.Security.Claims;

namespace monerate_server.domains.auth
{
    public class UserController : Controller
    {
        private readonly MyDbContext _db;
        private readonly IUserRepository _userRepository;
        public UserController(MyDbContext db,  IUserRepository userRepository)

        {
            _db = db;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("/auth")]
        public IActionResult Get()
        {
            return new OkObjectResult("Hello from auth");
        }


        [HttpPost]
        [Route("/register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        async public Task<IActionResult> RegisterAndLogin([FromBody] RegisterPostDto body)
        {
            var usernameFound = _userRepository.FindUserByUsername(body.Username);
            if (usernameFound != null)
            {
                return new BadRequestObjectResult("Username already exists");
            }

            var user = await _userRepository.Register(body);
            if (user == null)
            {
                return new BadRequestObjectResult("Could not register user");
            }

            return await this.Login(new LoginPostDto
            {
                Password = body.Password,
                UsernameOrEmail = body.Username
            });


        }

        [HttpPost]
        [Route("/login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponseDto))]
        public async Task<IActionResult> Login([FromBody] LoginPostDto loginPostDto)
        {
            var loginResponseDto = await _userRepository.Login(loginPostDto);
            if (loginResponseDto.User == null || string.IsNullOrEmpty(loginResponseDto.Token)){
                return new BadRequestObjectResult(new { message = "Username or password is incorrect" });
            }
            return new OkObjectResult(loginResponseDto);
        }

        [HttpGet]
        [Route("/auth/me")]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
            var claims = this.User;
            var userId = Int32.Parse(claims.FindFirst(ClaimTypes.Name).Value) ;

            var user = _userRepository.FindUserById(userId);
            
            return new OkObjectResult(user);
        }

    }
}
