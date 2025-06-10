using MediatR;
using Orders.Application.Common;
using Orders.Models.Dto.Common;

namespace Orders.Application.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest<BaseResponse<OrderDto>>
    {
        public Guid? Id { get; set; }

        public string? CustomerName { get; set; }

        public OrderStatus? Status { get; set; }

        public List<OrderItemRequestDto>? Items { get; set; } = null;
    }
}
