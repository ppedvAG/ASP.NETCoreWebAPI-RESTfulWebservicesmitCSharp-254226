using BusinessModel.Contracts;
using BusinessModel.Data;
using BusinessModel.Models;

namespace BusinessModel.Services;

// CRUD Operationen fuer die Fahrzeugverwaltung
public class InMemoryVehicleService : IVehicleService
{
    private readonly List<Auto> _autos = Seed.CarFaker.Generate(20).ToList();

    public Task<IEnumerable<Auto>> GetVehicles()
    {
        return Task.FromResult(_autos.AsEnumerable());
    }

    public Task<Auto?> GetVehicleById(Guid id)
    {
        var result = _autos.SingleOrDefault(x => x.Id == id);
        return Task.FromResult(result);
    }

    public Task<Guid> AddVehicle(Auto vehicle)
    {
        vehicle.Id = Guid.NewGuid();
        _autos.Add(vehicle);
        return Task.FromResult(vehicle.Id);
    }

    public Task<bool> DeleteVehicle(Guid id)
    {
        var index = _autos.FindIndex(x => x.Id == id);
        if (index != -1)
        {
            _autos.RemoveAt(index);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<bool> UpdateVehicle(Guid id, Auto vehicle)
    {
        var index = _autos.FindIndex(x => x.Id == id);
        if (index != -1)
        {
            _autos[index] = vehicle;
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }
}
