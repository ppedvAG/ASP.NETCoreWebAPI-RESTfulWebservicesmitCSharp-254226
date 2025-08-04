using BusinessModel.Models;
using VehicleManagement.Models;

namespace VehicleManagement.Mappers;

/// <summary>
/// Mapper welche sog. Extension Methoden verwendet
/// </summary>
/// <remarks>
/// Alternative: Install-Package AutoMapper
/// Mapping wird dann zur Laufzeit durchgeführt.
/// Kritikpunkte: 
/// - Verlust der Kontrolle 
/// - Fail-Fast ist nicht mehr gegeben
/// - Steigende Komplexität bei Transformation von Eigenschaften
/// </remarks>
public static class VehiclesMapper
{
    public static VehicleResultDto ToDto(this Auto domainModel) => new VehicleResultDto
    {
        Id = domainModel.Id,
        Manufacturer = domainModel.Manufacturer,
        Model = domainModel.Model,
        Type = domainModel.Type,
        FuelType = domainModel.Fuel,
        TopSpeed = domainModel.TopSpeed,
        Registered = domainModel.Registered.ToLongDateString(),
        Color = domainModel.Color,
        ColorAsString = domainModel.Color.ToString()
    };

    public static Auto ToDomainModel(this CreatedVehicleDto model) => new Auto
    {
        Manufacturer = model.Manufacturer,
        Model = model.Model,
        Type = model.Type,
        Fuel = model.FuelType,
        TopSpeed = model.TopSpeed,
        Registered = model.Registered,
        Color = model.Color
    };
}
