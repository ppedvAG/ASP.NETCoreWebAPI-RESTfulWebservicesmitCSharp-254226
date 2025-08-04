using BusinessModel.Models;

namespace BusinessModel.Contracts
{
    public interface IVehicleService
    {
        Task<IEnumerable<Auto>> GetVehicles();
        Task<Auto?> GetVehicleById(Guid id);

        Task<Guid> AddVehicle(Auto vehicle);
        Task<bool> DeleteVehicle(Guid id);
        Task<bool> UpdateVehicle(Guid id, Auto vehicle);
    }
}