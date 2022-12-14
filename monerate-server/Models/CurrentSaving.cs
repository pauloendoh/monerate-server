using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using monerate_server.domains.user;

namespace monerate_server.Models
{
    public class CurrentSaving
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public decimal Value { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }
    }
}
