using OrderService.WebApi.Models.Domain;

namespace OrderService.WebApi.Core.Contracts
{
    public interface IOrdersRepository
    {
        Task CancelOrder(string orderId);
        Task<List<Order>> GetAllOrders();
        Task<Order?> GetOrderById(string orderId);
        Task<string> PlaceOrder(string customerId, string productId);
    }
}