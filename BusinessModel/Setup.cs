using BusinessModel.Contracts;
using BusinessModel.Data;
using BusinessModel.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessModel;

public static class Setup
{
    public static void AddVehicleServices(this IServiceCollection services, string? connectionString)
    {
        // Ausnahmsweise als Singleton zu Demonstrationszwecken registriert
        //builder.Services.AddSingleton<IVehicleService, InMemoryVehicleService>();

        services.AddScoped<IVehicleService, VehicleService>();

        // Auch notwendig, um Mirgration-Script zu generieren
        services.AddSqlServer<VehicleDbContext>(connectionString);
    }
}
