using MediatR;
using OrderService.WebApi.Core.Commands;
using OrderService.WebApi.Core.Contracts;

namespace OrderService.WebApi.Core.Handlers
{
    public class OrderCommandHandler :
        IRequestHandler<PlaceOrderCommand, string>,
        IRequestHandler<CancelOrderCommand>
    {
        private readonly IOrdersRepository _repository;
        private readonly ILogger<OrderCommandHandler> _logger;

        public OrderCommandHandler(IOrdersRepository repository, ILogger<OrderCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<string> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            var orderId = await _repository.PlaceOrder(request.CustomerId, request.ProductId);
            _logger.LogInformation($"Created order for customer {request.CustomerId} and product {request.ProductId} with id {orderId}");
            
            return orderId;
        }

        public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            await _repository.CancelOrder(request.OrderId);
            _logger.LogInformation($"Canceled order with id {request.OrderId}");
        }
    }
}
