using System.ComponentModel.DataAnnotations;

namespace monerate_server.domains.auth.types
{
    public class SavingPostDto
    {
     

        
        
        public int? Id { get; set; }

        [Required]
        public decimal Value { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateTime{ get; set; }

    }
}
