using Orders.Contracts.Infrastructure;
using Orders.Domain.Entities;

namespace Orders.UnitTests.Mocks
{
    public class MockOrderRepository : IOrderRepository
    {
        private Dictionary<Guid, Order> _orders = new Dictionary<Guid, Order>();

        public async Task<Guid> AddAsync(Order order)
        {
            var id = Guid.NewGuid();
            _orders.Add(id, order);
            return await Task.FromResult(id);
        }

        public Task<Order> GetByIdAsync(Guid id)
        {
            if (_orders.ContainsKey(id))
            {
                return Task.FromResult(_orders[id]);
            }

            return Task.FromResult(
                new Order
                {
                    Id = id,
                    CustomerName = "Test Customer",
                    OrderDate = DateTime.Today,
                    Items = new List<OrderItem>(),
                    Status = Domain.Entities.OrderStatus.Pending,
                    TotalAmount = 0
                }
            );
        }

        public async Task<bool> UpdateAsync(Order order)
        {
            _orders[order.Id] = order;
            return await Task.FromResult(true);
        }
    }
}
