using BusinessModel.Contracts;
using BusinessModel.Models;
using BusinessModel.Services;
using Moq;

namespace BusinessModel.Tests.Tests;

[TestClass]
public sealed class CustomerServiceTests
{
    [TestMethod]
    public async Task AddCustomer_ValidCustomer_AddsCustomerAsync()
    {
        // Arrange
        using var db = new TestDatabase();

        var sut = new CustomerService(db.Context, Mock.Of<IFileService>());
        var customer = new Customer
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "jdoe@me.com"
        };

        // Act
        var id = await sut.AddCustomer(customer, null, null);


        // Wichtig: Nicht gegen Service testen
        var result = await db.Context.Customers.FindAsync(id);


        Assert.IsNotNull(result, "Customer not created!");
        Assert.AreEqual(customer.FirstName, result.FirstName);
        Assert.AreEqual(customer.LastName, result.LastName);
        Assert.AreEqual(customer.Email, result.Email);
        Assert.AreEqual(customer.ImageUrl, null);
    }

    [TestMethod]
    [DataRow("jdoe.jpg")]
    public async Task AddCustomer_ValidCustomerWithProfilePicture_AddsCustomerAsync(string expectedFileName)
    {
        // Arrange
        using var db = new TestDatabase();

        var expectedImageUrl = "https://example.com/" + expectedFileName;

        var mockFileService = new Mock<IFileService>();
        mockFileService
            .Setup(x => x.UploadFile(expectedFileName, It.IsAny<Stream>()))
            .ReturnsAsync(expectedImageUrl);

        var sut = new CustomerService(db.Context, mockFileService.Object);
        var customer = new Customer
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "jdoe@me.com",
            ImageUrl = expectedImageUrl,
        };

        // Act
        var id = await sut.AddCustomer(customer, expectedFileName, new MemoryStream());


        // Wichtig: Nicht gegen Service testen
        var result = await db.Context.Customers.FindAsync(id);


        Assert.IsNotNull(result, "Customer not created!");
        Assert.AreEqual(customer.FirstName, result.FirstName);
        Assert.AreEqual(customer.LastName, result.LastName);
        Assert.AreEqual(customer.Email, result.Email);
        Assert.AreEqual(expectedImageUrl, result.ImageUrl);

        // Pruefen, dass die Methode UploadFile exakt einmal aufgerufen wurde
        mockFileService.Verify(x => x.UploadFile(expectedFileName, It.IsAny<Stream>()), Times.Once);
    }
}
