using System.ComponentModel.DataAnnotations;

namespace monerate_server.domains.saving.types
{
    public class LoginPostDto
    {


        [Required]
        public string UsernameOrEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
