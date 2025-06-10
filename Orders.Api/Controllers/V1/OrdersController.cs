using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.Commands.CreateOrder;
using Orders.Application.Commands.UpdateOrder;
using Orders.Application.Queries.GetOrderById;

namespace Orders.Api.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {

        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderByIdAsync(Guid orderId)
        {
            var response = await _mediator.Send(new GetOrderByIdQuery(orderId));

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrderAsync([FromBody] UpdateOrderCommand command)
        {
            if (command is null) return BadRequest("Command cannot be null.");

            var response = await _mediator.Send(command);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderCommand command)
        {
            if (command is null) return BadRequest("Command cannot be null.");

            var response = await _mediator.Send(command);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
