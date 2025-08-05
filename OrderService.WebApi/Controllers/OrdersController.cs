using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.WebApi.Core.Commands;
using OrderService.WebApi.Core.Queries;

namespace OrderService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IMediator _mediator;

        public OrdersController(ILogger<OrdersController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        #region Queries
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            var query = new GetAllOrdersQuery();
            var result = await _mediator.Send(query, token);
            return Ok(result);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> Get(string orderId, CancellationToken token)
        {
            var query = new GetOrderByIdQuery(orderId);
            var result = await _mediator.Send(query, token);
            return Ok(result);
        }
        #endregion

        #region Commands
        [HttpPost("place")]
        public async Task<IActionResult> Place([FromBody] PlaceOrderCommand command, CancellationToken token)
        {
            var result = await _mediator.Send(command, token);
            return CreatedAtAction(nameof(Get), new { orderId = result }, result);
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> Cancel([FromBody] CancelOrderCommand command, CancellationToken token)
        {
            await _mediator.Send(command, token);
            return NoContent();
        }

        [HttpPost("{orderId}/cancel")]
        public async Task<IActionResult> Cancel([FromRoute] string orderId, CancellationToken token)
        {
            await _mediator.Send(new CancelOrderCommand { OrderId = orderId }, token);
            return NoContent();
        }
        #endregion
    }
}
