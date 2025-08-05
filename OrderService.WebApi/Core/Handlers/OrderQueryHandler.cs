using MediatR;
using OrderService.WebApi.Core.Contracts;
using OrderService.WebApi.Core.Queries;
using OrderService.WebApi.Models.Domain;

namespace OrderService.WebApi.Core.Handlers
{
    public class OrderQueryHandler :
        IRequestHandler<GetAllOrdersQuery, IEnumerable<Order>>,
        IRequestHandler<GetOrderByIdQuery, Order?>
    {
        private readonly IOrdersRepository _repository;
        private readonly ILogger<OrderCommandHandler> _logger;

        public OrderQueryHandler(IOrdersRepository repository, ILogger<OrderCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Order>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _repository.GetAllOrders();
            _logger.LogInformation($"Retrieved {orders.Count()} orders");

            return orders;
        }

        public async Task<Order?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetOrderById(request.OrderId);
            _logger.LogInformation($"Retrieved order with id {request.OrderId}");

            return order;
        }
    }
}
