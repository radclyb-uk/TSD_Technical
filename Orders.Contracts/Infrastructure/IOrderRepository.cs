using Orders.Domain.Entities;

namespace Orders.Contracts.Infrastructure
{

    public interface IOrderRepository : IRepository<Order>
    {
    }
}
