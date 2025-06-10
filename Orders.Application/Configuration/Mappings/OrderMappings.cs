using AutoMapper;
using Orders.Application.Commands.CreateOrder;
using Orders.Application.Commands.UpdateOrder;
using Orders.Domain.Entities;
using Orders.Models.Dto.Common;

namespace Orders.Application.Configuration.Mappings
{
    internal class OrderMappings : Profile
    {
        public OrderMappings()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemRequestDto>().ReverseMap();
            CreateMap<Order, CreateOrderCommand>().ReverseMap();
            CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        }
    }
}
