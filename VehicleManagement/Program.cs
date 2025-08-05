
using BusinessModel.Contracts;
using BusinessModel.Data;
using BusinessModel.Services;
using System.Text.Json.Serialization;
using WebApiContrib.Core.Formatter.Csv;

namespace VehicleManagement;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        // Ausnahmsweise als Singleton zu Demonstrationszwecken registriert
        builder.Services.AddSingleton<IVehicleService, InMemoryVehicleService>();
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                // Enum Serialization fixen: Farbe soll als String übertragen werden
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

                // Zirkellreferenzen sollen ignoriert werden
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            })
            // Weitere Formatters hinzufügen
            .AddXmlSerializerFormatters()
            // Install-Package WebApiContrib.Core.Formatter.Csv
            .AddCsvSerializerFormatters();

        // Auch notwendig, um Mirgration-Script zu generieren
        var connectionString = builder.Configuration.GetConnectionString("Default");
        builder.Services.AddSqlServer<VehicleDbContext>(connectionString);

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
