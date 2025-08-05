using MediatR;

namespace OrderService.WebApi.Core.Commands
{
    public class CancelOrderCommand : IRequest
    {
        public string OrderId { get; set; }
    }
}
