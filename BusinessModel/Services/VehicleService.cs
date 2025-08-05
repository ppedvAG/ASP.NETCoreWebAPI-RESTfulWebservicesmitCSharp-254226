using BusinessModel.Contracts;
using BusinessModel.Data;
using BusinessModel.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessModel.Services;

public class VehicleService : IVehicleService
{
    private readonly VehicleDbContext _context;

    public VehicleService(VehicleDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Auto>> GetVehicles()
    {
        return await _context.Vehicles.ToListAsync();
    }

    public async Task<Auto?> GetVehicleById(Guid id)
    {
        return await _context.Vehicles.SingleOrDefaultAsync(v => v.Id == id);
    }

    public async Task<Guid> AddVehicle(Auto vehicle)
    {
        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync();
        return vehicle.Id;
    }

    public async Task<bool> UpdateVehicle(Guid id, Auto vehicle)
    {
        var existingVehicle = await _context.Vehicles.SingleOrDefaultAsync(v => v.Id == id);
        if (existingVehicle != null)
        {
            // Aktualisiere die Eigenschaften des bestehenden Fahrzeugs
            existingVehicle.Manufacturer = vehicle.Manufacturer;
            existingVehicle.Model = vehicle.Model;
            existingVehicle.Type = vehicle.Type;
            existingVehicle.Fuel = vehicle.Fuel;
            existingVehicle.TopSpeed = vehicle.TopSpeed;
            existingVehicle.Color = vehicle.Color;
            existingVehicle.Registered = vehicle.Registered;

            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteVehicle(Guid id)
    {
        var vehicle = await _context.Vehicles.SingleOrDefaultAsync(v => v.Id == id);
        if (vehicle != null)
        {
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<IEnumerable<Auto>> GetVehiclesByRegistered(DateTime registered)
    {
        return await _context.Vehicles
            .Where(v => v.Registered.Equals(registered))
            .ToListAsync();
    }
}
