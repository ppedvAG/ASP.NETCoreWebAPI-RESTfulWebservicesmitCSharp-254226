using MediatR;
using OrderService.WebApi.Models.Domain;

namespace OrderService.WebApi.Core.Queries
{
    public class GetOrderByIdQuery : IRequest<Order>
    {
        public GetOrderByIdQuery(string orderId)
        {
            this.OrderId = orderId;
        }

        public string OrderId { get; }
    }
}
