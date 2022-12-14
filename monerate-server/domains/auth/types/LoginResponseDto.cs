using monerate_server.domains.user;
using System.ComponentModel.DataAnnotations;

namespace monerate_server.domains.auth.types
{
    public class LoginResponseDto
    {
     
        public string Token { get; set; }
        public User User { get; set; }

    }
}
