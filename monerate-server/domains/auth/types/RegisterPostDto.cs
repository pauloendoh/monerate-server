using System.ComponentModel.DataAnnotations;

namespace monerate_server.domains.auth.types
{
    public class RegisterPostDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(16)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
