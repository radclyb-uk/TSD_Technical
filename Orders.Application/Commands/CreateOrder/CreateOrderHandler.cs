using AutoMapper;
using MediatR;
using Orders.Application.Common;
using Orders.Contracts.Infrastructure;
using Orders.Domain.Entities;
using Orders.Models.Dto.Common;

namespace Orders.Application.Commands.CreateOrder
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, BaseResponse<OrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateOrderHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse<OrderDto>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            var response = new BaseResponse<OrderDto>();

            try
            {
                var order = _mapper.Map<Order>(command);
                order.TotalAmount = order.Items.Sum(oi => oi.Quantity * oi.UnitPrice);
                order.Status = Domain.Entities.OrderStatus.Pending;
                var newGuid = await _unitOfWork.Orders.AddAsync(order);

                if (newGuid != Guid.Empty)
                {
                    response.Data = _mapper.Map<OrderDto>(await _unitOfWork.Orders.GetByIdAsync(newGuid));
                    response.Success = true;
                    response.Message = "Create succeed!";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Order could not be created";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An error occurred while creating the order.";
                response.Errors!.Add(new ValidationError(ex.Message));
            }
            return response;
        }
    }
}
