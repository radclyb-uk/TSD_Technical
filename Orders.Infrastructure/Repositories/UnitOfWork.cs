using Orders.Contracts.Infrastructure;

namespace Orders.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IOrderRepository Orders { get; }

        public UnitOfWork(IOrderRepository orders)
        {
            Orders = orders ?? throw new ArgumentNullException(nameof(orders));
        }
    }
}
