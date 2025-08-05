
using BusinessModel;
using BusinessModel.Contracts;
using BusinessModel.Data;
using BusinessModel.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using WebApiContrib.Core.Formatter.Csv;

namespace VehicleManagement;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        var connectionString = builder.Configuration.GetConnectionString("Default");
        builder.Services.AddVehicleServices(connectionString);

        ConfigureAuthentication(builder);

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

        // Authentication ergänzen
        app.UseAuthentication();

        app.MapControllers();

        app.Run();
    }

    private static void ConfigureAuthentication(WebApplicationBuilder builder)
    {
        var jwtSettings = builder.Configuration.GetSection("Jwt");
        builder.Services.Configure<JwtOptions>(jwtSettings);
        var jwtOptions = jwtSettings.Get<JwtOptions>();

        builder.Services.AddTransient<ITokenService, JwtTokenService>();

        builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
        }).AddEntityFrameworkStores<VehicleDbContext>();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme =
            options.DefaultChallengeScheme =
            options.DefaultForbidScheme =
            options.DefaultScheme =
            options.DefaultSignInScheme =
            options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
            };
        });
    }
}
