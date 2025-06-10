using AutoMapper;
using MediatR;
using Orders.Application.Common;
using Orders.Contracts.Infrastructure;
using Orders.Domain.Entities;
using Orders.Models.Dto.Common;

namespace Orders.Application.Commands.UpdateOrder
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, BaseResponse<OrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateOrderHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse<OrderDto>> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            var response = new BaseResponse<OrderDto>();

            try
            {
                var baseOrder = await _unitOfWork.Orders.GetByIdAsync(command.Id!.Value);

                if (baseOrder.Status == Domain.Entities.OrderStatus.Delivered || baseOrder.Status == Domain.Entities.OrderStatus.Cancelled)
                {
                    response.Success = false;
                    response.Message = $"Order is already {baseOrder.Status} and cannot be updated.";
                    return response;
                }

                var order = _mapper.Map(command, baseOrder);
                order.TotalAmount = order.Items.Sum(oi => oi.Quantity * oi.UnitPrice);
                var success = await _unitOfWork.Orders.UpdateAsync(order);

                if (success)
                {
                    response.Data = _mapper.Map<OrderDto>(await _unitOfWork.Orders.GetByIdAsync(order.Id));
                    response.Success = true;
                    response.Message = "Update succeed!";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Order could not be update";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while updating the order.";
                response.Errors!.Add(new ValidationError(ex.Message));
            }
            return response;
        }
    }
}
