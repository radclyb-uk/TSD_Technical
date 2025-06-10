using MediatR;
using Orders.Application.Common;
using Orders.Models.Dto.Common;

namespace Orders.Application.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<BaseResponse<OrderDto>>
    {
        public string? CustomerName { get; set; }

        public DateTime? OrderDate { get; set; } = DateTime.UtcNow;

        public List<OrderItemRequestDto>? Items { get; set; } = null;
    }
}
