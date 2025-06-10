using MediatR;
using Orders.Application.Common;
using Orders.Models.Dto.Common;

namespace Orders.Application.Queries.GetOrderById
{
    public class GetOrderByIdQuery : IRequest<BaseResponse<OrderDto>>
    {
        public GetOrderByIdQuery(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; set; }
    }
}
