using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Drawing;

namespace BusinessModel.Models
{
    [DebuggerDisplay("{Color} {Manufacturer} {Model}, {Type}, {Fuel}")]
    public class Auto
    {
        // Der Eigenschaft Metadaten hinzufügen
        [Key, Column("VehicleId")]
        public Guid Id { get; set; }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public string Type { get; set; }

        public string Fuel { get; set; }

        public int TopSpeed { get; set; }

        public KnownColor Color { get; set; }

        public DateTime Registered { get; set; }
    }
}
