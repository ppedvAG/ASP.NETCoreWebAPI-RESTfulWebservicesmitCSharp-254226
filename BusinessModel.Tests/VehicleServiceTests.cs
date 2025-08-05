using BusinessModel.Data;
using BusinessModel.Services;

namespace BusinessModel.Tests;

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
}
