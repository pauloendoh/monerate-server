using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using monerate_server.Models;

namespace monerate_server.domains.user
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public List<CurrentSaving> CurrentSavings { get; set; }
    }
}
