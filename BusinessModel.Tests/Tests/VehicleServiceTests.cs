using BusinessModel.Data;
using BusinessModel.Models;
using BusinessModel.Services;
using Microsoft.EntityFrameworkCore;

namespace BusinessModel.Tests.Tests;

[TestClass]
public sealed class VehicleServiceTests
{
    [TestMethod]
    public async Task GetVehicleById_ValidId_ReturnsCorrectVehicleAsync()
    {
        using var db = new TestDatabase();

        // Arrange
        var sut = new VehicleService(db.Context);

        // Act
        var result = await sut.GetVehicleById(Guid.Parse(Seed.Vehicle1Id));

        // Assert
        Assert.IsNotNull(result, "Vehicle not found.");
        Assert.AreEqual(Seed.Vehicle1Manufacturer, result.Manufacturer);
        Assert.AreEqual(Seed.Vehicle1Model, result.Model);
        Assert.AreEqual(Seed.Vehicle1Fuel, result.Fuel);
        Assert.AreEqual(Seed.Vehicle1TopSpeed, result.TopSpeed);
    }

    [TestMethod]
    [DataRow(Seed.Vehicle1Id)]
    public async Task GetVehicleById_ByVehicleId_ReturnsCorrectVehicleAsync(string vehicleId)
    {
        using var db = new TestDatabase();

        // Arrange
        var sut = new VehicleService(db.Context);

        // Act
        var result = await sut.GetVehicleById(Guid.Parse(vehicleId));

        // Assert
        Assert.IsNotNull(result, "Vehicle not found.");
        Assert.AreEqual(Seed.Vehicle1Manufacturer, result.Manufacturer);
        Assert.AreEqual(Seed.Vehicle1Model, result.Model);
        Assert.AreEqual(Seed.Vehicle1Fuel, result.Fuel);
        Assert.AreEqual(Seed.Vehicle1TopSpeed, result.TopSpeed);
    }

    private static IEnumerable<object[]> GetVehiclesByRegistered_Data()
    {
        yield return new object[] { DateTime.Parse(Seed.Vehicle1Registered) };
        yield return new object[] { DateTime.Parse(Seed.Vehicle2Registered) };
    }

    [TestMethod]
    [DynamicData(nameof(GetVehiclesByRegistered_Data), DynamicDataSourceType.Method)]
    public async Task GetVehiclesByRegistered_ByRegistered_ReturnsCorrectVehicleAsync(DateTime registered)
    {
        using var db = new TestDatabase();

        // Arrange
        var sut = new VehicleService(db.Context);

        // Act
        var result = await sut.GetVehiclesByRegistered(registered);

        // Assert
        Assert.AreEqual(1, result.Count(), "Expected exact 1 vehicle.");
    }

    [TestMethod]
    public async Task GetVehicleById_InvalidId_ReturnsNullAsync()
    {
        using var db = new TestDatabase();

        // Arrange
        var sut = new VehicleService(db.Context);

        // Act
        var result = await sut.GetVehicleById(Guid.NewGuid());

        // Assert
        Assert.IsNull(result, "Expected null.");
    }


    [TestMethod]
    public async Task GetVehicles_NoParameters_ReturnsAllVehicles()
    {
        // Arrange
        using var db = new TestDatabase();

        var sut = new VehicleService(db.Context);
        string[] expectedFuels = ["Diesel", "Gasoline", "Hybrid", "Electric"];

        // Act
        var result = await sut.GetVehicles();

        // Assert
        Assert.AreEqual(100, result.Count(), "Expected 100 vehicles.");

        var actualFuels = result.Select(v => v.Fuel).Distinct().ToArray();
        CollectionAssert.AreEquivalent(expectedFuels, actualFuels);
    }

    [TestMethod]
    public async Task AddVehicle_ValidVehicle_AddsVehicleAsync()
    {
        // Arrange
        using var db = new TestDatabase();

        var sut = new VehicleService(db.Context);
        var vehicle = new Auto
        {
            Manufacturer = "Dacia",
            Model = "Dokker",
            Fuel = "Diesel",
            Type = "Minibus",
            TopSpeed = 100,
            Registered = new DateTime(2023, 1, 1)
        };

        // Act
        var id = await sut.AddVehicle(vehicle);


        // Wichtig: Nicht gegen Service testen
        var result = await db.Context.Vehicles.FindAsync(id);


        Assert.IsNotNull(result, "Vehicle not created!");
        Assert.AreEqual(vehicle.Manufacturer, result.Manufacturer);
        Assert.AreEqual(vehicle.Model, result.Model);
        Assert.AreEqual(vehicle.Fuel, result.Fuel);
        Assert.AreEqual(vehicle.TopSpeed, result.TopSpeed);
        Assert.AreEqual(vehicle.Color, result.Color);
        Assert.AreEqual(vehicle.Registered, result.Registered);
        Assert.AreEqual(vehicle.Type, result.Type);
    }

    [TestMethod]
    public async Task AddVehicle_MissingRequiredFields_AddsVehicleAsync()
    {
        // Arrange
        using var db = new TestDatabase();

        var sut = new VehicleService(db.Context);
        var vehicle = new Auto
        {
            Manufacturer = "Dacia",
            Model = "Dokker",
            Type = "Minibus",
            TopSpeed = 100,
            Registered = new DateTime(2023, 1, 1)
        };
        var expectedMessage = "Cannot insert the value NULL into column 'Fuel'";

        // Act
        var task = sut.AddVehicle(vehicle);

        // Assert
        var ex = await Assert.ThrowsExceptionAsync<DbUpdateException>(async () => await task);
        Assert.IsNotNull(ex.InnerException, "Expected inner exception.");
        Assert.IsTrue(ex.InnerException.Message.StartsWith(expectedMessage));
    }

    // TODO Update und Delete Tests als sowohl auch noch weitere Fehlerfaelle
}
