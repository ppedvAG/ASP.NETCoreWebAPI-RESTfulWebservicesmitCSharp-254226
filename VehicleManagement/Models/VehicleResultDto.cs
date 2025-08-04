using System.Drawing;

namespace VehicleManagement.Models;

public class VehicleResultDto
{
    public Guid Id { get; set; }

    public string Manufacturer { get; set; }

    public string Model { get; set; }

    public string Type { get; set; }

    public string FuelType { get; set; }

    public int TopSpeed { get; set; }

    public KnownColor Color { get; set; }

    public string ColorAsString { get; set; }

    public string Registered { get; set; }
}


