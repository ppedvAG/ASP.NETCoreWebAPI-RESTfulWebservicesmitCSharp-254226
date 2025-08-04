using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessModel.Models
{
    public class Order
    {
        [Key, Column("OrderId")]
        public Guid Id { get; set; }

        public long CustomerId { get; set; }

        public Customer Customer { get; set; }

        public List<Auto> Items { get; set; }
    }
}
