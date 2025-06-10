using AutoMapper;
using MediatR;
using Orders.Application.Common;
using Orders.Contracts.Infrastructure;
using Orders.Models.Dto.Common;

namespace Orders.Application.Queries.GetOrderById
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, BaseResponse<OrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOrderByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var response = new BaseResponse<OrderDto>();
            try
            {
                var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId);
                if (order is not null)
                {
                    response.Data = _mapper.Map<OrderDto>(order);
                    response.Success = true;
                    response.Message = "Order retrieved successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Order could not be retrieved";
                }

            } catch (Exception ex) {
                response.Success = false;
                response.Message = "An error occurred while retrieving the order.";
                response.Errors!.Add(new ValidationError(ex.Message));
            }

            return response;
        }
    }
}
