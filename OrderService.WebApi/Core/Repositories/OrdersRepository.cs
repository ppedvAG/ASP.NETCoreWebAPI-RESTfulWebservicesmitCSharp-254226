using OrderService.WebApi.Core.Contracts;
using OrderService.WebApi.Models.Domain;

namespace OrderService.WebApi.Core.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly List<Order> _orders =
        [
            new Order
            {
                CustomerId = "ALFKI",
                OrderId = "10248",
                ProductId = "Staubsauger#234"
            },
            new Order
            {
                CustomerId = "ACME-123",
                OrderId = "987",
                ProductId = "Wecker#4"
            },

        ];

        public Task<Order?> GetOrderById(string orderId)
            => Task.FromResult(_orders.SingleOrDefault(o => o.OrderId == orderId));

        public Task<List<Order>> GetAllOrders()
            // .ToList() gibt eine Kopie der Liste zurück
            => Task.FromResult(_orders.ToList());

        public Task<string> PlaceOrder(string customerId, string productId)
        {
            var orderId = Guid.NewGuid().ToString();
            _orders.Add(new Order { CustomerId = customerId, OrderId = orderId, ProductId = productId });
            return Task.FromResult(orderId);
        }

        public async Task CancelOrder(string orderId)
        {
            var order = await GetOrderById(orderId);
            if (order is not null)
            {
                order.Status = OrderStatus.Cancelled;
            }
        }
    }
}
