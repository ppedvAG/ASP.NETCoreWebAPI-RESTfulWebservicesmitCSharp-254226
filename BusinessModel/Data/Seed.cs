using Bogus;
using BusinessModel.Models;
using System.Drawing;

namespace BusinessModel.Data;

public class Seed
{
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

}
