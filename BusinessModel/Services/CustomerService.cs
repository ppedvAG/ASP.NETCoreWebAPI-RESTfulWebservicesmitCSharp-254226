using BusinessModel.Contracts;
using BusinessModel.Data;
using BusinessModel.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessModel.Services;

public class CustomerService
{
    private readonly VehicleDbContext context;
    private readonly IFileService _fileService;

    public CustomerService(VehicleDbContext context, IFileService fileService)
    {
        this.context = context;
        _fileService = fileService;
    }

    public async Task<IEnumerable<Customer>> GetCustomers()
    {
        return await context.Customers.ToListAsync();
    }

    public async Task<Customer?> GetCustomerById(Guid id)
    {
        return await context.Customers.SingleOrDefaultAsync(v => v.Id == id);
    }

    public async Task<Guid> AddCustomer(Customer entity, string? fileName, Stream? fileStream)
    {
        if (fileName != null && fileStream != null)
        {
            entity.ImageUrl = await _fileService.UploadFile(fileName, fileStream);
        }

        context.Customers.Add(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> UpdateCustomer(Guid id, Customer entity)
    {
        var existing = await context.Customers.SingleOrDefaultAsync(v => v.Id == id);
        if (existing != null)
        {
            // Aktualisiere die Eigenschaften des bestehenden Fahrzeugs
            existing.FirstName = entity.FirstName;
            existing.LastName = entity.LastName;
            existing.Email = entity.Email;
            existing.ImageUrl = entity.ImageUrl;

            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteCustomer(Guid id)
    {
        var entity = await context.Customers.SingleOrDefaultAsync(v => v.Id == id);
        if (entity != null)
        {
            context.Customers.Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
