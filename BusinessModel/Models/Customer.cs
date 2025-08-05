using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessModel.Models
{
    public class Customer
    {
        // Der Eigenschaft Metadaten hinzufügen
        [Key, Column("CustomerId")]
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string? ImageUrl { get; set; }
    }
}
