using MediatR;

namespace OrderService.WebApi.Core.Commands
{
    public class PlaceOrderCommand : IRequest<string>
    {
        public string CustomerId { get; set; }

        public string ProductId { get; set; }
    }
}
