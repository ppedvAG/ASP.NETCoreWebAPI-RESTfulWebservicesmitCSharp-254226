using MediatR;
using OrderService.WebApi.Models.Domain;

namespace OrderService.WebApi.Core.Queries
{
    public class GetAllOrdersQuery : IRequest<IEnumerable<Order>>
    {
    }
}
