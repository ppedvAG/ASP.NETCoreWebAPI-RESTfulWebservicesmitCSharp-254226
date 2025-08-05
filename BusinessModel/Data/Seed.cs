using Bogus;
using BusinessModel.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace BusinessModel.Data;

public class Seed
{
    #region Constants
    // Fuer Tests ist es besser die Testdaten nicht via Bogus zur Laufzeit generieren zu lassen
    // sondern hier explizit auszuschreiben. Weil wir einen Seed verwenden, ist diese Guid konstant.
    public const string Vehicle1Id = "098B6ECC-5714-B09A-36CF-D6280BB3C707";
    public const string Vehicle1Manufacturer = "Porsche";
    public const string Vehicle1Model = "A4";
    public const string Vehicle1Fuel = "Diesel";
    public const int Vehicle1TopSpeed = 100;
    #endregion

    private static readonly int ColorCount = Enum.GetNames(typeof(KnownColor)).Length;

    public static Faker<Customer> UserFaker => new Faker<Customer>()
            .UseSeed(42)
            .RuleFor(a => a.Id, f => f.Random.Guid())
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.FirstName, c.LastName));

    public static Faker<Auto> CarFaker => new Faker<Auto>()
            .UseSeed(42)
            .RuleFor(a => a.Id, f => f.Random.Guid())
            .RuleFor(a => a.Manufacturer, f => f.Vehicle.Manufacturer())
            .RuleFor(a => a.Model, f => f.Vehicle.Model())
            .RuleFor(a => a.Type, f => f.Vehicle.Type())
            .RuleFor(a => a.Fuel, f => f.Vehicle.Fuel())
            .RuleFor(a => a.TopSpeed, f => f.Random.Int(10, 30) * 10)
            .RuleFor(a => a.Color, f => (KnownColor)f.Random.Int(28, ColorCount))
            .RuleFor(a => a.Registered, f => f.Date.Between(new DateTime(1990, 1, 1), new DateTime(2022, 1, 1)));

    internal static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().HasData(UserFaker.Generate(10));
        modelBuilder.Entity<Auto>().HasData(CarFaker.Generate(100));
    }
}
