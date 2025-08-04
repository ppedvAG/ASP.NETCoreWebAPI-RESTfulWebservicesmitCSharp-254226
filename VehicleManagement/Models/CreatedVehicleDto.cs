using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace VehicleManagement.Models;

public class CreatedVehicleDto
{
    [Required(ErrorMessage = "Hersteller muss eingeben werden")]
    [Display(Name = "Hersteller")]
    [MaxLength(50)]
    public required string Manufacturer { get; set; }

    [Required(ErrorMessage = "Modelname muss eingeben werden")]
    [MaxLength(50)]
    public required string Model { get; set; }

    [Required(ErrorMessage = "Der Fahrzeugtyp muss eingeben werden")]
    [Display(Name = "Fahrzeugtyp")]
    public required string Type { get; set; }

    [Required(ErrorMessage = "Die Treibstoffart muss eingeben werden")]
    public required string FuelType { get; set; }

    public int TopSpeed { get; set; }

    public KnownColor Color { get; set; }

    [Required, DataType(DataType.Date)]
    public DateTime Registered { get; set; }
}
